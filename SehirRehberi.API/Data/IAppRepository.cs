using SehirRehberi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Data
{
    public interface IAppRepository
    {
        //CRUD Operasyonları için Yaptık
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T: class;
        bool SaveAll();

        //Veri Okuma Operasyonları Yapacaz

        // TÜm City datasını Çekmek için
        List<City> GetCities();
        //Şehirin photosu datasını çekmek için
        List<Photo> GetPhotosByCity(int cityid);
        //Belli bir Şehrin detayına gitmek için datasını çekecez
        City GetCityById(int cityId);
        //Belli Bir foto nun datasını çekmek için. 
        Photo GetPhoto(int id);


    }
}
