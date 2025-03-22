using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BLL.DTOs;
using BLL.IService;
using DAL.Repository;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class AuthenticationService: IAuthenticationService
    {

        protected IConfiguration _config;
        protected IGenericRepository<User> _userRepository;
        protected IGenericRepository<Contact> _contactRepository;
        protected IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(
            IConfiguration config,
            IGenericRepository<User> userRepository,
            IGenericRepository<Contact> contactRepository,
            IHttpContextAccessor httpContextAccessor

            )
        {
            _config = config;
            _userRepository = userRepository;
            _contactRepository = contactRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GenerateJSONWebToken(string username)
        {
            var secretKey = _config["Jwt:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("custom_info", "info"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, _userRepository.GetSingleOrDefault(x => x.Email == username).Role)
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

        public void RegisterUser(RegisterDTO registerDTO)
        {
            var listUser = _userRepository.Get(User => User.Email.ToLower() == registerDTO.Email.ToLower());
            if (listUser.Any())
            {
                throw new Exception("User already exist");
            }
            var salt = DateTime.Now.ToString("dddd");
            var passwordHash = HashPassword(registerDTO.Password, salt);
            int contactId = _contactRepository.Add(new Contact { Firstname = registerDTO.Firstname, Lastname = registerDTO.Lastname });
            _userRepository.Add(new User { Email = registerDTO.Email, PasswordHash = passwordHash, Salt = salt, Role = registerDTO.Role, ContactId = contactId });
        }

        public string Login(LoginDTO loginDTO)
        {
            User? user = _userRepository.GetSingleOrDefault(User => User.Email.ToLower() == loginDTO.Email.ToLower()) ?? throw new Exception("Login failed; Invalid userID or password");

            var passwordHash = HashPassword(loginDTO.Password, user.Salt);
            if (user.PasswordHash == passwordHash)
            {
                var token = GenerateJSONWebToken(loginDTO.Email);
                return token;
            }
            throw new Exception("Login failed; Invalid userID or password");
        }

        public int? GetContactId()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userEmail != null)
            {
                var user = _userRepository.GetSingleOrDefault(x => x.Email == userEmail);
                return user?.ContactId;
            }
            return null;
        }

        public bool IsAdmin()
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole("Administrator") ?? false;

        }

        public bool IsArtisan()
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole("Artisan") ?? false;

        }

        public bool IsCustomer()
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole("Customer") ?? false;
        }

        public bool IsDeliveryPartner()
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole("DeliveryPartner") ?? false;
        }
    }
}
