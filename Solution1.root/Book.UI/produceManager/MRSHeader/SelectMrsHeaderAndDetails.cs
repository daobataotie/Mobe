using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace Book.UI.produceManager.MRSHeader
{
    public partial class SelectMrsHeaderAndDetails : XtraForm
    {
        BL.MRSHeaderManager _mrsHeaderManager = new Book.BL.MRSHeaderManager();
        BL.MRSdetailsManager mRSdetailsManager = new Book.BL.MRSdetailsManager();
        private IList<Model.MRSdetails> List;
        //public IList<Model.MRSdetails> MrsDetails { get; set; }
        public IList<string> key { get; set; }

        public SelectMrsHeaderAndDetails()
        {
            InitializeComponent();
            //   MrsDetails = new List<Model.MRSdetails>();
            key = new List<string>();
            this.Load += new EventHandler(SelectMrsHeaderAndDetails_Load);
        }

        private void SelectMrsHeaderAndDetails_Load(object sender, EventArgs e)
        {
            //初始化泛型集合
            //  List = new List<Model.MRSHeader>();
            // MrsDetails = new List<Model.MRSdetails>();
            this.StartDate.DateTime = System.DateTime.Now.AddDays(-7);
            this.EndDate.DateTime = System.DateTime.Now;
          //  this.simple_SearCh.PerformClick();
        }

        private void simple_SearCh_Click(object sender, EventArgs e)
        {
            //  List.Clear();

            //foreach (Model.MRSHeader item in Temp)
            //{
            //    Model.MRSHeader Tp = this._mrsHeaderManager.GetDetails(item.MRSHeaderId);
            //    List.Add(Tp);
            //}
            this.bindingSourceMrsData.DataSource = this._mrsHeaderManager.SelectbyCondition(null, null, null, null, this.StartDate.DateTime, this.EndDate.DateTime, -1, null, null, this.txtCustomerId.Text, null);
            if (this.bindingSourceMrsData.Current != null)
            {
                List = this.mRSdetailsManager.Select(this.bindingSourceMrsData.Current as Model.MRSHeader);
                foreach (Model.MRSdetails detail in List)
                {
                    if (key.Contains(detail.MRSdetailsId))
                        detail.IsChecked = true;
                }
                this.gridControl2.DataSource = List;
            }
            else
                this.gridControl2.DataSource = null;

            this.gridControl2.RefreshDataSource();
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            //MrsDetails = (from d in (from head in List where head.IsNodTheCurent = true select head.Details.Where(hh => hh.IsChecked)) select d).Aggregate((a, b) => a.Concat(b)).ToList<Model.MRSdetails>();
            this.DialogResult = DialogResult.OK;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.bindingSourceMrsData.Current != null)
            {
                List = this.mRSdetailsManager.Select(this.bindingSourceMrsData.Current as Model.MRSHeader);
                foreach (Model.MRSdetails detail in List)
                {
                    if (key.Contains(detail.MRSdetailsId))
                        detail.IsChecked = true;
                }
                this.gridControl2.DataSource = List;
            }

            // temp.IsNodTheCurent = true;
            // this.gridControl2.DataSource = temp.Details;
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (this.bindingSourceMrsData.Current != null)
            {
                List = this.mRSdetailsManager.Select(this.bindingSourceMrsData.Current as Model.MRSHeader);
                foreach (Model.MRSdetails detail in List)
                {
                    if (key.Contains(detail.MRSdetailsId))
                        detail.IsChecked = true;
                }
                this.gridControl2.DataSource = List;
            }
            //Model.MRSHeader temp = this.bindingSourceMrsData.Current as Model.MRSHeader;
            //if (temp == null)
            //{
            //    this.gridControl2.DataSource = null;
            //    return;
            //}
            //temp.IsNodTheCurent = true;
            //this.gridControl2.DataSource = temp.Details;
        }

        private void gridView3_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnCheck")
            {
                Model.MRSdetails detail = this.gridView3.GetRow(e.RowHandle) as Model.MRSdetails;

                if ((bool)e.Value)
                {
                    key.Add(detail.MRSdetailsId);
                    //  MrsDetails.Add(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
                if (!(bool)e.Value)
                {
                    key.Remove(detail.MRSdetailsId);
                    //  MrsDetails.Remove(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
            }
        }

        private void checkEditALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditALL.Checked == true)
            {
                foreach (Model.MRSdetails detail in List)
                {
                    detail.IsChecked = true;
                    if (!key.Contains(detail.MRSdetailsId))
                        key.Add(detail.MRSdetailsId);
                }
            }
            if (checkEditALL.Checked == false)
            {
                foreach (Model.MRSdetails detail in List)
                {
                    detail.IsChecked = false;
                    if (key.Contains(detail.MRSdetailsId))
                        key.Remove(detail.MRSdetailsId);
                }
            }
            this.gridView3.UpdateCurrentRow();
            this.gridView3.PostEditor();
            this.gridControl2.RefreshDataSource();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            key.Clear();
            List.Clear();
            this.Close();
        }
    }
}
