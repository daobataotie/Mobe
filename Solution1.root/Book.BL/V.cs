using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class V
    {
        static V()
        {
            transactionController = (DA.ITransactionController)Accessors.Get("TransactionController");
        }
        private static IList<Model.Role> _role;
        private  static Model.SetDataFormat _setDataFormat;
        public static Model.SetDataFormat SetDataFormat
        { 
            get{return _setDataFormat;}
            set {_setDataFormat=value;} 
        }
        public static IList<Model.Role> RoleList
        {
            get { return _role; }
            set { _role = value; }
        }
        private static Model.Operators activeOperator;

        public static Model.Operators ActiveOperator
        {
            get { return activeOperator; }
            set { activeOperator = value; }
        }

        private static DA.ITransactionController transactionController;

        public static DA.ITransactionController TransactionController
        {
            get { return transactionController; }
            set { transactionController = value; }
        }

        public static void CommitTransaction()
        {
            transactionController.CommitTransaction();
        }
        public static void BeginTransaction()
        {
            transactionController.BeginTransaction();
        }

        public static void RollbackTransaction()
        {
            transactionController.RollbackTransaction();
        }
    }
}
