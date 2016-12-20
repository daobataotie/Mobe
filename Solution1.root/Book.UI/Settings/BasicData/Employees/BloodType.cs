using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Settings.BasicData.Employees
{
    public enum BloodType
    {
        /// <summary>
        /// A型
        /// </summary>
        A,
        /// <summary>
        /// B型
        /// </summary>
        B,
        /// <summary>
        /// O型
        /// </summary>
        O,
        /// <summary>
        /// AB型
        /// </summary>
        AB
    }

    public enum Gender
    {
        /// <summary>
        /// 女性
        /// </summary>
        Female,
        /// <summary>
        /// 男性
        /// </summary>
        Male        
    }

    public enum MarriyState 
    {
        /// <summary>
        /// 未婚
        /// </summary>
        Single,
        /// <summary>
        /// 已婚
        /// </summary>
        Married,
        /// <summary>
        /// 离异
        /// </summary>
        Divorced,
        /// <summary>
        /// 丧偶
        /// </summary>
        Widowed
    }

    public enum MilitaryState 
    {
        /// <summary>
        /// 未役
        /// </summary>
        NotService,
        /// <summary>
        /// 服役
        /// </summary>
        Service,
        /// <summary>
        /// 服役完毕
        /// </summary>
        FinishService
    }
}
