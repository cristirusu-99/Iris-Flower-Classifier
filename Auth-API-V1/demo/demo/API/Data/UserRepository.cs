using API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public bool Login(User user)
        {
            List<User> list= this.context.Users.ToList();
            User user1 = list.Find(c => c.Username.Equals(user.Username));

            using (var shaAlg = SHA3.Net.Sha3.Sha3384())
            {
                var aux = shaAlg.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash));
                user.PasswordHash = System.Text.Encoding.UTF8.GetString(aux);
            }

            if(user1.PasswordHash.Equals(user.PasswordHash))
            return true;
            else return false;
        }

        public bool Register(User user)
        {

            if(user.PasswordHash.Length < 8)
            {
                return false;
            }

             using (var shaAlg = SHA3.Net.Sha3.Sha3384())
            {

                var aux = shaAlg.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash));
                user.PasswordHash = System.Text.Encoding.UTF8.GetString(aux);
            }

            this.context.Add(user);
            this.context.SaveChanges();

            return true;
        }

        public void Update(User user)
        {
            this.context.Update(user);
            this.context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users.ToList();
        }

        public User GetById(int id)
        {
            return this.context.Users.Find(id);
        }

        public void Remove(User user)
        {
            this.context.Remove(user);
            this.context.SaveChanges();
        }

    }
}
