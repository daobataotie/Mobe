using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tool.CSManager
{
    public partial class ConnectionEditForm : DevExpress.XtraEditors.XtraForm
    {
        public ConnectionEditForm()
        {
            InitializeComponent();
        }

        public virtual Common.Connection Connection
        {
            get
            {
                return null;
            }
        }
    }
}