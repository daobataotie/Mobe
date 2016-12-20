using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 裴盾              完成时间:2009-5-9
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class ConditionProInDepotChooseForm : ConditionChooseForm
    {
        //Q51  生產領料單
        private ConditionProInDepotChoose condition;
        BL.PronoteHeaderManager pronoteManager = new BL.PronoteHeaderManager();

        public ConditionProInDepotChooseForm()
        {
            InitializeComponent();
            this.newChooseWorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlDepot.Choose = new Book.UI.Invoices.ChooseDepot();
            this.newChooseCustomer1.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseCustomer2.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.dateEdit1.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEdit2.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionProInDepotChoose;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProInDepotChoose();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEdit1.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit2.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEdit2.DateTime;
            }
            this.condition.Product = this.buttonEditPro.EditValue as Model.Product;
            this.condition.StartPronoteHeader = this.buttonEditPronote1.Text;
            this.condition.EndPronoteHeader = this.buttonEditPronote2.Text;
            this.condition.WorkHouse = this.newChooseWorkHouse.EditValue as Model.WorkHouse;
            this.condition.MDepot = this.newChooseContorlDepot.EditValue as Model.Depot;
            this.condition.MDepotPosition = this.newChooseContorlDepotPosition.EditValue as Model.DepotPosition;
            this.condition.Id1 = this.buttonEditId1.EditValue == null ? null : this.buttonEditId1.Text;
            this.condition.Id2 = this.buttonEditid2.EditValue == null ? null : this.buttonEditid2.Text;
            this.condition.Cusxoid = this.textEditCusXOId.EditValue == null ? null : this.textEditCusXOId.Text; ;
            this.condition.Customer1 = this.newChooseCustomer1.EditValue as Model.Customer;
            this.condition.Customer2 = this.newChooseCustomer2.EditValue as Model.Customer;
            this.condition.ProductState = this.comBoxProductState.SelectedIndex;
        }

        private void ConditionProInDepotChooseForm_Load(object sender, EventArgs e)
        {
            this.labelCusPro.Enabled = false;
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                this.labelCusPro.Text = (f.SelectedItem as Model.Product).CustomerProductName;
            }
            f.Dispose();
            GC.Collect();
        }

        private void buttonEditPronote1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(1);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPronote1.EditValue = f.SelectItem;
            }
            f.Dispose();
            GC.Collect();
        }

        private void buttonEditPronote2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(1);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPronote2.EditValue = f.SelectItem;
            }
            f.Dispose();
            GC.Collect();
        }

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            Model.Depot depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            if (depot != null)
            {
                this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(depot);
                this.newChooseContorlDepotPosition.EditValue = null;
            }
            else
            {
                this.newChooseContorlDepotPosition.Choose = null;
                this.newChooseContorlDepotPosition.EditValue = null;
            }
        }

        private void buttonEditId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceInDepot.SelectInDepotForm f = new Book.UI.produceManager.ProduceInDepot.SelectInDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditId1.EditValue = f.SelectItem == null ? null : f.SelectItem.ProduceInDepotId;
                this.buttonEditid2.EditValue = f.SelectItem == null ? null : f.SelectItem.ProduceInDepotId;
            }


        }

        private void buttonEditid2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceInDepot.SelectInDepotForm f = new Book.UI.produceManager.ProduceInDepot.SelectInDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditid2.EditValue = f.SelectItem == null ? null : f.SelectItem.ProduceInDepotId;
            }

        }
    }
}