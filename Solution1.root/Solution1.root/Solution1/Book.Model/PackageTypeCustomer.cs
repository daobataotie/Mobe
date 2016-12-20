//------------------------------------------------------------------------------
//
// file name：PackageTypeCustomer.cs
// author: peidun
// create date：2009-08-13 15:38:49
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 包装类型归属客户
	/// </summary>
	[Serializable]
	public partial class PackageTypeCustomer
	{
        public string CompanyPhone1 
        {
            get 
            {
                if (this.customer != null)
                    return customer.CustomerPhone;
                else 
                {
                    return string.Empty;
                }
            }
        }
        public string CompanyAddress 
        {
            get 
            {
                if (this.customer != null)
                    return customer.CustomerAddress;
                else 
                {
                    return string.Empty;
                }
            }
        }
	}
}
