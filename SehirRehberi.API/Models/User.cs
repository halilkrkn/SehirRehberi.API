using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class User
    {
        //İlişkilendirme olması için Konstraktra tanımlıyoruz
        //Yani User bir tane ama User ın  şehirleri birden fazla anlamına geliyor
        public User()
        {
            Cities = new List<City>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // burda ilişkilendirme yapıyoruz. ve User class ının Cities ile ilişkilendirdik
        //User ın Şehirleri birden fazla anlamına geliyor.
        public List<City> Cities { get; set; }


        //Buradaki Byte[] Veri Tabanındaki varbinary e denk geliyor.
    }
}
