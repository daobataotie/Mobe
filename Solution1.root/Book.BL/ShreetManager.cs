using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Book.Model;

namespace Book.BL
{
    public static class ShreetManager
    {
        private static readonly DA.IShreetService ShreetService = (DA.IShreetService)Accessors.Get("ShreetService");
        public static IList<Shreet> GetShreet()
        {
            return ShreetService.GetShreet();
        }
        public static IList<Shreet> GetShreetTx()
        {
            return ShreetService.GetShreetTx();
        }
        public static IList<Shreet> GetShreetByID(int Id)
        {
            return ShreetService.GetShreetByID(Id);
        }
        public static IList<Shreet> GetShreetByDistrictId(int districtId)
        {
            return ShreetService.GetShreetByDistrictId(districtId);
        }
        public static DataTable GetShreetByDistrictIdTwo(int districtId)
        {
            return ShreetService.GetShreetByDistrictIdTwo(districtId);
        }
        public static IList<Shreet> GetShreetByName(string name)
        {
            return ShreetService.GetShreetByName(name);
        }
        public static IList<Shreet> GetShreetByKeyName(string KeyName)
        {
            return ShreetService.GetShreetByKeyName(KeyName);
        }
        public static DataTable GetShreetByKeyNameTooo(string KeyName)
        {
            return ShreetService.GetShreetByKeyNameTooo(KeyName);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(Shreet model)
        {
            return ShreetService.Add(model);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Shreet model)
        {
            return ShreetService.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int ShreetId)
        {
            return ShreetService.Delete(ShreetId);
        }
    }
}
