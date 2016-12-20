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
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ������             ���ʱ��:2009-4-12
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionEChooseForm : ConditionAChooseForm
    {
       //��Ʒ����
        private BL.ProductManager productManager = new Book.BL.ProductManager();

        private ConditionE condition;

        private global::Helper.CompanyKind kind;


        /// <summary>
        /// �޲ι��캯��
        /// </summary>
        public ConditionEChooseForm()
        {
            InitializeComponent();
            this.newChooseSupStart.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseSupEnd.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
        }
        public ConditionEChooseForm(global::Helper.CompanyKind kind):this()
        {
            this.kind = kind;            
        }


        /// <summary>
        /// ��д���෽��
        /// </summary>
        protected override void OnOK()
        {
            if (condition == null)
                condition = new ConditionE();     
            condition.StartDate =this.dateEditStartDate.EditValue==null?global::Helper.DateTimeParse.NullDate: this.dateEditStartDate.DateTime;
            condition.EndDate = this.dateEditEndDate.EditValue == null ?DateTime.Now : this.dateEditEndDate.DateTime; ;
            condition.StartIdCompany = this.newChooseSupStart.EditValue==null?null:(this.newChooseSupStart.EditValue as Model.Supplier).Id;
            condition.EndIdCompany = this.newChooseSupEnd.EditValue==null?null:(this.newChooseSupEnd.EditValue as Model.Supplier).Id;
            condition.StartIdProduct = this.buttonEditProStart.EditValue==null?null:(this.buttonEditProStart.EditValue as Model.Product).Id;
            condition.EndIdProduct = this.buttonEditProEnd.EditValue==null?null:(this.buttonEditProEnd.EditValue as Model.Product).Id;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionE;
            }
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionEChooseForm_Load(object sender, EventArgs e)
        {
            //System.Collections.Generic.IList<Model.Company> companies = this.companyManager.Select(this.kind);
         //   System.Collections.Generic.IList<Model.Product> products = this.productManager.Select();

            //foreach (Model.Company company in companies)
            //{
            //    this.comboBoxEditCompanyStartId.Properties.Items.Add(string.Format("{0} {1}", company.CompanyId, company.CompanyName0));
            //    this.comboBoxEditCompanyEndId.Properties.Items.Add(string.Format("{0} {1}", company.CompanyId, company.CompanyName0));
            //}

            //foreach (Model.Product product in products)
            //{
            //    this.comboBoxEditProductStartId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
            //    this.comboBoxEditProductEndId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
            //}
        }

        private void comboBoxEditProductEndId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonEditProStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
            Invoices.ChooseProductForm f=new Book.UI.Invoices.ChooseProductForm();
            if(f.ShowDialog()!=DialogResult.OK) return;
            if((sender as  ButtonEdit).Name=="buttonEditProStart")
            {              
                this.buttonEditProStart.EditValue= f.SelectedItem as Model.Product;
            }
             if((sender as  ButtonEdit).Name=="buttonEditProEnd")
             {
                 this.buttonEditProEnd.EditValue = f.SelectedItem as Model.Product;
             }
          
        }
    }
}