//------------------------------------------------------------------------------
//
// file name：PronotePackageAccessor.cs
// author: mayanjun
// create date：2011-07-20 16:57:15
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
    /// Data accessor of PronotePackage
    /// </summary>
    public partial class PronotePackageAccessor : EntityAccessor, IPronotePackageAccessor
    {

        public DataSet GetDataTable(DateTime date)
        {
            DataSet data = new DataSet();
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select *,(select productname from product where productId=pp.productId) Pname from PronotePackage pp where year(PronotePackageDate)=" + date.Year + " and month(PronotePackageDate)=" + date.Month, conn);
            dataAdapter.Fill(data);
            return data;
        }

        public IList<Model.PronotePackage> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht=new Hashtable();
            ht.Add("begindate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.PronotePackage>("PronotePackage.SelectByDateRange", ht);
        }

        public void UpdateData(DataTable pronotePackage)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.InsertCommand = new SqlCommand("insert  into PronotePackage(PronotePackageId,PronoteHeaderId,PronotePackageDate,PronoteYearAndMonths,ProductId,FullProductNum,BadProductNum,BadPercent,Total,BlackPoint,GuoHuo,QiPao,WanMo,SuoShui,MianXu,ZaZhi,LiuHen,CaShang,PenYao,LouGuang,Others,GuaShang,Box,Feet,InsertTime,UpdateTime) values(@PronotePackageId,@PronoteHeaderId,@PronotePackageDate,@PronoteYearAndMonths,@ProductId,@FullProductNum,@BadProductNum,@BadPercent,@Total,@BlackPoint,@GuoHuo,@QiPao,@WanMo,@SuoShui,@MianXu,@ZaZhi,@LiuHen,@CaShang,@PenYao,@LouGuang,@Others,@GuaShang,@Box,@Feet,@InsertTime,@UpdateTime) ", conn);
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@PronotePackageId", SqlDbType.VarChar, 50, "PronotePackageId"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@PronoteHeaderId", SqlDbType.VarChar, 50, "PronoteHeaderId"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@PronotePackageDate", SqlDbType.DateTime, 50, "PronotePackageDate"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@PronoteYearAndMonths", SqlDbType.DateTime, 50, "PronoteYearAndMonths"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.VarChar, 50, "ProductId"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@FullProductNum", SqlDbType.Float, 50, "FullProductNum"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BadProductNum", SqlDbType.Float, 50, "BadProductNum"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BadPercent", SqlDbType.Float, 50, "BadPercent"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Total", SqlDbType.Float, 50, "Total"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@BlackPoint", SqlDbType.Int, 50, "BlackPoint"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@GuoHuo", SqlDbType.Int, 50, "GuoHuo"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@QiPao", SqlDbType.Int, 50, "QiPao"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@WanMo", SqlDbType.Int, 50, "WanMo"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@SuoShui", SqlDbType.Int, 50, "SuoShui"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@MianXu", SqlDbType.Int, 50, "MianXu"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@ZaZhi", SqlDbType.Int, 50, "ZaZhi"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@LiuHen", SqlDbType.Int, 50, "LiuHen"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@CaShang", SqlDbType.Int, 50, "CaShang"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@PenYao", SqlDbType.Int, 50, "PenYao"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@LouGuang", SqlDbType.Int, 50, "LouGuang"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Others", SqlDbType.Int, 50, "Others"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@GuaShang", SqlDbType.Int, 50, "GuaShang"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Box", SqlDbType.Int, 50, "Box"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@Feet", SqlDbType.Int, 50, "Feet"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@InsertTime", SqlDbType.DateTime, 50, "InsertTime"));
            dataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@UpdateTime", SqlDbType.DateTime, 50, "UpdateTime"));

            dataAdapter.UpdateCommand = new SqlCommand("update PronotePackage set PronoteHeaderId=@PronoteHeaderId,PronotePackageDate=@PronotePackageDate,PronoteYearAndMonths=@PronoteYearAndMonths,ProductId=@ProductId,FullProductNum=@FullProductNum,BadProductNum=@BadProductNum,BadPercent=@BadPercent,Total=@Total,BlackPoint=@BlackPoint,GuoHuo=@GuoHuo,QiPao=@QiPao,WanMo=@WanMo,SuoShui=@SuoShui,MianXu=@SuoShui,ZaZhi=@ZaZhi,LiuHen=@LiuHen,CaShang=@CaShang,PenYao=@PenYao,LouGuang=@LouGuang,Others=@Others,GuaShang=@GuaShang,Box=@Box,Feet=@Feet,InsertTime=@InsertTime,UpdateTime=@UpdateTime where PronotePackageId=@PronotePackageId", conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@PronotePackageId", SqlDbType.VarChar, 50, "PronotePackageId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@PronoteHeaderId", SqlDbType.VarChar, 50, "PronoteHeaderId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@PronotePackageDate", SqlDbType.DateTime, 50, "PronotePackageDate"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@PronoteYearAndMonths", SqlDbType.DateTime, 50, "PronoteYearAndMonths"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.VarChar, 50, "ProductId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@FullProductNum", SqlDbType.Float, 50, "FullProductNum"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BadProductNum", SqlDbType.Float, 50, "BadProductNum"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BadPercent", SqlDbType.Float, 50, "BadPercent"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Total", SqlDbType.Float, 50, "Total"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@BlackPoint", SqlDbType.Int, 50, "BlackPoint"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@GuoHuo", SqlDbType.Int, 50, "GuoHuo"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@QiPao", SqlDbType.Int, 50, "QiPao"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@WanMo", SqlDbType.Int, 50, "WanMo"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@SuoShui", SqlDbType.Int, 50, "SuoShui"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@MianXu", SqlDbType.Int, 50, "MianXu"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ZaZhi", SqlDbType.Int, 50, "ZaZhi"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@LiuHen", SqlDbType.Int, 50, "LiuHen"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@CaShang", SqlDbType.Int, 50, "CaShang"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@PenYao", SqlDbType.Int, 50, "PenYao"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@LouGuang", SqlDbType.Int, 50, "LouGuang"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Others", SqlDbType.Int, 50, "Others"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@GuaShang", SqlDbType.Int, 50, "GuaShang"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Box", SqlDbType.Int, 50, "Box"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Feet", SqlDbType.Int, 50, "Feet"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@InsertTime", SqlDbType.DateTime, 50, "InsertTime"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@UpdateTime", SqlDbType.DateTime, 50, "UpdateTime"));
            
            dataAdapter.DeleteCommand = new SqlCommand("delete from PronotePackage where PronotePackageId=@PronotePackageId", conn);
            SqlParameter spr = new SqlParameter("@PronotePackageId", SqlDbType.VarChar, 50, Model.PronotePackage.PRO_PronotePackageId);
            spr.IsNullable = true;
            spr.Direction = ParameterDirection.Output;
            dataAdapter.DeleteCommand.Parameters.Add(spr);
            dataAdapter.Update(pronotePackage);
        }
    }
}
