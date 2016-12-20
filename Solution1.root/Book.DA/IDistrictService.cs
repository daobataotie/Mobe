using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Book.Model;
namespace Book.DA
{
    public  interface  IDistrictService
    {
        IList<District> GetAllDistrict(string sql, SqlParameter[] paras);
        IList<District> GetDistrict();
        IList<District> GetDistrictByID(int Id);
        IList<District> GetDistrictByCityID(int cityId);
        DataTable GetDistrictByCityIDTwo(int cityId);
        IList<District> GetDistrictByName(string name);
        IList<District> GetDistrictByNameAndCityId(string name, int cityId);
        bool Add(District model);
        bool Update(District model);
        bool Delete(int districtId);

    }
}
