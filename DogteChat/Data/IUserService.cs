using DogteChat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DogteChat.Data
{
    public interface IUserService
    {
        Task<User> GetUserByTelegramId(int telegramId);
        Task CreateUser(User user);
        Task UpdateUser(User user);
    }

    public class UserService : IUserService
    {
        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByTelegramId(int telegramId)
        {
            throw new NotImplementedException();
        }
    }
}
