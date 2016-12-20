using System;
using System.Collections.Generic;

using System.Text;

using Book.Model;


namespace Book.BL
{
    public static class CityManager
    {
        private static readonly DA.ICityService CityService = (DA.ICityService)Accessors.Get("CityService");
    
        public static IList<City> GetCity()
        {
            return CityService.GetCity();
        }
        public static IList<City> GetCityByID(int Id)
        {
            return CityService.GetCityByID(Id);
        }
        public static IList<City> GetCityByName(string name)
        {
            return CityService.GetCityByName(name);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(City model)
        {
            return CityService.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(City model)
        {
            return CityService.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int CityId)
        {
            return CityService.Delete(CityId);
        }
    }
}
