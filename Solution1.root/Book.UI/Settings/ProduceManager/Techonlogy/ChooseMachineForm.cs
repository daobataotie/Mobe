using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.Model;
using Book.BL;

namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    public partial class ChooseMachineForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.PronoteMachineManager pronoteMachineManager = new Book.BL.PronoteMachineManager();
        private BL.ProceduresMachineManager proceManager = new ProceduresMachineManager();
        private IList<Model.PronoteMachine> prolist = new List<Model.PronoteMachine>();
        private IList<Model.PronoteMachine> plist = new List<Model.PronoteMachine>();
        private string PId = "";
        public ChooseMachineForm()
        {
            InitializeComponent();
            prolist = pronoteMachineManager.Select();
            this.bindingSource1.DataSource = prolist;
        }

        public ChooseMachineForm(IList<Model.PronoteMachine> list, PronoteProceduresDetail detail)
        {
            InitializeComponent();
            this.PId = detail.ProceduresId;
            this.plist = list;

            prolist = pronoteMachineManager.Select();
            foreach (Model.PronoteMachine machine in prolist)
            {
                foreach (Model.PronoteMachine item in plist)
                {
                    if (item.PronoteMachineId == machine.PronoteMachineId)
                        machine.IsChecked = true;
                }

            }
            this.bindingSource1.DataSource = prolist;
        }

        public ChooseMachineForm(IList<Model.PronoteMachine> list, string ProceduresId)
        {
            InitializeComponent();
            this.PId = ProceduresId;
            this.plist = list;

            prolist = pronoteMachineManager.Select();
            foreach (Model.PronoteMachine machine in prolist)
            {
                foreach (Model.PronoteMachine item in plist)
                {
                    if (item.PronoteMachineId == machine.PronoteMachineId)
                        machine.IsChecked = true;
                }

            }
            this.bindingSource1.DataSource = prolist;
        }

        private IList<Model.PronoteMachine> selectItem;

        public IList<Model.PronoteMachine> SelectItem
        {
            get { return selectItem; }
            set { selectItem = value; }
        }

        private void sbtn_sure_Click(object sender, EventArgs e)
        {
            this.proceManager.DelelteByProduresMachines(this.PId);
            selectItem = new List<Model.PronoteMachine>();
            foreach (Model.PronoteMachine pro in prolist)
            {
                if (pro.IsChecked == true)
                    this.selectItem.Add(pro);
            }
            this.DialogResult = DialogResult.OK;

        }

        private void sbtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}