using System;
using System.Collections.Generic;

using System.Text;

using System.Data;
using Book.Model;

namespace Book.BL
{
    public static class DistrictManager
    {
        private static readonly DA.IDistrictService DistrictService = (DA.IDistrictService)Accessors.Get("DistrictService");
        public static IList<District> GetDistrict()
        {
            return DistrictService.GetDistrict();
        }
        public static IList<District> GetDistrictByID(int Id)
        {
            return DistrictService.GetDistrictByID(Id);
        }
        public static IList<District> GetDistrictByCityID(int cityId)
        {
           return DistrictService.GetDistrictByCityID(cityId);
        }
        public static DataTable GetDistrictByCityIDTwo(int cityId) {
            return DistrictService.GetDistrictByCityIDTwo(cityId);
        }
        public static IList<District> GetDistrictByName(string name)
        {
           return DistrictService.GetDistrictByName(name);
        }
        public static IList<District> GetDistrictByNameAndCityId(string name, int cityId)
        {
            return DistrictService.GetDistrictByNameAndCityId(name, cityId);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(District model)
        {
           return DistrictService.Add(model);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(District model)
        {
           return DistrictService.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int districtId)
        {
           return DistrictService.Delete(districtId);
        }
    }
}
