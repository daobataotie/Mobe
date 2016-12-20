using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData;
using Book.BL;

namespace Book.UI.produceManager.PronoteMachine
{
    public partial class EditForm : BaseEditForm
    {

        private PronoteMachineManager pronoteMachineManager = new PronoteMachineManager();
        private Model.PronoteMachine pronoteMachine;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PronoteMachine.PROPERTY_ID, new AA(Properties.Resources.MachineIdIsNull, this.txtPronoteMachineId as Control));
            this.requireValueExceptions.Add(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME, new AA(Properties.Resources.MachineNameIsNull, this.txtPronoteMachineName as Control));
            this.invalidValueExceptions.Add(Model.PronoteMachine.PROPERTY_ID, new AA(Properties.Resources.MachineIdCF, this.txtPronoteMachineId as Control));
            this.invalidValueExceptions.Add(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME, new AA(Properties.Resources.MachineNameCF, this.txtPronoteMachineName as Control));
            this.invalidValueExceptions.Add(Model.PronoteMachine.PROPERTY_WORKHOUSEID, new AA(Properties.Resources.MachineNodeIsNull, this.newChooseContorlWorkH as Control));
            this.action = "view";
            this.newChooseContorlWorkH.Choose = new Book.UI.Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

        }

        #region 重載方法

        protected override void MoveLast()
        {
            this.pronoteMachine = this.pronoteMachineManager.Get(this.pronoteMachineManager.GetLast() == null ? "" : this.pronoteMachineManager.GetLast().PronoteMachineId);
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.PronoteMachine pronoteMachine = this.bindingSource1.Current as Model.PronoteMachine;
            this.pronoteMachineManager.Delete(pronoteMachine.PronoteMachineId);
        }
        protected override void Save()
        {
            this.pronoteMachine.Id = txtPronoteMachineId.Text;
            this.pronoteMachine.PronoteMachineName = txtPronoteMachineName.Text;
            this.pronoteMachine.IsChecked = false;
            if (newChooseContorlWorkH.EditValue != null)
                this.pronoteMachine.WorkHouseId = (newChooseContorlWorkH.EditValue as Model.WorkHouse).WorkHouseId.ToString();
            this.pronoteMachine.Quantity = Convert.ToDouble(this.spinEditQuantity.Text);
            this.pronoteMachine.MachineTotal = this.spinEditMachineTotal.Value;
            this.pronoteMachine.MachinePower = Convert.ToDouble(this.spinEditMachinePower.Text);
            this.pronoteMachine.ProductUnitId = this.textEditProductUnitId.Text;
            switch (this.action)
            {
                case "insert":
                    this.pronoteMachineManager.Insert(this.pronoteMachine);
                    break;

                case "update":
                    this.pronoteMachineManager.Update(this.pronoteMachine);
                    break;
            }

        }

        protected override void AddNew()
        {
            this.pronoteMachine = new Book.Model.PronoteMachine();
        }

        public override void Refresh()
        {



            //if (this.action == "view")
            //{
            //    if (this.bindingSource1.Current != null)
            //        this.pronoteMachine = this.bindingSource1.Current as Model.PronoteMachine;
            //}
            if (this.pronoteMachine == null)
            {
                this.pronoteMachine = new Book.Model.PronoteMachine();
                this.action = "insert";
            }
            else
            {
                if (this.action != "insert")
                    this.pronoteMachine = this.pronoteMachineManager.Get(this.pronoteMachine.PronoteMachineId);
            }

            this.txtPronoteMachineId.Text = this.pronoteMachine.Id;
            this.txtPronoteMachineName.Text = this.pronoteMachine.PronoteMachineName;
            this.newChooseContorlWorkH.EditValue = this.pronoteMachine.WorkHouse;

            this.spinEditQuantity.Text = this.pronoteMachine.Quantity.ToString();
            this.spinEditMachineTotal.Text = this.pronoteMachine.MachineTotal.ToString() ;
            this.spinEditMachinePower.Text = this.pronoteMachine.MachinePower.ToString();
            this.textEditProductUnitId.Text = this.pronoteMachine.ProductUnitId;

            this.bindingSource1.DataSource = this.pronoteMachineManager.Select();
            base.Refresh();
        }


        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            this.pronoteMachine = this.bindingSource1.Current as Model.PronoteMachine;
            if (pronoteMachine != null)
            {
                this.action = "view";
                this.Refresh();
            }
        }
    }
}
