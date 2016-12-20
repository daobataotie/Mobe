using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    public partial class ChooseTechonProceduresForm : Settings.BasicData.BaseChooseForm
    {
        private BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();
        private BL.TechonlogyHeaderManager techonlogyHeaderManager = new Book.BL.TechonlogyHeaderManager();
        private BL.TechnologydetailsManager technologydetailsManager = new Book.BL.
            TechnologydetailsManager();
        private BL.WorkHouseManager workHouseManger = new Book.BL.WorkHouseManager();
        public ChooseTechonProceduresForm()
        {
            InitializeComponent();
            this.manager = new BL.ProceduresManager();
        }
        protected override void LoadData()
        {
            //BL.WorkHouseManager managera = new BL.WorkHouseManager();
          //// this.bindingSourceTechonlogy.DataSource = this.techonlogyHeaderManager.Select();
            this.bindingSourceTechonlogy.DataSource = this.workHouseManger.Select();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.ProduceManager.Techonlogy.ProceduresEditForm();
        }
        private void bindingSourcetechonlogy_CurrentChanged(object sender, EventArgs e)
        {

            this.gridView1.OptionsBehavior.Editable = true;
       
            ////Model.TechonlogyHeader techonlogyHeader = this.bindingSourceTechonlogy.Current as Model.TechonlogyHeader;
           //// this.bindingSource1.DataSource = this.technologydetailsManager.Select(techonlogyHeader);
            Model.WorkHouse workHouse = this.bindingSourceTechonlogy.Current as Model.WorkHouse;

            this.bindingSource1.DataSource = this.proceduresManager.Select(workHouse.WorkHouseId);
        }

        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    //if (e.ListSourceRowIndex < 0) return;
        //    //IList<Model.Technologydetails> details = this.bindingSource1.DataSource as IList<Model.Technologydetails>;
        //    //if (details == null || details.Count < 1) return;
        //    //Model.Technologydetails detail = details[e.ListSourceRowIndex];
        //    //if (detail.Procedures == null) return;
        //    if (e.ListSourceRowIndex < 0) return;
        //    IList<Model.Procedures> details = this.bindingSource1.DataSource as IList<Model.Procedures>;
        //    if (details == null || details.Count < 1) return;
        //    Model.Procedures detail = details[e.ListSourceRowIndex];
        //    if (detail == null) return;
        //    switch (e.Column.Name)
        //    {
        //        case "gridColumnWorkhouse":
        //            e.DisplayText =  detail.WorkHouse.Workhousename;
        //            break;
        //        //case "gridColumnDesc":
        //        //    e.DisplayText = detail.Procedures.Proceduredescription;
        //        //    break;
        //        //case "gridColumnId":
        //        //    e.DisplayText = detail.Procedures.Id;
        //        //    break;


        //           // break;
        //    }
        //}

        //private void ChooseTechonProceduresForm_Load(object sender, EventArgs e)
        //{

        //}

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == this.gridColumn1.Name)
            {               
                Model.Procedures procedures = this.gridView1.GetRow(e.RowHandle) as Model.Procedures;
                //if (procedures.IsChecked == null || procedures.IsChecked.Value == false)
                //    procedures.IsChecked = true;
                //else
                //    procedures.IsChecked = false;

                if ((bool)e.Value)
                {
                    BasicData.Products.EditForm._proceduresStatic.Add(procedures);
                }
                if (!(bool)e.Value)
                {
                    BasicData.Products.EditForm._proceduresStatic.Remove(procedures);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == this.gridColumn1.Name)
            //{
            //    this.gridControl1.RefreshDataSource();
            //    Model.Procedures procedures = this.gridView1.GetRow(e.RowHandle) as Model.Procedures;
            //    if (procedures.IsChecked == null) return;
            //    if (procedures.IsChecked.Value)
            //    {
            //        BasicData.Products.EditForm._proceduresStatic.Add(procedures);
            //    }
            //    else
            //    {
            //        BasicData.Products.EditForm._proceduresStatic.Remove(procedures);
            //    }


            //}
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}