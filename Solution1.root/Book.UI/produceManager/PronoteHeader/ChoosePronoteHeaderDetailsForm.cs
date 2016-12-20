using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PronoteHeader
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  裴盾            完成时间:2010-03-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChoosePronoteHeaderDetailsForm : DevExpress.XtraEditors.XtraForm
    {
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        Model.PronoteHeader pronoteHeader = new Book.Model.PronoteHeader();
        public static IList<Model.PronoteHeader> _pronoteHeaderList;
        private IList<Model.PronoteHeader> DetailList;
        private Model.WorkHouse workHouseIndepot;
        private int flag = 0;
        private int type = -1;   //0，非生產入库，1生产入库 生产入库时查询前部门转入 前部门合计等数量
        //默认能多选

        public ChoosePronoteHeaderDetailsForm()
        {
            InitializeComponent();
            // this.pronoteHeader.Details = PronotedetailsManager.Select();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1).Date;
            this.dateEditEndDate.DateTime = DateTime.Now;
            _pronoteHeaderList = new List<Model.PronoteHeader>();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.checkEdit1.Checked = true;
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.newChooseWorkHorse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
        }

        public Model.PronoteHeader SelectItem
        {
            get { return this.bindingSource1.Current as Model.PronoteHeader; }
        }

        /// <summary>
        /// 1:单选
        /// </summary>
        /// <param name="tag"></param>
        public ChoosePronoteHeaderDetailsForm(int tag)
            : this()
        {
            flag = tag;
            this.newChooseWorkHorse.Enabled = false;
            // this.pronoteHeader.Details = PronotedetailsManager.Select();

        }
        //i=0，非生產入库，1生产入库 生产入库时查询前部门转入 前部门合计等数量
        public ChoosePronoteHeaderDetailsForm(Model.WorkHouse workHouseIndepot, int i)
            : this()
        {
            this.workHouseIndepot = workHouseIndepot;
            this.type = i;
            this.newChooseWorkHorse.Enabled = false;
            this.newChooseWorkHorse.EditValue = workHouseIndepot;
        }

        private void ChoosePronoteHeaderDetailsForm_Load(object sender, EventArgs e)
        {
            if (flag == 1)
                gridView1.Columns[0].Visible = false;
            if (type == 0)
            {
                gridColumn17.Visible = false;
                gridColumn14.Visible = false;
                gridColumn15.Visible = false;
                gridColumn16.Visible = false;
            }
            if (type == -1)
            {
                gridColumn17.Visible = false;
                gridColumn14.Visible = false;
                gridColumn15.Visible = false;
                gridColumn16.Visible = false;
                gridColumn7.Visible = false;
            }
            //DetailList = this.pronoteHeaderManager.GetByDate(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, null, null, null, null, null, -1, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, true);

            //this.bindingSource1.DataSource = DetailList;
        }

        //确定
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //foreach (Model.Pronotedetails Pronotedetails in pronoteHeader.Details)
            //{
            //    if (Pronotedetails.Checkeds == true)
            //    {
            //        produceManager.ProduceInDepot.EditForm._pronotedetails.Add(Pronotedetails);
            //    }
            //}

            this.DialogResult = DialogResult.OK;
        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.PronoteHeader> details = this.bindingSource1.DataSource as IList<Model.PronoteHeader>;
            //if (details == null || details.Count < 1) return;
            //Model.Product products = details[e.ListSourceRowIndex].Product;
            ////Model.InvoiceXODetail invoiceXODetail = new BL.InvoiceXODetailManager().Get(details[e.ListSourceRowIndex].InvoiceXODetailId);
            ////Model.CustomerProducts cusProducts = details[e.ListSourceRowIndex].PrimaryKey;
            //switch (e.Column.Name)
            //{
            //    //case "gridColumn2":
            //    //    if (detail == null) return;
            //    //    e.DisplayText = detail.PronoteDate.Value.ToString("yyyy-MM-dd");
            //    //    break;
            //    case "gridColumnProId":
            //        if (products == null) return;
            //        e.DisplayText = string.IsNullOrEmpty(products.Id) ? "" : products.Id; ;
            //        break;
            //    //case "gridColumn7":
            //    //    if (invoiceXODetail == null) return;
            //    //    e.DisplayText = invoiceXODetail.InvoiceXODetailQuantity.ToString(); ;
            //    //    break;
            //    case "gridColumnCusProName":
            //        if (products == null) return;
            //        e.DisplayText = string.IsNullOrEmpty(products.CustomerProductName) ? "" : products.CustomerProductName;
            //        break;
            //}
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_pronoteHeaderList == null) return;
            if (e.Column.Name == "gridColumnChecked")
            {
                Model.PronoteHeader header = this.gridView1.GetRow(e.RowHandle) as Model.PronoteHeader;

                if (header != null)
                {
                    if ((bool)e.Value)
                    {
                        _pronoteHeaderList.Add(header);
                    }
                    if (!(bool)e.Value)
                    {
                        for (int i = 0; i < _pronoteHeaderList.Count; i++)
                        {
                            if (_pronoteHeaderList[i].PronoteHeaderID == header.PronoteHeaderID)
                            {
                                _pronoteHeaderList.RemoveAt(i);
                                break;
                            }
                        }

                    }
                }
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            DateTime startTime = global::Helper.DateTimeParse.NullDate;
            DateTime endTime = global::Helper.DateTimeParse.EndDate;
            if (this.dateEditStartDate.EditValue != null)
            {
                startTime = this.dateEditStartDate.DateTime;
            }
            if (this.dateEditEndDate.EditValue != null)
            {
                endTime = this.dateEditEndDate.DateTime;
            }

            if (type == 0) //质检
                DetailList = this.pronoteHeaderManager.GetByDateZJ(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text, this.buttonEditPro1.EditValue as Model.Product, null, null, -1, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, this.checkEdit1.Checked, this.TXTproNameKey.Text, this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text);
            else if (type == 1) //生产入库
                DetailList = this.pronoteHeaderManager.GetByDate(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text, this.buttonEditPro1.EditValue as Model.Product, null, null, -1, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, this.checkEdit1.Checked, this.TXTproNameKey.Text, this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text);
            else
            {
                this.workHouseIndepot = this.newChooseWorkHorse.EditValue as Model.WorkHouse;
                DetailList = this.pronoteHeaderManager.GetByDateMa(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text, this.buttonEditPro1.EditValue as Model.Product, null, null, -1, this.workHouseIndepot == null ? null : this.workHouseIndepot.WorkHouseId, this.checkEdit1.Checked, this.TXTproNameKey.Text, this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text);
            }


            if (DetailList != null)
            {
                int flag = 0;
                for (int i = 0; i < _pronoteHeaderList.Count; i++)
                {
                    foreach (Model.PronoteHeader detail in DetailList)
                    {
                        if (_pronoteHeaderList[i].PronoteHeaderID == detail.PronoteHeaderID)
                        {
                            detail.Checkeds = true;
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                        break;
                }
            }
            this.bindingSource1.DataSource = DetailList;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Model.Pronotedetails Pronotedetails = this.bindingSource1.Current as Model.Pronotedetails;
            //if (Pronotedetails != null)
            //{
            //    produceManager.ProduceInDepot.EditForm._pronotedetails.Add(Pronotedetails);
            //}
            this.DialogResult = DialogResult.OK;
        }


        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if ((this.bindingSource1.Current as Model.PronoteHeader).IsClose.Value)
                return;
            if (MessageBox.Show("結案后,此加工單將不能做領料,退料與入庫動作,是否繼續?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.gridView1.SetRowCellValue(DetailList.IndexOf(this.bindingSource1.Current as Model.PronoteHeader), this.gridColumn13, true);
            this.pronoteHeaderManager.UpdateHeaderIsClse((this.bindingSource1.Current as Model.PronoteHeader).PronoteHeaderID, true);
            //从当前集合删除该项（2012.12.7）
            this.bindingSource1.Remove(this.bindingSource1.Current);
        }

        //加工单查看
        private void ItemHyperLinkPronoteHeaderID_Click(object sender, EventArgs e)
        {
            Model.PronoteHeader d = (this.bindingSource1.Current as Model.PronoteHeader);
            if (d != null)
            {
                PronoteHeader.EditForm f = new EditForm(d);
                f.ShowDialog();
            }
        }

        private void buttonEditPro1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro1.EditValue = form.SelectedItem as Model.Product;
            }
            form.Dispose();
            GC.Collect();
        }
    }
}