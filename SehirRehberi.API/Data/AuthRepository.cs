using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SehirRehberi.API.Models;

namespace SehirRehberi.API.Data
{
    //AuthRepository İmplemantasyonu ile Login Register UserExist i İmplemente ettik.
    public class AuthRepository : IAuthRepository
    {
        DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }


        //Login = GİRİŞ işlemlerini yapıyoruz.
        public async Task<User> Login(string userName, string password)
        {
            //KUllanıcının gönderdiği şifrenin hashlenmiş versiyonuyla veritabınındaki daha önce kayıtlı hash birbirine uyuyor mu diye kontrol etme.
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            //Kullanıcı var mı diye kontrol ettik
            if (user == null)
            {
                return null;

            }
            if (!VerfyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
            
        }

        private bool VerfyPasswordHash(string password, byte[] userpasswordHash, byte[] userpasswordSalt)
        {
            //HMACSHA algoritmasıyla hash  oluşturuyoruz.
            //Yani Şifreleme işlemleri Yapıyoruz.
            // Burda key ile salt ve Hash değerimiz set etmiş olduk.
            //Burada passwordSalt ı gönderdiğimiz için işlemi ona göre yapacaz.
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userpasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=userpasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }



        // REGİSTER = Kayıt olma işlemlerini Yapıyoruz ve burda password u hash leyip ve saltlayacaz.
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            // Burada out ile referans tiple çekiyoruz işlemleri.
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

            //Şuan kullanıcı kayıt işlemeri Tamamlandı.
           
        }

        //Bu methodu birçok yerde kullanmak için yaptık.
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //HMACSHA algoritmasıyla hash  oluşturuyoruz.
            //Yani Şifreleme işlemleri Yapıyoruz.
            // Burda key ile salt ve Hash değerimiz set etmiş olduk.
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Kullanıcı Var mı işlemleri yaptık.
        public async Task<bool> UserExist(string userName)
        {
            if (await _context.Users.AnyAsync(x=>x.UserName == userName))
            {
                return true;

            }
            return false;
        }
    }
}
