using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly ContextDb _contextDb;
        
        public UserRepository(ContextDb contextDb) 
        {
            _contextDb = contextDb;
        }

        public async Task<bool> VerifyPasswordAsync(string login, string password)
        {
            var user =  await _contextDb.UsersTable.FirstOrDefaultAsync(u => u.Login == login);
            Console.WriteLine($"user init {user.Login}");
            if (user == null)
            {
                Console.WriteLine($"user is null {user.Login}");
                return false; 
            }

            Console.WriteLine($"user is {user.Password == HashPassword(password)}");

            return user.Password == HashPassword(password);
        }

        public async Task<int> GetNextAvailableUserIdAsync()
        {
            var usedIds = await _contextDb.UsersTable
                                          .OrderBy(u => u.Id)
                                          .Select(u => u.Id)
                                          .ToListAsync();

            int nextAvailableId = 1;

            foreach (var id in usedIds)
            {
                if (id == nextAvailableId)
                {
                    nextAvailableId++;
                }
                else
                {
                    break;
                }
            }

            return nextAvailableId;
        }

        public async Task CreateUserAsync(Users user)
        {
            bool userExists = await _contextDb.UsersTable.AnyAsync(u => u.Login == user.Login);
            
            if (userExists)
            {
                throw new Exception("User with this login already exists.");
            }
            
            user.Password = HashPassword(user.Password);

            await _contextDb.UsersTable.AddAsync(user);
            await _contextDb.SaveChangesAsync();
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _contextDb.UsersTable.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {user} was not found.");
            }
            user.Password = HashPassword(user.Password);
            _contextDb.UsersTable.Remove(user);

            await _contextDb.SaveChangesAsync();
        }

        public async Task DeleteUserByLoginAsync(string login)
        {
            var user = await _contextDb.UsersTable.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {user} was not found.");
            }

            _contextDb.UsersTable.Remove(user);

            await _contextDb.SaveChangesAsync();
        }

        public async Task<Users?> GetUserByIdAsync(int id)
        {
            return await _contextDb.UsersTable.FindAsync(id);
        }

        public async Task<Users?> GetUserByLoginAsync(string login)
        {
            return await _contextDb.UsersTable.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _contextDb.UsersTable.ToListAsync();
        }

        public async Task UpdateUserAsync(Users user)
        {
            _contextDb.UsersTable.Update(user);
            await _contextDb.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
