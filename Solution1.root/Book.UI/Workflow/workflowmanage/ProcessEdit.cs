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
//------------------------------------------------------------------------------
//
// 说明：该文件中的内容主要处理过程
//
// author: 徐彦飞
// create date：2009-12-15 上午 11:45:01
//
//------------------------------------------------------------------------------
namespace Book.UI.Workflow.workflowmanage
{
    public partial class ProcessEdit : BaseEditForm
    {
        // 流程编号
        string wid;

        Model.process editprocess;
        // 过程管理
        BL.ProcessManager processM = new Book.BL.ProcessManager();
        // 操作员管理实例
        BL.OperatorsManager operatorsm = new Book.BL.OperatorsManager();
        // 部门管理实例
        BL.DepartmentManager departmentm = new Book.BL.DepartmentManager();

        BL.accepterattribManager _accepterattribM = new Book.BL.accepterattribManager();

        IList<Model.process> processlist;

        Model.Operators currentoperators;

        BL.RoleManager mRoleManager = new Book.BL.RoleManager();

        //Model.Role currentRole;
        IList<Model.Role> mSelectRole;

        public ProcessEdit(string wfid)
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.process.PRO_processname, new AA(Properties.Resources.RequireDataForNames, this.textBox_name));
            this.requireValueExceptions.Add(Model.process.PRO_andrule, new AA(Properties.Resources.Andrule, this.textBox_name));

