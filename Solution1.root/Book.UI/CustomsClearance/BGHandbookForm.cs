using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.ExcelClass;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
namespace Book.UI.CustomsClearance
{
    public partial class BGHandbookForm : Settings.BasicData.BaseEditForm
    {
        private BL.BGHandbookManager bGHandbookManager = new Book.BL.BGHandbookManager();
        private BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new Book.BL.BGHandbookDetail1Manager();
        private BL.BGHandbookDetail2Manager bGHandbookDetail2Manager = new Book.BL.BGHandbookDetail2Manager();
        private Model.BGHandbook _bGHandbook;
        private BL.InvoiceXODetailManager xodetailmanader = new Book.BL.InvoiceXODetailManager();
        private BL.InvoiceXSDetailManager xsdetailmanager = new Book.BL.InvoiceXSDetailManager();
        private BL.BGProductDepotOutDetailManager bGProductDepotOutDetailManager = new Book.BL.BGProductDepotOutDetailManager();
        private int flag = 0;//是否更改转出到手册数量或者手册号
        private IList<Model.BGHandbookDetail2> Detail2ZhjuanCe = new List<Model.BGHandbookDetail2>();
        public BGHandbookForm()
        {
            InitializeComponent();

            this.newChooseEmployee0.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseEmployee1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
            if (this.action != "view")
            {
                this.barButtonItem1.Enabled = false;
                this.barButtonItem3.Enabled = false;
                this.barButtonItem2.Enabled = false;
            }

            else
            {
                this.barButtonItem1.Enabled = true;
                this.barButtonItem3.Enabled = true;
                this.barButtonItem2.Enabled = true;

            }
            this.simpleButtonRemove.Click += new EventHandler(simpleButtonRemove_Click);
        }

        public BGHandbookForm(Model.BGHandbook bGHandbook)
            : this()
        {
            this._bGHandbook = bGHandbook;
            this._bGHandbook.Detail1 = this.bGHandbookDetail1Manager.Select(bGHandbook.BGHandbookId);
            this._bGHandbook.Detail2 = this.bGHandbookDetail2Manager.Select(bGHandbook.BGHandbookId);
            this.action = "view";
        }

        public BGHandbookForm(Model.BGHandbook bGHandbook, string action)
            : this()
        {
            this._bGHandbook = bGHandbook;
            this._bGHandbook.Detail1 = this.bGHandbookDetail1Manager.Select(bGHandbook.BGHandbookId);
            this._bGHandbook.Detail2 = this.bGHandbookDetail2Manager.Select(bGHandbook.BGHandbookId);
            this.action = "view";
        }

        protected override void AddNew()
        {

            if (this._bGHandbook != null)
            {
                this._bGHandbook.BGHandbookId = this.bGHandbookManager.GetId();
                this._bGHandbook.Id = null;
                this._bGHandbook.BGHandbookDate = DateTime.Now;
                this._bGHandbook.Employee = BL.V.ActiveOperator.Employee;
                this._bGHandbook.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                this._bGHandbook.AuditEmp = null;
                this._bGHandbook.AuditEmpId = null;
                this._bGHandbook.Detail1.Clear();
                this._bGHandbook.Detail2.Clear();

            }
            else
            {

                this._bGHandbook = new Book.Model.BGHandbook();
                this._bGHandbook.BGHandbookId = this.bGHandbookManager.GetId();
                this._bGHandbook.BGHandbookDate = DateTime.Now;
                this._bGHandbook.Employee = BL.V.ActiveOperator.Employee;
                this._bGHandbook.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                this._bGHandbook.AuditEmp = null;
                this._bGHandbook.AuditEmpId = null;
            }
            this.action = "insert";
        }
        public override void Refresh()
        {


            if (this._bGHandbook == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._bGHandbook = this.bGHandbookManager.GetDetails(_bGHandbook.BGHandbookId);

                }
            }
            this.textId.Text = _bGHandbook.BGHandbookId;
            this.dateDate.EditValue = _bGHandbook.BGHandbookDate;
            this.textEdit1.Text = this._bGHandbook.Id;
            this.newChooseEmployee0.EditValue = this._bGHandbook.Employee;
            this.newChooseEmployee1.EditValue = this._bGHandbook.AuditEmp;
            this.textEditAudit.Text = this.GetAuditName(this._bGHandbook.AuditState);
            this.dateUpdate.EditValue = this._bGHandbook.UpdateTime;
            this.bindingSource1.DataSource = this._bGHandbook.Detail1;
            this.bindingSource2.DataSource = this._bGHandbook.Detail2;
            this.checkEditEffect.Checked = this._bGHandbook.IsEffect == "0" ? false : true;

            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;

