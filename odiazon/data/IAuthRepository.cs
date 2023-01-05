using odiazon.data_response;
using odiazon.models.m_user;

namespace odiazon.data
{
    public interface IAuthRepository
    {
        Task<Response<int>> Register(User user, string password);
        Task<Response<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}