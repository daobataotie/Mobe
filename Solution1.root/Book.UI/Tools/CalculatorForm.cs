using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Tools
{
    public partial class CalculatorForm : DevExpress.XtraEditors.XtraForm
    {
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void textEditExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.textEditExpression.Text.Trim() != "")
            {
                double result = 0;
                string expression = this.textEditExpression.Text;
                bool successed;

                try
                {
                    result = UT.Calculator.Eval(expression);
                    successed = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    successed = false;
                }

                if (successed)
                {
                    this.memoEditResult.Text = this.memoEditResult.Text + System.Environment.NewLine + string.Format("{0} ==> {1}", expression, result);
                    this.memoEditResult.SelectionStart = this.memoEditResult.Text.Length;
                    this.memoEditResult.ScrollToCaret();

                    this.textEditExpression.Text = result.ToString();
                    this.textEditExpression.Select(this.textEditExpression.Text.Length, 0);
                }
                else
                {
                    this.textEditExpression.SelectAll();
                }
            }
        }


    }
}