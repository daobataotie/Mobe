using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace Book.BL
{
    public abstract class BaseManager
    {
        private static readonly DA.IEntityAccessor entityAccessor = (DA.IEntityAccessor)Accessors.Get("EntityAccessor");
        private static readonly DA.IAcademicBackGroundAccessor academicBackGroundAccessor = (DA.IAcademicBackGroundAccessor)Accessors.Get("AcademicBackGroundAccessor");
        
        private DateTime datetime = DateTime.Now;
        protected string GetEntityId(DateTime dt)
        {
            string settingId = this.GetSettingId();
            string invoiceKind = this.GetInvoiceKind().ToLower();
            datetime = dt;
            if (string.IsNullOrEmpty(invoiceKind) || string.IsNullOrEmpty(settingId))
                return string.Empty;

            string rule = Settings.Get(settingId);

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

            int a = sequenceval;
            sequenceval++;

            string d2 = string.Format("{0:d2}", datetime.Day);
            string m2 = string.Format("{0:d2}", datetime.Month);
            string y2 = string.Format("{0:d2}", datetime.Year);
            string y4 = string.Format("{0:d4}", datetime.Year);

            string n4 = string.Format("{0:d4}", (int)sequenceval);

            string n1 = string.Format("{0:d1}", sequenceval);
            string n2 = string.Format("{0:d2}", sequenceval);
            string n3 = string.Format("{0:d3}", sequenceval);
            string n5 = string.Format("{0:d5}", sequenceval);
            string n6 = string.Format("{0:d6}", sequenceval);
            string n7 = string.Format("{0:d7}", sequenceval);
            string n8 = string.Format("{0:d8}", sequenceval);
            string n9 = string.Format("{0:d9}", sequenceval);
            string n10 = string.Format("{0:d10}", sequenceval);


            return rule.Replace("{D2}", d2).Replace("{M2}", m2).Replace("{Y2}", y2).Replace("{Y4}", y4).Replace("{N}", n4).Replace("{N1}", n1).Replace("{N2}", n2).Replace("{N3}", n3).Replace("{N4}", n4).Replace("{N5}", n5).Replace("{N6}", n6).Replace("{N7}", n7).Replace("{N8}", n8).Replace("{N9}", n9).Replace("{N10}", n10);
        }

        public void ModifySequence()
        {
            string invoiceKind = this.GetInvoiceKind().ToLower();
            if (string.IsNullOrEmpty(invoiceKind)) return;
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, datetime.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, datetime.Year, datetime.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, datetime.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);
            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);
        }

        public string GetId()
        {
            return this.GetEntityId(DateTime.Now);
        }

        public virtual string GetId(DateTime dateTime)
        {
            return this.GetEntityId(dateTime);
        }

        protected virtual string GetSettingId()
        {
            return string.Empty;
        }

        protected virtual string GetInvoiceKind()
        {
            return string.Empty;
        }

        public IList<T> DataReaderBind<T>(string sql, SqlParameter[] parems, CommandType type)
        {
            return entityAccessor.DataReaderBind<T>(sql, parems, type);
        }

        /// <summary>
        /// 四舍五入方法.
        /// </summary>
        /// <param name="objTarget">要操作的double类型数字</param>
        /// <param name="mIndex">欲保留小数位数</param>
        /// <returns></returns>
        public double GetSiSheWuRu(double objTarget, int mIndex)
        {
            double a1 = Math.Pow(10, mIndex);
            double a2 = Math.Pow(10, mIndex + 1);
            double b1 = Math.Truncate(objTarget * a1);
            double b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5 || b2 % 10 <= -5)
            {
                return objTarget > 0 ? (b1 + 1) / a1 : (b1 - 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }

        /// <summary>
        /// 四舍五入方法.
        /// </summary>
        /// <param name="objTarget">要操作的Decimal类型数字</param>
        /// <param name="mIndex">欲保留小数位数</param>
        /// <returns></returns>
        public decimal GetSiSheWuRu(decimal objTarget, int mIndex)
        {

            decimal a1 = decimal.Parse(Math.Pow(10, mIndex).ToString());
            decimal a2 = decimal.Parse(Math.Pow(10, mIndex + 1).ToString());
            decimal b1 = Math.Truncate(objTarget * a1);
            decimal b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5 || b2 % 10 <= -5)
            {
                return objTarget > 0 ? (b1 + 1) / a1 : (b1 - 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }

        /// <summary>
        /// 判断该字段是否有重复值,返回false 不存在,true 存在
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public bool JudgeValueExists(string strsql)
        {
            return entityAccessor.JudgeValueExists(strsql);
        }

        public int UpdateSql(string sql)
        {
            return entityAccessor.UpdateSql(sql);
        }

        public DateTime? JudgeHasNewVersion<T>(T objTarget, string PrimaryKeyId)
        {
            return entityAccessor.JudgeHasNewVersion<T>(objTarget, PrimaryKeyId);
        }
        public DataSet Query(string SQLString, int Times, string tabelName)
        {
            return entityAccessor.Query(SQLString, Times, tabelName);
        }
        public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            return entityAccessor.ExecuteReader( SQLString,  cmdParms);
        }
        public DataSet QueryProc(string procName, SqlParameter[] pars, string tabelName)
        {
            return entityAccessor.QueryProc(procName, pars, tabelName);
        }
     
    }

}
