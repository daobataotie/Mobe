//------------------------------------------------------------------------------
//
// file name：Material.cs
// author: mayanjun
// create date：2013-5-4 16:09:34
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 原料净重
    /// </summary>
    [Serializable]
    public partial class Material
    {
        //public override string ToString()
        //{
        //    return this.Id;
        //}
        public bool Check { get; set; }

        private int _num;

        public int Num
        {
            get { return _num == 0 ? 1 : _num; }
            set { _num = value; }
        }
    }
}
