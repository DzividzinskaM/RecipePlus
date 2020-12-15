using RecipePlusApi.Data.Interfaces;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.ManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Repositories
{
    public class AdminRepository : IAdmin
    {
        RecipeContext _context;

        public AdminRepository(RecipeContext context)
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


        public void Create(AdminModel model)
        {

            Admin admin = new Admin()
            {
                AdminName = model.AdminName,
                Login = model.Login,
                Password = GetHashString(model.Password).ToString()
            };

            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public Admin Get(int id)
        {
            return _context.Admins.Find(id);
        }

        public bool isAuth(AdminModel model)
        {
            var admin = _context.Admins
                .Where(a => a.Login == model.Login && a.Password == GetHashString(model.Password).ToString()).FirstOrDefault();

            if (admin != null)
                return true;

            return false;
        }

        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins;
        }
    }
}
