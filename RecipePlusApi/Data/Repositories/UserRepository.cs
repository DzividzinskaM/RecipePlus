using RecipePlusApi.Data.Interfaces;
using RecipePlusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using RecipePlusApi.ViewModels.UserModels;

namespace RecipePlusApi.Data.Repositories
{
    public class UserRepository : IUser
    {
        RecipeContext _context;

        public UserRepository(RecipeContext context)
        {
            _context = context;
        }


        private Guid GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash);
        }

        public void Create(RegisterModel model)
        {
            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = GetHashString(model.Password).ToString()
            };

            _context.Users.Add(user);

            _context.SaveChanges();
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public bool isAuth(LoginModel model)
        {
            User user = _context.Users.Where(u => u.Email == model.Login).FirstOrDefault();

            if (user == null)
                return false;

            if (user.Password == GetHashString(model.Password).ToString())
                return true;

            return false;
        }
    }
}
