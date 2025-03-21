using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BLL.IService;
using DAL.Repository;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class AuthenticationService: IAuthenticationService
    {

        protected IConfiguration _config;
        protected IGenericRepository<User> _userRepository;

        public AuthenticationService(
            IConfiguration config,
            IGenericRepository<User> userRepository
            )
        {
            _config = config;
            _userRepository = userRepository;
        }

        private string GenerateJSONWebToken(string username)
        {
            var secretKey = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("custom_info", "info"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password, string salt)
        {

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                    password: Encoding.UTF8.GetBytes(password),
                    salt: Encoding.UTF8.GetBytes(salt),
                    iterations: 10,
                    hashAlgorithm: HashAlgorithmName.SHA512,
                    outputLength: 10);

            return Convert.ToHexString(hash);
        }

        public void RegisterUser(string username, string password)
        {
            User? user = _userRepository.GetSingleOrDefault(User => User.Username.ToLower() == username.ToLower()) ?? throw new Exception("User already exist");
            var salt = DateTime.Now.ToString("dddd"); // get the day of week. Ex: Sunday
            var passwordHash = HashPassword(password, salt);
            _userRepository.Add(new User(username, passwordHash, salt));
        }

        public string Login(string username, string password)
        {
            User? user = _userRepository.GetSingleOrDefault(User => User.Username.ToLower() == username.ToLower()) ?? throw new Exception("Login failed; Invalid userID or password");

            var passwordHash = HashPassword(password, user.Salt);
            if (user.passwordHash == passwordHash)
            {
                var token = GenerateJSONWebToken(username);
                return token;
            }
            throw new Exception("Login failed; Invalid userID or password");
        }
    }
}
