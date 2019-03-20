//------------------------------------------------------------------------------
//
// file name：Customer.cs
// author: peidun
// create date：2009-08-03 9:37:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 客户
    /// </summary>
    [Serializable]
    public partial class Customer : IComparable
    {
        public override string ToString()
        {
            return this._customerShortName;
        }

        //private long _isert


        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Customer)
            {
                Customer company = (Customer)obj;
                return company._customerFullName.CompareTo(this._customerFullName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Customer)
            {
                return (obj as Model.Customer).CustomerId == this._customerId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private System.Collections.Generic.IList<Model.CustomerContact> contacts = new System.Collections.Generic.List<Model.CustomerContact>();

        public System.Collections.Generic.IList<Model.CustomerContact> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }
        //private bool _customerCheck;
        //public bool customerCheck
        //{
        //    get { return _customerCheck; }
        //    set{
        //        _customerCheck = value;
        //    }

        //}

        //private System.Collections.Generic.IList<Model.CustomerMarks> marks = new System.Collections.Generic.List<Model.CustomerMarks>();

        ///// <summary>
        ///// 唛头
        ///// </summary>
        //public System.Collections.Generic.IList<Model.CustomerMarks> Marks
        //{
        //    get { return marks; }
        //    set { marks = value; }
        //}

        public bool IsChecked { get; set; }
    }
}
