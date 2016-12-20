//------------------------------------------------------------------------------
//
// file name：SequenceManager.cs
// author: peidun
// create date：2008/7/26 9:31:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Sequence.
    /// </summary>
    public partial class SequenceManager
    {
        public static int GetCurrentVal(string key)
        {
            Model.Sequence sequence = accessor.Get(key);
            if (sequence == null)
                return 0;

            return sequence.Val.Value;
        }

        public static int GetNextVal(string key)
        {
            Model.Sequence sequence = accessor.Get(key);
            if (sequence == null)
            {
                sequence = new Model.Sequence();
                sequence.Key = key;
                sequence.Val = 0;
            }
            sequence.Val++;
            if (accessor.HasRows(key))
                accessor.Update(sequence);
            else
                accessor.Insert(sequence);

            return sequence.Val.Value;
        }

        public static void Increment(string key)
        {
            Model.Sequence sequence = accessor.Get(key);
            if (sequence == null)
            {
                sequence = new Model.Sequence();
                sequence.Key = key;
                sequence.Val = 0;
            }
            sequence.Val++;
            if (accessor.HasRows(key))
                accessor.Update(sequence);
            else
                accessor.Insert(sequence);
        }
        //public static void IncrementVal(string key,int val)
        //{
        //    Model.Sequence sequence = accessor.Get(key);
        //    if (sequence == null)
        //    {
        //        sequence = new Model.Sequence();
        //        sequence.Key = key;
        //        sequence.Val = 0;
        //    }
        //    sequence.Val=val;//修改上方法
        //    if (accessor.HasRows(key))
        //        accessor.Update(sequence);
        //    else
        //        accessor.Insert(sequence);
        //}

    }
}

