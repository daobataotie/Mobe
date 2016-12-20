//------------------------------------------------------------------------------
//
// file name：BomComponentInfoManager.cs
// author: peidun
// create date：2009-08-25 17:08:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BomComponentInfo.
    /// </summary>
    public partial class BomComponentInfoManager:BaseManager
    {
		
		/// <summary>
		/// Delete BomComponentInfo by primary key.
		/// </summary>
		public void Delete(string priamryKey)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(priamryKey);
		}

		/// <summary>
		/// Insert a BomComponentInfo.
		/// </summary>
        public void Insert(Model.BomComponentInfo bomComponentInfo)
        {
			//
			// todo:add other logic here
			//

            accessor.Insert(bomComponentInfo);
        }
		
		/// <summary>
		/// Update a BomComponentInfo.
		/// </summary>
        public void Update(Model.BomComponentInfo bomComponentInfo)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(bomComponentInfo);
        }

        public IList<Model.BomComponentInfo>  Select(Model.BomParentPartInfo bomParentPartInfo)
        {
            //
            // todo: add other logic here.
            //
           return    accessor.Select(bomParentPartInfo);
        }

        public IList<Model.BomComponentInfo> SelectNotContent(Model.BomParentPartInfo bom)
        {
            return accessor.SelectNotContent(bom);
        }

        public IList<Model.BomComponentInfo> SelectLessInfoByHeaderId(string BomId)
        {
            return null;
        }

        public Model.BomComponentInfo IsExistsIndexOfBom(Model.BomComponentInfo bomcom)
        {
            return accessor.IsExistsIndexOfBom(bomcom);
        }

        
    }
}

