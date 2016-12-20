using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DetailsList : Settings.BasicData.BaseListForm
    {
        Hashtable htFormName = new Hashtable();
        string _formpc = string.Empty;

        public DetailsList()
        {
            InitializeComponent();
            this.manager = new BL.PCExportReportANSIDetailManager();


            //组成窗体集合
            htFormName.Add("MSJY", "目視檢驗");
            htFormName.Add("QXD", "清晰度");
            htFormName.Add("LJDS", "棱鏡度數&&棱鏡平衡度數");
            htFormName.Add("KJGTSL", "可見光透視率");
            htFormName.Add("ZWXTSL", "紫外線透視率");
            htFormName.Add("QMDS", "球面度數");
            htFormName.Add("SGDS", "散光度數");
            htFormName.Add("GSCJCS", "高速衝擊測試");
            htFormName.Add("YZZLZJCS", "圓錐墜落撞擊測試");
            htFormName.Add("JPCTCS", "鏡片穿透測試");
            htFormName.Add("WDCS", "霧度測試");
            htFormName.Add("NRXCS", "耐燃性測試");
            htFormName.Add("CSAGX", "屈光/光學");
            htFormName.Add("CSAPGPCL", "偏光偏差量");
            htFormName.Add("CSAQXD", "清晰度");
            htFormName.Add("CSAWDCS", "霧度測試");
            htFormName.Add("CSAKJGTSL", "可見光透視率");
            htFormName.Add("CSAGSCJCS", "高速衝擊測試");
            htFormName.Add("CEENCONSTRUCTION", "结构");
            htFormName.Add("CEENQMDS", "球面度數");
            htFormName.Add("CEENSGDS", "散光度數");
            htFormName.Add("CEENLJDS", "棱鏡度數");
            htFormName.Add("CEENZB", "座標");
            htFormName.Add("CEENTSL", "透視率");
            htFormName.Add("CEENBMPZ", "表面品質");
            htFormName.Add("CEENZSCJ", "直式衝擊");
            htFormName.Add("CEENGSCJ", "高速衝擊");
            htFormName.Add("CEENJH", "記號");
            htFormName.Add("CEENZX", "資訊");
            htFormName.Add("CEENUVCF", "UV成份");
            htFormName.Add("ASNL", "年輪");
            htFormName.Add("ASCCSL", "抽測數量");
            htFormName.Add("ASWGCS", "外觀測試");
            htFormName.Add("ASJRWDX", "加熱穩定性");
            htFormName.Add("ASZB", "座標");
            htFormName.Add("ASQMDSZSD", "球面度數折射度");
            htFormName.Add("ASWD", "霧度");
            htFormName.Add("ASZSCJSL", "中速衝擊數率");
            htFormName.Add("ASGSCJSL", "高速衝擊數率");
            htFormName.Add("ASTGCJSL", "特高速衝擊數率");
            htFormName.Add("ASCTCS", "穿透測試");
            htFormName.Add("ASNRX", "耐燃性");
            htFormName.Add("ASNSX", "耐蝕性");
            htFormName.Add("ASJH", "記號");
            htFormName.Add("JISJPWG", "鏡片外觀");
            htFormName.Add("JISJPLJD", "鏡片棱鏡度");
            htFormName.Add("JISJPQGD", "鏡片曲光度");
            htFormName.Add("JISJPSGD", "鏡片散光度");
            htFormName.Add("JISJPTGL", "鏡片透光率");
            htFormName.Add("JISJPNCCX", "鏡片耐衝擊性");
            htFormName.Add("JISJPBMNMHDK", "鏡片表面耐磨耗抵抗");
            htFormName.Add("JISJPNREX", "鏡片耐熱性");
            htFormName.Add("JISJPNSX", "鏡片耐蝕性");
            htFormName.Add("JISJPNRAX", "鏡片耐燃性");
            htFormName.Add("JISWCPWG", "完成品外觀");
            htFormName.Add("JISWCPNCCX", "完成品耐衝擊性");
            htFormName.Add("JISWCPJMXDYSY", "完成品緊密性第一試驗鏡腳拉伸");
            htFormName.Add("JISWCPJMXDESY", "完成品緊密性第二試驗中樑吊掛測試機");
            htFormName.Add("JISWCPTDQD", "完成品頭帶強度");
            htFormName.Add("JISWCPNXDX", "完成品耐消毒性");
            htFormName.Add("JISWCPGZ", "完成品構造");
            htFormName.Add("JISWCPCL", "完成品材料");
            htFormName.Add("JISWCPJHBS", "完成品記號標示");
            htFormName.Add("JISWCPBZSJH", "完成品包裝上記號");
            htFormName.Add("JISWCPSYSC", "完成品使用手冊");

        }

        public DetailsList(string FormPC)
            : this()
        {
            this._formpc = FormPC;
            this.Text = htFormName[FormPC].ToString();
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIDetailManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, this._formpc);
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIDetailManager).SelectByDateRage(condition.StartDate, condition.EndDate, this._formpc);
                this.gridControl1.RefreshDataSource();

                this.gridView1.GroupPanelText = condition.StartDate.ToShortDateString() + "~" + condition.EndDate.ToShortDateString() + "的記錄";
            }
            f.Dispose();
            GC.Collect();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new DetailsForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            //return new DetailsForm(this.bindingSource1.Current as Model.PCExportReportANSIDetail);
            Type type = typeof(DetailsForm);
            return (DetailsForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
    }
}