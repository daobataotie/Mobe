using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Employees
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：FamilyMemberEditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-22
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class FamilyMemberEditForm : DevExpress.XtraEditors.XtraForm
    {
        private Model.FamilyMembers _familyMember;

        public Model.FamilyMembers FamilyMember
        {
            get { return _familyMember; }
            set { _familyMember = value; }
        }
        private string action = "insert";
        public FamilyMemberEditForm()
        {
            InitializeComponent();
        }

        public FamilyMemberEditForm(Model.FamilyMembers member)
            : this()
        {
            this._familyMember = member;
            this.action = "update";


            this.textEditFamilyMembersName.Text = member.FamilyMembersName;
            this.textEditPersonId.Text = member.PersonId;
            this.textEditRelation.Text = member.Relation;
            this.dateEditBirthday.EditValue = member.Birthday;
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            if (this.action == "insert") 
            {
                _familyMember = new Book.Model.FamilyMembers();
            }
            this._familyMember.FamilyMembersId = Guid.NewGuid().ToString();
            this._familyMember.Birthday = this.dateEditBirthday.DateTime;
            this._familyMember.FamilyMembersName = this.textEditFamilyMembersName.Text;
            this._familyMember.PersonId = this.textEditPersonId.Text;
            this._familyMember.Relation = this.textEditRelation.Text;
            this.DialogResult = DialogResult.OK;
        }

        
    }
}