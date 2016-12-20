using System;
using System.Collections.Generic;

using System.Text;

namespace Book.Model
{
    [Serializable]
    public class Shreet
    {
        private int shreetId;

        public int ShreetId
        {
            get { return shreetId; }
            set { shreetId = value; }
        }
        private string shreetName;

        public string ShreetName
        {
            get { return shreetName; }
            set { shreetName = value; }
        }
        private int districtId;

        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }
        private District district;

        public District District
        {
            get { return district; }
            set { district = value; }
        }
        public override string ToString()
        {
            return this.shreetName;
        }
    }
}
