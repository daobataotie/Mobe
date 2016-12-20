//------------------------------------------------------------------------------
//
// file name：IBomParentPartInfoAccessor.cs
// author: peidun
// create date：2009-08-25 17:08:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BomParentPartInfo
    /// </summary>
    public partial interface IBomParentPartInfoAccessor : IAccessor
    {
        Book.Model.BomParentPartInfo Get(Book.Model.Product product);
        IList<Model.BomParentPartInfo> SelectNotContent();
        Book.Model.BomParentPartInfo Get(Book.Model.Product product, Model.Customer customer);
        //void   Delete(Book.Model.Product product, Model.Customer customer);
        void Delete(Book.Model.Product product);
        //void DeleteByInProductCustomer(Book.Model.Product product, Model.Customer customer);
        IList<Model.BomParentPartInfo> SelectNotContentByCustomer(Model.Customer customer);
        bool Exists_Field(string sqlWhere);
        IList<Model.BomParentPartInfo> SelectByIdOrNameKey(string bomid, string proid, string productName, string customerProductName);
        Model.BomParentPartInfo GetPrev1(Model.BomParentPartInfo e);
        bool HasRowsBefore1(Model.BomParentPartInfo e);
        bool HasRowsAfter1(Model.BomParentPartInfo e);
        Model.BomParentPartInfo GetFirst1();
        Model.BomParentPartInfo GetLast1();
        Model.BomParentPartInfo GetNext1(Model.BomParentPartInfo e);
        bool HasRows1();
        DataSet SelectNotContentDataSet();
        DataSet SelectDataSet();

        Model.BomParentPartInfo Select_ProductId(string productid);

    }
}

