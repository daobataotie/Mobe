using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.Options
{
    public partial class SettingDataFormat : DevExpress.XtraEditors.XtraForm
    {
        Model.SetDataFormat _sdf;
        BL.SetDataFormatManager _sdfManager = new Book.BL.SetDataFormatManager();

        public SettingDataFormat()
        {
            InitializeComponent();
            _sdf = _sdfManager.GetFirst();
            if (_sdf == null)
            {
                _sdf = new Model.SetDataFormat();
                _sdf.SetDataFormatId = Guid.NewGuid().ToString();
            }
        }

        private void SettingDataFormat_Load(object sender, EventArgs e)
        {
            this.chkattrHasQian.Checked = _sdf.attrHasQian.HasValue ? _sdf.attrHasQian.Value : false;
            this.spCGDJXiao.EditValue = _sdf.CGDJXiao.HasValue ? _sdf.CGDJXiao.Value : 0;
            this.spCGDJZheng.EditValue = _sdf.CGDJZheng.HasValue ? _sdf.CGDJZheng.Value : 0;
            this.spCGJEXiao.EditValue = _sdf.CGJEXiao.HasValue ? _sdf.CGJEXiao.Value : 0;
            this.spCGJEZheng.EditValue = _sdf.CGJEZheng.HasValue ? _sdf.CGJEZheng.Value : 0;
            this.spCGSLXiao.EditValue = _sdf.CGSLXiao.HasValue ? _sdf.CGSLXiao.Value : 0;
            this.spCGSLZheng.EditValue = _sdf.CGSLZheng.HasValue ? _sdf.CGSLZheng.Value : 0;
            this.spCGZJXiao.EditValue = _sdf.CGZJXiao.HasValue ? _sdf.CGZJXiao.Value : 0;
            this.spCGZJZheng.EditValue = _sdf.CGZJZheng.HasValue ? _sdf.CGZJZheng.Value : 0;
            this.spKCDJXiao.EditValue = _sdf.KCDJXiao.HasValue ? _sdf.KCDJXiao.Value : 0;
            this.spKCDJZheng.EditValue = _sdf.KCDJZheng.HasValue ? _sdf.KCDJZheng.Value : 0;
            this.spKCJEXiao.EditValue = _sdf.KCJEXiao.HasValue ? _sdf.KCJEXiao.Value : 0;
            this.spKCJEZheng.EditValue = _sdf.KCJEZheng.HasValue ? _sdf.KCJEZheng.Value : 0;
            this.spKCSLXiao.EditValue = _sdf.KCSLXiao.HasValue ? _sdf.KCSLXiao.Value : 0;
            this.spKCSLZheng.EditValue = _sdf.KCSLZheng.HasValue ? _sdf.KCSLZheng.Value : 0;
            this.spKCZJXiao.EditValue = _sdf.KCZJXiao.HasValue ? _sdf.KCZJXiao.Value : 0;
            this.spKCZJZheng.EditValue = _sdf.KCZJZheng.HasValue ? _sdf.KCZJZheng.Value : 0;
            this.spKJJEXiao.EditValue = _sdf.KJJEXiao.HasValue ? _sdf.KJJEXiao.Value : 0;
            this.spKJJEZheng.EditValue = _sdf.KJJEZheng.HasValue ? _sdf.KJJEZheng.Value : 0;
            this.spKJZJXiao.EditValue = _sdf.KJZJXiao.HasValue ? _sdf.KJZJXiao.Value : 0;
            this.spKJZJZheng.EditValue = _sdf.KJZJZheng.HasValue ? _sdf.KJZJZheng.Value : 0;
            this.spXSDJXiao.EditValue = _sdf.XSDJXiao.HasValue ? _sdf.XSDJXiao.Value : 0;
            this.spXSDJZheng.EditValue = _sdf.XSDJZheng.HasValue ? _sdf.XSDJZheng.Value : 0;
            this.spXSJEXiao.EditValue = _sdf.XSJEXiao.HasValue ? _sdf.XSJEXiao.Value : 0;
            this.spXSJEZheng.EditValue = _sdf.XSJEZheng.HasValue ? _sdf.XSJEZheng.Value : 0;
            this.spXSSLXiao.EditValue = _sdf.XSSLXiao.HasValue ? _sdf.XSSLXiao.Value : 0;
            this.spXSSLZheng.EditValue = _sdf.XSSLZheng.HasValue ? _sdf.XSSLZheng.Value : 0;
            this.spXSZJXiao.EditValue = _sdf.XSZJXiao.HasValue ? _sdf.XSZJXiao.Value : 0;
            this.spXSZJZheng.EditValue = _sdf.XSZJZheng.HasValue ? _sdf.XSZJZheng.Value : 0;
        }

        private void barBtn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this._sdf.attrHasQian = this.chkattrHasQian.Checked;
            this._sdf.CGDJXiao = this.spCGDJXiao.EditValue == null ? 0 : int.Parse(this.spCGDJXiao.EditValue.ToString());
            this._sdf.CGDJZheng = this.spCGDJZheng.EditValue == null ? 0 : int.Parse(this.spCGDJZheng.EditValue.ToString());
            this._sdf.CGJEXiao = this.spCGJEXiao.EditValue == null ? 0 : int.Parse(this.spCGJEXiao.EditValue.ToString());
            this._sdf.CGJEZheng = this.spCGJEZheng.EditValue == null ? 0 : int.Parse(this.spCGJEZheng.EditValue.ToString());
            this._sdf.CGSLXiao = this.spCGSLXiao.EditValue == null ? 0 : int.Parse(this.spCGSLXiao.EditValue.ToString());
            this._sdf.CGSLZheng = this.spCGSLZheng.EditValue == null ? 0 : int.Parse(this.spCGSLZheng.EditValue.ToString());
            this._sdf.CGZJXiao = this.spCGZJXiao.EditValue == null ? 0 : int.Parse(this.spCGZJXiao.EditValue.ToString());
            this._sdf.CGZJZheng = this.spCGZJZheng.EditValue == null ? 0 : int.Parse(this.spCGZJZheng.EditValue.ToString());
            this._sdf.KCDJXiao = this.spKCDJXiao.EditValue == null ? 0 : int.Parse(this.spKCDJXiao.EditValue.ToString());
            this._sdf.KCDJZheng = this.spKCDJZheng.EditValue == null ? 0 : int.Parse(this.spKCDJZheng.EditValue.ToString());
            this._sdf.KCJEXiao = this.spKCJEXiao.EditValue == null ? 0 : int.Parse(this.spKCJEXiao.EditValue.ToString());
            this._sdf.KCJEZheng = this.spKCJEZheng.EditValue == null ? 0 : int.Parse(this.spKCJEZheng.EditValue.ToString());
            this._sdf.KCSLXiao = this.spKCSLXiao.EditValue == null ? 0 : int.Parse(this.spKCSLXiao.EditValue.ToString());
            this._sdf.KCSLZheng = this.spKCSLZheng.EditValue == null ? 0 : int.Parse(this.spKCSLZheng.EditValue.ToString());
            this._sdf.KCZJXiao = this.spKCZJXiao.EditValue == null ? 0 : int.Parse(this.spKCZJXiao.EditValue.ToString());
            this._sdf.KCZJZheng = this.spKCZJZheng.EditValue == null ? 0 : int.Parse(this.spKCZJZheng.EditValue.ToString());
            this._sdf.KJJEXiao = this.spKJJEXiao.EditValue == null ? 0 : int.Parse(this.spKJJEXiao.EditValue.ToString());
            this._sdf.KJJEZheng = this.spKJJEZheng.EditValue == null ? 0 : int.Parse(this.spKJJEZheng.EditValue.ToString());
            this._sdf.KJZJXiao = this.spKJZJXiao.EditValue == null ? 0 : int.Parse(this.spKJZJXiao.EditValue.ToString());
            this._sdf.KJZJZheng = this.spKJZJZheng.EditValue == null ? 0 : int.Parse(this.spKJZJZheng.EditValue.ToString());
            this._sdf.XSDJXiao = this.spKJZJZheng.EditValue == null ? 0 : int.Parse(this.spXSDJXiao.EditValue.ToString());
            this._sdf.XSDJZheng = this.spXSDJZheng.EditValue == null ? 0 : int.Parse(this.spXSDJZheng.EditValue.ToString());
            this._sdf.XSJEXiao = this.spXSJEXiao.EditValue == null ? 0 : int.Parse(this.spXSJEXiao.EditValue.ToString());
            this._sdf.XSJEZheng = this.spXSJEZheng.EditValue == null ? 0 : int.Parse(this.spXSJEZheng.EditValue.ToString());
            this._sdf.XSSLXiao = this.spXSSLXiao.EditValue == null ? 0 : int.Parse(this.spXSSLXiao.EditValue.ToString());
            this._sdf.XSSLZheng = this.spXSSLZheng.EditValue == null ? 0 : int.Parse(this.spXSSLZheng.EditValue.ToString());
            this._sdf.XSZJXiao = this.spXSZJXiao.EditValue == null ? 0 : int.Parse(this.spXSZJXiao.EditValue.ToString());
            this._sdf.XSZJZheng = this.spXSZJZheng.EditValue == null ? 0 : int.Parse(this.spXSZJZheng.EditValue.ToString());

            this._sdfManager.Insert(this._sdf);
            MessageBox.Show("存儲成功!");
        }
    }
}