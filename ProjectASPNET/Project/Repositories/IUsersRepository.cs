using Project.Models;

namespace Project.Repositories
{
    public interface IUsersRepository
    {
        Task <IEnumerable<Users>> GetAllUsersAsync();
        Task <Users?> GetUserByIdAsync(int id);
        Task <Users?> GetUserByLoginAsync(string login);
        Task DeleteUserByLoginAsync (string login);
        Task DeleteUserByIdAsync (int id);
        Task CreateUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task<bool> VerifyPasswordAsync(string login, string password);
    }
}
