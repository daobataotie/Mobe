using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Department
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: 
   // �� �� ����CustomerProductListForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-10-15
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm1
    {

        #region ׃�������x
        IList<Model.Department> _detail = new List<Model.Department>();
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        // private Model.Department department = null;
        private System.Data.DataTable data = null;
        #endregion

        #region �o�K���캯��
        public EditForm()
        {
            InitializeComponent();

            // this.requireValueExceptions.Add(Model.Department.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId,this.gridControl1 as Control));
            this.requireValueExceptions.Add(Model.Department.PROPERTY_DEPARTMENTNAME, new AA(Properties.Resources.RequireDataForName, this.gridControl1 as Control));

            //  this.invalidValueExceptions.Add(Model.Department.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.Department.PROPERTY_DEPARTMENTNAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));

            this.action = "view";
        }
        #endregion

        #region ���d����
        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            // try
            {

                DataRowView dr = this.bindingSource1.Current as DataRowView;
                this.departmentManager.Delete(dr.Row.ItemArray[0].ToString());
                this.data = departmentManager.GetAll();
            }
            //catch
            //{
            //    MessageBox.Show(Properties.Resources.DeleteError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            {
                return;
            }

            //switch (this.action)
            //{
            //    case "insert":
            //        this.departmentManager.Update(this._detail);
            //        Workflowinsert wfinsert = new Workflowinsert();
            //        if (wfinsert.Checkwfbytablescode("����"))
            //        {
            //            wfinsert.insertwfrecord("����", "Ʒ�܆T���", "1BC953F1-CC5B-4113-9ED5-69D6FB74F710");
            //        }
            //        break;
            //    case "update":
            //        this.departmentManager.Update(this._detail);
            //        break;
            //    default:
            //        break;
            //}

            this.departmentManager.SaveInfo(data);
        }
        protected override void AddNew()
        {
            Model.Department department = new Book.Model.Department();
            department.DepartmentId = Guid.NewGuid().ToString();
            department.DepartmentName = string.Empty;
            this.data.Rows.Add(department);
            this.bindingSource1.Position =this.bindingSource1.Count - 1;
        }
        public override void Refresh()
        {          
           
            if (this.action == "view")
            {
                this.data = departmentManager.GetAll();
                this.bindingSource1.DataSource = data;
                this.bindingSource1.Position = this.bindingSource1.Count-1;
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
        protected override void grid_keyDpwn()
        {

            Model.Department department = new Book.Model.Department();
            department.DepartmentId = Guid.NewGuid().ToString();
            //department.Id = string.Empty;
            department.DepartmentName = string.Empty;
            // department.DepartmentDescription = string.Empty;
            this.data.Rows.Add(department);
            this.bindingSource1.Position = this.bindingSource1.Count - 1;
            this.gridControl1.RefreshDataSource();
        }
        protected override void grid_KeyDelete()
        {
            DataRowView dr = this.bindingSource1.Current as DataRowView;
            this.data.Rows.Remove(dr.Row);
        }
        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.data = departmentManager.GetAll();
            this.bindingSource1.DataSource = data;
        }

    }
}


