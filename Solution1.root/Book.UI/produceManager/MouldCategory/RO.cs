using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.ProductMouldTest _ProductMouldTest)
        {
            InitializeComponent();
            if (_ProductMouldTest == null)
                return;

            //人员绑定
            this.LbEmployeeId.Text = _ProductMouldTest.Employee == null ? null : _ProductMouldTest.Employee.ToString();
            this.LbInjectPerson.Text = _ProductMouldTest.Emp_Employee == null ? null : _ProductMouldTest.Emp_Employee.ToString();
            this.LbMouldStrengthenPerson.Text = _ProductMouldTest.Emp_Employee2 == null ? null : _ProductMouldTest.Emp_Employee2.ToString();
            this.LbFourWayPerson.Text = _ProductMouldTest.Emp_Employee3 == null ? null : _ProductMouldTest.Emp_Employee3.ToString();
            this.LbImpactPerson.Text = _ProductMouldTest.Emp_Employee4 == null ? null : _ProductMouldTest.Emp_Employee4.ToString();
            this.LbContentPerson.Text = _ProductMouldTest.Emp_Employee5 == null ? null : _ProductMouldTest.Emp_Employee5.ToString();
            this.LbDevelopRemarksPerson.Text = _ProductMouldTest.Emp_Employee6 == null ? null : _ProductMouldTest.Emp_Employee6.ToString();
            this.LbManagerOne.Text = _ProductMouldTest.Emp_Employee7 == null ? null : _ProductMouldTest.Emp_Employee7.ToString();
            this.LbManagerTwo.Text = _ProductMouldTest.Emp_Employee8 == null ? null : _ProductMouldTest.Emp_Employee8.ToString();

            //试模
            this.LbMouldId.Text = _ProductMouldTest.Mould == null ? null : _ProductMouldTest.Mould.ToString();
            this.LbProductMouldId.Text = _ProductMouldTest.MouldId == null ? null : _ProductMouldTest.MouldId.ToString();
            this.LbInFactoryDate.Text = _ProductMouldTest.InFactoryDate == null ? null : _ProductMouldTest.InFactoryDate.Value.ToShortDateString();
            this.LbProductMouldTestDate.Text = _ProductMouldTest.ProductMouldTestDate == null ? null : _ProductMouldTest.ProductMouldTestDate.Value.ToShortDateString();
            this.LbOutFactoryDate.Text = _ProductMouldTest.OutFactoryDate == null ? null : _ProductMouldTest.OutFactoryDate.Value.ToShortDateString();
            this.LbSupplierId.Text = _ProductMouldTest.Supplier == null ? null : _ProductMouldTest.Supplier.ToString();
            this.LbProductMaterialId.Text = _ProductMouldTest.ProductMaterial == null ? null : _ProductMouldTest.ProductMaterial.ToString();
            this.LbProductCategoryName.Text = _ProductMouldTest.ProductCategoryName == null ? null : _ProductMouldTest.ProductCategoryName.ToString();
            this.LbMouldSize.Text = _ProductMouldTest.MouldSize == null ? null : _ProductMouldTest.MouldSize.ToString();
            this.LbMouldWeight.Text = _ProductMouldTest.MouldWeight == null ? null : _ProductMouldTest.MouldWeight.ToString() + " ·g";
            this.LbMouldMount.Text = _ProductMouldTest.MouldMount == null ? null : _ProductMouldTest.MouldMount.ToString();
            this.LbPronoteMachineId.Text = _ProductMouldTest.PronoteMachine == null ? null : _ProductMouldTest.PronoteMachine.ToString() + "  號機";
            //生产部、品管科
            this.LbMouldInjectionDate.Text = _ProductMouldTest.MouldInjectionDate == null ? null : _ProductMouldTest.MouldInjectionDate.Value.ToShortDateString();
            this.LbMouldStrengthenDate.Text = _ProductMouldTest.MouldStrengthenDate == null ? null : _ProductMouldTest.MouldStrengthenDate.Value.ToShortDateString();
            this.LbFourWayDate.Text = _ProductMouldTest.MouldStrengthenDate == null ? null : _ProductMouldTest.MouldStrengthenDate.Value.ToShortDateString();
            this.LbImpactDate.Text = _ProductMouldTest.ImpactDate == null ? null : _ProductMouldTest.ImpactDate.Value.ToShortDateString();
            this.LbDevelopRemarksDate.Text = _ProductMouldTest.DevelopRemarksDate == null ? null : _ProductMouldTest.DevelopRemarksDate.Value.ToShortDateString();


            //备注
            this.lblInjectionNote.Text = _ProductMouldTest.InjectionNote;
            this.lblStrengThenNote.Text = _ProductMouldTest.StrengThenNote;
            this.lblQualityControlNote.Text = _ProductMouldTest.QualityControlNote;
            
            if (_ProductMouldTest.InstallBool == true)
            {
                this.CheckInstallBool.Checked = true;
            }
            else 
            {
                this.CheckInstallBoolTwo.Checked = true;
            }
            if (_ProductMouldTest.MouldInjectionType.Value == true)
                this.ShapeMouldInjectionType.FillColor = Color.Black;
            if (_ProductMouldTest.MouldStrengthenType.Value == true)
                this.ShapeMouldStrengthenType.FillColor = Color.Black;
            if (_ProductMouldTest.InstallType != null)
            {
                switch (_ProductMouldTest.InstallType)
                {
                    case "AN":
                        this.ShapeInstallOne.FillColor = Color.Black;
                        break;
                    case "ANSI":
                        this.ShapeInstallTwo.FillColor = Color.Black;
                        break;
                    case "JIS":
                        this.ShapeInstallThree.FillColor = Color.Black;
                        break;
                    case "CE":
                        this.ShapeInstallFour.FillColor = Color.Black;
                        break;
                    case "CSA":
                        this.ShapeInstallFive.FillColor = Color.Black;
                        break;
                }
            }
            if (_ProductMouldTest.Content == true)
            {
                this.CheckContentType.Checked = true;
            }
            else 
            {
                this.CheckContentTypeTwo.Checked = true;
            }

            if (!string.IsNullOrEmpty(_ProductMouldTest.ContentDetail)) 
            {
                string[] ckg = new string[3];
                ckg = _ProductMouldTest.ContentDetail.Split('*');
                this.LbContentDetail.Text=ckg[0].ToString()+"*"+ckg[1].ToString();
                this.LbContentDetailTwo.Text = ckg[2].ToString() + "mm";
            }

            if (_ProductMouldTest.ContentType.Value==true)
            {
                this.CheckContentBool.Checked = true;
            }
            else 
            {
                this.CheckContentBoolTwo.Checked = true;
            }

            this.LbContentDate.Text = _ProductMouldTest.ContentDate == null ? null : _ProductMouldTest.ContentDate.Value.ToShortDateString();
            this.LbDevelopRemarks.Text = _ProductMouldTest.DevelopRemarks == null ? null : _ProductMouldTest.DevelopRemarks.ToString();

            if (_ProductMouldTest.ClearType.Value==true)
                this.ShapeClearType.FillColor = Color.Black;
            if (_ProductMouldTest.ClearBool == true)
            {
                this.CheckClearBool.Checked = true;
            }
            else 
            {
                this.CheckClearBoolTwo.Checked=true;
            }

            if (_ProductMouldTest.OpticsType.Value == true)
                this.ShapeOpticsType.FillColor = Color.Black;
            if (_ProductMouldTest.OpticsBool == true)
            {
                this.CheckOpticsBool.Checked = true;
            }
            else 
            {
                this.CheckOpticsBoolTwo.Checked = true;
            }

            if (_ProductMouldTest.LaserType.Value == true)
                this.ShapeLaserType.FillColor = Color.Black;
            if (_ProductMouldTest.LaserBool == true)
            {
                this.CheckLaserBool.Checked = true;
            }
            else 
            {
                this.CheckLaserBoolTwo.Checked = true;
            }

            if (_ProductMouldTest.ImpaceType.Value == true)
                this.ShapeImpactType.FillColor = Color.Black;
            if (_ProductMouldTest.ImpactBool == true)
            {
                this.CheckImpactBool.Checked = true;
            }
            else 
            {
                this.CheckImpactBoolTwo.Checked = true;
            }
        }              
    }
}
