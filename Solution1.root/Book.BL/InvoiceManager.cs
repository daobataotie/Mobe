using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Book.BL
{
    public abstract class InvoiceManager : BaseManager
    {
        private BL.RoleAuditingManager roleAuditingManager = new RoleAuditingManager();
        private int isUpdate = 0; //如果执行过修改 为1；
        // private static readonly DA.IInvoiceZSDetailAccessor invoiceZSDetailAccessor = (DA.IInvoiceZSDetailAccessor)Accessors.Get("InvoiceZSDetailAccessor");
        public string GetNewId(DateTime datetime)
        {
            string invoiceKind = this.GetInvoiceKind().ToLower();
            string rule = Settings.Get("InvoiceNumberRuleOf" + invoiceKind.ToUpper());
            if (string.IsNullOrEmpty(rule))
                return string.Empty;
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, datetime.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, datetime.Year, datetime.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, datetime.ToString("yyyy-MM-dd"));

            //SequenceManager.Increment(sequencekey_y);
            //SequenceManager.Increment(sequencekey_m);
            //SequenceManager.Increment(sequencekey_d);


            string sequencekey = invoiceKind;

            if (rule.IndexOf("{D2}") >= 0)
                sequencekey = sequencekey_d;
            else if (rule.IndexOf("{M2}") >= 0)
                sequencekey = sequencekey_m;
            else if (rule.IndexOf("{Y2}") >= 0 || rule.IndexOf("{Y4}") >= 0)
                sequencekey = sequencekey_y;
            else
                sequencekey = invoiceKind;





            int sequenceval = SequenceManager.GetCurrentVal(sequencekey);

            sequenceval++;

            string d2 = string.Format("{0:d2}", datetime.Day);
            string m2 = string.Format("{0:d2}", datetime.Month);
            string y2 = string.Format("{0:d2}", datetime.Year);
            string y4 = string.Format("{0:d4}", datetime.Year);
            string n4 = string.Format("{0:d4}", sequenceval);

            string n1 = string.Format("{0:d1}", sequenceval);
            string n2 = string.Format("{0:d2}", sequenceval);
            string n3 = string.Format("{0:d3}", sequenceval);
            string n5 = string.Format("{0:d5}", sequenceval);
            string n6 = string.Format("{0:d6}", sequenceval);
            string n7 = string.Format("{0:d7}", sequenceval);
            string n8 = string.Format("{0:d8}", sequenceval);
            string n9 = string.Format("{0:d9}", sequenceval);
            string n10 = string.Format("{0:d10}", sequenceval);

            //if(rule.IndexOf("{N}") >= 0 || rule.IndexOf("{N1}") >= 0 || rule.IndexOf("{N2}") >= 0 || rule.IndexOf("{N3}") >= 0 || rule.IndexOf("{N4}") >= 0 || rule.IndexOf("{N5}") >= 0 || rule.IndexOf("{N6}") >= 0 || rule.IndexOf("{N7}") >= 0 || rule.IndexOf("{N8}") >= 0 || rule.IndexOf("{N9}") >= 0 || rule.IndexOf("{N10}") >= 10 )
            return rule.Replace("{D2}", d2).Replace("{M2}", m2).Replace("{Y2}", y2).Replace("{Y4}", y4).Replace("{N}", n4).Replace("{N1}", n1).Replace("{N2}", n2).Replace("{N3}", n3).Replace("{N4}", n4).Replace("{N5}", n5).Replace("{N6}", n6).Replace("{N7}", n7).Replace("{N8}", n8).Replace("{N9}", n9).Replace("{N10}", n10);
            //else

        }

        public string GetNewId()
        {
            return this.GetNewId(DateTime.Now);
        }

        public static string GetEmployeeNewId()
        {
            DateTime datetime = DateTime.Now;
            string invoiceKind = "emp";
            string rule = null;
            rule = Settings.Get("EmployeeNumberRule");
            if (string.IsNullOrEmpty(rule))
                return string.Empty;
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, datetime.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, datetime.Year, datetime.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, datetime.ToString("yyyy-MM-dd"));

            string sequencekey = invoiceKind;

            if (rule.IndexOf("{D2}") >= 0)
                sequencekey = sequencekey_d;
            else if (rule.IndexOf("{M2}") >= 0)
                sequencekey = sequencekey_m;
            else if (rule.IndexOf("{Y2}") >= 0 || rule.IndexOf("{Y4}") >= 0)
                sequencekey = sequencekey_y;
            else
                sequencekey = invoiceKind;


            int sequenceval = SequenceManager.GetCurrentVal(sequencekey);

            sequenceval++;

            string d2 = string.Format("{0:d2}", datetime.Day);
            string m2 = string.Format("{0:d2}", datetime.Month);
            string y2 = string.Format("{0:d2}", datetime.Year);
            string y4 = string.Format("{0:d4}", datetime.Year);
            string n4 = string.Format("{0:d5}", sequenceval);

            string Id = rule.Replace("{D2}", d2).Replace("{M2}", m2).Replace("{Y2}", y2).Replace("{Y4}", y4).Replace("{N}", n4);

            return Id;

        }

        public static string GetNewId(DateTime datetime, Helper.CompanyKind companyKind)
        {
            string invoiceKind = companyKind.ToString();
            string rule = null;
            rule = Settings.Get("CompanyNumberRuleOf" + invoiceKind.ToUpper());
            if (string.IsNullOrEmpty(rule))
                return string.Empty;
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, datetime.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, datetime.Year, datetime.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, datetime.ToString("yyyy-MM-dd"));

            string sequencekey = invoiceKind;

            if (rule.IndexOf("{D2}") >= 0)
                sequencekey = sequencekey_d;
            else if (rule.IndexOf("{M2}") >= 0)
                sequencekey = sequencekey_m;
            else if (rule.IndexOf("{Y2}") >= 0 || rule.IndexOf("{Y4}") >= 0)
                sequencekey = sequencekey_y;
            else
                sequencekey = invoiceKind;


            int sequenceval = SequenceManager.GetCurrentVal(sequencekey);

            sequenceval++;

            string d2 = string.Format("{0:d2}", datetime.Day);
            string m2 = string.Format("{0:d2}", datetime.Month);
            string y2 = string.Format("{0:d2}", datetime.Year);
            string y4 = string.Format("{0:d4}", datetime.Year);
            string n4 = string.Format("{0:d5}", sequenceval);

            string Id = rule.Replace("{D2}", d2).Replace("{M2}", m2).Replace("{Y2}", y2).Replace("{Y4}", y4).Replace("{N}", n4);

            return Id;

        }
        private string _invoiceName;

        public string InvoiceName
        {
            get { return _invoiceName; }
            set { _invoiceName = value; }
        }
        public void Insert(Model.Invoice invoice)
        {



            _ValidateForInsert(invoice);
            //MethodInfo methodinfo = this.GetType().GetMethod("HasRows", new Type[] { typeof(string) });
            //bool f = (bool)methodinfo.Invoke(this, new object[] { invoice.InvoiceId });

            //if (f) 
            //{
            //    throw new Helper.InvalidValueException(Model.Invoice.PROPERTY_INVOICEID);
            //}

            try
            {
                V.BeginTransaction();
                invoice.InsertTime = invoice.InsertTime == null ? DateTime.Now : invoice.InsertTime;
                TiGuiExists(invoice);
                string tableName = this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Manager"));
                if (roleAuditingManager.IsNeedAuditByTableName(tableName))
                {

                    //  Model.RoleAuditing roleAuditing = this.roleAuditingManager.SelectByInvoiceIdAndTable(invoice.InvoiceId, tableName);
                    //if (   isUpdate == 0) //代表 添加
                    {

                        invoice.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;

                        Model.RoleAuditing roleAuditing = new Book.Model.RoleAuditing();
                        roleAuditing.RoleAuditingId = Guid.NewGuid().ToString();
                        roleAuditing.AuditRank = 0;
                        roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                        if (roleAuditing.NextAuditRole != null)
                            roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                        roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                        roleAuditing.Employee0Id = V.ActiveOperator.EmployeeId;
                        roleAuditing.InsertTime = DateTime.Now;
                        roleAuditing.InvoiceId = invoice.InvoiceId;

                        roleAuditing.InvoiceName = new OperationManager().GetOperationNamebyTabel(tableName);
                        roleAuditing.TableName = tableName;
                        this.roleAuditingManager.Insert(roleAuditing);


                    }

                }
                else
                    invoice.AuditState = (int)global::Helper.InvoiceAudit.NoUsing;



                _Insert(invoice);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, invoice.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, invoice.InsertTime.Value.Year, invoice.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, invoice.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }
        private void TiGuiExists(Model.Invoice model)
        {
            MethodInfo methodinfo = this.GetType().GetMethod("HasRows", new Type[] { typeof(string) });
            bool f = (bool)methodinfo.Invoke(this, new object[] { model.InvoiceId });
            if (f)
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.InvoiceId = this.GetNewId();
                TiGuiExists(model);
            }
        }
        public void TurnNormal(string invoiceId)
        {
            MethodInfo methodInfo = this.GetType().GetMethod("Get");
            Model.Invoice invoice = (Model.Invoice)methodInfo.Invoke(this, new object[] { invoiceId });
            if (invoice == null)
                throw new ArgumentException();

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value != Helper.InvoiceStatus.Draft)
                throw new InvalidOperationException();

            try
            {
                V.BeginTransaction();
                _TurnNormal(invoice);
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        public void TurnNull(string invoiceId)
        {
            MethodInfo methodInfo = this.GetType().GetMethod("Get");
            Model.Invoice invoice = (Model.Invoice)methodInfo.Invoke(this, new object[] { invoiceId });
            if (invoice == null)
                return;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value != Helper.InvoiceStatus.Normal)
                throw new InvalidOperationException();

            try
            {
                V.BeginTransaction();
                _TurnNull(invoice);
                this.Delete(invoiceId);

                roleAuditingManager.DeleteByInvoiceIdAndTable(invoiceId, this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Manager")));
                //DA.IInvoiceAccessor accessor = this.GetAccessor();
                //methodInfo = accessor.GetType().GetMethod("Delete", new Type[] { typeof(string) });
                //if (methodInfo != null)
                //{
                //    methodInfo.Invoke(accessor, new object[] { invoice.InvoiceId });
                //}
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        public virtual void Delete(string invoiceId)
        {
            try
            {
                DA.IInvoiceAccessor accessor = this.GetAccessor();
                MethodInfo methodInfo = accessor.GetType().GetMethod("Delete", new Type[] { typeof(string) });
                if (methodInfo != null)
                {
                    //if (accessor == Book.DA.IInvoiceZSAccessor)
                    //{
                    //invoiceZSDetailAccessor.Delete(invoice);
                    //}
                    methodInfo.Invoke(accessor, new object[] { invoiceId });

                }
            }
            catch
            {
                V.RollbackTransaction();
                throw new Helper.InvalidValueException("DeleteError");
            }
        }

        public void InsertUpdate(Model.Invoice invoice)
        {
            DA.IInvoiceAccessor accessor = this.GetAccessor();
            if (accessor != null)
            {
                MethodInfo methodInfo = accessor.GetType().GetMethod("HasRows", new Type[] { typeof(string) });
                bool hasRows = (bool)methodInfo.Invoke(accessor, new object[] { invoice.InvoiceId });

                if (hasRows && invoice.InsertTime != null)
                    this.Update(invoice);
                else
                    this.Insert(invoice);

            }
        }

        public void Update(Model.Invoice invoice)
        {
            _ValidateForUpdate(invoice);

            try
            {
                V.BeginTransaction();

                if (invoice.AuditState.Value == 4)
                {

                    string tableName = this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Manager"));
                    Model.RoleAuditing roleAuditing = this.roleAuditingManager.SelectByInvoiceIdAndTable(invoice.InvoiceId, tableName);

                    roleAuditing.AuditRank = 0;
                    roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                    if (roleAuditing.NextAuditRole != null)
                        roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                    roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                    roleAuditing.Employee0Id = V.ActiveOperator.EmployeeId;
                    this.roleAuditingManager.Update(roleAuditing);

                }
                else
                    invoice.AuditState = (int)global::Helper.InvoiceAudit.NoUsing;


                _Update(invoice);
                V.CommitTransaction();
            }
            catch (Exception ex)
            {
                V.RollbackTransaction();
                throw ex;
            }
        }


        protected abstract string GetInvoiceKind();
        protected abstract void _Insert(Model.Invoice invoice);
        protected abstract void _Update(Model.Invoice invoice);
        protected abstract void _TurnNormal(Model.Invoice invoice);
        protected abstract void _TurnNull(Model.Invoice invoice);

        #region Helpers

        protected virtual DA.IInvoiceAccessor GetAccessor()
        {
            return null;
        }
        private int a = 0;
        protected virtual void _ValidateForInsert(Model.Invoice invoice)
        {

            if (a == 1)
            {
                if (invoice.InvoiceId == null)
                    throw new Helper.RequireValueException("Id");
            }
            else
            {
                if (string.IsNullOrEmpty(invoice.InvoiceId))
                    throw new Helper.RequireValueException("Id");
            }

            //if (invoice.Employee0 == null)
            //    throw new Helper.RequireValueException("Employee0");
        }

        protected virtual void _ValidateForUpdate(Model.Invoice invoice)
        {
            a = 1;
            MethodInfo methodInfo = this.GetType().GetMethod("Get");
            Model.Invoice invoiceOriginal = (Model.Invoice)methodInfo.Invoke(this, new object[] { invoice.InvoiceId });
            if (invoiceOriginal == null)
                throw new ArgumentException();

            //if (invoice.InvoiceStatus != invoiceOriginal.InvoiceStatus)
            //    throw new InvalidOperationException();

            // 验证数据必须是完整的
            if (invoice.InvoiceId == null)
                throw new Helper.RequireValueException("Id");

            ////if (invoice.Employee0 == null)
            ////    throw new Helper.RequireValueException("Employee0");


            // 如果是单据是正式的或作废的，那么不能修改关键信息
            switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    if (string.IsNullOrEmpty(invoice.InvoiceId))
                        throw new Helper.RequireValueException("Id");

                    ////if (invoice.Employee0 == null)
                    ////    throw new Helper.RequireValueException("Employee0");
                    break;

                case Helper.InvoiceStatus.Normal:
                    break;
                case Helper.InvoiceStatus.Null:
                    break;
                default:
                    break;
            }


        }

        #endregion


    }
}
