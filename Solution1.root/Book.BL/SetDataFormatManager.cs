//------------------------------------------------------------------------------
//
// file name：SetDataFormatManager.cs
// author: mayanjun
// create date：2012-4-5 15:34:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SetDataFormat.
    /// </summary>
    public partial class SetDataFormatManager
    {
        public void Delete(string setDataFormatId)
        {
            accessor.Delete(setDataFormatId);
        }

        public void Insert(Model.SetDataFormat setDataFormat)
        {
            try
            {
                BL.V.BeginTransaction();

                this.Delete(setDataFormat.SetDataFormatId);

                setDataFormat.InsertTime = DateTime.Now;
                accessor.Insert(setDataFormat);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.SetDataFormat setDataFormat)
        {
            accessor.Update(setDataFormat);
        }
        public Model.SetDataFormat GetData()
        { 
             Model.SetDataFormat setDataFormat;
            setDataFormat=this.GetFirst();
            if (setDataFormat == null)
            {
                setDataFormat = new Book.Model.SetDataFormat();
                foreach (var item in setDataFormat.GetType().GetProperties())
                {

                    if (item.PropertyType.FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                    {
                        item.SetValue(setDataFormat, 0,null);
                    }
               
                }

            }
            return setDataFormat;

        }
    }
}

