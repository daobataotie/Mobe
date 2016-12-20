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

// �� �� ��: ���             ���ʱ��:2009-4-3
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        protected Condition condition;  

        /// <summary>
        /// �޲ι���
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��һ����������
        /// </summary>
        /// <param name="condition"></param>
        public BaseForm(Condition condition)
            : this()
        {
            this.condition = condition;
        }

        protected virtual DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch ((string)e.Item.Tag)
            {
                case "print":
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        DevExpress.XtraReports.UI.XtraReport r = this.GetReport();
                        if (r != null)
                        {
                            r.ShowPreview();
                        }
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    break;

                default:
                    break;
            }
        }

        protected virtual void DoQuery()
        {
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            this.DoQuery();
        }
    }
}