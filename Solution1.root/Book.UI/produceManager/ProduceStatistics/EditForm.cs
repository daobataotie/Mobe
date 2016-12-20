using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
namespace Book.UI.produceManager.ProduceStatistics
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.ProduceStatistics produceStatistics = new Book.Model.ProduceStatistics();
        BL.ProduceStatisticsManager produceStatisticsManager = new Book.BL.ProduceStatisticsManager();


        BL.ProduceStatisticsDetailManager produceStatisticsDetailManager = new Book.BL.ProduceStatisticsDetailManager();

        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private IList<Model.ProduceStatisticsDetail> _Details = new List<Model.ProduceStatisticsDetail>();
        private string _pronoteHeaderID;
        private string _proceduresId;
        public EditForm()
        {
            InitializeComponent();
            //this.requireValueExceptions.Add(Model.ProduceStatistics.PRO_ProduceStatisticsId, new AA(Properties.Resources.RequireDataForId, this.textEditPronoteHeader));
          
           // this.newChooseContorlEmployeeId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "insert";
        }
        /// <summary>
        /// 带一个参构造函数
        /// </summary>
        public EditForm(Model.ProduceStatistics produceStatistics)
            : this()
        {
            //this.produceStatistics = produceStatistics;
            //this.produceStatistics.Details = this.produceStatisticsDetailManager.Select(produceStatistics);
            this.action = "update";
        }
        /// <summary>
        /// 带两个参构造函数
        /// </summary>
        public EditForm(Model.ProduceStatistics produceStatistics, string action)
            : this()
        {
            //this.produceStatistics = produceStatistics;
            //this.produceStatistics.Details = this.produceStatisticsDetailManager.Select(produceStatistics);
            this.action = action;
        }

        /// <summary>
        /// 带两个参构造函数
        /// </summary>
        public EditForm(string PronoteHeaderID, string ProceduresId, string action)
            : this()
        {
            this._pronoteHeaderID = PronoteHeaderID;
            this._proceduresId = ProceduresId;
            //this.produceStatistics = produceStatistics;
            //this.produceStatistics.Details = this.produceStatisticsDetailManager.Select(produceStatistics);
            this.action = action;
        }
        #region Save  新增

        protected override void Save()
        {
            //this.produceStatistics.ProduceStatisticsId = this.textEditPronoteHeader.Text;
            //this.produceStatistics.Description = this.memoEditDescription.Text;
          
            //this.produceStatistics.Employee = (this.newChooseContorlEmployeeId.EditValue as Model.Employee);
            //if (this.produceStatistics.Employee != null)
            //{
            //    this.produceStatistics.EmployeeId = this.produceStatistics.Employee.EmployeeId;
            //}
            //this.produceStatistics.PronoteHeaderID = this.textEditPronoteHeaderID.Text;
            //if (this.produceStatistics.Procedures != null)
            //{
            //    this.produceStatistics.ProceduresId = this.produceStatistics.Procedures.ProceduresId;
            //}
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceStatisticsDate.DateTime, new DateTime()))
            //{
            //    this.produceStatistics.ProduceStatisticsDate = global::Helper.DateTimeParse.NullDate;
            //}
            //else
            //{
            //    this.produceStatistics.ProduceStatisticsDate = this.dateEditProduceStatisticsDate.DateTime;
            //}
            //if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            //    return;

            switch (this.action)
            {
                case "insert":
                    this.produceStatisticsDetailManager.UpdateList(this._Details);
                    break;

                case "update":
                    this.produceStatisticsDetailManager.UpdateList(this._Details);
                    break;
            }

        }
