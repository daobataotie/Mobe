using System;
using System.Collections.Generic;
using System.Text;

namespace Tool.DBManager
{
    public abstract class DBMS
    {

        public abstract string Name
        {
            get;
        }

        public abstract string Description
        {
            get;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public abstract void CreateDatabase(System.Collections.Hashtable parameters);

        private static DBMS _access;
        private static DBMS _sqlserver;

        public static DBMS Access
        {
            get
            {
                if (_access == null)
                    _access = new Access();

                return _access;
            }
        }

        public static DBMS SQLServer
        {
            get
            {
                if (_sqlserver == null)
                    _sqlserver = new SQLServer();

                return _sqlserver;
            }
        }

    }
}
