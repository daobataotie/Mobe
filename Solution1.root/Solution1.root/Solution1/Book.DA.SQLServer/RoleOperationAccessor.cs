//------------------------------------------------------------------------------
//
// file name:RoleOperationAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:51
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of RoleOperation
    /// </summary>
    public partial class RoleOperationAccessor : EntityAccessor, IRoleOperationAccessor
    {
        #region IRoleOperationAccessor 成员


        public IList<Book.Model.RoleOperation> Select(Book.Model.Role role)
        {
            return sqlmapper.QueryForList<Model.RoleOperation>("RoleOperation.select_byRole", role.RoleId);
        }

        public IList<Book.Model.RoleOperation> SelectIsSearch(Book.Model.Operators operations)
        {
            return sqlmapper.QueryForList<Model.RoleOperation>("RoleOperation.select_byOperatorsId", operations.OperatorsId);
        }
        public IList<Book.Model.RoleOperation> SelectbyOperatorsKeyTag(Book.Model.Operators operations, string keytag)
        {
            Hashtable ht = new Hashtable();
            ht.Add("operatorsId",operations.OperatorsId);
            ht.Add("keytag", keytag);

            return sqlmapper.QueryForList<Model.RoleOperation>("RoleOperation.select_byOperatorsKeyTag", ht);
        }
        /// <summary>
        /// 根据父查询最子节点
        /// </summary>
        /// <returns></returns>
        public DataSet SelectByType(string parenId, string roleId)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT a.OperationName,a.OperationId ,a.KeyTag ,a.KeyUrl ,isnull([TABLE].RoleOperationId,newid()) as RoleOperationId,[TABLE].PossAdd,[TABLE].PossUpdate,[TABLE].PossDelete,[TABLE].PossSearch,[TABLE].PossAuditing,[TABLE].PossPrint,[TABLE].PossExport ,[TABLE].PossReportEdit,[TABLE].roleid from  Operation a LEFT JOIN (SELECT e.OperationName,e.OperationId ,e.KeyTag  ,e.KeyUrl  ,l.PossExport,l.PossReportEdit,l.roleid,l.RoleOperationId,l.PossAdd,l.PossUpdate,l.PossDelete,l.PossSearch,l.PossAuditing,l.PossPrint from  RoleOperation  l right join Operation e     on e.OperationId = l.OperationId     where ParentOperationId='" + parenId + "'     and (l.roleid='" + roleId + "' or l.roleid is null ))[TABLE]      ON a.OperationId=[TABLE].OperationId WHERE a.ParentOperationId='" + parenId + "' order by a.OperationId", conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "RoleOperation");
            return ds;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SelectDate"></param>
        /// <returns></returns>
        public int UpdateTable(DataSet ds, string roleid)
        {


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.Delete(ds.Tables[0].Rows[i]["RoleOperationId"].ToString());
            }


            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand("insert into RoleOperation values(@RoleOperationId,'" + roleid + "',@OperationId,1,@PossAdd,@PossUpdate,@PossDelete,@PossSearch,@PossAuditing,@PossPrint,@PossExport,@PossReportEdit,@KeyTag,@KeyUrl) ", conn);

            SqlParameter[] par = new SqlParameter[]
            {
                 new SqlParameter("@RoleOperationId",SqlDbType.VarChar,50,"RoleOperationId"),
                new SqlParameter("@OperationId",SqlDbType.VarChar,50,"OperationId"),
                new SqlParameter("@PossAdd",SqlDbType.Bit,4,"PossAdd"),
                new SqlParameter("@PossUpdate",SqlDbType.Bit,4,"PossUpdate"),
                new SqlParameter("@PossDelete",SqlDbType.Bit,4,"PossDelete"),
                 new SqlParameter("@PossSearch",SqlDbType.Bit,4,"PossSearch"),
                new SqlParameter("@PossAuditing",SqlDbType.Bit,4,"PossAuditing"),
                new SqlParameter("@PossPrint",SqlDbType.Bit,4,"PossPrint"),
                 new SqlParameter("@PossExport",SqlDbType.Bit,4,"PossExport"),
                new SqlParameter("@PossReportEdit",SqlDbType.Bit,4,"PossReportEdit"),
                new SqlParameter("@KeyTag",SqlDbType.VarChar,200,"KeyTag"),
                  new SqlParameter("@KeyUrl",SqlDbType.VarChar,200,"KeyUrl")
            };

            //da.UpdateCommand = new SqlCommand("update   RoleOperation set newid(),'" + roleid + "',@OperationId,null,@PossAdd,@PossAdd,@PossUpdate,@PossDelete,@PossAuditing,@PossPrint,@PossExport,@PossReportEdit,@KeyTag) ", conn);

            //SqlParameter[] par1 = new SqlParameter[]
            //{
            //    new SqlParameter("@OperationId",SqlDbType.VarChar,50,"OperationId"),
            //    new SqlParameter("@PossAdd",SqlDbType.Bit,4,"PossAdd"),
            //    new SqlParameter("@PossUpdate",SqlDbType.Bit,4,"PossUpdate"),
            //        new SqlParameter("@PossDelete",SqlDbType.Bit,4,"PossDelete"),
            //    new SqlParameter("@PossAuditing",SqlDbType.Bit,4,"PossAuditing"),
            //    new SqlParameter("@PossPrint",SqlDbType.Bit,4,"PossPrint"),
            //     new SqlParameter("@PossExport",SqlDbType.Bit,4,"PossExport"),
            //    new SqlParameter("@PossReportEdit",SqlDbType.Bit,4,"PossReportEdit"),
            //    new SqlParameter("@KeyTag",SqlDbType.VarChar,200,"KeyTag")
            //};

            da.UpdateCommand.Parameters.AddRange(par);

            int result = da.Update(ds.Tables[0]);
            return result;
        }

        #endregion
    }
}
