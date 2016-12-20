using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using Book.UI.Settings.BasicData;
using Book.Model;
namespace Book.UI.Query
{
    public partial class ConditionProduceStatisticsCheckChooseForm : ConditionAChooseForm
    {
        private ConditionProduceStatisticsCheck condition;
        public ConditionProduceStatisticsCheckChooseForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载，指定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionMaterialChooseForm_Load(object sender, EventArgs e)
        {

        }

        #region 重写父类方法
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionProduceStatisticsCheck;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProduceStatisticsCheck();
            this.condition.StartDate = this.dateEditStartDate.Text == "" ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.Text == "" ? System.DateTime.Now : this.dateEditEndDate.DateTime;
            this.condition.StartProduceStatisticsCheckId = this.buttonEditProduceStatisticsCheckId1.EditValue == null ? null : this.buttonEditProduceStatisticsCheckId1.EditValue.ToString();
            this.condition.EndProduceStatisticsCheckId = this.buttonEditProduceStatisticsCheckId2.EditValue == null ? null : this.buttonEditProduceStatisticsCheckId2.EditValue.ToString();
          
            this.condition.StartPronoteHeaderID = this.buttonEditPronoteHeader1.EditValue == null ? null : this.buttonEditPronoteHeader1.EditValue.ToString();
            this.condition.EndPronoteHeaderID = this.buttonEditPronoteHeader2.EditValue == null ? null : this.buttonEditPronoteHeader2.EditValue.ToString();
           


        }
        #endregion
        private void buttonEditProduceStatisticsCheckId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceStatisticsCheck.ListForm form = new produceManager.ProduceStatisticsCheck.ListForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceStatisticsCheckId1.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceStatisticsCheckId;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditProduceStatisticsCheckId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceStatisticsCheck.ListForm form = new produceManager.ProduceStatisticsCheck.ListForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceStatisticsCheckId2.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceStatisticsCheckId;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditPronoteHeader1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPronoteHeader1.EditValue = form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditPronoteHeader2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPronoteHeader2.EditValue = form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID;
            }
            GC.Collect();
            form.Dispose();
        }
    }
}