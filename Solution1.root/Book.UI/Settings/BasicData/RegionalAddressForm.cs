using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

using Book.Model;
using Book.BL;
namespace Book.UI.Settings.BasicData
{
    public partial class RegionalAddressForm : DevExpress.XtraEditors.XtraForm
    {
        //public float X;
        //public float Y;
        //public float y;
        City city = new City();
        District district = new District();
        Shreet shreet = new Shreet();
        int i = 0;
        int jjyy = 0;
        public RegionalAddressForm()
        {
            InitializeComponent();
        }
        public RegionalAddressForm(int i):this()
        {
            this.i = i;
        }
        private void RegionalAddressForm_Load(object sender, EventArgs e)
        {
            //this.Resize += new EventHandler(Form1_Resize);

            //X = this.Width;
            //Y = this.Height;
            ////y = this.statusStrip1.Height;
            //setTag(this);

            Bind();
        }
        //private void setTag(Control cons)
        //{
        //    foreach (Control con in cons.Controls)
        //    {
        //        con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
        //        if (con.Controls.Count > 0)
        //            setTag(con);
        //    }
        //}
        //private void setControls(float newx, float newy, Control cons)
        //{
        //    foreach (Control con in cons.Controls)
        //    {

        //        if (con.Tag != null)
        //        {
        //            string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
        //            float a = Convert.ToSingle(mytag[0]) * newx;
        //            con.Width = (int)a;
        //            a = Convert.ToSingle(mytag[1]) * newy;
        //            con.Height = (int)(a);
        //            a = Convert.ToSingle(mytag[2]) * newx;
        //            con.Left = (int)(a);
        //            a = Convert.ToSingle(mytag[3]) * newy;
        //            con.Top = (int)(a);
        //            Single currentSize = Convert.ToSingle(mytag[4]) * newy;

        //            //改变字体大小
        //            con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
        //        }
        //        if (con.Controls.Count > 0)
        //        {
        //            setControls(newx, newy, con);
        //        }
        //    }
        //}
        //void Form1_Resize(object sender, EventArgs e)
        //{
        //    // throw new Exception("The method or operation is not implemented.");
        //    float newx = (this.Width) / X;
        //    //  float newy = (this.Height - this.statusStrip1.Height) / (Y - y);
        //    float newy = this.Height / Y;
        //    setControls(newx, newy, this);
        //    //this.Text = this.Width.ToString() + " " + this.Height.ToString();

        //}
        private void Bind()
        {
            this.bindingSourceCity.DataSource = CityManager.GetCity();
            city = this.listBoxControlCity.SelectedValue as City;
            district = this.listBoxControlDistrict.SelectedValue as District;
            if (jjyy == 0)
            {
                shreet = this.listBoxControlStreet.SelectedValue as Shreet;
            }
            jjyy = 0;
            //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrict();
            //this.bindingSourceStreet.DataSource = ShreetManager.GetShreet();
            this.textEditCity.Enabled = false;
            this.textEditDistrict.Enabled = false;
            this.textEditStreet.Enabled = false;

            this.simpleButtonAdd.Enabled = true;
            this.simpleButtonDelete.Enabled = true;
            this.simpleButtonUpdate.Enabled = true;
            this.simpleButtonSave.Enabled = false;
        }

        private void bindingSourceCity_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void bindingSourceDistrict_CurrentChanged(object sender, EventArgs e)
        {
           
        }

        private void bindingSourceStreet_CurrentChanged(object sender, EventArgs e)
        {
            
        }
        int flog = 0;
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            flog = 1;
            this.textEditStreet.Text = "";
            this.textEditCity.Enabled = true;
            this.textEditDistrict.Enabled = true;
            this.textEditStreet.Enabled = true;

