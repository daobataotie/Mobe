using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Options
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���޾�            ���ʱ��:2009-10-6
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class BaseOptionsPage : DevExpress.XtraEditors.XtraUserControl
    {
        public BaseOptionsPage()
        {
            InitializeComponent();
        }

        public virtual void DoSave()
        {

        }

        public virtual void DoLoad()
        {

        }
    }
}
