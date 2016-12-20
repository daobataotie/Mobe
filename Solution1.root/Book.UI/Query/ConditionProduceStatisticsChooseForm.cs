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
    public partial class ConditionProduceStatisticsChooseForm : ConditionAChooseForm
    {
        private ConditionProduceStatistics condition;
        public ConditionProduceStatisticsChooseForm()
        {
            InitializeComponent();
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
                this.condition = value as ConditionProduceStatistics;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProduceStatistics();
            this.condition.StartDate = this.dateEditStartDate.Text == "" ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.Text == "" ? System.DateTime.Now : this.dateEditEndDate.DateTime;
            this.condition.StartProduceStatisticsId = this.buttonEditProduceStatisticsId1.EditValue == null ? null : this.buttonEditProduceStatisticsId1.EditValue.ToString();
            this.condition.EndProduceStatisticsId = this.buttonEditProduceStatisticsId2.EditValue == null ? null : this.buttonEditProduceStatisticsId2.EditValue.ToString();

            this.condition.StartPronoteHeaderID = this.buttonEditPronoteHeader1.EditValue == null ? null : this.buttonEditPronoteHeader1.EditValue.ToString();
            this.condition.EndPronoteHeaderID = this.buttonEditPronoteHeader2.EditValue == null ? null : this.buttonEditPronoteHeader2.EditValue.ToString();


            
        }
        #endregion

        private void buttonEditProduceStatisticsId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceStatistics.ListForm form = new produceManager.ProduceStatistics.ListForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceStatisticsId1.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceStatisticsId;
            }
            GC.Collect();
            form.Dispose();
        }

        private void buttonEditProduceStatisticsId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceStatistics.ListForm form = new produceManager.ProduceStatistics.ListForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceStatisticsId2.EditValue = form.SelectItem == null ? null : (form.SelectItem).ProduceStatisticsId;
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