            this.simpleButtonAdd.Enabled = false;
            this.simpleButtonDelete.Enabled = false;
            this.simpleButtonUpdate.Enabled = false;
            this.simpleButtonSave.Enabled = true;
        }

        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                shreet = this.listBoxControlStreet.SelectedValue as Shreet;
                if (shreet != null)
                {
                    ShreetManager.Delete(shreet.ShreetId);
                    if (district != null)
                    {
                        //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(district.DistrictId);
                        IList<Shreet> list = null;
                        DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(district.DistrictId);
                        if (table1 != null)
                        {
                            if (table1.Rows.Count > 0)
                            {
                                list = new List<Shreet>();
                                foreach (DataRow row in table1.Rows)
                                {
                                    Shreet shreet1 = new Shreet();
                                    shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                    shreet1.ShreetName = row["shreetName"].ToString();
                                    shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                    list.Add(shreet1);
                                }
                            }
                        }
                        this.bindingSourceStreet.DataSource = list;
                    }
                    shreet = this.listBoxControlStreet.SelectedValue as Shreet;
                    this.textEditStreet.Text = "";
                }
                else
                {
                    if (district != null)
                    {
                        district = this.listBoxControlDistrict.SelectedValue as District;
                        DistrictManager.Delete(district.DistrictId);
                        if (city != null)
                        {
                            //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrictByCityID(city.CityId);
                            IList<District> list1 = null;
                            DataTable table = DistrictManager.GetDistrictByCityIDTwo(city.CityId);
                            if (table != null)
                            {
                                if (table.Rows.Count > 0)
                                {
                                    list1 = new List<District>();
                                    foreach (DataRow row in table.Rows)
                                    {
                                        District district1 = new District();
                                        district1.DistrictId = int.Parse(row["districtId"].ToString());
                                        district1.DistrictName = row["districtName"].ToString();
                                        district1.CityId = int.Parse(row["cityId"].ToString());
                                        list1.Add(district1);
                                    }
                                }
                            }
                            this.bindingSourceDistrict.DataSource = list1;
                        }
                        district = this.listBoxControlDistrict.SelectedValue as District;
                        this.textEditStreet.Text = "";
                        this.textEditDistrict.Text = "";
                    }
                    else
                    {
                        if (city != null)
                        {
                            bool i = CityManager.Delete(city.CityId);
                            this.bindingSourceCity.DataSource = CityManager.GetCity();
                        }
                        city = this.listBoxControlCity.SelectedValue as City;
                        this.textEditCity.Text = "";
                        this.textEditDistrict.Text = "";
                        this.textEditStreet.Text = "";
                    }
                }
            }
            Bind();
        }

        private void simpleButtonUpdate_Click(object sender, EventArgs e)
        {
            if (this.textEditStreet.Text == "")
            {
                MessageBox.Show("資料為空,請選擇！");
                return;
            }
            flog = 2;
            this.textEditCity.Enabled = true;
            this.textEditDistrict.Enabled = true;
            this.textEditStreet.Enabled = true;

            this.simpleButtonAdd.Enabled = false;
            this.simpleButtonDelete.Enabled = false;
            this.simpleButtonUpdate.Enabled = false;
            this.simpleButtonSave.Enabled = true;
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (this.textEditCity.Text == "")
            {
                MessageBox.Show("資料不全,無法儲存!");
                return;
            }
            if (this.textEditDistrict.Text == "")
            {
                MessageBox.Show("資料不全,無法儲存!");
                return;
            }
            if (this.textEditStreet.Text == "")
            {
                MessageBox.Show("資料不全,無法儲存!");
                return;
            }
            if (city == null)
            {
                city = new City();
            }
            if (district == null)
            {
                district = new District();
            }
            if (shreet == null)
            {
                shreet = new Shreet();
            }
            city.CityName = this.textEditCity.Text;
            district.DistrictName = this.textEditDistrict.Text;
            shreet.ShreetName = this.textEditStreet.Text;
            if (flog == 1)
            {
                if (CityManager.GetCityByName(city.CityName) == null)
                {
                    bool i = CityManager.Add(city);
                    if (i == true)
                    {
                        district.CityId = CityManager.GetCityByName(city.CityName)[0].CityId;
                        bool j = DistrictManager.Add(district);
                        if (j == true)
                        {
                            shreet.DistrictId = DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].DistrictId;
                            bool k = ShreetManager.Add(shreet);
                            if (k == true)
                            {
                                MessageBox.Show("添加成功!");
                                //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrictByCityID(district.CityId);
                                //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);
                                IList<District> list1 = null;
                                DataTable table = DistrictManager.GetDistrictByCityIDTwo(district.CityId);
                                if (table != null)
                                {
                                    if (table.Rows.Count > 0)
                                    {
                                        list1 = new List<District>();
                                        foreach (DataRow row in table.Rows)
                                        {
                                            District district1 = new District();
                                            district1.DistrictId = int.Parse(row["districtId"].ToString());
                                            district1.DistrictName = row["districtName"].ToString();
                                            district1.CityId = int.Parse(row["cityId"].ToString());
                                            list1.Add(district1);
                                        }
                                    }
                                }
                                this.bindingSourceDistrict.DataSource = list1;

                                IList<Shreet> list = null;
                                DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                                if (table1 != null)
                                {
                                    if (table1.Rows.Count > 0)
                                    {
                                        list = new List<Shreet>();
                                        foreach (DataRow row in table1.Rows)
                                        {
                                            Shreet shreet1 = new Shreet();
                                            shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                            shreet1.ShreetName = row["shreetName"].ToString();
                                            shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                            list.Add(shreet1);
                                        }
                                    }
                                }
                                this.bindingSourceStreet.DataSource = list;
                            }
                        }
                    }
                }
                else
                {
                    if (DistrictManager.GetDistrictByName(district.DistrictName) == null)
                    {
                        district.CityId = CityManager.GetCityByName(city.CityName)[0].CityId;
                        bool i = DistrictManager.Add(district);
                        if (i == true)
                        {
                            shreet.DistrictId = DistrictManager.GetDistrictByName(district.DistrictName)[0].DistrictId;
                            bool k = ShreetManager.Add(shreet);
                            if (k == true)
                            {
                                MessageBox.Show("添加成功!");
                                //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrictByCityID(district.CityId);
                                //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);
                                IList<District> list1 = null;
                                DataTable table = DistrictManager.GetDistrictByCityIDTwo(district.CityId);
                                if (table != null)
                                {
                                    if (table.Rows.Count > 0)
                                    {
                                        list1 = new List<District>();
                                        foreach (DataRow row in table.Rows)
                                        {
                                            District district1 = new District();
                                            district1.DistrictId = int.Parse(row["districtId"].ToString());
                                            district1.DistrictName = row["districtName"].ToString();
                                            district1.CityId = int.Parse(row["cityId"].ToString());
                                            list1.Add(district1);
                                        }
                                    }
                                }
                                this.bindingSourceDistrict.DataSource = list1;

                                IList<Shreet> list = null;
                                DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                                if (table1 != null)
                                {
                                    if (table1.Rows.Count > 0)
                                    {
                                        list = new List<Shreet>();
                                        foreach (DataRow row in table1.Rows)
                                        {
                                            Shreet shreet1 = new Shreet();
                                            shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                            shreet1.ShreetName = row["shreetName"].ToString();
                                            shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                            list.Add(shreet1);
                                        }
                                    }
                                }
                                this.bindingSourceStreet.DataSource = list;
                            }
                        }
                    }
                    else
                    {
                        if (DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId) == null)
                        {
                            district.CityId = CityManager.GetCityByName(city.CityName)[0].CityId;
                            bool i = DistrictManager.Add(district);
                            if (i == true)
                            {
                                shreet.DistrictId = DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].DistrictId;
                                bool k = ShreetManager.Add(shreet);
                                if (k == true)
                                {
                                    MessageBox.Show("添加成功!");
                                    //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrictByCityID(district.CityId);
                                    //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);
                                    IList<District> list1 = null;
                                    DataTable table = DistrictManager.GetDistrictByCityIDTwo(district.CityId);
                                    if (table != null)
                                    {
                                        if (table.Rows.Count > 0)
                                        {
                                            list1 = new List<District>();
                                            foreach (DataRow row in table.Rows)
                                            {
                                                District district1 = new District();
                                                district1.DistrictId = int.Parse(row["districtId"].ToString());
                                                district1.DistrictName = row["districtName"].ToString();
                                                district1.CityId = int.Parse(row["cityId"].ToString());
                                                list1.Add(district1);
                                            }
                                        }
                                    }
                                    this.bindingSourceDistrict.DataSource = list1;

                                    IList<Shreet> list = null;
                                    DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                                    if (table1 != null)
                                    {
                                        if (table1.Rows.Count > 0)
                                        {
                                            list = new List<Shreet>();
                                            foreach (DataRow row in table1.Rows)
                                            {
                                                Shreet shreet1 = new Shreet();
                                                shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                                shreet1.ShreetName = row["shreetName"].ToString();
                                                shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                                list.Add(shreet1);
                                            }
                                        }
                                    }
                                    this.bindingSourceStreet.DataSource = list;
                                }
                            }
                        }
                        else
                        {
                            if (DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].CityId != CityManager.GetCityByName(city.CityName)[0].CityId)
                            {
                                district.CityId = CityManager.GetCityByName(city.CityName)[0].CityId;
                                bool i = DistrictManager.Add(district);
                                if (i == true)
                                {
                                    shreet.DistrictId = DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].DistrictId;
                                    bool k = ShreetManager.Add(shreet);
                                    if (k == true)
                                    {
                                        MessageBox.Show("添加成功!");
                                        //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrictByCityID(district.CityId);
                                        //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);
                                        IList<District> list1 = null;
                                        DataTable table = DistrictManager.GetDistrictByCityIDTwo(district.CityId);
                                        if (table != null)
                                        {
                                            if (table.Rows.Count > 0)
                                            {
                                                list1 = new List<District>();
                                                foreach (DataRow row in table.Rows)
                                                {
                                                    District district1 = new District();
                                                    district1.DistrictId = int.Parse(row["districtId"].ToString());
                                                    district1.DistrictName = row["districtName"].ToString();
                                                    district1.CityId = int.Parse(row["cityId"].ToString());
                                                    list1.Add(district1);
                                                }
                                            }
                                        }
                                        this.bindingSourceDistrict.DataSource = list1;

                                        IList<Shreet> list = null;
                                        DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                                        if (table1 != null)
                                        {
                                            if (table1.Rows.Count > 0)
                                            {
                                                list = new List<Shreet>();
                                                foreach (DataRow row in table1.Rows)
                                                {
                                                    Shreet shreet1 = new Shreet();
                                                    shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                                    shreet1.ShreetName = row["shreetName"].ToString();
                                                    shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                                    list.Add(shreet1);
                                                }
                                            }
                                        }
                                        this.bindingSourceStreet.DataSource = list;
                                    }
                                }
                            }
                            else
                            {
                                if (ShreetManager.GetShreetByName(shreet.ShreetName) == null)
                                {
                                    shreet.DistrictId = DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].DistrictId;

                                    bool i = ShreetManager.Add(shreet);
                                    if (i == true)
                                    {
                                        MessageBox.Show("添加成功!");
                                        //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);

                                        IList<Shreet> list = null;
                                        DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                                        if (table1 != null)
                                        {
                                            if (table1.Rows.Count > 0)
                                            {
                                                list = new List<Shreet>();
                                                foreach (DataRow row in table1.Rows)
                                                {
                                                    Shreet shreet1 = new Shreet();
                                                    shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                                    shreet1.ShreetName = row["shreetName"].ToString();
                                                    shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                                    list.Add(shreet1);
                                                }
                                            }
                                        }
                                        this.bindingSourceStreet.DataSource = list;
                                    }
                                }
                                else
                                {
                                    int iCount = 0;
                                    foreach (Shreet ss in ShreetManager.GetShreetByName(shreet.ShreetName))
                                    {
                                        if (ss.DistrictId == DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].DistrictId)
                                        {
                                            iCount = 1;
                                        }
                                    }
                                    if (iCount == 0)
                                    {
                                        shreet.DistrictId = DistrictManager.GetDistrictByNameAndCityId(district.DistrictName, district.CityId)[0].DistrictId;
                                        bool i = ShreetManager.Add(shreet);
                                        if (i == true)
                                        {
                                            MessageBox.Show("添加成功!");
                                            //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);

                                            IList<Shreet> list = null;
                                            DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                                            if (table1 != null)
                                            {
                                                if (table1.Rows.Count > 0)
                                                {
                                                    list = new List<Shreet>();
                                                    foreach (DataRow row in table1.Rows)
                                                    {
                                                        Shreet shreet1 = new Shreet();
                                                        shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                                                        shreet1.ShreetName = row["shreetName"].ToString();
                                                        shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                                                        list.Add(shreet1);
                                                    }
                                                }
                                            }
                                            this.bindingSourceStreet.DataSource = list;
                                        }
                                    }
                                    else
                                    {
                                        iCount = 0;
                                        jjyy = 1;
                                        MessageBox.Show("資料重複,無法新增此筆資料!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (flog == 2)
            {
                ShreetManager.Update(shreet);
                if (DistrictManager.GetDistrictByName(district.DistrictName) == null)
                {
                    DistrictManager.Update(district);
                }
                else
                { 
                    int iCount = 0;
                    foreach(District d in DistrictManager.GetDistrictByName(district.DistrictName))
                    {
                        if (d.CityId == city.CityId)
                        {
                            iCount = 1;
                        }
                    }
                    if (iCount == 0)
                    {
                        DistrictManager.Update(district);
                    }
                    iCount = 0;
                }
                if (CityManager.GetCityByName(city.CityName) == null)
                {
                    CityManager.Update(city);
                }

                MessageBox.Show("修改成功!");
                //this.bindingSourceDistrict.DataSource = DistrictManager.GetDistrictByCityID(district.CityId);
                //this.bindingSourceStreet.DataSource = ShreetManager.GetShreetByDistrictId(shreet.DistrictId);
                IList<District> list1 = null;
                DataTable table = DistrictManager.GetDistrictByCityIDTwo(district.CityId);
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        list1 = new List<District>();
                        foreach (DataRow row in table.Rows)
                        {
                            District district1 = new District();
                            district1.DistrictId = int.Parse(row["districtId"].ToString());
                            district1.DistrictName = row["districtName"].ToString();
                            district1.CityId = int.Parse(row["cityId"].ToString());
                            list1.Add(district1);
                        }
                    }
                }
                this.bindingSourceDistrict.DataSource = list1;

                IList<Shreet> list = null;
                DataTable table1 = ShreetManager.GetShreetByDistrictIdTwo(shreet.DistrictId);
                if (table1 != null)
                {
                    if (table1.Rows.Count > 0)
                    {
                        list = new List<Shreet>();
                        foreach (DataRow row in table1.Rows)
                        {
                            Shreet shreet1 = new Shreet();
                            shreet1.ShreetId = int.Parse(row["shreetId"].ToString());
                            shreet1.ShreetName = row["shreetName"].ToString();
                            shreet1.DistrictId = int.Parse(row["districtId"].ToString());
                            list.Add(shreet1);
                        }
                    }
                }
                this.bindingSourceStreet.DataSource = list;
            }
            flog = 0;
            Bind();
        }

        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = "";
        }

        private void listBoxControlStreet_DoubleClick(object sender, EventArgs e)
        {
            shreet = this.listBoxControlStreet.SelectedValue as Shreet;
            if (shreet != null)
            {
                this.textEditStreet.Text = shreet.ShreetName;
                IList<District> dListDistrict = DistrictManager.GetDistrictByID(shreet.DistrictId);
                if (dListDistrict!= null)
                {
                    this.textEditDistrict.Text = dListDistrict[0].DistrictName;
                    if (dListDistrict[0].City != null)
                    {
                        this.textEditCity.Text = dListDistrict[0].City.CityName;
                        this.textEditAddress.Text = dListDistrict[0].City.CityName + dListDistrict[0].DistrictName + shreet.ShreetName;
                    }
                    else
                    {
                        this.textEditAddress.Text = dListDistrict[0].DistrictName + shreet.ShreetName;
                    }
                }
                else
                {
                    this.textEditAddress.Text = shreet.ShreetName;
                }
            }
            else
            {
                this.textEditStreet.Text = "";
                this.textEditAddress.Text = "";
            }
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            //if (i == 1)
           // {
              Settings.Options.CompanyInfo._address = this.textEditAddress.Text;
           // }
            //if (i == 2)
            //{
            //    UI.CustomerManage._address = this.textEditAddress.Text;
            //}
            this.DialogResult = DialogResult.OK;
        }

        private void buttonRd_Click(object sender, EventArgs e)
        {
            IList<Shreet> list = null;
            DataTable table = ShreetManager.GetShreetByKeyNameTooo(this.buttonRd.Text);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        list.Add(shreet);
                    }
                }
            }
            this.bindingSourceStreet.DataSource = list;
        }

        private void buttonStreet_Click(object sender, EventArgs e)
        {
            IList<Shreet> list = null;
            DataTable table = ShreetManager.GetShreetByKeyNameTooo(this.buttonStreet.Text);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        list.Add(shreet);
                    }
                }
            }
            this.bindingSourceStreet.DataSource = list;
        }

        private void buttonLane_Click(object sender, EventArgs e)
        {
            IList<Shreet> list = null;
            DataTable table = ShreetManager.GetShreetByKeyNameTooo(this.buttonLane.Text);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        list.Add(shreet);
                    }
                }
            }
            this.bindingSourceStreet.DataSource = list;
        }

        private void buttonSection_Click(object sender, EventArgs e)
        {
            IList<Shreet> list = null;
            DataTable table = ShreetManager.GetShreetByKeyNameTooo(this.buttonSection.Text);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        list.Add(shreet);
                    }
                }
            }
            this.bindingSourceStreet.DataSource = list;
        }

        private void buttonVillage_Click(object sender, EventArgs e)
        {
            IList<Shreet> list = null;
            DataTable table = ShreetManager.GetShreetByKeyNameTooo(this.buttonVillage.Text);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<Shreet>();
                    foreach (DataRow row in table.Rows)
                    {
                        Shreet shreet = new Shreet();
                        shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                        shreet.ShreetName = row["shreetName"].ToString();
                        shreet.DistrictId = int.Parse(row["districtId"].ToString());
                        list.Add(shreet);
                    }
                }
            }
            this.bindingSourceStreet.DataSource = list;
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            this.bindingSourceStreet.DataSource =  ShreetManager.GetShreetTx();;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button1.Text;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button21.Text;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button20.Text;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button19.Text;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button18.Text;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button17.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button5.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button4.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button2.Text;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button36.Text;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button37.Text;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button38.Text;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button39.Text;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button35.Text;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button34.Text;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button33.Text;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button32.Text;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button25.Text;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button24.Text;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button23.Text;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button22.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button11.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button12.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button13.Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button14.Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button15.Text;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button16.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button10.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button9.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button8.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button7.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button6.Text;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button26.Text;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button27.Text;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button28.Text;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button29.Text;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button30.Text;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            this.textEditAddress.Text = this.textEditAddress.Text + this.button31.Text;
        }

        private void listBoxControlCity_Click(object sender, EventArgs e)
        {
            city = this.listBoxControlCity.SelectedValue as City;
            if (city != null)
            {
                this.textEditCity.Text = city.CityName;
                this.textEditDistrict.Text = "";
                this.textEditStreet.Text = "";
                IList<District> list = null;
                DataTable table = DistrictManager.GetDistrictByCityIDTwo(city.CityId);
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        list = new List<District>();
                        foreach (DataRow row in table.Rows)
                        {
                            District district = new District();
                            district.DistrictId = int.Parse(row["districtId"].ToString());
                            district.DistrictName = row["districtName"].ToString();
                            district.CityId = int.Parse(row["cityId"].ToString());
                            list.Add(district);
                        }
                    }
                }
                this.bindingSourceDistrict.DataSource = list;
                this.bindingSourceStreet.DataSource = null;
            }
            else
            {
                this.textEditDistrict.Text = "";
                this.textEditStreet.Text = "";
                this.textEditCity.Text = "";
            }
        }

        private void listBoxControlDistrict_Click(object sender, EventArgs e)
        {
            district = this.listBoxControlDistrict.SelectedValue as District;
            if (district != null)
            {
                this.textEditDistrict.Text = district.DistrictName;
                this.textEditStreet.Text = "";
                
                IList<Shreet> list = null;
                DataTable table = ShreetManager.GetShreetByDistrictIdTwo(district.DistrictId);
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        list = new List<Shreet>();
                        foreach (DataRow row in table.Rows)
                        {
                            Shreet shreet = new Shreet();
                            shreet.ShreetId = int.Parse(row["shreetId"].ToString());
                            shreet.ShreetName = row["shreetName"].ToString();
                            shreet.DistrictId = int.Parse(row["districtId"].ToString());
                            list.Add(shreet);
                        }
                    }
                }
                this.bindingSourceStreet.DataSource =list;
            }
            else
            {
                this.textEditStreet.Text = "";
                this.textEditDistrict.Text = "";
            }
        }

        private void listBoxControlStreet_Click(object sender, EventArgs e)
        {
            shreet = this.listBoxControlStreet.SelectedValue as Shreet;
            if (shreet != null)
            {
                this.textEditStreet.Text = shreet.ShreetName;
                IList<District> dList = DistrictManager.GetDistrictByID(shreet.DistrictId);
                if (dList != null)
                {
                    this.textEditDistrict.Text = dList[0].DistrictName;
                }
                this.bindingSourceDistrict.DataSource =dList;
            }
            else
            {
                this.textEditStreet.Text = "";
            }
        }

        private void RegionalAddressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Options.CompanyInfo._address = this.textEditAddress.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
