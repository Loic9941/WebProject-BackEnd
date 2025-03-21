using BLL.DTOs;

namespace BLL.IService
{
    public interface IAuthenticationService
    {
        public void RegisterUser(RegisterDTO registerDTO);
        public string Login(LoginDTO loginDTO);
    }
}
