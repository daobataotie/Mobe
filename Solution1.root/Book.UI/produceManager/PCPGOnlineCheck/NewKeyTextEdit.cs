using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class NewKeyTextEdit : UserControl
    {
        public NewKeyTextEdit()
        {
            InitializeComponent();
        }

        private NewKeyTextEdit _Kup;
        [Browsable(true)]
        [Description("按上向上的箭头")]
        public NewKeyTextEdit Kup
        {
            get { return _Kup; }
            set { _Kup = value; }
        }
        private NewKeyTextEdit _Kdown;
        [Browsable(true)]
        [Description("按下向上的箭头")]
        public NewKeyTextEdit Kdown
        {
            get { return _Kdown; }
            set { _Kdown = value; }
        }
        private NewKeyTextEdit _Kleft;
        [Browsable(true)]
        [Description("按左向上的箭头")]
        public NewKeyTextEdit Kleft
        {
            get { return _Kleft; }
            set { _Kleft = value; }
        }
        private NewKeyTextEdit _Kright;
        [Browsable(true)]
        [Description("按右向上的箭头")]
        public NewKeyTextEdit Kright
        {
            get { return _Kright; }
            set { _Kright = value; }
        }

        public string mTextValue
        {
            set { this.textEdit1.Text = value; }
            get { return this.textEdit1.Text; }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (this.Kup != null)
                        this.Kup.Focus();
                    break;
                case Keys.Down:
                    if (this.Kdown != null)
                        this.Kdown.Focus();
                    break;
                case Keys.Left:
                    if (this.Kleft != null)
                        this.Kleft.Focus();
                    break;
                case Keys.Right:
                    if (this.Kright != null)
                        this.Kright.Focus();
                    break;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(string.IsNullOrEmpty(this.textEdit1.Text) ? "0" : this.textEdit1.Text);
                this.textEdit1.BackColor = Color.White;
            }
            catch
            {
                this.textEdit1.BackColor = Color.PaleVioletRed;
            }
        }
    }
}
