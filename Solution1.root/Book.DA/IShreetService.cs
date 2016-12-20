using System;
using System.Collections.Generic;
using System.Text;
using Book.Model;
using System.Data;
using System.Data.SqlClient;
namespace Book.DA
{
    public interface IShreetService
    {
        IList<Shreet> GetAllShreet(string sql, SqlParameter[] paras);
        IList<Shreet> GetShreet();
        IList<Shreet> GetShreetTx();
        IList<Shreet> GetShreetByID(int Id);
        IList<Shreet> GetShreetByDistrictId(int districtId);
        DataTable GetShreetByDistrictIdTwo(int districtId);
        IList<Shreet> GetShreetByName(string name);
        IList<Shreet> GetShreetByKeyName(string KeyName);
        DataTable GetShreetByKeyNameTooo(string KeyName);
        bool Add(Shreet model);
        bool Update(Shreet model);
        bool Delete(int ShreetId);

    }
}
