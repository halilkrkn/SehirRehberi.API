using SehirRehberi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Data
{
    //Authentication işlemleri için Ayrı bir Repository yaptık.
    //Bunun için REgister Login Veritabanına bağlanacaz.
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExist(string userName);
    }
}
