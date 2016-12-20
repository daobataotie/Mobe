using System;
using System.Collections.Generic;

using System.Text;

namespace Book.Model
{
    [Serializable]
    public class City
    {
        private int cityId;

        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }
        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
        public override string ToString()
        {
            return this.cityName;
        }
    }
}
