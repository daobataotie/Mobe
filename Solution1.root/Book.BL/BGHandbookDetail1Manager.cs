//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail1Manager.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDetail1.
    /// </summary>
    public partial class BGHandbookDetail1Manager : BaseManager
    {
        private static readonly DA.IBGHandbookDetail2Accessor Detail2Accessor = (DA.IBGHandbookDetail2Accessor)Accessors.Get("BGHandbookDetail2Accessor");
        private static readonly DA.IBGHandbookAccessor bGHandbookAccessor = (DA.IBGHandbookAccessor)Accessors.Get("BGHandbookAccessor");

        /// <summary>
        /// Delete BGHandbookDetail1 by primary key.
        /// </summary>
        public void Delete(string bGHandbookDetail1Id)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGHandbookDetail1Id);
        }

        /// <summary>
        /// Insert a BGHandbookDetail1.
        /// </summary>
        public void Insert(Model.BGHandbookDetail1 bGHandbookDetail1)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGHandbookDetail1);
        }

        /// <summary>
        /// Update a BGHandbookDetail1.
        /// </summary>
        public void Update(Model.BGHandbookDetail1 bGHandbookDetail1)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(bGHandbookDetail1);
        }
        public IList<Book.Model.BGHandbookDetail1> Select(string pac)
        {
            return accessor.Select(pac);
        }
        public void UpdateBee(string HandbookId, string HandbookProductId, double quantity)//已定已出
        {
            //2014年12月15日开始出库单出货后“已出数量”增加，手册成品出货单出货后“已定已出”增加，现在反过来。

            //detail1.UpQuantity = detail1.Quantity - beeQuantity;
            //Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;


            IList<Model.BGHandbook> _bGHandbookList = new BGHandbookManager().Select(HandbookId);
            if (_bGHandbookList == null) return;
            foreach (Model.BGHandbook _bGHandbook in _bGHandbookList)
            {


                //多单据
                _bGHandbook.Detail1 = accessor.Select(_bGHandbook.BGHandbookId);
                _bGHandbook.Detail2 = Detail2Accessor.Select(_bGHandbook.BGHandbookId);


                // 成品区
                IList<Model.BGHandbookDetail1> detailList = _bGHandbook.Detail1.Where(d => d.Id.ToString() == HandbookProductId && !string.IsNullOrEmpty(d.ProName)).ToList<Model.BGHandbookDetail1>();
                //  Model.BGHandbookDetail1 detail = accessor.SelectBGProduct(HandbookId, HandbookProductId);

                //detailList[0].BeeQuantity = Convert.ToDouble(detailList[0].BeeQuantity) + quantity;
                //detailList[0].UpQuantity = Convert.ToDouble(detailList[0].UpQuantity) - quantity;
                detailList[0].YdycQuantity = Convert.ToDouble(detailList[0].YdycQuantity) + quantity;
                //this.UpdateMap("update BGHandbookDetail1 set BeeQuantity=isnull( BeeQuantity,0)+" + quantity + ",UpQuantity= isnull(UpQuantity,0)-" + quantity + "  where BGHandbookId='" + _bGHandbook.BGHandbookId + "' and Id='" + xsdetail.HandbookProductId + "'");
                accessor.Update(detailList[0]);

                //Model.BGHandbookDetail2 detail2;
                ////子件 1区
                //IList<Model.BGHandbookDetail1> detail1List = _bGHandbook.Detail1.Where(d => d.Id.ToString() == HandbookProductId && !string.IsNullOrEmpty(d.Column1)).ToList<Model.BGHandbookDetail1>();


                //foreach (Model.BGHandbookDetail1 detail1m in detail1List)
                //{
                //    detail2 = _bGHandbook.Detail2.Where(d => d.Id == detail1m.LId).ToList<Model.BGHandbookDetail2>()[0];

                //    double? a = detail2.LilunHaoYong - detail1m.LiLunHaoYongJingSun;

                //    detail1m.LiLunHaoYongJing = (double)GetSiSheWuRu(Convert.ToDecimal(detailList[0].BeeQuantity) * Convert.ToDecimal(detail1m.LjingQuantity), 2);
                //    detail1m.LiLunHaoYongJingSun = (double)GetSiSheWuRu(Convert.ToDecimal(detailList[0].BeeQuantity) * Convert.ToDecimal(detail1m.LjingQuantity) / (1 - Convert.ToDecimal(detail1m.Lsunhaolv) * 0.01M), 2);

                //    accessor.Update(detail1m);

                //    detail2.LilunHaoYong = a + detail1m.LiLunHaoYongJingSun;
                //    detail2.LilunStock = detail2.LbejinQuantity - detail2.LilunHaoYong;
                //    Detail2Accessor.Update(detail2);

            }

        }

        public void UpdateUpQuantity(Model.BGHandbookDetail1 detail)//剩余数量
        {
            //2014年12月15日开始出库单出货后“已出数量”增加，手册成品出货单出货后“已定已出”增加，现在反过来。
            double b = Convert.ToDouble(detail.LiLunHaoYongJingSun);
            detail.UpQuantity = Convert.ToDouble(detail.Quantity) - Convert.ToDouble(detail.BeeQuantity);
            detail.LiLunHaoYongJing = (double)GetSiSheWuRu(Convert.ToDecimal(detail.BeeQuantity) * Convert.ToDecimal(detail.LjingQuantity), 2);
            detail.LiLunHaoYongJingSun = (double)GetSiSheWuRu(Convert.ToDecimal(detail.BeeQuantity) * Convert.ToDecimal(detail.LjingQuantity) / (1 - Convert.ToDecimal(detail.Lsunhaolv) * 0.01M), 2);

            accessor.Update(detail);

            Model.BGHandbookDetail2 detail2 = Detail2Accessor.SelectBGProduct(detail.BGHandbook.Id, (detail.LId == null ? "" : detail.LId.ToString()));

            if (detail2 != null)
            {

                double a = Convert.ToDouble(detail2.LilunHaoYong) - b;

                detail2.LilunHaoYong = a + Convert.ToDouble(detail.LiLunHaoYongJingSun);
                detail2.LilunStock = Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(detail2.LilunHaoYong);
                Detail2Accessor.Update(detail2);
            }
        }

        public void UpdateBeeIn(string HandbookId, string HandbookProductId, double quantity) //已进口
        {

            //detail1.UpQuantity = detail1.Quantity - beeQuantity;
            //Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
            IList<Model.BGHandbookDetail2> _bGHandbookList = Detail2Accessor.SelectbyShouceandId(HandbookId, HandbookProductId);

            if (_bGHandbookList == null) return;
            foreach (Model.BGHandbookDetail2 detail2 in _bGHandbookList)
            {      //多单据
                if (!detail2.LbejinQuantity.HasValue) detail2.LbejinQuantity = 0;

                detail2.LbejinQuantity = detail2.LbejinQuantity + quantity;
                detail2.UpQuantity = detail2.LjingSunliang - detail2.LbejinQuantity - (detail2.ZhuanCeInQuantity.HasValue ? detail2.ZhuanCeInQuantity : 0);
                detail2.HaiKeJInQuantity = detail2.UpQuantity - (detail2.YaoJInQuantity.HasValue ? detail2.YaoJInQuantity : 0);

                detail2.LilunStock = detail2.LbejinQuantity - (detail2.LilunHaoYong.HasValue ? detail2.LilunHaoYong : 0);
                detail2.JinKouiMoney = GetSiSheWuRu(Convert.ToDecimal(detail2.LbejinQuantity) * (detail2.LPrice.HasValue ? detail2.LPrice.Value : 0m), 2);
                Detail2Accessor.Update(detail2);

            }
        }

        public void UpdateYidingweiru(Model.InvoiceCODetail codetail, double quantity)//要进量 已定未入
        {

            //detail1.UpQuantity = detail1.Quantity - beeQuantity;
            //Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
            IList<Model.BGHandbookDetail2> _bGHandbookList = Detail2Accessor.SelectbyShouceandId(codetail.HandbookId, codetail.HandbookProductId);

            if (_bGHandbookList == null) return;
            foreach (Model.BGHandbookDetail2 detail2 in _bGHandbookList)
            {      //多单据
                if (!detail2.YaoJInQuantity.HasValue) detail2.YaoJInQuantity = 0;
                if (!detail2.UpQuantity.HasValue) detail2.UpQuantity = 0;
                detail2.YaoJInQuantity = detail2.YaoJInQuantity + quantity;
                detail2.HaiKeJInQuantity = detail2.UpQuantity - detail2.YaoJInQuantity;
                Detail2Accessor.Update(detail2);
            }
        }
        /// <summary>
        /// 已定未出
        /// </summary>
        /// <param name="codetail"></param>
        /// <param name="quantity"></param>
        public void UpdateYDWC(Model.InvoiceXODetail xodetail, double quantity)// 已定未出
        {

            //detail1.UpQuantity = detail1.Quantity - beeQuantity;
            //Model.BGHandbookDetail1 detail1 = bindingSource1.Current as Model.BGHandbookDetail1;
            Model.BGHandbookDetail1 detail = accessor.SelectBGProduct(xodetail.HandbookId, xodetail.HandbookProductId);
            if (detail != null)
            {
                if (!detail.YdwcQuantity.HasValue) detail.YdwcQuantity = 0;
                //if (!detail.BeeQuantity.HasValue) detail.BeeQuantity = 0;

                detail.YdwcQuantity = detail.YdwcQuantity + quantity;
                //detail.UpQuantity = detail.Quantity - detail.BeeQuantity;
                //detail.UpQuantity = detail.Quantity - detail.BeeQuantity - detail.YdwcQuantity;
                accessor.Update(detail);
            }
        }


        public string SelectProName(string BGHandBookId, string Id)
        {
            return accessor.SelectProName(BGHandBookId, Id);
        }
        public Model.BGHandbookDetail1 SelectBGProduct(string BGHandBookId, string Id)
        {
            return accessor.SelectBGProduct(BGHandBookId, Id);
        }

        /// <summary>
        /// 手册预警
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetBGPrompt()
        {
            return accessor.GetBGPrompt();
        }
    }
}


