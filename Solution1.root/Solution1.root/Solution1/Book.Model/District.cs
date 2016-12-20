using System;
using System.Collections.Generic;

using System.Text;

namespace Book.Model
{
    [Serializable]
    public class District
    {
        private int districtId;

        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }
        private string districtName;

        public string DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }
        private int cityId;

        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }
        private City city;

        public City City
        {
            get { return city; }
            set { city = value; }
        }
        public override string ToString()
        {
            return this.districtName;
        }
    }
}
