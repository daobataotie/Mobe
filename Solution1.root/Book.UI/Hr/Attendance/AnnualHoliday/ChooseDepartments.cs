using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.Hr.Attendance.AnnualHoliday
{
    public partial class ChooseDepartments : Book.UI.Settings.BasicData.BaseChooseForm
    {
        private int _allCount = 0;
        public ChooseDepartments()
        {
            InitializeComponent();
            //this.manager = new BL.DepartmentManager();
        }

        public ChooseDepartments(string DepartMentIds)
            : this()
        {
            IList<Model.Department> deplist = new BL.DepartmentManager().Select();
            if (string.IsNullOrEmpty(DepartMentIds))
            {
                foreach (Model.Department de in deplist)
                    de.IsChecked = true;
            }
            else
            {
                foreach (Model.Department de in deplist)
                {
                    if (DepartMentIds.Contains(de.DepartmentId))
                        de.IsChecked = true;
                    else
                        de.IsChecked = false;
                }
            }

            this._allCount = deplist.Count;
            this.bindingSource1.DataSource = deplist;
        }

        public string SelectDepartNames
        {
            get
            {
                IList<string> strlist = (from Model.Department de in (this.bindingSource1.DataSource as IList<Model.Department>)
                                         where de.IsChecked == true
                                         select de.DepartmentName).ToList<string>();
                if (strlist.Count == this._allCount)
                {
                    return "";
                }
                else
                {
                    return strlist.Aggregate((s1, s2) => string.Format("{0},{1}", s1, s2));
                }
            }
        }

        public string SelectDepartIds
        {
            get
            {
                IList<string> strlist = (from Model.Department de in (this.bindingSource1.DataSource as IList<Model.Department>)
                                         where de.IsChecked == true
                                         select de.DepartmentId).ToList<string>();
                if (strlist != null && strlist.Count != 0)
                {
                    return strlist.Aggregate((s1, s2) => string.Format("{0},{1}", s1, s2));
                }
                else
                {
                    return "";
                }
            }
        }

        private void ChooseDepartments_Load(object sender, EventArgs e)
        {
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridView1.OptionsFind.AlwaysVisible = false;

            this.colCheck.OptionsColumn.AllowEdit = true;
            this.colDepartname.OptionsColumn.AllowEdit = false;
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