#endregion
        #region  删除
        protected override void Delete()
        {
            Model.ProduceStatisticsDetail detail= this.bindingSourceDetails.Current as Model.ProduceStatisticsDetail;
            this.produceStatisticsManager.Delete(detail.ProduceStatisticsDetailId);
        }

        #endregion
            #region 刷新
        public override void Refresh()
        {

            this._Details = this.produceStatisticsDetailManager.SelectbyPronoteHeaderProcedures(this._pronoteHeaderID,this._proceduresId);
            this.bindingSourceDetails.DataSource = this._Details;
            if (this.action == "insert")
            {
                Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
                detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
                detail.DetailDate =DateTime.Now;
                //cate.Description = string.Empty;
                detail.PronoteHeaderID = this._pronoteHeaderID;
                detail.ProceduresId = this._proceduresId;
                detail.ProduceQuantity = 0;
                detail.HeGeQuantity = 0;
                detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                this._Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
         #endregion
               //下一笔
        //protected override void MoveNext()
        //{
        //    Model.ProduceStatistics produceStatistics = this.produceStatisticsManager.GetNext(this.produceStatistics);
        //    if (produceStatistics == null)
        //        throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
        //    this.produceStatistics = this.produceStatisticsManager.Get(produceStatistics.ProduceStatisticsId);
        //}
        ////上一笔
        //protected override void MovePrev()
        //{
        //    Model.ProduceStatistics produceStatistics = this.produceStatisticsManager.GetPrev(this.produceStatistics);
        //    if (produceStatistics == null)
        //        throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
        //    this.produceStatistics = this.produceStatisticsManager.Get(produceStatistics.ProduceStatisticsId);
        //}

        ////首笔
        //protected override void MoveFirst()
        //{
        //    this.produceStatistics = this.produceStatisticsManager.Get(this.produceStatisticsManager.GetFirst() == null ? "" : this.produceStatisticsManager.GetFirst().ProduceStatisticsId);
        //}

        ////尾笔
        //protected override void MoveLast()
        //{
        //    // if (produceStatistics == null)
        //    {
        //        this.produceStatistics = this.produceStatisticsManager.Get(this.produceStatisticsManager.GetLast() == null ? "" : this.produceStatisticsManager.GetLast().ProduceStatisticsId);
        //    }
        //}
        protected override bool HasRows()
        {
            return this.produceStatisticsManager.HasRows();
        }

        //protected override bool HasRowsNext()
        //{
        //    return this.produceStatisticsManager.HasRowsAfter(this.produceStatistics);
        //}

        //protected override bool HasRowsPrev()
        //{
        //    return this.produceStatisticsManager.HasRowsBefore(this.produceStatistics);
        //}
           protected override void AddNew()
        {

            //this.produceStatistics = new Model.ProduceStatistics();
            //this.produceStatistics.ProduceStatisticsId = this.produceStatisticsManager.GetId();
            //this.produceStatistics.ProduceStatisticsDate = DateTime.Now;
            //this.produceStatistics.Details = new List<Model.ProduceStatisticsDetail>();

            //if (this.action == "insert")
            //{

            //    Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
            //    detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
            //    detail.DetailDate = DateTime.Now;
            //    //cate.Description = string.Empty;
            //    detail.PronoteHeaderID = this._pronoteHeaderID;
            //    detail.ProceduresId = this._proceduresId;
            //    detail.ProduceQuantity = 0;
            //    detail.HeGeQuantity = 0;
            //    detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
            //    this._Details.Add(detail);
               
              
            //}
        }

    

        private void buttonEditProceduresId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        //     Settings.ProduceManager.Techonlogy.ChooseProceduresForm f = new Settings.ProduceManager.Techonlogy.ChooseProceduresForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //       this.produceStatistics.Procedures = f.SelectedItem as Model.Procedures;
        //       if (this.produceStatistics.Procedures != null)
        //        {
        //            this.buttonEditProceduresId.EditValue = this.produceStatistics.Procedures.Id;
        //        }
        //    }
        //    f.Dispose();
        //    System.GC.Collect();
        }
        private bool CanAdd(IList<Model.ProduceStatisticsDetail> list)
        {
            foreach (Model.ProduceStatisticsDetail detail in list)
            {
                if (detail.BusinessHoursType == null )
                    return false;
            }
            return true;
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
               // if (this.CanAdd(this.produceStatistics.Details))
                {
                    if (e.KeyData == Keys.Enter)
                    {

                        Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
                        detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
                        detail.DetailDate = DateTime.Now;
                        //cate.Description = string.Empty;
                        detail.PronoteHeaderID = this._pronoteHeaderID;
                        detail.ProceduresId = this._proceduresId;
                        detail.ProduceQuantity = 0;
                        detail.HeGeQuantity = 0;
                        detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                        this._Details.Add(detail);
                        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                   
                    }
                }
                //if (e.KeyData == Keys.Delete)
                //{
                //   // this.simpleButtonRemove.PerformClick();
                //}
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            //if (this.bindingSourceDetails.Current != null)
            //{
            //    this._Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceStatisticsDetail);

            //    if (this._Details.Count == 0)
            //    {

            //        Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
            //        detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
            //        detail.DetailDate = DateTime.Now;
            //        //cate.Description = string.Empty;
            //        detail.PronoteHeaderID = this._pronoteHeaderID;
            //        detail.ProceduresId = this._proceduresId;
            //        detail.ProduceQuantity = 0;
            //        detail.HeGeQuantity = 0;
            //        detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
            //        this._Details.Add(detail);
            //        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            //        this.gridControl1.RefreshDataSource();
            //    }

            //    this.gridControl1.RefreshDataSource();
            //}
        }

        private void barButtonPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PronoteHeader.ChoosePronoteHeaderDetailsForm f = new PronoteHeader.ChoosePronoteHeaderDetailsForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList.Count != 0)
            //    {
            //        Model.PronoteHeader pronoteHeader = PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList[0];
            //        this.produceStatistics.PronoteHeader = pronoteHeader;
            //       // this.textEditPronoteHeaderID.Text = pronoteHeader.PronoteHeaderID;
            //        if (pronoteHeader.Product != null)
            //        {
            //            this.textEditProductName.Text = pronoteHeader.Product.ProductName;
            //            this.spinEditQuantity.EditValue = pronoteHeader.DetailsSum;
            //        }
            //    }
            //}

        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            //this.textEditPronoteHeader.Text = this._pronoteHeaderID;
            //this.buttonEditProceduresId.Text = this._proceduresId;

            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectOnActive();
           // this.textEditProductName.Text=this.
        }
    }
}