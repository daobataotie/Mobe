//------------------------------------------------------------------------------
//
// file name：RoleAuditing.cs
// author: mayanjun
// create date：2012/10/19 12:01:58
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// Table_227
	/// </summary>
	[Serializable]
	public partial class RoleAuditing
	{
        public  string AuditStateName
        {
            get
            {
                string b = string.Empty;
                switch (this._auditState.HasValue ? this._auditState.Value : 0)
                {

                    case 0: b = "未啟用";
                        break;
                    case 1: b = "待審核";
                        break;
                    case 2: b = "審核中";
                        break;
                    case 3: b = "已審核";
                        break;
                    case 4: b = "棄核";
                        break;
                    default: b = "未啟用";
                        break;
                }
                return b;
            }
        }

        private string _employee1Name;
        public string Employee1Name
        {
            get { return _employee1Name; }
            set { this._employee1Name = value; }
        }
        private string _employee0Name;
        public string Employee0Name
        {
            get { return _employee0Name; }
            set { this._employee0Name = value; }
        }

	}
}
