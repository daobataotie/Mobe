using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Invoices;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class ConditionCChooseForm : ConditionChooseForm
    {
        /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ������             ���ʱ��:2009-4-3
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
        #region Data
        private ConditionC condition;

        #endregion

        public ConditionCChooseForm()
        {
        
            InitializeComponent();
        }

        //��дȷ����ť����
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionC();

            //this.condition.Company = this.buttonEditCompany.EditValue as Model.Company;
            this.condition.ExpiringDate = this.dateEditInvoiceDate.DateTime;

        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionC;
            }
        }


        /// <summary>
        /// ѡ��λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                ChooseCustoms f = new ChooseCustoms();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    (sender as ButtonEdit).EditValue = f.SelectedItem;
                }
            }
            else
            {
                (sender as ButtonEdit).EditValue = null;
            }
        }
    }
}