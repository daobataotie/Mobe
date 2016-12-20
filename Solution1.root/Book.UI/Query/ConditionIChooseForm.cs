using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
/*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ������             ���ʱ��:2009-4-20
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionIChooseForm : ConditionChooseForm
    {
        //private BL.CompanyManager companyManager = new Book.BL.CompanyManager();

        private global::Helper.CompanyKind kind;

        private ConditionI condition;


        /// <summary>
        /// �޲ι���
        /// </summary>
        /// <param name="kind"></param>
        public ConditionIChooseForm(global::Helper.CompanyKind kind)
        {            
            InitializeComponent();

            this.kind = kind;
        }


        #region ��д���෽��
        protected override void OnOK()
        {
            if (condition == null)
                condition = new ConditionI();
            condition.StartCompanyId = this.comboBoxEditStartId.Text.Split(new char[] { ' ' })[0];
            condition.EndCompanyId = this.comboBoxEditEndId.Text.Split(new char[] { ' ' })[0];
        }
        

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionI;
            }
        }
        #endregion


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionIChooseForm_Load(object sender, EventArgs e)
        {
            //System.Collections.Generic.IList<Model.Company> companys = this.companyManager.Select(this.kind);

            //foreach (Model.Company company in companys)
            //{
            //    this.comboBoxEditStartId.Properties.Items.Add(string.Format("{0} {1}", company.CompanyId,company.CompanyName0));
            //    this.comboBoxEditEndId.Properties.Items.Add(string.Format("{0} {1}", company.CompanyId, company.CompanyName0));
            //}
        }
    }
}