using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.Linq;

namespace Book.UI.produceManager.ProduceStatistics
{
    public partial class ListForms : Settings.BasicData.BaseListForm
    {
        public ListForms()
        {
            InitializeComponent();
            this.manager = new BL.ProduceStatisticsDetailManager();
        }
        protected override BaseEditForm GetEditForm()
        {
            return new EditForm1();
        }

        public Model.ProduceStatistics SelectItem
        {
            get { return this.bindingSource1.Current as Model.ProduceStatistics; }
        }

        
        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(produceManager.ProduceStatistics.EditForm1);
            return (produceManager.ProduceStatistics.EditForm1)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            //return null;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Model.ProduceStatisticsDetail> pstList = this.bindingSource1.DataSource as IList<Model.ProduceStatisticsDetail>;
            if (pstList == null)
            {
                MessageBox.Show("生產車間數量資料為空！");
                return;
            }
            if (pstList.Count == 0)
            {
                MessageBox.Show("生產車間數量資料為空！");
                return;
            }
            produceManager.ProduceStatistics.RO f = new RO(pstList);
            f.ShowPreviewDialog();
        }

        private void simpleButton_Search_Click(object sender, EventArgs e)
        {
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SearchByConditionForm form = new SearchByConditionForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                SearchByCondition condition = form.Condition as SearchByCondition;
                this.bindingSource1.DataSource = ((BL.ProduceStatisticsDetailManager)this.manager).SelectByDateRangeAndPronoteHeaderId(condition.StartDate, condition.EndDate, condition.PronoteHeaderId);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}