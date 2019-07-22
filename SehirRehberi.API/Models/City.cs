using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class City
    {
        // BUrda ise City bir tane ama City nin Şehitleri birden fazla anlamına geliyor.
        public City()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //burada Photo ve User ı ilişkilendirdik. Yani City classının photo ve User ı var demek istedik

            //City nin photo su birden fazla anlamına geliyor
        public List<Photo> Photos { get; set; }
        //Yani City nin kullanıcı bir tane User mevcut.
        public User User { get; set; }
    }
}
