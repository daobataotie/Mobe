//------------------------------------------------------------------------------
//
// file name：AcademicBackGroundManager.cs
// author: peidun
// create date：2009-09-02 上午 10:38:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcademicBackGround.
    /// </summary>
    public partial class AcademicBackGroundManager : BaseManager
    {
		/// <summary>
		/// Delete AcademicBackGround by primary key.
		/// </summary>
		public void Delete(string academicBackGroundId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(academicBackGroundId);
		}

		/// <summary>
		/// Insert a AcademicBackGround.
		/// </summary>
        public void Insert(Model.AcademicBackGround academicBackGround)
        {
			//
			// todo:add other logic here
			//
            Validate(academicBackGround);
            academicBackGround.InsertTime = DateTime.Now;
            academicBackGround.AcademicBackGroundId = Guid.NewGuid().ToString();
            
            accessor.Insert(academicBackGround);
        }
		
		/// <summary>
		/// Update a AcademicBackGround.
		/// </summary>
        public void Update(Model.AcademicBackGround academicBackGround)
        {
			//
			// todo: add other logic here.
			//
            Validate(academicBackGround);
            academicBackGround.UpdateTime = DateTime.Now;
            accessor.Update(academicBackGround);
        }
        private void Validate(Model.AcademicBackGround academic)
        {
            if (string.IsNullOrEmpty(academic.AcademicBackGroundName))
            {
                throw new Helper.RequireValueException(Model.AcademicBackGround.PROPERTY_ACADEMICBACKGROUNDNAME);
            }
            if (accessor.IsExistName(academic.AcademicBackGroundId, academic.AcademicBackGroundName))
                throw new Helper.InvalidValueException(Model.AcademicBackGround.PROPERTY_ACADEMICBACKGROUNDNAME);
        }


        public void UpdateDataTable(DataTable accounts)
        {
             accessor.UpdateDataTable(accounts);
        }
        //protected override string GetInvoiceKind()
        //{
        //    return base.GetInvoiceKind();
        //}

        //protected override string GetSettingId()
        //{
        //    return base.GetSettingId();
        //}
        public DataSet SelectNoModel()
        {
            return accessor.SelectNoModel();
        }

        public bool Selectbyname(string academname)
        {
            return accessor.Selectbyname(academname);
        }

        public bool IsExistName(string id, string name)
        {
            return accessor.IsExistName(id, name);
        }
    }
}

