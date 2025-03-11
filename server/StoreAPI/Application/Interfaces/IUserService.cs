using StoreAPI.Application.DTOs;

namespace StoreAPI.Application.Interfaces;

public interface IUserService
{
    Task<bool> Signup(SignupDto model);
    Task<string> Login(LoginDto model);
}