            Formload();//窗体初始化
            wid = wfid;
            Processload(wfid);
            action = "insert";
            //bindingSource1.MoveFirst();
            //this.currentRole = bindingSource1.Current as Model.Role;
        }

        public ProcessEdit(string processid, string action)
        {
            InitializeComponent();
            this.action = action;

            this.editprocess = processM.GetProcessbyid(processid);

            Formload();

            this.textBox_name.Text = editprocess.processname;
            if (editprocess.Processpre != null)
                this.comboBox_procespre.Text = (processM.GetProcessbyid(editprocess.Processpre)).processname;
            this.comboBox_rule.Text = editprocess.andrule;
            this.textBox_description.Text = editprocess.descript;
            this.comboBox_condition.Text = editprocess.condition;
            // this.comboBox_procespre.Properties.Items.Remove(editprocess.processname);
            BL.accepterattribManager accepm = new Book.BL.accepterattribManager();

            Model.accepterattrib a = null;

            foreach (Model.accepterattrib at in accepm.Select())
            {
                if (at.processId == processid)
                {
                    a = at;
                }
            }

            if (a != null)
            {
                string acceid = a.RoleId;

                foreach (Model.Operators op in (new BL.OperatorsManager()).Select())
                {
                    if (op.OperatorsId == acceid)
                    {
                        this.currentoperators = op;
                    }
                }
            }
        }

        public void Formload()
        {
            loadperson();   //角色
            loaddepart();   //部门
            loadrule();     //规则
        }

        private void Processload(string wfid)
        {
            processlist = processM.SelectProcessbywf(wfid);
            foreach (Model.process pro in processlist)
            {
                if (pro.processType != "结束")
                {
                    this.comboBox_procespre.Properties.Items.Add(pro.processname);
                }
            }
        }

        // 加载角色表方法
        public void loadperson()
        {
            //IList<Model.Operators> operatorslist = operatorsm.Select();
            //this.bindingSource1.DataSource = operatorslist;
            this.mSelectRole = mRoleManager.Select();
            if (this.editprocess != null)
            {
                IList<Model.accepterattrib> maccepter = this._accepterattribM.SelectByProcessId(this.editprocess.processId);
                foreach (Model.accepterattrib mAcc in maccepter)
                {
                    foreach (Model.Role mr in this.mSelectRole)
                    {
                        if (mAcc.Role.RoleId == mr.RoleId)
                        {
                            mr.Checked = true;
                        }
                    }
                }
            }
            this.bindingSource1.DataSource = this.mSelectRole;
        }

        // 加载部门方法
        public void loaddepart()
        {

        }

        // 加载规则方法
        public void loadrule()
        {
            this.comboBox_rule.Properties.Items.Add("超过半数");
            this.comboBox_rule.Properties.Items.Add("少部分通过");
            this.comboBox_rule.Properties.Items.Add("半数通过");
            this.comboBox_rule.Properties.Items.Add("全通过");
            this.comboBox_rule.Properties.Items.Add("全拒绝");
        }

        private bool checkname(string id, string name)
        {
            int n = 0;
            foreach (Model.process pro in (new BL.ProcessManager()).SelectProcessbywf(id))
            {
                if (pro.processname == name)
                    n++;
            }
            return (n > 0) ? false : true;
        }

        //重写
        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (action)
            {
                case "insert":
                    Model.process preprocess = new Book.Model.process();
                    foreach (Model.process pro in processlist)
                    {
                        if (pro.processname == this.comboBox_procespre.Text)
                        {
                            preprocess = pro;
                        }
                    }
                    Model.process nextprocess = processM.GetProcessbyid(preprocess.Processnex);
                    //待添加的过程
                    Model.process currentproccess = new Book.Model.process();
                    currentproccess.WorkflowId = wid;
                    currentproccess.processId = Guid.NewGuid().ToString();
                    if (this.checkname(wid, this.textBox_name.Text))
                    {
                        currentproccess.processname = this.textBox_name.Text;
                        currentproccess.andrule = this.comboBox_rule.Text;
                        currentproccess.condition = this.comboBox_condition.Text;
                        currentproccess.descript = this.textBox_description.Text;
                        currentproccess.Processpre = preprocess.processId;
                        currentproccess.Processnex = nextprocess == null ? null : nextprocess.processId;
                        currentproccess.InsertTime = DateTime.Now;
                        currentproccess.number = 1;
                        currentproccess.processType = "中间";
                        preprocess.Processnex = currentproccess.processId;
                        if (nextprocess != null)
                            nextprocess.Processpre = currentproccess.processId;


                        processM.Insert(currentproccess);
                        processM.Update(preprocess);
                        processM.Update(nextprocess);

                        BL.accepterattribManager accepterm = new Book.BL.accepterattribManager();

                        Model.accepterattrib accepter = new Book.Model.accepterattrib();

                        IList<Model.Role> mrlist = (from Model.Role r in this.mSelectRole
                                                    where r.Checked == true
                                                    select r).ToList<Model.Role>();

                        foreach (Model.Role currentRole in mrlist)
                        {
                            accepter.accepterattribID = Guid.NewGuid().ToString();
                            accepter.processId = currentproccess.processId;
                            accepter.acceptertype = "个人";
                            accepter.RoleId = currentRole.RoleId;

                            accepterm.Insert(accepter);
                        }
                    }
                    break;
                case "update":
                    updateprocess();
                    break;
            }
        }

        //修改过程
        private void updateprocess()
        {
            Model.process propeocess1 = processM.GetProcessbyid(editprocess.Processpre);
            Model.process nextprocess1 = processM.GetProcessbyid(editprocess.Processpre);
            //修改名称
            if (this.textBox_name.Text != editprocess.processname)
            {
                editprocess.processname = this.textBox_name.Text;
            }
            //修改规则
            if (this.comboBox_rule.Text != editprocess.andrule)
            {
                editprocess.andrule = this.comboBox_rule.Text;
            }
            editprocess.condition = this.comboBox_condition.Text;

            //修改角色
            IList<Model.Role> mrlist = (from Model.Role r in this.mSelectRole
                                        where r.Checked == true
                                        select r).ToList<Model.Role>();
            //删除原有
            this._accepterattribM.DeleteByProcessId(editprocess.processId);
            //新增
            Model.accepterattrib accepter = new Book.Model.accepterattrib();
            foreach (Model.Role r in mrlist)
            {
                accepter.accepterattribID = Guid.NewGuid().ToString();
                accepter.processId = editprocess.processId;
                accepter.acceptertype = "个人";
                accepter.RoleId = r.RoleId;
                this._accepterattribM.Insert(accepter);
            }


            //修改上一过程
            if (this.comboBox_procespre.Text != propeocess1.processname)
            {
                Model.process proprocess2 = processM.SelectProcessbyname(this.comboBox_procespre.Text);
                Model.process nextprocess2 = processM.GetProcessbyid(proprocess2.Processnex);

                editprocess.Processpre = proprocess2.processId;
                editprocess.Processnex = nextprocess2.processId;

                propeocess1.Processnex = nextprocess1.processId;
                nextprocess1.Processpre = propeocess1.processId;

                proprocess2.Processnex = editprocess.processId;
                nextprocess2.Processpre = editprocess.processId;

                processM.Update(editprocess);
                processM.Update(propeocess1);
                processM.Update(nextprocess1);
                processM.Update(proprocess2);
                processM.Update(nextprocess2);
                return;
            }
            processM.Update(editprocess);
        }
    }
}