using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Book.UI.Invoices
{
    [Serializable]
    [ComVisible(false)]
    public delegate void MyHandle(object sender, ChooseItem item);
    public delegate void EventHandle();

    public partial class NewChooseContorl : UserControl
    {
        /// <summary>
        /// 选择后
        /// </summary>
        public event MyHandle AfterChoose;

        public object EditValue
        {
            get
            {
                if (choose != null)
                    return choose.EditValue;
                else
                    return null;
            }
            set
            {
                if (this.EditValueChanging != null)
                {
                    EditValueChanging();
                }

                if (choose != null)
                {
                    choose.EditValue = value;
                    this.buttonEditId.Text = choose.ButtonText;
                    this.labelControlName.Text = choose.LableText;
                }
                else
                {
                    this.buttonEditId.Text = "";
                    this.labelControlName.Text = "";
                }

                if (this.EditValueChanged != null)
                {
                    EditValueChanged(this.buttonEditId, new EventArgs());
                }
            }
        }

        public string Id
        {
            get
            {
                return buttonEditId.Text;
            }
        }

        [Browsable(true)]
        [DefaultValue(true)]
        public bool ButtonReadOnly
        {
            get { return this.buttonEditId.Properties.ReadOnly; }
            set { this.buttonEditId.Properties.ReadOnly = value; }
        }

        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowButton
        {
            get
            {
                return this.buttonEditId.Properties.Buttons[0].Visible;
            }
            set
            {
                this.buttonEditId.Properties.Buttons[0].Visible = value;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            height = 21;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        public NewChooseContorl()
        {
            InitializeComponent();

        }

        private IChoose choose;

        public IChoose Choose
        {
            set
            {
                choose = value;
                this.buttonEditId.Properties.ReadOnly = false;
                this.buttonEditId.Properties.Buttons[0].Visible = true;

                if (AfterChoose != null)
                {
                    AfterChoose(this, item);
                }
            }
        }

        private void MyClick(ref ChooseItem item)
        {
            if (choose != null)
                choose.MyClick(ref item);
        }

        ChooseItem item = null;

        private void buttonEditId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.MyClick(ref item);
            if (item != null)
            {
                choose.EditValue = item.EditValue;
                this.buttonEditId.Text = choose.ButtonText;
                this.labelControlName.Text = choose.LableText;
            }
        }

        private void buttonEditId_Leave(object sender, EventArgs e)
        {
            if (!IsLoseFouce())
            {
                this.buttonEditId.Focus();
            }
        }

        private bool IsLoseFouce()
        {
            string id = this.buttonEditId.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                this.EditValue = null;
                return true;
            }
            item = new ChooseItem(id);
            this.MyLeave(ref item);
            if (item.EditValue != null)
            {
                //if (choose != null)
                //    choose.EditValue = item.EditValue;
                //this.buttonEditId.Text = choose.ButtonText;
                //this.labelControlName.Text = choose.LableText;
                this.EditValue = item.EditValue;
                return true;
            }
            else
            {
                this.EditValue = null;

                return false;
            }
        }

        private void MyLeave(ref ChooseItem item)
        {
            if (choose != null)
                choose.MyLeave(ref item);
        }

        private void NewChooseContorl_Load(object sender, EventArgs e)
        {

        }

        private void buttonEditId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (IsLoseFouce())
                    SendKeys.Send("{TAB}");
                else
                {
                    this.buttonEditId.Focus();
                }
            }
        }

        //清空id后，清空员工 2015年11月16日
        private void buttonEditId_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(buttonEditId.Text))
                this.choose = null;
        }
        public event EventHandle EditValueChanging;
        public event System.EventHandler EditValueChanged;
        private void buttonEditId_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }
    }
}
