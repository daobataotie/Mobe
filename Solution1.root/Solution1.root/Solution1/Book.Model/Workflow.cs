//------------------------------------------------------------------------------
//
// file name：Workflow.cs
// author: peidun
// create date：2009-11-18 15:44:42
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 工作流表
	/// </summary>
	[Serializable]
	public partial class Workflow
	{

        public override string ToString()
        {
          
            return this._workflowname;

       
        }
        private System.Collections.Generic.IList<Model.process> details;

        public System.Collections.Generic.IList<Model.process> Details
        {
            get { return details; }
            set { details = value; }
        }
           
	}
}
