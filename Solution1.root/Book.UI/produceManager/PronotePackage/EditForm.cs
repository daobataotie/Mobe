using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.produceManager.PronoteHeader;
using System.Linq;
using Book.UI.Query;

namespace Book.UI.produceManager.PronotePackage
{
    public partial class EditForm : DevExpress.XtraEditors.XtraForm
    {

        #region 對象變量的定義
        private System.Data.DataSet PronoterPackage;
        private BL.PronotePackageManager _pronotePackageManager = new BL.PronotePackageManager();
        private BL.ProductManager productmanager = new BL.ProductManager();
        private DataSet removeData;
        #endregion

        public EditForm()
        {
            InitializeComponent();
            for (int i = 0; i < 12; i++)
                this.ComBoxDate.Properties.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy年MM月"));
            this.ComBoxDate.SelectedIndex = 0;
            this.bindingSourcePronotePackage.DataSource = this.PronoterPackage.Tables[0];
            removeData = this.PronoterPackage;
            removeData.Tables[0].Rows.Clear();
        }


        private void ButtonSelect_PronoterHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChoosePronoteHeaderDetailsForm f = new ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                IList<Model.PronoteHeader> temps = ChoosePronoteHeaderDetailsForm._pronoteHeaderList;
                foreach (var item in temps)
                {
                    System.Data.DataRow tempRow = this.PronoterPackage.Tables[0].NewRow();
                    tempRow[Model.PronotePackage.PRO_PronotePackageId] = Guid.NewGuid().ToString();
                    tempRow[Model.PronotePackage.PRO_PronoteHeaderId] = item.PronoteHeaderID;
                    tempRow[Model.PronotePackage.PRO_PronotePackageDate] = System.DateTime.Now;
                    tempRow[Model.PronotePackage.PRO_ProductId] = item.Product.ProductId;
                    tempRow["Pname"] = item.Product.ToString();
                    tempRow[Model.PronotePackage.PRO_BadPercent] = 0;
                    tempRow[Model.PronotePackage.PRO_BadProductNum] = 0;
                    tempRow[Model.PronotePackage.PRO_BlackPoint] = 0;
                    tempRow[Model.PronotePackage.PRO_Box] = 0;
                    tempRow[Model.PronotePackage.PRO_CaShang] = 0;
                    tempRow[Model.PronotePackage.PRO_Feet] = 0;
                    tempRow[Model.PronotePackage.PRO_FullProductNum] = 0;
                    tempRow[Model.PronotePackage.PRO_GuaShang] = 0;
                    tempRow[Model.PronotePackage.PRO_GuoHuo] = 0;
                    tempRow[Model.PronotePackage.PRO_LiuHen] = 0;
                    tempRow[Model.PronotePackage.PRO_LouGuang] = 0;
                    tempRow[Model.PronotePackage.PRO_MianXu] = 0;
                    tempRow[Model.PronotePackage.PRO_Others] = 0;
                    tempRow[Model.PronotePackage.PRO_PenYao] = 0;
                    tempRow[Model.PronotePackage.PRO_QiPao] = 0;
                    tempRow[Model.PronotePackage.PRO_SuoShui] = 0;
                    tempRow[Model.PronotePackage.PRO_Total] = 0;
                    tempRow[Model.PronotePackage.PRO_WanMo] = 0;
                    tempRow[Model.PronotePackage.PRO_ZaZhi] = 0;
                    this.PronoterPackage.Tables[0].Rows.Add(tempRow);
                }

            }
        }

        private void ButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            //System.Data.DataTable temps = this.bindingSourcePronotePackage.DataSource as System.Data.DataTable;
            //if (this.PronoterPackage.HasChanges())
            {
                this._pronotePackageManager.UpdateData(this.PronoterPackage.Tables[0]);
                MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            //System.Data.DataRow temps = this.PronoterPackage.Tables[0].Rows[this.bindingSourcePronotePackage.Position] as System.Data.DataRow;
            //temps.Delete();
            //if (temps == null || temps.Rows.Count == 0) return;
            //this.PronoterPackage.Tables[0].Rows.RemoveAt(this.bindingSourcePronotePackage.Position);
            this.PronoterPackage.Tables[0].Rows[this.bindingSourcePronotePackage.Position].Delete();
            //this.PronoterPackage.AcceptChanges();
            this.gridControl1.RefreshDataSource();
        }

        private void ComBoxDate_SelectedValueChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(this.ComBoxDate.Text + "01日");
            if (dt == null) return;
            this.PronoterPackage = this._pronotePackageManager.GetDataTable(dt);
            this.bindingSourcePronotePackage.DataSource = this.PronoterPackage.Tables[0];
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnFullProductNum || e.Column == this.gridColumn5)
            {
                decimal full = decimal.Zero;
                decimal buliang = decimal.Zero;

                if (e.Column == this.gridColumnFullProductNum)
                {
                    decimal.TryParse(e.Value.ToString(), out full);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn5) == null ? "" : this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn5).ToString(), out buliang);
                }
                if (e.Column == this.gridColumn5)
                {
                    decimal.TryParse(e.Value.ToString(), out buliang);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnFullProductNum).ToString(), out full);
                }
                if (buliang != 0)
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnBadPercent, (buliang / (full + buliang) * 100) == 0 ? "" : (buliang / (full + buliang) * 100).ToString("##.##"));
                else
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnBadPercent, 0);
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn7, full + buliang);

            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm f = new ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ConditionA condition = f.Condition as ConditionA;
                RO report = new RO(condition);
                report.ShowPreview();
            }
        }
    }
}