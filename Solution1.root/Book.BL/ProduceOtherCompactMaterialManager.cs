//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactMaterialManager.cs
// author: mayanjun
// create date：2010-12-2 16:11:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherCompactMaterial.
    /// </summary>
    public partial class ProduceOtherCompactMaterialManager
    {
		
		/// <summary>
		/// Delete ProduceOtherCompactMaterial by primary key.
		/// </summary>
		public void Delete(string produceOtherCompactMaterialId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceOtherCompactMaterialId);
		}

		/// <summary>
		/// Insert a ProduceOtherCompactMaterial.
		/// </summary>
        public void Insert(Model.ProduceOtherCompactMaterial produceOtherCompactMaterial)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceOtherCompactMaterial);
        }
		
		/// <summary>
		/// Update a ProduceOtherCompactMaterial.
		/// </summary>
        public void Update(Model.ProduceOtherCompactMaterial produceOtherCompactMaterial)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceOtherCompactMaterial);
        }

        public IList<Model.ProduceOtherCompactMaterial> SelectIsInDepotMaterialDetail(Model.ProduceOtherCompact com)
        {
            return accessor.SelectIsInDepotMaterialDetail(com);
        }
        public IList<Model.ProduceOtherCompactMaterial> Select(Model.ProduceOtherCompact ProduceOtherCompact)
        {
            return accessor.Select(ProduceOtherCompact);
        
        }
        public IList<Model.ProduceOtherCompactMaterial> SelectCompactAndFlag(Model.ProduceOtherCompact ProduceOtherCompact)
        {
            return accessor.SelectCompactAndFlag(ProduceOtherCompact);
        }
    }
}

