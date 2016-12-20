using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace Book.UI.Accounting.CurrencyCategory
{
    public partial class EditForm : BaseEditForm
    {
        Model.CurrencyCategory CurrencyCategory;
        BL.CurrencyCategoryManager CurrencyCategoryManager = new Book.BL.CurrencyCategoryManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.CurrencyCategory.PRO_AtCurrencyCategoryId, new AA("請選擇貨幣種類。", this.newChooseContorlAtCurrencyCategoryId));
            this.newChooseContorlAtCurrencyCategoryId.Choose = new Accounting.CurrencyCategory.ChooseAtCurrencyCategory();
            this.action = "insert";
        }
        #region Override
        protected override void AddNew()
        {
            this.CurrencyCategory = new Model.CurrencyCategory();
            IList<Model.CurrencyCategory> cc = CurrencyCategoryManager.SelectByEffectDate();
            if (cc != null)
            {
                if (cc.Count > 0)
                {
                    this.CurrencyCategory.ExchangeFloat = cc[0].ExchangeFloat;
                }
            }
        }
        protected override void Save()
        {
            this.CurrencyCategory.AtCurrencyCategory = this.newChooseContorlAtCurrencyCategoryId.EditValue as Model.AtCurrencyCategory;
            if (this.CurrencyCategory.AtCurrencyCategory != null)
            {
                this.CurrencyCategory.AtCurrencyCategoryId = this.CurrencyCategory.AtCurrencyCategory.AtCurrencyCategoryId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEffectDate.DateTime, new DateTime()))
            {
                this.CurrencyCategory.EffectDate = null;
            }
            else
            {
                this.CurrencyCategory.EffectDate = this.dateEditEffectDate.DateTime;
            }
            this.CurrencyCategory.ExchangeRate=this.spinEditExchangeRate.EditValue==null?0:double.Parse(this.spinEditExchangeRate.EditValue.ToString());
            this.CurrencyCategory.ExchangeFloat = this.spinEditExchangeFloat.EditValue == null ? 0 :int.Parse(this.spinEditExchangeFloat.EditValue.ToString());

          

            if (this.radioGroupConvertModel.SelectedIndex==0)
            {
                this.CurrencyCategory.ConvertModel =0;
            }
            else
            {
                this.CurrencyCategory.ConvertModel =1;
            }
            if (this.radioGroupExchangeRateModel.SelectedIndex == 0)
            {
                this.CurrencyCategory.ExchangeRateModel = 0;
            }
            else
            {
                this.CurrencyCategory.ExchangeRateModel = 1;
            }
            this.CurrencyCategory.MaxError = this.spinEditMaxError.EditValue == null ? 0 : double.Parse(this.spinEditMaxError.EditValue.ToString());
            this.CurrencyCategory.AdjustExchangeRate = this.spinEditAdjustExchangeRate.EditValue == null ? 0 : double.Parse(this.spinEditAdjustExchangeRate.EditValue.ToString());
            switch (this.action)
            {
                case "insert":
                    this.CurrencyCategoryManager.Insert(this.CurrencyCategory);
                    break;
                case "update":
                    this.CurrencyCategoryManager.Update(this.CurrencyCategory);
                    break;
                default:
                    break;
            }   
        }
        public override void Refresh()
        {

            if (this.CurrencyCategory == null)
            {
                this.CurrencyCategory = new Book.Model.CurrencyCategory();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.CurrencyCategoryManager.Select();
            this.newChooseContorlAtCurrencyCategoryId.EditValue = this.CurrencyCategory.AtCurrencyCategory;
             this.spinEditAdjustExchangeRate.EditValue = this.CurrencyCategory.AdjustExchangeRate;
             this.spinEditExchangeFloat.EditValue = this.CurrencyCategory.ExchangeFloat;
             this.spinEditExchangeRate.EditValue = this.CurrencyCategory.ExchangeRate;
             this.spinEditMaxError.EditValue = this.CurrencyCategory.MaxError;
             if (global::Helper.DateTimeParse.DateTimeEquls(this.CurrencyCategory.EffectDate, global::Helper.DateTimeParse.NullDate))
             {
                 this.dateEditEffectDate.EditValue = null;
             }
             else
             {
                 this.dateEditEffectDate.EditValue = this.CurrencyCategory.EffectDate;
             }
             if (this.CurrencyCategory.ConvertModel == 0)
             {
                 this.radioGroupConvertModel.SelectedIndex = 0;
             }
             else
             {
                 this.radioGroupConvertModel.SelectedIndex = 1;
             }
             if (this.CurrencyCategory.ExchangeRateModel == 0)
             {
                 this.radioGroupExchangeRateModel.SelectedIndex = 0;
             }
             else
             {
                 this.radioGroupExchangeRateModel.SelectedIndex = 1;
             }
            switch (this.action)
            {
                case "insert":
                    this.newChooseContorlAtCurrencyCategoryId.ShowButton = true;
                    this.newChooseContorlAtCurrencyCategoryId.ButtonReadOnly = false;
                    this.spinEditAdjustExchangeRate.Properties.ReadOnly = false;
                    this.spinEditExchangeFloat.Properties.ReadOnly = false;
                    this.spinEditExchangeRate.Properties.ReadOnly = false;
                    this.spinEditMaxError.Properties.ReadOnly = false;
                    this.dateEditEffectDate.Properties.ReadOnly = false;
                    this.dateEditEffectDate.Properties.Buttons[0].Visible = true;
                    this.radioGroupConvertModel.Properties.ReadOnly = false;
                    this.radioGroupExchangeRateModel.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.newChooseContorlAtCurrencyCategoryId.ShowButton = true;
                    this.newChooseContorlAtCurrencyCategoryId.ButtonReadOnly = false;
                    this.spinEditAdjustExchangeRate.Properties.ReadOnly = false;
                    this.spinEditExchangeFloat.Properties.ReadOnly = false;
                    this.spinEditExchangeRate.Properties.ReadOnly = false;
                    this.spinEditMaxError.Properties.ReadOnly = false;
                     this.dateEditEffectDate.Properties.ReadOnly = false;
                    this.dateEditEffectDate.Properties.Buttons[0].Visible = true;
                     this.radioGroupConvertModel.Properties.ReadOnly = false;
                    this.radioGroupExchangeRateModel.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.newChooseContorlAtCurrencyCategoryId.ShowButton = false;
                    this.newChooseContorlAtCurrencyCategoryId.ButtonReadOnly = true;
                    this.spinEditAdjustExchangeRate.Properties.ReadOnly = true;
                    this.spinEditExchangeFloat.Properties.ReadOnly = true;
                    this.spinEditExchangeRate.Properties.ReadOnly = true;
                    this.spinEditMaxError.Properties.ReadOnly = true;
                     this.dateEditEffectDate.Properties.ReadOnly = true;
                    this.dateEditEffectDate.Properties.Buttons[0].Visible = false;
                     this.radioGroupConvertModel.Properties.ReadOnly = true;
                    this.radioGroupExchangeRateModel.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        /// <summary>
        /// 删除
        /// </summary>
        protected override void Delete()
        {
            if (this.CurrencyCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.CurrencyCategoryManager.Delete(this.CurrencyCategory.CurrencyCategoryId);
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.spinEditExchangeRate,this });
        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.CurrencyCategory productEpiboly = this.bindingSource1.Current as Model.CurrencyCategory;
                if (productEpiboly != null)
                {
                    this.CurrencyCategory = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        private void spinEditExchangeRate_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void spinEditAdjustExchangeRate_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void spinEditExchangeFloat_EditValueChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0.");
            for (decimal i = 0; i < this.spinEditExchangeFloat.Value; i++)
            {
                sb.Append("0");
            }
            this.spinEditAdjustExchangeRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditAdjustExchangeRate.Properties.EditFormat.FormatString = sb.ToString();

            this.spinEditAdjustExchangeRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditAdjustExchangeRate.Properties.DisplayFormat.FormatString = sb.ToString();

            this.spinEditExchangeRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditExchangeRate.Properties.EditFormat.FormatString = sb.ToString();

            this.spinEditExchangeRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditExchangeRate.Properties.DisplayFormat.FormatString = sb.ToString();
        }
    }
}