//------------------------------------------------------------------------------
//
// file name：PackageDetailsAccessor.cs
// author: peidun
// create date：2009-08-12 9:45:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of PackageDetails
    /// </summary>
    public partial class PackageDetailsAccessor : EntityAccessor, IPackageDetailsAccessor
    {
        #region IPackageDetailsAccessor Members


        public IList<Book.Model.PackageDetails> Select(Book.Model.PackageType type)
        {
            return sqlmapper.QueryForList<Model.PackageDetails>("PackageDetails.select_by_packageTypeId", type.PackageTypeId);
        }

        public void Delete(Book.Model.PackageType packageType)
        {
            sqlmapper.Delete("PackageDetails.delete_by_packagetypeid", packageType.PackageTypeId);
        }

        #endregion
    }
}
