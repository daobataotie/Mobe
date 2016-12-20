//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：accessors.cs
// author: peidun
// create date：2008/6/16 10:27:45
//
//------------------------------------------------------------------------------
using System;
using System.Reflection;

namespace Book.BL
{
    /// <summary>
    /// 数据层工厂
    /// </summary>
    internal class Accessors
    {
      
		#region Static members
        static Accessors()
        {
            try
            {

                if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessorImplementionsLocation"] != null)
                    assemblyName = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessorImplementionsLocation"].Value;
               // assemblyName = "Book.DA.SQLServer";// System.Configuration.ConfigurationManager.AppSettings["accessorImplementionsLocation"];
            }
            catch
            {
            }
        }

        /// <summary>
        /// ??????????
        /// </summary>
        private static readonly string assemblyName;
		
		#endregion
		
		#region Static Methods
		
        /// <summary>
        /// ??????????
        /// </summary>
        internal static DA.IAccessor Get(string accessorName)
        {
            try
            {
                if (assemblyName == null) return null;

                System.Reflection.Assembly assembly = Assembly.Load(assemblyName);
                string typeName = string.Format("{0}.{1}", assemblyName, accessorName);            
                return (DA.IAccessor)assembly.CreateInstance(typeName);
                
            }
            catch(Exception ex) 
            {              
               throw ex;

            }
        }
		
		#endregion
    }
}

