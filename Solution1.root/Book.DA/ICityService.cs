using System;
using System.Collections.Generic;
using System.Text;
using Book.Model;
using System.Data.SqlClient;
namespace Book.DA
{
    public interface ICityService
    {
        IList<City> GetAllCity(string sql, SqlParameter[] paras);
        IList<City> GetCity();
        IList<City> GetCityByID(int Id);
        IList<City> GetCityByName(string name);
        bool Add(City model);
        bool Update(City model);
        bool Delete(int CityId);
    }
}
