using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace Book.UI.General
{
        //*----------------------------------------------------------------
        // Copyright (C) 2008 - 2010  飛馳軟件有限公司
        //                     版權所有 圍著必究

        // 编 码 人:  裴盾             完成时间:2009-10-10
        // 修改原因：
        // 修 改 人:                          修改时间:
        // 修改原因：
        // 修 改 人:                          修改时间:
        //----------------------------------------------------------------*/
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private IList<Common.Connection> connections;
        protected BL.OperatorsManager operatorsManager;
        public LoginForm()
        {
            InitializeComponent();
        }

        #region 原数据库连接窗体
        public LoginForm(IList<Common.Connection> connections)
            : this()
        {
            this.connections = connections;

        }

        private void PopulateConnections()
        {
            this.comboBoxEditConnections.Properties.Items.Clear();
            string conn = string.Empty;
            string username = string.Empty;

            String configFile = Application.ExecutablePath + ".config";
            XmlDocument document = new XmlDocument();
            document.Load(configFile);

            XmlNodeList nodes = document.SelectNodes("/configuration/userSettings/Book.UI.Properties.Settings/setting");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["name"].Value == "connection")
                {
                    conn = node.FirstChild.InnerText;
                }
                if (node.Attributes["name"].Value == "username")
                {
                    username = node.FirstChild.InnerText;
                }
            }
            foreach (Common.Connection connection in this.connections)
            {
                this.comboBoxEditConnections.Properties.Items.Add(connection);
                if (connection.Name == conn)
                {
                    this.comboBoxEditConnections.SelectedIndex = this.connections.IndexOf(connection);       
                }
                //string data = Module.mGetSetting(Module.BASE_NAME, Module.BASE_INFO);
                //if (connection.Name == data)
                //{
                //    this.comboBoxEditConnections.SelectedIndex = this.connections.IndexOf(connection);
                //}
              

            }
            //string operatorname = Module.mGetSetting(Module.USER_NAME, Module.USER_INFO);
            this.comboBoxEditOperator.Text = username;// Module.mGetSetting(Module.USER_NAME, Module.USER_INFO);
        }
        public Common.Connection SelectedConnection
        {
            get
            {
                return this.comboBoxEditConnections.SelectedItem as Common.Connection;
            }
        }

        #endregion
        private void LoginForm_Load(object sender, EventArgs e)
        {

            this.PopulateConnections();

            //IList<Model.Operators> operators = this.operatorsManager.SelectOperators();
            //this.comboBoxEditOperator.Properties.Items.Clear();
            //foreach (Model.Operators _operator in operators)
            //{
            //    this.comboBoxEditOperator.Properties.Items.Add(_operator);
            //}
            //this.comboBoxModule.DataSource = Module.Modules;
            //this.comboBoxModule.DisplayMember = "ModuleText";
            //this.comboBoxModule.ValueMember = "ModuleName";



        }

        private void simpleButtonLogin_Click(object sender, EventArgs e)
        {
            //数据库连接判断


            if (this.comboBoxEditConnections.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.RequireChooseConnection);
                return;
            }
            //正在检测连接...
            //  this.labelControl1.Text = Properties.Resources.CheckIngConnection;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            Common.Connection connection = this.comboBoxEditConnections.SelectedItem as Common.Connection;
            if (!connection.Awailable)
            {
                this.labelControl1.Text = Properties.Resources.ConnectionCannotUse;
                Cursor.Current = Cursors.Default;
                return;
            }



            //Cursor.Current = Cursors.Default;

            //用户名 密码

            string operatorName = this.comboBoxEditOperator.Text.Trim();
            string password = this.textEditPassWord.Text.Trim();



            Model.Operators _operator = this.operatorsManager.GetByOperatorName(operatorName);

            if (_operator == null)
            {
                MessageBox.Show(Properties.Resources.NoThisUser, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_operator.Password != password)
            {
                MessageBox.Show(Properties.Resources.PassWordError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this._operator = _operator;


            //this._currentModule = this.comboBoxModule.SelectedItem as Module;
            this._currentModule = new Module("Stock", Properties.Resources.DepotMana, "Book.UI.MainForm");

            this.DialogResult = DialogResult.OK;

            //string dataname = SelectedConnection.Name;
            //string username = _operator.OperatorName;

            //Module.mSaveSetting(Module.BASE_NAME, dataname, Module.BASE_INFO);
            //Module.mSaveSetting(Module.USER_NAME, username, Module.USER_INFO);
            String configFile = Application.ExecutablePath + ".config";
            XmlDocument document = new XmlDocument();
            document.Load(configFile);

            XmlNodeList nodes = document.SelectNodes("/configuration/userSettings/Book.UI.Properties.Settings/setting");
            foreach (XmlNode node in nodes)
            {
                switch (node.Attributes["name"].Value)
                {
                    case "connection":
                        node.FirstChild.InnerText = SelectedConnection.Name;
                        break;
                    case "username":
                        node.FirstChild.InnerText = _operator.OperatorName;
                        break;
                }
                
                document.Save(configFile);
            }
        }

        private Model.Operators _operator;

        public Model.Operators Operator
        {
            get { return _operator; }
        }

        private Module _currentModule;

        public Module CurrentModule
        {

            get
            {
                return _currentModule;
            }
        }

        private void comboBoxEditOperator_QueryPopUp(object sender, CancelEventArgs e)
        {

            if (this.comboBoxEditConnections.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.RequireChooseConnection);
                return;
            }

            IList<Model.Operators> operators = this.operatorsManager.SelectOrderByName();
            this.comboBoxEditOperator.Properties.Items.Clear();
            foreach (Model.Operators _operator in operators)
            {
                this.comboBoxEditOperator.Properties.Items.Add(_operator);
            }
            //this.comboBoxModule.DataSource = Module.Modules;
            //this.comboBoxModule.DisplayMember = "ModuleText";
            //this.comboBoxModule.ValueMember = "ModuleName";





        }

        private void comboBoxModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBoxEditConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.ModifyConfigurations(this.SelectedConnection);
            this.operatorsManager = new Book.BL.OperatorsManager();
        }



        //private void comboBoxEditConnections_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.labelControl1.Text = "";
        //    //保存config

        //    string a = BL.Settings.CompanyAddress1;

        //}
    }
    public class Module
    {

        public Module(string name, string text, string className)
        {
            _moduleName = name;
            _moduleText = text;
            _className = className;
        }

        private string _moduleName;

        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }
        private string _moduleText;

        public string ModuleText
        {
            get { return _moduleText; }
            set { _moduleText = value; }
        }

        private string _className;

        public string ClassName
        {
            get
            { return _className; }
            set
            { _className = value; }
        }

        static IList<Module> modules = new List<Module>();

        public static IList<Module> Modules
        {
            get
            {
                if (modules.Count < 1)
                {
                    //modules.Add(new Module("BaseSetting", Properties.Resources.BaseSetting, "Book.UI.MainForm2"));
                    //modules.Add(new Module("Produce", Properties.Resources.ProduceMana, "Book.UI.Erp.Produce.MainForm"));
                    //modules.Add(new Module("ImportStock", Properties.Resources.StockMana, "Book.UI.MainForm"));
                    //modules.Add(new Module("ExportStock", Properties.Resources.OutProMana, "Book.UI.MainForm"));
                    modules.Add(new Module("Stock", Properties.Resources.DepotMana, "Book.UI.MainForm"));
                }
                return modules;
            }
        }

        //#region 注册表----lyl


        //public const string COMP_NAME = "專業ERP開發公司(飛馳)";
        //public const string SOFT_VSON = "10.0.1";
        //public const string BASE_INFO = "currentdatabase";
        //public const string USER_INFO = "currentoperator";
        //public const string BASE_NAME = "database";
        //public const string USER_NAME = "username";

        ///// <summary>
        ///// 读取注册表
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="ChildAppName"></param>
        ///// <returns></returns>
        //public static string mGetSetting(string Key, string ChildAppName)
        //{
        //    Microsoft.Win32.RegistryKey mKey;
        //    string SysKey;
        //    SysKey = "SOFTWARE\\" + COMP_NAME + "\\" + SOFT_VSON + "\\" + ChildAppName;
        //    mKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SysKey, true);
        //    if (mKey == null)
        //    {
        //        mKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(SysKey);
        //    }
        //    if (mKey.GetValue(Key) == null) return null;
        //    string databasename = mKey.GetValue(Key).ToString();
        //    mKey.Close();

        //    return databasename;
        //}

        ///// <summary>
        ///// 写入注册表
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Setting"></param>
        ///// <param name="ChildAppName"></param>
        //public static void mSaveSetting(string Key, string Setting, string ChildAppName)
        //{
        //    Microsoft.Win32.RegistryKey mKey;
        //    string SysKey;
        //    SysKey = "SOFTWARE\\" + COMP_NAME + "\\" + SOFT_VSON + "\\" + ChildAppName;
        //    mKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SysKey, true);
        //    if (mKey == null)
        //    {
        //        mKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(SysKey);
        //    }
        //    mKey.SetValue(Key, Setting);
        //    mKey.Close();
        //}
        //#endregion
    }
}