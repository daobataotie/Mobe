using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-5-7
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public class ConditionProInDepotChoose : Condition
    {

        private DateTime startDate;
        private DateTime endDate;
        private string startPronoteHeader;
        private string endPronoteHeader;
        private Model.Depot _mDepot;

        public Model.Depot MDepot
        {
            get { return _mDepot; }
            set { _mDepot = value; }
        }
        private Model.DepotPosition _mDepotPosition;

        public Model.DepotPosition MDepotPosition
        {
            get { return _mDepotPosition; }
            set { _mDepotPosition = value; }
        }

        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        public string StartPronoteHeader
        {
            get { return this.startPronoteHeader; }
            set { this.startPronoteHeader = value; }
        }

        public string EndPronoteHeader
        {
            get { return this.endPronoteHeader; }
            set { this.endPronoteHeader = value; }
        }
        private Model.Product _product;
        public Model.Product Product
        {
            get { return this._product; }
            set { this._product = value; }
        }
        private Model.WorkHouse _workHouse;
        public Model.WorkHouse WorkHouse
        {
            get { return this._workHouse; }
            set { this._workHouse = value; }
        }
        private string _id1;
        public string Id1
        {
            get { return this._id1; }
            set { this._id1 = value; }
        }
        private string _id2;
        public string Id2
        {
            get { return this._id2; }
            set { this._id2 = value; }
        }
        private string _cusxoid;
        public string Cusxoid
        {
            get { return this._cusxoid; }
            set
            {
                this._cusxoid = value;
            }
        }
        private Model.Customer _customer1;
        public Model.Customer Customer1
        {
            get { return this._customer1; }
            set { this._customer1 = value; }
        }
        private Model.Customer _customer2;
        public Model.Customer Customer2
        {
            get { return this._customer2; }
            set { this._customer2 = value; }
        }

        //商品状态
        public int ProductState { get; set; }


    }
}