                case "update":

                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    break;

            }
            base.Refresh();
            newChooseEmployee0.Enabled = false;
            newChooseEmployee1.Enabled = false;
        }

        protected override void Save()
        {
            _bGHandbook.BGHandbookId = this.textId.Text;
            _bGHandbook.BGHandbookDate = this.dateDate.DateTime;
            this._bGHandbook.Id = this.textEdit1.Text;
            this._bGHandbook.Employee = this.newChooseEmployee0.EditValue as Model.Employee;
            this._bGHandbook.AuditEmp = this.newChooseEmployee1.EditValue as Model.Employee;
            this._bGHandbook.IsEffect = this.checkEditEffect.Checked ? "1" : "0";
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;

            if (this.bGHandbookManager.HasEffect(this._bGHandbook.BGHandbookId, this._bGHandbook.Id) && this._bGHandbook.IsEffect == "1")
            {
                throw new Helper.MessageValueException("该手册号已存在其他生效版本，相同手册号只能有一个生效！");
            }
            switch (this.action)
            {
                case "insert":
                    this.bGHandbookManager.Insert(this._bGHandbook);

                    break;

                case "update":
                    this.bGHandbookManager.Update(this._bGHandbook);
                    if (flag == 1)
                    {
                        foreach (Model.BGHandbookDetail2 model in Detail2ZhjuanCe)
                        {
                            this.bGHandbookDetail2Manager.UpdateCeIn(model.ZhuanRuShouCeId, model.Id.ToString(), model.ZhuanCeOutQuantity.HasValue ? model.ZhuanCeOutQuantity.Value : 0);
                        }
                        flag = 0;
                    }

                    break;
            }

        }
        protected override bool HasRows()
        {
            return this.bGHandbookManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.bGHandbookManager.HasRowsAfter(this._bGHandbook);
        }

        protected override bool HasRowsPrev()
        {
            return this.bGHandbookManager.HasRowsBefore(this._bGHandbook);
        }

        protected override void MoveFirst()
        {
            this._bGHandbook = this.bGHandbookManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._bGHandbook = this.bGHandbookManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGHandbook model = this.bGHandbookManager.GetNext(this._bGHandbook);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbook = model;
        }

        protected override void MovePrev()
        {
            Model.BGHandbook model = this.bGHandbookManager.GetPrev(this._bGHandbook);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbook = model;
        }

        protected override void Delete()
        {

            if (this._bGHandbook == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.bGHandbookManager.Delete(this._bGHandbook);
            this._bGHandbook = this.bGHandbookManager.GetNext(this._bGHandbook);
            if (this._bGHandbook == null)
            {
                this._bGHandbook = this.bGHandbookManager.GetLast();
            }
        }


        //导入
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelClass1 ec = new ExcelClass1();
                ec.Open(openFileDialog1.FileName);

                try
                {
                    BL.V.BeginTransaction();
                    Model.BGHandbook bghandbook = null;
                    action = "insert";

                    Model.BGHandbookDetail1 detail1;
                    Model.BGHandbookDetail2 detail2;

                    int m = 0;
                    int e3;
                    for (int i = 6; i <= ec.wb.Worksheets.Count; i++)
                    {

                        bghandbook = new Book.Model.BGHandbook();
                        bghandbook.Detail1 = new List<Model.BGHandbookDetail1>();
                        bghandbook.Detail2 = new List<Model.BGHandbookDetail2>();
                        bghandbook.BGHandbookId = this.bGHandbookManager.GetId();
                        bghandbook.BGHandbookDate = DateTime.Now;
                        bghandbook.Employee = BL.V.ActiveOperator.Employee;
                        bghandbook.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                        bghandbook.AuditEmpId = BL.V.ActiveOperator.EmployeeId;
                        bghandbook.AuditState = 1;

                        bghandbook.InsertTime = DateTime.Now;

                        m = 0;
                        e3 = 0;
                        Microsoft.Office.Interop.Excel.Worksheet ss = (Microsoft.Office.Interop.Excel.Worksheet)ec.wb.Worksheets[i];

                        bghandbook.Id = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[1, 1]).Text.ToString();
                        if (string.IsNullOrEmpty(bghandbook.Id))
                            continue;
                        int flag = 0;
                        for (int j = 3; j < 2500; j++)
                        {
                            try
                            {

                                if (flag == 0)
                                {


                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 4]).Text.ToString().Trim() == "合计")
                                    {
                                        flag = 1;
                                        continue;
                                    }

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString() == "")
                                        continue;
                                    else if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString() == "" && ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString() != "") //多子件
                                    {
                                        m = m + 1;


                                        detail1 = new Book.Model.BGHandbookDetail1();
                                        detail1.BGHandbookDetail1Id = Guid.NewGuid().ToString();
                                        detail1.NOId = m;

                                        int c = j;
                                        decimal beeQuantity = decimal.Zero;
                                        for (c = j; c > 2; c--)
                                        {
                                            if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[c, 3]).Text.ToString() != "")
                                            {
                                                detail1.Id = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[c, 1]).Text.ToString());
                                                beeQuantity = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[c, 6]).Text.ToString());
                                                break;
                                            }
                                        }

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString() != "")
                                            detail1.LId = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString());
                                        detail1.Column1 = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString();
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                            detail1.Ljingliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString() != "")
                                            detail1.LjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString() != "")
                                            detail1.LjingQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString());
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString() != "")
                                            detail1.Lsunhaolv = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString());

                                        detail1.LiLunHaoYongJing = (double)GetDecimal(beeQuantity * decimal.Parse(detail1.LjingQuantity.ToString()), 2);
                                        detail1.LiLunHaoYongJingSun = (double)GetDecimal(beeQuantity * decimal.Parse(detail1.LjingQuantity.ToString()) / (1 - decimal.Parse(detail1.Lsunhaolv.ToString()) * 0.01M), 2);




                                        bghandbook.Detail1.Add(detail1);
                                    }
                                    else
                                    {
                                        m = m + 1;



                                        detail1 = new Book.Model.BGHandbookDetail1();
                                        detail1.BGHandbookDetail1Id = Guid.NewGuid().ToString();
                                        detail1.NOId = m;
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 1]).Text.ToString() != "")
                                            detail1.Id = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 1]).Text.ToString());


                                        detail1.CusProName = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 2]).Text.ToString();
                                        detail1.ProName = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 3]).Text.ToString();
                                        detail1.ProGuiGe = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 4]).Text.ToString();

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 5]).Text.ToString() != "")
                                            detail1.Quantity = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 5]).Text.ToString());
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString() != "")
                                        {
                                            detail1.BeeQuantity = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 6]).Text.ToString());
                                            detail1.BeeQuantityTemp = detail1.BeeQuantity;
                                        }

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() != "")
                                            detail1.UpQuantity = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString());

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString() != "")
                                            detail1.LId = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString());
                                        detail1.Column1 = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString();
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                            detail1.Ljingliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());
                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString() != "")
                                            detail1.LjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString() != "")
                                            detail1.LjingQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString());

                                        if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString() != "")
                                            detail1.Lsunhaolv = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 13]).Text.ToString());
                                        bghandbook.Detail1.Add(detail1);
                                    }
                                }

                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString().Trim() == "序号")
                                {
                                    flag = 2;
                                    continue;
                                }
                                if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 4]).Text.ToString().Trim() == "序号")
                                {
                                    flag = 3;
                                    continue;
                                }
                                if (flag == 2)
                                {
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString().Trim() == "")
                                        continue;
                                    detail2 = new Book.Model.BGHandbookDetail2();
                                    detail2.BGHandbookDetail2Id = Guid.NewGuid().ToString();
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() != "")
                                        detail2.Id = int.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString());
                                    detail2.ProName = ((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString();

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                        detail2.Ljingliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString() != "")
                                        detail2.LjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString() != "")
                                        detail2.LLastjingSunliang = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 14]).Text.ToString() != "")
                                        detail2.Lchazhi = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 14]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 15]).Text.ToString() != "")

                                        detail2.LPrice = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 15]).Text.ToString());


                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 16]).Text.ToString() != "")
                                        detail2.Lmoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 16]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 18]).Text.ToString() != "")
                                        detail2.LciciMoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 18]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 19]).Text.ToString() != "")
                                        detail2.JinKouiMoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 19]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 20]).Text.ToString() != "")
                                        detail2.LastMoney = decimal.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 20]).Text.ToString());
                                    bghandbook.Detail2.Add(detail2);
                                }
                                if (flag == 3)
                                {
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 4]).Text.ToString() == "")
                                        break;


                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString() != "")

                                        bghandbook.Detail2[e3].LbejinQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 7]).Text.ToString());

                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString() != "")
                                        bghandbook.Detail2[e3].ZhuanCeInQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 8]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString() != "")
                                        bghandbook.Detail2[e3].UpQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 9]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString() != "")
                                        bghandbook.Detail2[e3].YaoJInQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 10]).Text.ToString());
                                    if (((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString() != "")
                                        bghandbook.Detail2[e3].HaiKeJInQuantity = double.Parse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 11]).Text.ToString());

                                    double a = 0;
                                    double.TryParse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 12]).Text.ToString(), out a);
                                    bghandbook.Detail2[e3].LilunHaoYong = a;

                                    double b = 0;

                                    double.TryParse(((Microsoft.Office.Interop.Excel.Range)ss.Cells[j, 14]).Text.ToString(), out b);
                                    bghandbook.Detail2[e3].LilunStock = b;
                                    e3++;


                                }

                            }
                            catch (Exception exc)
                            {
                                throw new Exception(j + exc.Message);


                            }
                        }






                        this.bGHandbookManager.Insert(bghandbook);

                    }

                    BL.V.CommitTransaction();
                    this._bGHandbook = this.bGHandbookManager.GetLast();
                    this.action = "view";
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    BL.V.RollbackTransaction();
                    ec.Close();
                    throw ex;

                }
                ec.Close();
                ec.release_xlsObj();


            }
        }
        //导出
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelClass.ExcelClass1 ex = new ExcelClass1();
            ex.Create();
            // ex.AddSheet("130101");
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);


            ex.UniteCells(sheet, 1, 1, 1, 4);
            sheet.Cells[1, 1] = this._bGHandbook.Id;
            sheet.Cells[2, 1] = "成品项号";
            sheet.Cells[2, 2] = "客户型号";
            sheet.Cells[2, 3] = "商品名称";
            sheet.Cells[2, 4] = "成品规格";

            sheet.Cells[2, 5] = "成品备案数量";
            sheet.Cells[2, 6] = "已出数量";
            sheet.Cells[2, 7] = "剩余量";
            sheet.Cells[2, 8] = "料件项号";
            sheet.Cells[2, 9] = "原料";
            sheet.Cells[2, 10] = "净量";
            sheet.Cells[2, 11] = "净+损";
            sheet.Cells[2, 12] = "净KG";
            sheet.Cells[2, 13] = "损耗%";
            int count1 = this._bGHandbook.Detail1.Count;

            // ex.AddSheet("130101");
            for (int i = 0; i < this._bGHandbook.Detail1.Count; i++)
            {
                sheet.Cells[i + 3, 1] = this._bGHandbook.Detail1[i].Id;
                sheet.Cells[i + 3, 2] = this._bGHandbook.Detail1[i].CusProName;
                sheet.Cells[i + 3, 3] = this._bGHandbook.Detail1[i].ProName;
                sheet.Cells[i + 3, 4] = this._bGHandbook.Detail1[i].ProGuiGe;

                sheet.Cells[i + 3, 5] = this._bGHandbook.Detail1[i].Quantity.HasValue ? this._bGHandbook.Detail1[i].Quantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 6] = this._bGHandbook.Detail1[i].BeeQuantity.HasValue ? this._bGHandbook.Detail1[i].BeeQuantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 7] = this._bGHandbook.Detail1[i].UpQuantity.HasValue ? this._bGHandbook.Detail1[i].UpQuantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 8] = this._bGHandbook.Detail1[i].LId;
                sheet.Cells[i + 3, 9] = this._bGHandbook.Detail1[i].Column1;
                sheet.Cells[i + 3, 10] = this._bGHandbook.Detail1[i].Ljingliang.HasValue ? this._bGHandbook.Detail1[i].Ljingliang.Value.ToString("0.00") : "";
                sheet.Cells[i + 3, 11] = this._bGHandbook.Detail1[i].LjingSunliang.HasValue ? this._bGHandbook.Detail1[i].LjingSunliang.Value.ToString("0.00") : "";
                sheet.Cells[i + 3, 12] = this._bGHandbook.Detail1[i].LjingQuantity.HasValue ? this._bGHandbook.Detail1[i].LjingQuantity.Value.ToString() : "";
                sheet.Cells[i + 3, 13] = this._bGHandbook.Detail1[i].Lsunhaolv.HasValue ? this._bGHandbook.Detail1[i].Lsunhaolv.Value.ToString() : "";

            }

            sheet.Cells[this._bGHandbook.Detail1.Count + 6, 3] = "合计";

            sheet.Cells[this._bGHandbook.Detail1.Count + 6, 5] = this._bGHandbook.Detail1.Sum(a => a.Quantity).ToString();
            sheet.Cells[this._bGHandbook.Detail1.Count + 6, 6] = this._bGHandbook.Detail1.Sum(a => a.BeeQuantity).ToString();

            sheet.Cells[this._bGHandbook.Detail1.Count + 9, 9] = "料件表";

            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 7] = "序号";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 9] = "原料";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 10] = "总净";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 11] = "总净+损";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 12] = "前次备案的总净+损";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 14] = "差值";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 15] = "单价";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 16] = "金额（差值）";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 17] = "已经进口";
            sheet.Cells[this._bGHandbook.Detail1.Count + 11, 18] = "此次总额";
            int m = 0;
            int flag = 0;
            for (m = 0; m < this._bGHandbook.Detail2.Count; m++)
            {
                sheet.Cells[m + count1 + 12, 7] = this._bGHandbook.Detail2[m].Id;
                sheet.Cells[m + count1 + 12, 9] = this._bGHandbook.Detail2[m].ProName;
                sheet.Cells[m + count1 + 12, 10] = this._bGHandbook.Detail2[m].Ljingliang.HasValue ? this._bGHandbook.Detail2[m].Ljingliang.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 11] = this._bGHandbook.Detail2[m].LjingSunliang.HasValue ? this._bGHandbook.Detail2[m].LjingSunliang.Value.ToString("0.00") : "";

                sheet.Cells[m + count1 + 12, 12] = this._bGHandbook.Detail2[m].LLastjingSunliang.HasValue ? this._bGHandbook.Detail2[m].LLastjingSunliang.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 14] = this._bGHandbook.Detail2[m].Lchazhi.HasValue ? this._bGHandbook.Detail2[m].Lchazhi.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 15] = this._bGHandbook.Detail2[m].LPrice.HasValue ? this._bGHandbook.Detail2[m].LPrice.Value.ToString("0.######") : "";
                sheet.Cells[m + count1 + 12, 16] = this._bGHandbook.Detail2[m].Lmoney.HasValue ? this._bGHandbook.Detail2[m].Lmoney.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 17] = this._bGHandbook.Detail2[m].LbejinQuantity.HasValue ? this._bGHandbook.Detail2[m].LbejinQuantity.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 18] = this._bGHandbook.Detail2[m].LciciMoney.HasValue ? this._bGHandbook.Detail2[m].LciciMoney.Value.ToString("0.00") : "";
                sheet.Cells[m + count1 + 12, 19] = this._bGHandbook.Detail2[m].JinKouiMoney.HasValue ? this._bGHandbook.Detail2[m].JinKouiMoney.Value.ToString("0.######") : "";
                sheet.Cells[m + count1 + 12, 20] = this._bGHandbook.Detail2[m].LastMoney.HasValue ? this._bGHandbook.Detail2[m].LastMoney.Value.ToString("0.######") : "";


            }

            flag = m + count1 + 12 + 6;

            sheet.Cells[flag, 4] = "序号 ";
            sheet.Cells[flag, 5] = "原料";
            sheet.Cells[flag, 6] = "1备案量 ";
            sheet.Cells[flag, 7] = "2已经进口 ";
            sheet.Cells[flag, 8] = "3转册";
            sheet.Cells[flag, 9] = "4剩余量";
            sheet.Cells[flag, 10] = "5要进量";
            sheet.Cells[flag, 11] = "6可进口数量";
            sheet.Cells[flag, 12] = "7理论耗用";
            sheet.Cells[flag, 13] = "8";
            sheet.Cells[flag, 14] = "9=2-7理论库存";




            for (int n = 0; n < this._bGHandbook.Detail2.Count; n++)
            {
                sheet.Cells[n + flag + 1, 4] = this._bGHandbook.Detail2[n].Id;
                sheet.Cells[n + flag + 1, 5] = this._bGHandbook.Detail2[n].ProName;
                sheet.Cells[n + flag + 1, 6] = this._bGHandbook.Detail2[n].LjingSunliang.HasValue ? this._bGHandbook.Detail2[n].LjingSunliang.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 7] = this._bGHandbook.Detail2[n].LbejinQuantity.HasValue ? this._bGHandbook.Detail2[n].LbejinQuantity.Value.ToString("0.00") : "";

                sheet.Cells[n + flag + 1, 8] = this._bGHandbook.Detail2[n].ZhuanCeInQuantity.HasValue ? this._bGHandbook.Detail2[n].ZhuanCeInQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 9] = this._bGHandbook.Detail2[n].UpQuantity.HasValue ? this._bGHandbook.Detail2[n].UpQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 10] = this._bGHandbook.Detail2[n].YaoJInQuantity.HasValue ? this._bGHandbook.Detail2[n].YaoJInQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 11] = this._bGHandbook.Detail2[n].HaiKeJInQuantity.HasValue ? this._bGHandbook.Detail2[n].HaiKeJInQuantity.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 12] = this._bGHandbook.Detail2[n].LilunHaoYong.HasValue ? this._bGHandbook.Detail2[n].LilunHaoYong.Value.ToString("0.00") : "";
                sheet.Cells[n + flag + 1, 14] = this._bGHandbook.Detail2[n].LilunStock.HasValue ? this._bGHandbook.Detail2[n].LilunStock.Value.ToString("0.00") : "";

            }



            string y = "L" + (this._bGHandbook.Detail1.Count + this._bGHandbook.Detail2.Count * 2 + 200).ToString();

            Range oRange = sheet.get_Range("A1", y);
            oRange.WrapText = true;
            oRange.EntireRow.AutoFit();

            ex.setBorder(sheet, 1, 2, 13, this._bGHandbook.Detail1.Count + 6, 2);
            ex.setBorder(sheet, 7, this._bGHandbook.Detail1.Count + 11, 11, this._bGHandbook.Detail1.Count + 11 + this._bGHandbook.Detail2.Count, 2);
            ex.setBorder(sheet, 4, m + count1 + 12 + 6, 12, m + count1 + 12 + 6 + this._bGHandbook.Detail2.Count, 2);

            //宽度
            ex.SetWidth(sheet, "A:A", 7.13);
            ex.SetWidth(sheet, "B:B", 12.25);
            ex.SetWidth(sheet, "C:C", 12.50);
            ex.SetWidth(sheet, "D:D", 12.50);
            ex.SetWidth(sheet, "E:E", 10.50);
            ex.SetWidth(sheet, "F:F", 11.88);
            ex.SetWidth(sheet, "B:B", 10.75);
            ex.SetWidth(sheet, "G:G", 13.38);
            ex.SetWidth(sheet, "H:H", 4.25);
            ex.SetWidth(sheet, "I:I", 17.00);
            ex.SetWidth(sheet, "J:J", 13.88);
            ex.SetWidth(sheet, "K:K", 12.50);
            ex.SetWidth(sheet, "L:L", 16.63);
            ex.SetWidth(sheet, "M:M", 5.75);

            //对齐方式
            ((Range)sheet.Columns["A:B", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["C:D", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["E:G", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["H:H", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["I:L", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Rows["2:2", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();



        }

        //备案变更 复制 数据 到新备案
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Model.BGHandbook bghand = this._bGHandbook;
            bghand.BGHandbookId = this.bGHandbookManager.GetId(DateTime.Now);
            bghand.BGHandbookDate = DateTime.Now;
            bghand.AuditEmpId = null;
            bghand.AuditState = 1;
            bghand.UpdateTime = null;
            bghand.YouXiaoDate = DateTime.Now.Date.AddMonths(15);
            foreach (Model.BGHandbookDetail1 detail1 in bghand.Detail1)
            {
                detail1.BGHandbookDetail1Id = Guid.NewGuid().ToString();
            }
            foreach (Model.BGHandbookDetail2 detail2 in bghand.Detail2)
            {
                detail2.BGHandbookDetail2Id = Guid.NewGuid().ToString();
                detail2.LLastjingSunliang = detail2.LjingSunliang;
            }
            this.action = "insert";
            this.Refresh();
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == gridColumnQuantity.Name || e.Column.Name == gridColumnBeeQuantity.Name || e.Column.Name == gridColumn13.Name || e.Column.Name == gridColumn14.Name)
            //{
            try
            {
                if (e.Column.Name == gridColumnQuantity.Name)
                {
                    decimal quantity = decimal.Zero;      //备案

                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);

                    Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
                    if (!detail1.YdwcQuantity.HasValue || detail1.YdwcQuantity.ToString() == "") detail1.YdwcQuantity = 0;

                    detail1.UpQuantity = Convert.ToDouble(detail1.Quantity) - Convert.ToDouble(detail1.BeeQuantity);

                    foreach (Model.BGHandbookDetail1 de in this._bGHandbook.Detail1.Where(d => d.Id == detail1.Id && !string.IsNullOrEmpty(d.Column1)).ToList<Model.BGHandbookDetail1>())
                    {
                        de.Ljingliang = (double)this.GetDecimal(quantity * Convert.ToDecimal(de.LjingQuantity), 2);
                        de.LjingSunliang = (double)this.GetDecimal(quantity * Convert.ToDecimal(de.LjingQuantity) / (1 - Convert.ToDecimal(de.Lsunhaolv) * 0.01M), 2);
                        Detail2(de.LId);

                    }


                }
                if (e.Column.Name == gridColumnBeeQuantity.Name)
                {
                    double beeQuantity = 0;      //已出数量
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out beeQuantity);

                    Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
                    if (!detail1.YdwcQuantity.HasValue || detail1.YdwcQuantity.ToString() == "") detail1.YdwcQuantity = 0;
                    detail1.UpQuantity = Convert.ToDouble(detail1.Quantity) - Convert.ToDouble(detail1.BeeQuantity);



                    IList<Model.BGHandbookDetail1> detail1List = this._bGHandbook.Detail1.Where(d => d.Id == (bindingSource1.Current as Model.BGHandbookDetail1).Id && !string.IsNullOrEmpty(d.Column1)).ToList<Model.BGHandbookDetail1>();

                    Model.BGHandbookDetail2 detail2 = new Model.BGHandbookDetail2();
                    foreach (Model.BGHandbookDetail1 detail1m in detail1List)
                    {
                        detail2 = this._bGHandbook.Detail2.Where(d => d.Id == detail1m.LId).ToList<Model.BGHandbookDetail2>()[0];
                        double? a = detail2.LilunHaoYong - detail1m.LiLunHaoYongJingSun;
                        detail1m.LiLunHaoYongJing = (double)GetDecimal(Convert.ToDecimal(beeQuantity) * Convert.ToDecimal(detail1m.LjingQuantity), 2);
                        detail1m.LiLunHaoYongJingSun = (double)GetDecimal(Convert.ToDecimal(beeQuantity) * Convert.ToDecimal(detail1m.LjingQuantity) / (1 - Convert.ToDecimal(detail1m.Lsunhaolv) * 0.01M), 2);
                        detail2.LilunHaoYong = a + detail1m.LiLunHaoYongJingSun;
                        detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                        detail1m.BeeQuantityTemp = beeQuantity;
                    }


                    //  Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                    ////////////////////////////////////////////////////计算理论损耗



                }
                if (e.Column.Name == gridColumn13.Name)  //净重/kg
                {

                    Model.BGHandbookDetail1 detail1 = this._bGHandbook.Detail1.Where(d => d.Id == (bindingSource1.Current as Model.BGHandbookDetail1).Id && !string.IsNullOrEmpty(d.ProName)).ToList<Model.BGHandbookDetail1>()[0];
                    decimal quantity = Convert.ToDecimal(detail1.Quantity);

                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn11, GetDecimal(quantity * Convert.ToDecimal(e.Value), 2));
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn12, GetDecimal(quantity * Convert.ToDecimal(e.Value) / (1 - (decimal)((bindingSource1.Current as Model.BGHandbookDetail1).Lsunhaolv == null ? 0 : (bindingSource1.Current as Model.BGHandbookDetail1).Lsunhaolv) * 0.01M), 2));
                    ////////////////计算理论损耗
                    Model.BGHandbookDetail1 curr = bindingSource1.Current as Model.BGHandbookDetail1;



                    double? beeQuantity = detail1.BeeQuantity;
                    Model.BGHandbookDetail2 detail2 = this._bGHandbook.Detail2.Where(d => d.Id == curr.LId).ToList<Model.BGHandbookDetail2>()[0];
                    double? a = detail2.LilunHaoYong - curr.LiLunHaoYongJingSun;


                    curr.LiLunHaoYongJing = (double)GetDecimal(Convert.ToDecimal(beeQuantity) * Convert.ToDecimal(quantity), 2);

                    curr.LiLunHaoYongJingSun = (double)GetDecimal(Convert.ToDecimal(beeQuantity) * Convert.ToDecimal(quantity) / (1 - Convert.ToDecimal(curr.Lsunhaolv) * 0.01M), 2);
                    detail2.LilunHaoYong = a + curr.LiLunHaoYongJingSun;
                    detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                    Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                }
                if (e.Column.Name == gridColumn14.Name)
                {
                    Model.BGHandbookDetail1 detail1 = this._bGHandbook.Detail1.Where(d => d.Id == (bindingSource1.Current as Model.BGHandbookDetail1).Id && !string.IsNullOrEmpty(d.ProName)).ToList<Model.BGHandbookDetail1>()[0];

                    decimal quantity = Convert.ToDecimal(detail1.Quantity);
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn12, GetDecimal(quantity * (decimal)((bindingSource1.Current as Model.BGHandbookDetail1).LjingQuantity == null ? 0 : (bindingSource1.Current as Model.BGHandbookDetail1).LjingQuantity) / (1 - Convert.ToDecimal(e.Value) * 0.01M), 2));

                    ////////////////计算理论损耗
                    Model.BGHandbookDetail1 curr = bindingSource1.Current as Model.BGHandbookDetail1;

                    double? beeQuantity = detail1.BeeQuantity;
                    Model.BGHandbookDetail2 detail2 = this._bGHandbook.Detail2.Where(d => d.Id == curr.LId).ToList<Model.BGHandbookDetail2>()[0];
                    double? a = detail2.LilunHaoYong - curr.LiLunHaoYongJingSun;


                    curr.LiLunHaoYongJing = (double)GetDecimal(Convert.ToDecimal(beeQuantity) * Convert.ToDecimal(curr.LjingQuantity), 2);

                    curr.LiLunHaoYongJingSun = (double)GetDecimal(Convert.ToDecimal(beeQuantity) * Convert.ToDecimal(curr.LjingQuantity) / (1 - Convert.ToDecimal(curr.Lsunhaolv) * 0.01M), 2);
                    detail2.LilunHaoYong = a + curr.LiLunHaoYongJingSun;
                    detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                    Detail2((bindingSource1.Current as Model.BGHandbookDetail1).LId);
                }
                if (e.Column.Name == gridColumnYdwc.Name)//已定未出
                {
                    Model.BGHandbookDetail1 curr = bindingSource1.Current as Model.BGHandbookDetail1;
                    if (!curr.YdwcQuantity.HasValue || curr.YdwcQuantity.ToString() == "") curr.YdwcQuantity = 0;

                    curr.UpQuantity = Convert.ToDouble(curr.Quantity) - Convert.ToDouble(curr.BeeQuantity);


                }

                this.gridControl1.RefreshDataSource();
                this.gridControl2.RefreshDataSource();
            }
            catch
            {
                MessageBox.Show("数据有误！");
                return;
            }
        }

        private void Detail2(int? lid)
        {
            IEnumerable<Model.BGHandbookDetail2> detail = this._bGHandbook.Detail2.Where(d => d.Id == lid);
            if (detail == null || detail.Count() == 0) return;
            Model.BGHandbookDetail2 detail2 = detail.ToList<Model.BGHandbookDetail2>()[0];
            detail2.Ljingliang = (double)this._bGHandbook.Detail1.Where(d => d.LId == lid).Sum(d => d.Ljingliang);
            detail2.LjingSunliang = (double)this._bGHandbook.Detail1.Where(d => d.LId == lid).Sum(d => d.LjingSunliang);
            detail2.Lchazhi = detail2.LjingSunliang - detail2.LLastjingSunliang;
            detail2.Lmoney = GetDecimal((detail2.LPrice.HasValue ? detail2.LPrice : 0m) * Convert.ToDecimal(detail2.Lchazhi), 2);
            detail2.LciciMoney = GetDecimal((detail2.LPrice.HasValue ? detail2.LPrice : 0m) * Convert.ToDecimal(detail2.LjingSunliang), 2);
            detail2.JinKouiMoney = GetDecimal((detail2.LPrice.HasValue ? detail2.LPrice : 0m) * Convert.ToDecimal(detail2.LbejinQuantity), 2);
            detail2.LastMoney = GetDecimal((detail2.LPrice.HasValue ? detail2.LPrice : 0m) * Convert.ToDecimal(detail2.LLastjingSunliang), 2);
            detail2.UpQuantity = Convert.ToDouble(detail2.LjingSunliang) - Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(detail2.ZhuanCeInQuantity);
            detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(detail2.YaoJInQuantity);


        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //   if (e.Column.Name == gridColumnLast.Name || e.Column.Name == gridColumnPrice.Name || e.Column.Name == gridColumnZhuanCeIn.Name || e.Column.Name == gridColumnZhuanCeOut.Name || e.Column.Name == gridColumnZhuanRuShouCeId.Name)
            {

                if (e.Column.Name == gridColumnLast.Name)
                {

                    double lastquantity = 0;      //上次备案
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out lastquantity);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    detail2.Lchazhi = detail2.LjingSunliang - lastquantity;

                    detail2.Lmoney = GetDecimal(Convert.ToDecimal(detail2.Lchazhi) * (detail2.LPrice.HasValue ? detail2.LPrice : 0m), 2);
                    detail2.LastMoney = GetDecimal((decimal)lastquantity * (detail2.LPrice.HasValue ? detail2.LPrice : 0m), 2);
                }

                if (e.Column.Name == gridColumnPrice.Name) //修改单价
                {

                    decimal price = decimal.Zero;      //单价
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out price);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    detail2.Lmoney = GetDecimal(Convert.ToDecimal(detail2.Lchazhi) * price, 2);

                    detail2.LciciMoney = GetDecimal(Convert.ToDecimal(detail2.LjingSunliang) * price, 2);

                    detail2.JinKouiMoney = GetDecimal(Convert.ToDecimal(detail2.LbejinQuantity) * price, 2);
                    detail2.LastMoney = GetDecimal(Convert.ToDecimal(detail2.LLastjingSunliang) * price, 2);
                }

                if (e.Column.Name == gridColumnbejinQuantity.Name) //修改已进口
                {

                    double bejinQuantity = 0;      //已进口
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out bejinQuantity);

                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;

                    detail2.UpQuantity = Convert.ToDouble(detail2.LjingSunliang) - Convert.ToDouble(bejinQuantity) - Convert.ToDouble(detail2.ZhuanCeInQuantity);
                    detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(detail2.YaoJInQuantity);
                    detail2.LilunStock = bejinQuantity - detail2.LilunHaoYong;
                    detail2.JinKouiMoney = GetDecimal((decimal)bejinQuantity * (detail2.LPrice.HasValue ? detail2.LPrice : 0m), 2);
                }
                if (e.Column.Name == gridColumnYaoJInQuantity.Name) //修改要进量  
                {

                    double yaoJInQuantity = 0;      //要进量
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out yaoJInQuantity);
                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;

                    detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(yaoJInQuantity);

                }
                if (e.Column.Name == gridColumnZhuanCeOut.Name || e.Column.Name == gridColumnZhuanRuShouCeId.Name) //是否更改转出到手册数量或者手册号
                {
                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    flag = 1;
                    Detail2ZhjuanCe.Add(detail2);

                }
                if (e.Column.Name == gridColumnZhuanCeIn.Name) //修改转入数量
                {
                    Model.BGHandbookDetail2 detail2 = bindingSource2.Current as Model.BGHandbookDetail2;
                    double quantity = 0;      //要进量
                    double.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);

                    detail2.UpQuantity = Convert.ToDouble(detail2.LjingSunliang) - Convert.ToDouble(detail2.LbejinQuantity) - quantity;
                    detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(detail2.YaoJInQuantity);

                }
                this.gridControl2.RefreshDataSource();

            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelClass.ExcelClass1 ex = new ExcelClass1();
            ex.Create();
            // ex.AddSheet("130101");
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);
            sheet.Name = "理论耗用";
            ex.UniteCells(sheet, 1, 1, 1, 4);
            sheet.Cells[1, 1] = this._bGHandbook.Id;


            sheet.Cells[2, 1] = "成品项号";
            sheet.Cells[2, 2] = "客户型号";
            sheet.Cells[2, 3] = "成品规格";
            sheet.Cells[2, 4] = "成品数量";
            sheet.Cells[2, 5] = "已出数量";
            sheet.Cells[2, 6] = "剩余量";
            sheet.Cells[2, 7] = "料件项号";
            sheet.Cells[2, 8] = "原料";
            sheet.Cells[2, 9] = "净量";
            sheet.Cells[2, 10] = "净+损";
            sheet.Cells[2, 11] = "净KG";
            sheet.Cells[2, 12] = "损耗%";
            int count1 = this._bGHandbook.Detail1.Count;

            // ex.AddSheet("130101");
            for (int i = 0; i < this._bGHandbook.Detail1.Count; i++)
            {
                sheet.Cells[i + 3, 1] = this._bGHandbook.Detail1[i].Id;
                sheet.Cells[i + 3, 2] = this._bGHandbook.Detail1[i].CusProName;
                sheet.Cells[i + 3, 3] = this._bGHandbook.Detail1[i].ProName;
                sheet.Cells[i + 3, 4] = this._bGHandbook.Detail1[i].BeeQuantity.HasValue ? this._bGHandbook.Detail1[i].BeeQuantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 5] = this._bGHandbook.Detail1[i].BeeQuantity.HasValue ? this._bGHandbook.Detail1[i].BeeQuantity.Value.ToString("0.####") : "";
                sheet.Cells[i + 3, 6] = 0;
                sheet.Cells[i + 3, 7] = this._bGHandbook.Detail1[i].LId;
                sheet.Cells[i + 3, 8] = this._bGHandbook.Detail1[i].Column1;
                sheet.Cells[i + 3, 9] = this._bGHandbook.Detail1[i].LiLunHaoYongJing.HasValue ? this._bGHandbook.Detail1[i].LiLunHaoYongJing.Value.ToString("0.00") : "";
                sheet.Cells[i + 3, 10] = this._bGHandbook.Detail1[i].LiLunHaoYongJingSun.HasValue ? this._bGHandbook.Detail1[i].LiLunHaoYongJingSun.Value.ToString("0.00") : "";
                sheet.Cells[i + 3, 11] = this._bGHandbook.Detail1[i].LjingQuantity.HasValue ? this._bGHandbook.Detail1[i].LjingQuantity.Value.ToString() : "";
                sheet.Cells[i + 3, 12] = this._bGHandbook.Detail1[i].Lsunhaolv.HasValue ? this._bGHandbook.Detail1[i].Lsunhaolv.Value.ToString() : "";

            }
            int a = this._bGHandbook.Detail1.Count + 7;

            sheet.Cells[a, 3] = "合计";
            string heji = this._bGHandbook.Detail1.Sum(bee => bee.BeeQuantity).ToString();
            sheet.Cells[a, 4] =
            sheet.Cells[a, 5] = heji;
            sheet.Cells[a + 3, 8] = "料件表";
            sheet.Cells[a + 5, 6] = "序号";
            sheet.Cells[a + 5, 8] = "原料";
            sheet.Cells[a + 5, 9] = "总净";
            sheet.Cells[a + 5, 10] = "总净+损";
            sheet.Cells[a + 5, 11] = "理论损耗/废料";

            for (int i = 0; i < this._bGHandbook.Detail2.Count; i++)
            {

                sheet.Cells[a + 6 + i, 6] = this._bGHandbook.Detail2[i].Id;
                sheet.Cells[a + 6 + i, 8] = this._bGHandbook.Detail2[i].ProName;
                double haoyongjing = this._bGHandbook.Detail1.Where(p => p.Id == this._bGHandbook.Detail2[i].Id).Sum(b => b.LiLunHaoYongJing).Value;
                sheet.Cells[a + 6 + i, 9] = haoyongjing.ToString();
                double haoyong = this._bGHandbook.Detail2[i].LilunHaoYong.HasValue ? this._bGHandbook.Detail2[i].LilunHaoYong.Value : 0;
                sheet.Cells[a + 6 + i, 10] = haoyong.ToString();
                sheet.Cells[a + 6 + i, 11] = haoyong - haoyongjing;
            }

            string y = "L" + (a + 5 + this._bGHandbook.Detail2.Count + 100).ToString();

            Range oRange = sheet.get_Range("A1", y);
            oRange.WrapText = true;
            oRange.EntireRow.AutoFit();

            ex.setBorder(sheet, 1, 2, 12, a, 2);
            ex.setBorder(sheet, 6, a + 5, 11, a + 5 + this._bGHandbook.Detail2.Count, 2);

            //宽度
            ex.SetWidth(sheet, "A:A", 5.88);
            ex.SetWidth(sheet, "B:B", 13.38);
            ex.SetWidth(sheet, "C:C", 11.63);
            ex.SetWidth(sheet, "D:D", 10.50);
            ex.SetWidth(sheet, "E:E", 11.88);
            ex.SetWidth(sheet, "F:F", 10.75);
            ex.SetWidth(sheet, "B:B", 13.38);
            ex.SetWidth(sheet, "G:G", 4.25);
            ex.SetWidth(sheet, "H:H", 17.00);
            ex.SetWidth(sheet, "I:I", 13.88);
            ex.SetWidth(sheet, "J:J", 12.50);
            ex.SetWidth(sheet, "K:K", 13.63);
            ex.SetWidth(sheet, "L:L", 5.75);

            //对齐方式
            ((Range)sheet.Columns["A:B", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["C:C", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["D:G", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["H:H", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Columns["I:L", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range)sheet.Rows["2:2", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();



        }
        private BL.InvoicePackingManager invoicePackingManager = new Book.BL.InvoicePackingManager();

        /// <summary>
        /// 出口明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelClass.ExcelClass1 ex = new ExcelClass1();
            ex.Create();
            // ex.AddSheet("130101");
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);
            sheet.Name = "出口明细";
            // ex.UniteCells(sheet, 1, 1, 1, 4);
            sheet.Cells[1, 1] = this._bGHandbook.Id;


            sheet.Cells[1, 1] = "品名";

            sheet.Cells[1, 3] = "项号";

            sheet.Cells[2, 1] = "报关单号";
            sheet.Cells[2, 2] = "客户名称";
            sheet.Cells[2, 3] = "客户订单号";
            sheet.Cells[2, 4] = "出口日期(出厂)";
            sheet.Cells[2, 5] = "出口金额";

            sheet.Cells[4, 1] = "合计";
            sheet.Cells[4, 5] = "USD";

            ex.SetWidth(sheet, "A:A", 22.13);
            ex.SetWidth(sheet, "B:B", 23.13);
            ex.SetWidth(sheet, "C:C", 20.13);



            string sql = "SELECT id,ProName FROM BGHandbookDetail1 WHERE ProName IS NOT NULL AND ProName <>'' AND BGHandbookDetail1.BGHandbookId=(SELECT TOP 1 BGHandbookId FROM BGHandbook WHERE AuditState=3 ORDER BY BGHandbookId desc)  ORDER BY id ";
            IList<Model.BGHandbookDetail1> detail1List = this.bGHandbookDetail1Manager.DataReaderBind<Model.BGHandbookDetail1>(sql, null, CommandType.Text);

            int i = 0;
            for (i = 0; i < detail1List.Count; i++)
            {

                sheet.Cells[1, i + 6] = detail1List[i].Id;
                sheet.Cells[2, i + 6] = detail1List[i].ProName;
                sheet.Cells[3, i + 6] = "数量";
            }

            sheet.Cells[2, i + 6] = "合计";
            ((Range)sheet.Rows["1:1", System.Type.Missing]).RowHeight = 33.75;
            ((Range)sheet.Rows["1:1", System.Type.Missing]).Font.Bold = true;
            Range oRange = (Range)sheet.Rows["2:2", System.Type.Missing];

            // oRange.RowHeight = 66.75;
            ((Range)sheet.Columns["D:E", System.Type.Missing]).ColumnWidth = 10.88;

            //string y = "L" + (a + 5 + this._bGHandbook.Detail2.Count + 100).ToString();

            //Range oRange = sheet.get_Range("A1", y);
            oRange.WrapText = true;
            oRange.EntireRow.AutoFit();
            Range oRange1 = (Range)sheet.get_Range("A1", "E2");
            oRange1.Font.Size = 14;
            oRange1.Font.Bold = true;

            ex.setBorder(sheet, 1, 1, i + 6, 4, 2);

            //对角线
            //Range m_objRange = sheet.get_Range("A1", "A1");

            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlDash;
            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown].ColorIndex = XlColorIndex.xlColorIndexAutomatic;

            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlLineStyleNone;
            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlLineStyleNone;
            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
            //m_objRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlLineStyleNone;

            string sql1 = "  SELECT i.*,c.CustomerShortName as CustomerFullName FROM InvoicePacking i left join customer c on i.xocustomerid=c.customerid  WHERE InvoicePackingid IN(SELECT InvoicePackingid FROM InvoicePackingDetail WHERE HandbookId='" + this._bGHandbook.Id + "') order by  InvoicePackingDate";

            IList<Model.InvoicePacking> invoiceList = this.bGHandbookDetail1Manager.DataReaderBind<Model.InvoicePacking>(sql1, null, CommandType.Text);


            int j = 0;
            IList<Model.InvoicePackingDetail> detailPackDetailList;
            IList<Model.InvoicePackingDetail> detailPackDetailListSum = new List<Model.InvoicePackingDetail>();
            for (j = 0; j < invoiceList.Count; j++)
            {
                sheet.Cells[4 + (j + 1) * 2, 2] = invoiceList[j].CustomerFullName;
                sheet.Cells[4 + (j + 1) * 2, 3] = invoicePackingManager.SelectCustomerInvoiceId(invoiceList[0].InvoicePackingId);// 客户订单号
                sheet.Cells[4 + (j + 1) * 2, 4] = invoiceList[j].InvoicePackingDate.Value.ToString("yyMMdd");
                //  sheet.Cells[4 + (j + 1) * 2, 5] = invoiceList[j].HeJi;
                sql1 = "  SELECT HandbookProductId,PackingNum FROM InvoicePackingDetail  WHERE InvoicePackingid ='" + invoiceList[j].InvoicePackingId + "' and HandbookId='" + this._bGHandbook.Id + "' and HandBookProductId is not null";

                detailPackDetailList = this.bGHandbookDetail1Manager.DataReaderBind<Model.InvoicePackingDetail>(sql1, null, CommandType.Text);
                foreach (Model.InvoicePackingDetail detail in detailPackDetailList)
                {
                    if (!string.IsNullOrEmpty(detail.HandbookProductId))
                    {
                        detailPackDetailListSum.Add(detail);

                        IList<Model.BGHandbookDetail1> hand = detail1List.Where(d => d.Id.ToString() == detail.HandbookProductId).ToList<Model.BGHandbookDetail1>();

                        if (hand != null && hand.Count > 0)
                        {
                            double a = 0;
                            if (!string.IsNullOrEmpty(((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4 + (j + 1) * 2, 6 + detail1List.IndexOf(hand[0])]).Text.ToString()))
                                a = double.Parse(((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4 + (j + 1) * 2, 6 + detail1List.IndexOf(hand[0])]).Text.ToString());

                            ((Range)sheet.Cells[4 + (j + 1) * 2, 6 + detail1List.IndexOf(hand[0])]).NumberFormatLocal = "@";
                            sheet.Cells[4 + (j + 1) * 2, 6 + detail1List.IndexOf(hand[0])] = (a + (detail.PackingNum.HasValue ? detail.PackingNum.Value : 0)).ToString("0.00");
                        }
                    }
                }
                ((Range)sheet.Cells[4 + (j + 1) * 2, i + 6]).NumberFormatLocal = "@";

                sheet.Cells[4 + (j + 1) * 2, i + 6] = string.Format("{0:0.00}", detailPackDetailList.Sum(d => d.PackingNum.HasValue ? d.PackingNum : 0).Value);


            }


            for (j = 0; j < detail1List.Count; j++)
            {
                ((Range)sheet.Cells[4, 6 + j]).NumberFormatLocal = "@";

                sheet.Cells[4, 6 + j] = detailPackDetailListSum.Where(d => d.HandbookProductId == detail1List[j].Id.ToString()).Sum(f => f.PackingNum.HasValue ? f.PackingNum.Value : 0).ToString("0.00");
            }

            ((Range)sheet.Cells[4, 6 + j]).NumberFormatLocal = "@";
            sheet.Cells[4, 6 + j] = detailPackDetailListSum.Sum(f => f.PackingNum.HasValue ? f.PackingNum.Value : 0).ToString("0.00");

            Range excelRange = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[4, detail1List.Count + 6]);
            excelRange.Select();
            ex.app.ActiveWindow.FreezePanes = true;


            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();
        }

        /// <summary>
        /// 进口明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ExcelClass.ExcelClass1 ex = new ExcelClass1();
            ex.Create();
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.app.Application.Worksheets.get_Item(1);
            sheet.Name = "进口明细";
            // ex.UniteCells(sheet, 1, 1, 1, 4);
            sheet.Cells[1, 1] = "品名";
            sheet.Cells[1, 3] = "项号";
            sheet.Cells[2, 1] = "报关单号";
            sheet.Cells[2, 3] = "进口日期";

            sheet.Cells[4, 3] = "合计";

            ex.SetWidth(sheet, "A:A", 22.13);
            ex.SetWidth(sheet, "B:B", 20);
            ex.SetWidth(sheet, "C:C", 20.13);

            string sql = "SELECT id,ProName FROM BGHandbookDetail2 WHERE ProName IS NOT NULL AND ProName <>'' AND BGHandbookDetail2.BGHandbookId=(SELECT TOP 1 BGHandbook.BGHandbookId FROM BGHandbook WHERE AuditState=3 ORDER BY BGHandbook.BGHandbookId desc)  ORDER BY id ";
            IList<Model.BGHandbookDetail2> detail2List = this.bGHandbookDetail2Manager.DataReaderBind<Model.BGHandbookDetail2>(sql, null, CommandType.Text);

            Range oRange = (Range)sheet.Rows["2:2", System.Type.Missing];
            oRange.WrapText = true;
            //oRange.EntireRow.AutoFill((Range)sheet.Rows["2:2", System.Type.Missing], XlAutoFillType.xlFillFormats);

            int i = 0;
            for (i = 0; i < detail2List.Count + 1; i++)
            {
                ex.UniteCells(sheet, 1, 4 + i * 2, 1, 5 + i * 2);
                ex.UniteCells(sheet, 2, 4 + i * 2, 2, 5 + i * 2);
            }
            for (i = 0; i < detail2List.Count; i++)
            {
                sheet.Cells[1, i * 2 + 4] = detail2List[i].Id;
                sheet.Cells[2, i * 2 + 4] = detail2List[i].ProName;
                sheet.Cells[3, i * 2 + 4] = "数量";
                sheet.Cells[3, i * 2 + 5] = "金额";
            }
            sheet.Cells[3, i * 2 + 4] = "数量";
            sheet.Cells[3, i * 2 + 5] = "金额";
            sheet.Cells[1, i * 2 + 4] = "合计";
            ((Range)sheet.Rows["1:1", System.Type.Missing]).RowHeight = 36;
            ((Range)sheet.Rows["2:2", System.Type.Missing]).RowHeight = 57;
            ((Range)sheet.Rows["1:1", System.Type.Missing]).Font.Bold = true;
            ((Range)sheet.Columns["D:E", System.Type.Missing]).ColumnWidth = 10;
            ex.setBorder(sheet, 1, 1, (i + 1) * 2 + 3, 4, 2);

            string sql1 = "SELECT * FROM InvoiceCG WHERE InvoiceId IN (SELECT InvoiceId FROM InvoiceCGDetail WHERE HandbookId='" + this._bGHandbook.Id + "') ORDER BY InvoiceDate";

            IList<Model.InvoiceCG> invoiceList = this.bGHandbookDetail2Manager.DataReaderBind<Model.InvoiceCG>(sql1, null, CommandType.Text);

            int j = 0;
            IList<Model.InvoiceCGDetail> cgDetailList = new List<Model.InvoiceCGDetail>();
            IList<Model.InvoiceCGDetail> cgDetailListSum = new List<Model.InvoiceCGDetail>();
            for (j = 0; j < invoiceList.Count; j++)
            {
                sheet.Cells[4 + (j + 1) * 2 - 1, 3] = invoiceList[j].InvoiceDate.Value.ToString("yyMMdd");

                sql1 = "SELECT HandbookProductId,InvoiceCGDetailQuantity FROM InvoiceCGDetail WHERE InvoiceId='" + invoiceList[j].InvoiceId + "' AND HandbookId='" + this._bGHandbook.Id + "' AND HandbookProductId IS NOT NULL";

                cgDetailList = this.bGHandbookDetail2Manager.DataReaderBind<Model.InvoiceCGDetail>(sql1, null, CommandType.Text);
                foreach (Model.InvoiceCGDetail detail in cgDetailList)
                {
                    if (detail != null)
                        cgDetailListSum.Add(detail);
                    if (detail.HandbookProductId != "")
                    {
                        IList<Model.BGHandbookDetail2> hand = detail2List.Where(d => d.Id.ToString() == detail.HandbookProductId).ToList<Model.BGHandbookDetail2>();

                        if (hand != null && hand.Count > 0)
                        {
                            double a = 0;
                            if (!string.IsNullOrEmpty(((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4 + (j + 1) * 2 - 1, 4 + detail2List.IndexOf(hand[0]) * 2]).Text.ToString()))
                                a = double.Parse(((Microsoft.Office.Interop.Excel.Range)sheet.Cells[4 + (j + 1) * 2 - 1, 4 + detail2List.IndexOf(hand[0]) * 2]).Text.ToString());

                            ((Range)sheet.Cells[4 + (j + 1) * 2 - 1, 4 + detail2List.IndexOf(hand[0]) * 2]).NumberFormatLocal = "@";
                            sheet.Cells[4 + (j + 1) * 2 - 1, 4 + detail2List.IndexOf(hand[0]) * 2] = (a + (detail.InvoiceCGDetailQuantity.HasValue ? detail.InvoiceCGDetailQuantity.Value : 0)).ToString("0.00");
                        }
                    }
                }
                ((Range)sheet.Cells[4 + (j + 1) * 2 - 1, i * 2 + 4]).NumberFormatLocal = "@";

                sheet.Cells[4 + (j + 1) * 2 - 1, i * 2 + 4] = string.Format("{0:0.00}", cgDetailList.Sum(d => d.InvoiceCGDetailQuantity.HasValue ? d.InvoiceCGDetailQuantity : 0).Value);
            }

            for (j = 0; j < detail2List.Count; j++)
            {
                ((Range)sheet.Cells[4, 4 + j * 2]).NumberFormatLocal = "@";
                sheet.Cells[4, 4 + j * 2] = cgDetailListSum.Where(d => d.HandbookProductId == detail2List[j].Id.ToString()).Sum(f => f.InvoiceCGDetailQuantity.HasValue ? f.InvoiceCGDetailQuantity.Value : 0).ToString("0.00");
            }

            ((Range)sheet.Cells[4, 4 + i * 2]).NumberFormatLocal = "@";
            sheet.Cells[4, 4 + i * 2] = cgDetailListSum.Sum(f => f.InvoiceCGDetailQuantity.HasValue ? f.InvoiceCGDetailQuantity.Value : 0).ToString("0.00");

            ex.app.ActiveWindow.FreezePanes = true;
            ((Range)sheet.Rows["1:2", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)sheet.Columns["C:C", System.Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ex.app.Visible = true;
            ex.release_xlsObj();
            GC.Collect();
        }

        //重计算理论耗用
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重新计算理论耗用及理论库存？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            Model.BGHandbookDetail1 chengItem;
            foreach (Model.BGHandbookDetail1 detail1 in this._bGHandbook.Detail1)
            {
                if (!string.IsNullOrEmpty(detail1.Column1))
                {
                    chengItem = this._bGHandbook.Detail1.Where(d => d.Id == detail1.Id && !string.IsNullOrEmpty(d.ProName)).ToList<Model.BGHandbookDetail1>()[0];
                    chengItem.BeeQuantity = this.bGProductDepotOutDetailManager.SumQuantityByHandbook(this._bGHandbook.Id, (detail1.Id.HasValue ? detail1.Id.ToString() : ""));
                    chengItem.UpQuantity = Convert.ToDouble(chengItem.Quantity) - Convert.ToDouble(chengItem.BeeQuantity);

                    detail1.LiLunHaoYongJing = (double)GetDecimal(Convert.ToDecimal(chengItem.BeeQuantity) * Convert.ToDecimal(detail1.LjingQuantity), 2);
                    detail1.LiLunHaoYongJingSun = (double)GetDecimal(Convert.ToDecimal(chengItem.BeeQuantity) * Convert.ToDecimal(detail1.LjingQuantity) / (1 - Convert.ToDecimal(detail1.Lsunhaolv) * 0.01M), 2);
                }
            }
            foreach (Model.BGHandbookDetail2 detail2 in this._bGHandbook.Detail2)
            {
                if (!string.IsNullOrEmpty(detail2.ProName))
                {
                    detail2.LilunHaoYong = this._bGHandbook.Detail1.Where(d => d.Column1 == detail2.ProName && d.LiLunHaoYongJingSun.HasValue && d.LiLunHaoYongJingSun.ToString() != "").Sum(s => s.LiLunHaoYongJingSun);
                    detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                }
            }
            this.gridControl1.RefreshDataSource();
            this.gridControl2.RefreshDataSource();



        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.action = "view";
            this.Refresh();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.BGHandbook.PRO_BGHandbookId;
        }

        protected override int AuditState()
        {
            return this._bGHandbook.AuditState.HasValue ? this._bGHandbook.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "BGHandbook" + "," + this._bGHandbook.BGHandbookId;
        }
        #endregion

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                Model.BGHandbookDetail1 detail1 = new Book.Model.BGHandbookDetail1();
                detail1.BGHandbookDetail1Id = Guid.NewGuid().ToString();
                this._bGHandbook.Detail1.Add(detail1);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail1);
                this.gridControl1.RefreshDataSource();
            }
            else
            {
                Model.BGHandbookDetail2 detail2 = new Book.Model.BGHandbookDetail2();
                detail2.BGHandbookDetail2Id = Guid.NewGuid().ToString();
                this._bGHandbook.Detail2.Add(detail2);
                this.bindingSource2.Position = this.bindingSource2.IndexOf(detail2);
                this.gridControl2.RefreshDataSource();
            }
        }

        void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.bindingSource1.Remove(this.bindingSource1.Current as Model.BGHandbookDetail1);
                this.gridControl1.RefreshDataSource();
            }
            else
            {
                this.bindingSource2.Remove(this.bindingSource2.Current as Model.BGHandbookDetail2);
                this.gridControl2.RefreshDataSource();
            }
        }

        /// <summary>
        /// 重新计算 已定未出/已出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCountYDWC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重新计算已定未出/已出？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            foreach (Model.BGHandbookDetail1 detail1 in this._bGHandbook.Detail1)
            {
                detail1.YdwcQuantity = this.xodetailmanader.SumOrderQuantityByHandbook(this._bGHandbook.Id, (detail1.Id.HasValue ? detail1.Id.ToString() : "")) - this.xsdetailmanager.SumBeeQuantityByHandbook(this._bGHandbook.Id, (detail1.Id.HasValue ? detail1.Id.ToString() : ""));

                detail1.YdycQuantity = this.xsdetailmanager.SumBeeQuantityByHandbook(this._bGHandbook.Id, (detail1.Id.HasValue ? detail1.Id.ToString() : ""));
            }
            this.gridControl1.RefreshDataSource();
        }

        private void btn_ReCountStock_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重新计算料件理论库存,剩余/可进量？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            foreach (Model.BGHandbookDetail2 detail2 in this._bGHandbook.Detail2)
            {
                detail2.LilunStock = Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(detail2.LilunHaoYong);
                detail2.UpQuantity = Convert.ToDouble(detail2.LjingSunliang) - Convert.ToDouble(detail2.LbejinQuantity);
                detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(detail2.YaoJInQuantity);
            }
            this.gridControl2.RefreshDataSource();
        }
    }
}