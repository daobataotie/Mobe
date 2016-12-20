using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
namespace Book.UI.Settings.ProduceManager.ProceduresOther
{
    public partial class ProceduresPriceListForm : Settings.BasicData.BaseListForm
    {
        public ProceduresPriceListForm()
        {
            InitializeComponent();
            this.manager = new BL.ProceduresPriceManager();
        }
        protected override BaseEditForm GetEditForm()
        {
            return new ProceduresPriceEditForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(ProceduresPriceEditForm);
            return (ProceduresPriceEditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProceduresPrice> details = this.bindingSource1.DataSource as IList<Model.ProceduresPrice>;
            if (details == null || details.Count < 1) return;
            Model.BomParentPartInfo bom = details[e.ListSourceRowIndex].Bom;
            if (bom == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnBomId":
                    e.DisplayText = bom.Id;
                    break;
                case "gridColumnProductName":
                    e.DisplayText =bom.Product==null?null:bom.Product.ProductName;
                    break;
                case "gridColumnFg":
                    e.DisplayText = bom.Product == null ? null : bom.Product.ProductSpecification;
                    break;
            }
        }
    }
}