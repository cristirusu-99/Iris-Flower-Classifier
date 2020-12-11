using API.Entities;
using System.Collections.Generic;

namespace API.Data
{
    public interface IUserRepository
    {
        bool Register(User user);
        bool Login(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Remove(User user);
        void Update(User user);
    }
}