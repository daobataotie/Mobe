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

// �� �� ��: ���             ���ʱ��:2009-4-11
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ConditionDChooseForm : ConditionChooseForm
    {
        //��Ʒ����
        private BL.ProductManager productManager = new Book.BL.ProductManager();

        private ConditionD condition;

        public ConditionDChooseForm()
        {
            InitializeComponent();
        }


        #region ��д���෽��
        protected override void OnOK()
        {
            if (condition == null)
                condition = new ConditionD();
            condition.StartId = this.comboBoxEditStartId.Text.Split(new char[] { ' ' })[0];
            condition.EndId = this.comboBoxEditEndId.Text.Split(new char[] { ' ' })[0];

        }
        #endregion

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionD;
            }
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionDChooseForm_Load(object sender, EventArgs e)
        {
            System.Collections.Generic.IList<Model.Product> products = productManager.Select();
            foreach (Model.Product product in products)
            {
                this.comboBoxEditStartId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
                this.comboBoxEditEndId.Properties.Items.Add(string.Format("{0} {1}", product.ProductId, product.ProductName));
            }
        }
    }
}