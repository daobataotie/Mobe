using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Book.UI
{
    class Operations
    {
        delegate void D(string formname);
        delegate void D1(System.Windows.Forms.Form form);
        delegate void D2(System.Windows.Forms.Form form);
        delegate void D3(System.Windows.Forms.Form form);
        delegate void D5(System.Windows.Forms.Form form, object obj);
        //直接打开报表

        private static IDictionary<string, Delegate> operations;

        static Operations()

        {
            operations = new Dictionary<string, Delegate>();

            operations.Add("invoices.xo.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceXO", new Book.UI.Query.ConditionXChooseForm()); });
            
            //销售订单明细表
            operations.Add("invoices.xo.edit-detail1", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceXOlist", new Book.UI.Query.ConditionXChooseForm()); });
            operations.Add("invoices.co.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceCO", new Book.UI.Query.ConditionCOChooseForm()); });
            operations.Add("invoices.co.edit-detail1", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceCOlist", new Book.UI.Query.ConditionCOChooseForm()); });
            operations.Add("Query.OutDepotDetail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.OutDepotDetail", new Book.UI.Query.OutDepotForm()); });
            operations.Add("Query.InDepotDetail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.InDepotDetail", new Book.UI.Query.InDepotForm()); });
            operations.Add("Query.OutAndInDepotDetail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.OutAndInDepotDetail", new Book.UI.Query.OutAndInDepotForm()); });

            operations.Add("accountPayable.acInvoiceXOBill.editForm-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("AccountPayable.AccQuery.ROInvoiceXOBillDetail", new Book.UI.AccountPayable.AccQuery.ConditionAcInvoiceXOBillChooseForm()); });

            //应付账款明细账
            operations.Add("accountpayable.accquery.suppliermaychoose", (D1)delegate(System.Windows.Forms.Form form) { MM("AccountPayable.AccQuery.SupplierMayDetail", new Book.UI.AccountPayable.AccQuery.SupplierMayChooseForm()); });
            //应付账款明细表
            operations.Add("accountpayable.accquery.suppliermaychooseBiao", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceCGlistBiao", new Book.UI.Query.ConditionCOChooseForm()); });

            //应收账款明细账
            operations.Add("accountpayable.accquery.customermaychoose", (D1)delegate(System.Windows.Forms.Form form) { MM("AccountPayable.AccQuery.CustomerMayDetail", new Book.UI.AccountPayable.AccQuery.CustomerMayChooseForm()); });
            //应收账款明细表
            operations.Add("accountpayable.accquery.customermaychooseBiao", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceXSlistBiao", new Book.UI.Query.ConditionXChooseForm()); });


            operations.Add("accountpayable.accquery.colcuishou", (D1)delegate(System.Windows.Forms.Form form) { MM("AccountPayable.AccQuery.ColCuiShou", new Book.UI.AccountPayable.AccQuery.ChooseColCuiShouForm()); });

            operations.Add("produceManager.ProduceOtherExitReturnMaterial", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceOtherReturnMaterial.EditForm", form); });
            operations.Add("produceManager.produceOtherExitMaterial.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceOtherExitMaterial.EditForm", form); });
            operations.Add("produceManager.pronotePackage.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PronotePackage.EditForm", form); });
            operations.Add("accountPayable.acInvoiceCOBill.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcInvoiceCOBill.EditForm", form); });
            operations.Add("accountPayable.acInvoiceXOBill.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcInvoiceXOBill.EditForm", form); });

            operations.Add("accountPayable.acCollection.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcCollection.EditForm", form); });
            operations.Add("accountPayable.acPayment.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcPayment.EditForm", form); });
            operations.Add("accountPayable.acbeginbillReceivable.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcbeginbillReceivable.EditForm", form); });
            operations.Add("accountPayable.acbeginAccountPayable.editForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcbeginAccountPayable.EditForm", form); });
            //财务,项目设置
            operations.Add("AccountPayable.APParameterSet.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.APParameterSet.EditForm", form); });
            operations.Add("setting.stockLimitations.StockEditorForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.StockEditorForm", form); });
            operations.Add("setting.stockLimitations.depotInForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.DepotInForm", form); });
            operations.Add("setting.stockLimitations.outStockEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.OutStockEditForm", form); });
            operations.Add("settings.BasicData.companyeditform", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Company.CompanyEditForm", form); });
            operations.Add("produceManager.mouldCategory.productMouldTestEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MouldCategory.ProductsMouldTestEditForm", form); });
            operations.Add("produceManager.mouldCategory.productMouldEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MouldCategory.ProductMouldEditForm", form); });
            operations.Add("produceManager.mouldCategory.productMaterialEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MouldCategory.ProductMaterialEditForm", form); });
            operations.Add("produceManager.MouldCategory", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MouldCategory.EditForm", form); });
            operations.Add("produceManager.PronoteHeader.plan", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PronoteHeader.PlanForm", form); });
            operations.Add("produceManager.pronoteheader.produceprocedures", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PronoteHeader.ProduceProceduresForm", form); });


            //质检
            operations.Add("produceManager.PCImpactCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCImpactCheck.EditForm", form); });
            operations.Add("produceManager.PCFinishCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCFinishCheck.EditForm", form); });
            operations.Add("produceManager.PCOtherCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCOtherCheck.EditForm", form); });
            operations.Add("produceManager.ANSIPCImpactCheck.EditForm-ANSI", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.ANSIPCImpactCheck.EditForm-ANSI", form); });
            operations.Add("produceManager.PCDoubleImpactCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCDoubleImpactCheck.EditForm", form); });
            operations.Add("produceManager.PCPGOnlineCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCPGOnlineCheck.EditForm", form); });
            operations.Add("produceManager.PCParameter.PCParameterForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCParameterSet.EditForm", form); });
            operations.Add("produceManager.PCOpticsCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCOpticsCheck.EditForm", form); });
            operations.Add("produceManager.PCFogCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCFogCheck.EditForm", form); });
            operations.Add("produceManager.PCPenetrateCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCPenetrateCheck.EditForm", form); });
            operations.Add("produceManager.ExportSendMail.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ExportSendMail.EditForm", form); });
            operations.Add("produceManager.ExportSendMail.MailParameterSet", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ExportSendMail.MailParameterSet", form); });


            //外销报告
            operations.Add("produceManager.PCExportReportANSI.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.EditForm", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-MSJY", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-MSJY", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-QXD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-QXD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-LJDS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-LJDS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-KJGTSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-KJGTSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ZWXTSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ZWXTSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-QMDS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-QMDS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-SGDS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-SGDS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-GSCJCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-GSCJCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-YZZLZJCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-YZZLZJCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JPCTCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JPCTCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-WDCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-WDCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-NRXCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-NRXCS", form); });

            //CSA外销报告
            operations.Add("produceManager.PCExportReportANSI.CSAEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.CSAEditForm", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CSAWDCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CSAWDCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CSAGX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CSAGX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CSAQXD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CSAQXD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CSAPGPCL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CSAPGPCL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CSAKJGTSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CSAKJGTSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CSAGSCJCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CSAGSCJCS", form); });

            //CEEN外销报告
            operations.Add("produceManager.PCExportReportANSI.CEENEditsForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.CEENEditsForm", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENCONSTRUCTION", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENCONSTRUCTION", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENQMDS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENQMDS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENSGDS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENSGDS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENLJDS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENLJDS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENZB", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENZB", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENTSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENTSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENBMPZ", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENBMPZ", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENZSCJ", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENZSCJ", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENGSCJ", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENGSCJ", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENJH", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENJH", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENZX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENZX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-CEENUVCF", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-CEENUVCF", form); });

            //AS外销报告
            operations.Add("produceManager.PCExportReportANSI.ASEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.ASEditForm", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASNL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASNL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASCCSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASCCSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASWGCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASWGCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASJRWDX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASJRWDX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASZB", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASZB", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASQMDSZSD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASQMDSZSD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASWD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASWD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASZSCJSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASZSCJSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASGSCJSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASGSCJSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASTGCJSL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASTGCJSL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASCTCS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASCTCS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASNRX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASNRX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASNSX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASNSX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-ASJH", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-ASJH", form); });

            //JIS外销报告
            operations.Add("produceManager.PCExportReportANSI.JISEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.JISEditForm", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPWG", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPWG", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPLJD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPLJD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPQGD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPQGD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPSGD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPSGD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPTGL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPTGL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPNCCX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPNCCX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPBMNMHDK", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPBMNMHDK", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPNREX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPNREX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPNSX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPNSX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISJPNRAX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISJPNRAX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPWG", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPWG", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPNCCX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPNCCX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPJMXDYSY", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPJMXDYSY", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPJMXDESY", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPJMXDESY", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPTDQD", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPTDQD", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPNXDX", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPNXDX", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPGZ", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPGZ", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPCL", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPCL", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPJHBS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPJHBS", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPBZSJH", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPBZSJH", form); });
            operations.Add("produceManager.PCExportReportANSI.DetailsForm-JISWCPSYSC", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.DetailsForm-JISWCPSYSC", form); });

            #region HR   薪资
            //临时卡
            operations.Add("HR.Attendance.TempCardlist", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.TempCard.TempCardEdit", form); });
            //餐费管理
            operations.Add("HR.Attendance.Mealslist", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Meals.MealslistForm", form); });

            //年假管理
            operations.Add("HR.Attendance.AnnualHoliday", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.AnnualHoliday.AnnualHolidayForm", form); });
            //排班管理
            operations.Add("HR.Attendance.Flextime", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Flextime.FlextimelistForm", form); });
            //区段弹性排班管理
            operations.Add("hr.attendance.rangeflextime", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.FlecTime.FlexTimeListForm", form); });

            //休假管理
            operations.Add("HR.Attendance.Leavelist", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Leave.LeavelistForm", form); });
            operations.Add("HR.Attendance.LeaveType", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Leave.LeaveTypeForm", form); });
            #endregion

            //借出
            operations.Add("hr.attendance.lendrecord", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.LendRecord.LendRecordForm", form); });
            operations.Add("produceManager.PronoteMachine", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PronoteMachine.EditForm", form); });

            #region 加班
            operations.Add("hr.attendance.overTime", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.OverTime.OverTimeForm", form); });
            #endregion

            #region 打卡记录
            operations.Add("hr.attendance.clockdata", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.ClockData.ClockForm", form); });
            #endregion
            #region 考勤管理
            //出勤记录
            operations.Add("hr.attendance.atten", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Atten.AttenForm", form); });
            //异常出勤记录
            operations.Add("hr.attendance.atten1", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Atten.AnormalySalaryForm", form); });
            //重新考勤
            operations.Add("hr.attendance.Reatten", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Attendance.Atten.ReattenForm", form); });
            #endregion
            //列印月薪资
            operations.Add("setting.hr.salary.salaryset.CalCrystalReportForm1", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Salary.Salaryset.CalCrystalReportForm1", form); });
            //薪资计算
            operations.Add("HR.Salary.SalaryCalculation", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Salary.Salaryset.CalculationForm", form); });
            //薪资设定
            operations.Add("HR.Salaryset.SalarysetForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Salary.Salaryset.EditForm", form); });
            //人是参数 学历等
            operations.Add("HR.Parameter.ParameterListForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Hr.Parameter.HrParameterForm", form); });
            operations.Add("company.freightForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Company.FreightForm", form); });
            #region manufacting data

            #region basic data

            operations.Add("manufactdata.manufact_basicdata.pricinglist", (D1)delegate(System.Windows.Forms.Form form) { M1("ManufacturData.Manufact_basicdata.Pricing.ListForm", form); });
            //ManufacturData.Manufact_basicdate.Pricing.ListForm
            //xxForm
            //operations.Add("manufactdata.manufact_basicdata.workhouselist", (D1)delegate(System.Windows.Forms.Form form) { M1("ManufacturData.Manufact_basicdate.Workhouse.ListForm", form); });



            //产生生产计划

            operations.Add("producemanager.createproduce.editform", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.createProduce.EditForm", form); });
            //销售预测
            operations.Add("producemanager.invoicexosalesfor", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.InvoiceXOSalesFor.EditForm", form); });
            //工作中心
            operations.Add("Settings.ProduceManager.WorkHouse.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.WorkHouse.EditForm", form); });

            //工作中心日志
            operations.Add("Settings.ProduceManager.Workhouselog.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.Workhouselog.EditForm", form); });
            #endregion

            #endregion

            #region workflow
            //workflow manage
            operations.Add("workflow.workflowmanage.workflowlist", (D1)delegate(System.Windows.Forms.Form form) { M1("Workflow.workflowmanage.ListForm", form); });
            operations.Add("workflow.Tablem.tablelist", (D1)delegate(System.Windows.Forms.Form form) { M1("Workflow.Tablem.ListForm", form); });
            //审批工作
            operations.Add("workflow.workflowmanage.exam.myexamwork", (D1)delegate(System.Windows.Forms.Form form) { M1("Workflow.currentwork.PersonworkForm", form); });
            //待我审批
            operations.Add("workflow.workflowmanage.exam.myexaming", (D1)delegate(System.Windows.Forms.Form form) { M1("Workflow.currentwork.ExamForm", form); });

            //生产计划
            operations.Add("produceManager.MPSheader", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MPSheader.EditForm", form); });
            //物料需求??
            operations.Add("produceManager.MRSHeader", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MRSHeader.EditForm", form); });


            //生产通知
            operations.Add("produceManager.pronoteHeader.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PronoteHeader.EditForm", form); });

            //加工领料
            operations.Add("produceManager.ProduceMaterial", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceMaterial.EditForm", form); });

            //生产退料
            operations.Add("produceManager.ProduceMaterialExit", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceMaterialExit.EditForm", form); });

            //成品入庫
            operations.Add("produceManager.ProduceInDepot", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceInDepot.EditForm", form); });
            //----生产入库--- 生产不良率统计表
            operations.Add("produceManager.ProduceInDepot.ROProuceInDepotDefectRate", (D1)delegate(System.Windows.Forms.Form form) { MM("produceManager.ProduceInDepot.ROProuceInDepotDefectRate", new produceManager.ProduceInDepot.ChooseDefectRate()); });
            //----生产入库--- 商品不良率统计表
            operations.Add("produceManager.ProduceInDepot.ROProductDefectRate", (D1)delegate(System.Windows.Forms.Form form) { MM("produceManager.ProduceInDepot.ROProductDefectRate", new produceManager.ProduceInDepot.ChooseProductDefectRate()); });

            //外发合同
            operations.Add("produceManager.ProduceOtherCompact", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceOtherCompact.EditForm", form); });

            //外发领料
            operations.Add("produceManager.ProduceOtherMaterial", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceOtherMaterial.EditForm", form); });

            //外发退料
            operations.Add("produceManager.ProduceOtherExitMaterial", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceOtherExitMaterial.EditForm", form); });

            //外发入库
            operations.Add("produceManager.ProduceOtherInDepot", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceOtherInDepot.Editform", form); });




            //物料清单
            operations.Add("settings.producemanager.bommanagerform", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.BomEdit", form); });

            //物料清单一览
            operations.Add("settings.producemanager.bomlistform", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.BomList", form); });

            //物料类型
            operations.Add("settings.producemanager.materialtype", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.MaterialType.EditForm", form); });

            //工艺管理
            operations.Add("settings.producemanager.techonlogy", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.Techonlogy.EidtForm", form); });

            //工序管理
            operations.Add("settings.producemanager.techonlogys", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.Techonlogy.ProceduresEditForm", form); });
            //工序价格
            //  operations.Add("Settings.ProduceManager.ProceduresOther.ProceduresPriceEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.ProceduresOther.ProceduresPriceListForm", form); });

            //z转移单
            operations.Add("produceManager.Techonlogy.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.Techonlogy.EditForm", form); });
            //生产车间数量
            operations.Add("produceManager.ProduceStatistics.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceStatistics.EditForm", form); });
            operations.Add("produceManager.ProduceStatistics.ListForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceStatistics.ListForms", form); });
            operations.Add("produceManager.ProduceStatistics.EditForm1", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceStatistics.EditForm1", form); });
            //生产车间质检
            operations.Add("produceManager.ProduceStatisticsCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceStatisticsCheck.EditForm", form); });
            operations.Add("produceManager.ProduceStatisticsCheck.ListForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceStatisticsCheck.ListForm", form); });
            //工艺与工序关系明细

            operations.Add("settings.producemanager.techonlogy2", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.Techonlogy.TechnologydetailsEditFrom", form); });
            //物料加工工序           

            operations.Add("settings.producemanager.manprocedureprocess", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.ManProcedureProcess.EditForm", form); });
            #endregion
            operations.Add("settings.basicdata.list1form", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Products.List1Form", form); });
            //客户包装信息
            operations.Add("settings.basicdata.customs.customerpackage", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.CustomerPackage.EditForm", form); });
            //加工组
            operations.Add("settings.basicdata.customs.customerprocessing", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.CustomerProcessing.EditForm", form); });


            operations.Add("settings.basicdata.incomingkinds", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.IncomingKinds.EditForm", form); });
            operations.Add("settings.basicdata.outgoingkinds", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.OutgoingKinds.EditForm", form); });
            operations.Add("settings.basicdata.depots", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Depots.AddDepotpositionForm", form); });
            operations.Add("settings.basicdata.productcategories", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.ProductCategories.EditForm", form); });
            operations.Add("settings.basicdata.accounts", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Accounts.EditForm", form); });
            operations.Add("settings.basicdata.employees", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Employees.EditForm", form); });
            operations.Add("settings.basicdata.products", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Products.EditForm", form); });
            operations.Add("settings.basicdata.companylevels", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.CompanyLevels.ListForm", form); });
            operations.Add("settings.basicdata.paymethods", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.PayMethods.ListForm", form); });
            operations.Add("settings.basicdata.DepotPosition", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Depots.AddDepotpositionForm", form); });
            //库存盘点
            operations.Add("setting.stockLimitations.editform", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.EditForm", form); });
            //库存盘点差异表
            operations.Add("setting.stockLimitations.stockdiff", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.StockCheckForm", form); });

            //加工类型
            operations.Add("customsClearance.bGHandbook", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGHandbookForm", form); });


            operations.Add("settings.basicdata.packagetype", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.PackageType.ListForm", form); });

            operations.Add("settings.basicdata.ProductUnit", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.ProductUnit.ListForm", form); });
            operations.Add("settings.basicdata.UnitGroup", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.UnitGroup.EditForm", form); });
            operations.Add("settings.basicdata.bank", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Bank.EditForm", form); });
            operations.Add("settings.basicdata.AcademicBackGround", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.AcademicBackGround.EditForm", form); });
            operations.Add("settings.basicdata.company", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Company.EditForm", form); });
            operations.Add("settings.basicdata.businesshours", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.BusinessHours.EditForm", form); });

            operations.Add("settings.basicData.processcategory.editform", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.ProcessCategory.EditForm", form); });
            operations.Add("settings.basicData.customs.processing.editform", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.Processing.ProcessingForm", form); });


            operations.Add("Settings.BasicDate.ceshi", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.csshi", form); });
            operations.Add("settings.basicData.customs.customerproduct.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.CustomerProductForm", form); });

            operations.Add("settings.producemanager.bomedit", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProduceManager.BomEdit", form); });
            operations.Add("settings.basicdata.freightedcompany", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.FreightedCompany.ListForm", form); });
            //客户
            operations.Add("settings.privileges.roles.legalPowerEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.Privileges.Roles.LegalPowerEditForm", form); });
            operations.Add("settings.basicdata.customers", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.EditForm", form); });
            operations.Add("settings.basicdata.suppliers", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Supplier.EditForm", form); });
            //供应商商品/加工对照
            operations.Add("settings.basicdata.SupplierProductProcesscategoryEdit", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Supplier.SupplierProductProcesscategoryEdit", form); });


            operations.Add("settings.basicdata.duty", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Duty.EditForm", form); });
            operations.Add("settings.basicdata.suppliercategory", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.SupplierCategory.EditForm", form); });
            operations.Add("settings.basicdata.department", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Department.EditForm", form); });
            operations.Add("settings.basicdata.custominspectionrule", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.CustomInspectionRule.ListForm", form); });
            operations.Add("settings.basicdata.qualitytestplan", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.QualityTestPlan.ListForm", form); });

            operations.Add("settings.periodbeginning.accountsbalance", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.PeriodBeginning.AccountsBalance.ListForm", form); });
            operations.Add("settings.periodbeginning.productcost", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.PeriodBeginning.ProductCost.ListForm", form); });
            operations.Add("settings.periodbeginning.r", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.PeriodBeginning.RP.ListFormR", form); });
            operations.Add("settings.periodbeginning.p", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.PeriodBeginning.RP.ListFormP", form); });
            operations.Add("settings.periodbeginning.stock", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.PeriodBeginning.Stock.ListForm", form); });
            operations.Add("settings.profitmargin", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.ProfitMargin.MainForm", form); });
            operations.Add("settings.stocklimitations", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.MainForm", form); });


            operations.Add("settings.privileges.operators", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.Privileges.Operators.MainForm", form); });
            operations.Add("settings.privileges.roles", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.Privileges.Roles.MainForm", form); });
            operations.Add("settings.options", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.Options.MainForm", form); });
            operations.Add("companyinfo", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.Options.CompanyInfo", form); });
            //功能设置
            operations.Add("settings.options.SettingDataFormat", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.Options.SettingDataFormat", form); });


            operations.Add("invoices.cj.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CJ.ListForm", form); });
            operations.Add("invoices.co.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CO.ListForm", form); });
            operations.Add("invoices.cf.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CF.ListForm", form); });
            operations.Add("invoices.cg.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CG.ListForm", form); });
            operations.Add("invoices.ct.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CT.ListForm", form); });
            operations.Add("invoices.xj.xsform", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XJ.XSForm", form); });
            operations.Add("invoices.xj.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XJ.ListForm", form); });
            operations.Add("invoices.xo.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XO.ListForm", form); });
            operations.Add("invoices.xs.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XS.ListForm", form); });
            operations.Add("invoices.xt.edit-list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XT.ListForm", form); });
            operations.Add("invoices.bs.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.BS.ListForm", form); });
            operations.Add("invoices.by.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.BY.ListForm", form); });
            operations.Add("invoices.hz.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.HZ.ListForm", form); });
            operations.Add("invoices.zs.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZS.ListForm", form); });
            operations.Add("invoices.pt.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.PT.ListForm", form); });
            operations.Add("invoices.zz.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZZ.ListForm", form); });
            operations.Add("invoices.fk.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.FK.ListForm", form); });
            operations.Add("invoices.sk.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.SK.ListForm", form); });
            operations.Add("invoices.ft.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.FT.ListForm", form); });
            operations.Add("invoices.qi.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.QI.ListForm", form); });
            operations.Add("invoices.qo.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.QO.ListForm", form); });
            operations.Add("invoices.qk.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.QK.ListForm", form); });
            operations.Add("invoices.jr.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.JR.ListForm", form); });
            operations.Add("invoices.jc.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.JC.ListForm", form); });
            operations.Add("invoices.hr.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.HR.ListForm", form); });
            operations.Add("invoices.hc.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.HC.ListForm", form); });
            operations.Add("invoices.pi.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.PI.ListForm", form); });
            operations.Add("invoices.po.list", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.PO.ListForm", form); });




            operations.Add("query.q01", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.InvoicesListForm", form); });
            operations.Add("query.q02", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q02Form", form); });
            operations.Add("query.q03", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q03Form", form); });
            operations.Add("query.q04", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q04Form", form); });
            operations.Add("query.q05", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q05Form", form); });
            operations.Add("query.q06", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q06Form", form); });
            operations.Add("query.q07", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q07Form", form); });
            operations.Add("query.q08", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q08Form", form); });
            operations.Add("query.q09", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q09Form", form); });
            operations.Add("query.q10", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q10Form", form); });
            operations.Add("query.q11", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q11Form", form); });
            operations.Add("query.q12", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q12Form", form); });
            operations.Add("query.q13", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q13Form", form); });
            operations.Add("query.q14", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q14Form", form); });

            operations.Add("Q15", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q15JiShiForm", form); });

            operations.Add("query.q15", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q15Form", form); });
            //operations.Add("query.q50", (D1)delegate(System.Windows.Forms.Form form) { M3("Query.Q50Form", form); });
            operations.Add("invoices.xo.edit1", (D5)delegate(System.Windows.Forms.Form form, object obj) { M5("Invoices.XO.EditForm", form, obj); });
            operations.Add("invoices.xs.edit1", (D5)delegate(System.Windows.Forms.Form form, object obj) { M5("Invoices.XS.EditForm", form, obj); });
            //采购订单
            operations.Add("invoices.co.edit1", (D5)delegate(System.Windows.Forms.Form form, object obj) { M5("Invoices.CO.EditForm", form, obj); });

            operations.Add("invoices.cg.edit1", (D5)delegate(System.Windows.Forms.Form form, object obj) { M5("Invoices.CG.EditForm", form, obj); });

            operations.Add("invoices.cj.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CJ.EditForm", form); });
            operations.Add("invoices.cf.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CF.EditForm", form); });

            operations.Add("invoices.cg.cgForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CO.CGForm", form); });
            operations.Add("invoices.co.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CO.EditForm", form); });
            operations.Add("invoices.cg.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CG.EditForm", form); });
            operations.Add("invoices.ct.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.CT.EditForm", form); });
            operations.Add("invoices.xj.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XJ.EditForm", form); });
            operations.Add("invoices.xo.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XO.EditForm", form); });
            operations.Add("invoices.xt.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XT.EditForm", form); });

            //明细报表
            //operations.Add("invoices.cg.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q16", new Book.UI.Query.ConditionCOChooseForm()); });
            operations.Add("invoices.cg.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceCGlist", new Book.UI.Query.ConditionCOChooseForm()); });


            operations.Add("invoices.xs.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROInvoiceXSlist2", new Book.UI.Query.ConditionXChooseForm()); });



            operations.Add("invoices.xs.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.XS.EditForm", form); });
            operations.Add("invoices.bs.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.BS.EditForm", form); });
            operations.Add("invoices.by.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.BY.EditForm", form); });
            operations.Add("invoices.hz.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.HZ.EditForm", form); });
            operations.Add("invoices.zs.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZS.EditForm", form); });
            operations.Add("invoices.pt.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.PT.EditForm", form); });
            operations.Add("invoices.zz.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZZ.EditForm", form); });
            operations.Add("invoices.fk.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.FK.EditForm", form); });
            operations.Add("invoices.sk.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.SK.EditForm", form); });
            operations.Add("invoices.ft.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.FT.EditForm", form); });
            operations.Add("invoices.qi.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.QI.EditForm", form); });
            operations.Add("invoices.qo.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.QO.EditForm", form); });
            operations.Add("invoices.qk.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.QK.EditForm", form); });

            operations.Add("invoices.pi.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.PI.EditForm", form); });
            operations.Add("invoices.po.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.PO.EditForm", form); });
            operations.Add("invoices.jr.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.JR.EditForm", form); });
            operations.Add("invoices.jc.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.JC.EditForm", form); });
            operations.Add("invoices.hr.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.HR.EditForm", form); });
            operations.Add("invoices.hc.edit", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.HC.EditForm", form); });

            operations.Add("changemypassword", (D1)delegate(System.Windows.Forms.Form form) { M2("General.ChangeMyPasswordForm", form); });
            operations.Add("about", (D1)delegate(System.Windows.Forms.Form form) { M2("General.AboutBox", form); });


            //明细报表
            operations.Add("produceManager.pronotePackage.ro", (D1)delegate(System.Windows.Forms.Form form) { MM("produceManager.PronotePackage.RO", new Book.UI.Query.ConditionAChooseForm()); });

            operations.Add("invoices.ct.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q17", new Book.UI.Query.ConditionAChooseForm()); });
            operations.Add("query.q18", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q18", new Book.UI.Query.ConditionAChooseForm()); });
            operations.Add("query.q19", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q19", new Book.UI.Query.ConditionAChooseForm()); });
            operations.Add("settings.stockLimitations.stockDiff", (D1)delegate(System.Windows.Forms.Form form) { MM("Settings.StockLimitations.StockDiffReport", new Book.UI.Query.ConditionAChooseForm()); });

            operations.Add("query.q21", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q21", new Book.UI.Query.ConditionAChooseForm()); });
            operations.Add("query.q22", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q22", new Book.UI.Query.ConditionAChooseForm()); });
            operations.Add("invoices.xt.edit-detail", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q23", new Book.UI.Query.ConditionAChooseForm()); });
            //  operations.Add("query.q24", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q24", new Book.UI.Query.ConditionDChooseForm()); });//m7
            operations.Add("query.q25", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q25", new Book.UI.Query.ConditionAChooseForm()); });

            operations.Add("query.q26", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q26", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Supplier)); });
            operations.Add("query.q27", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q27", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Customer)); });
            operations.Add("query.q28", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q28", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Supplier)); });

            operations.Add("query.q29", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q29", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Customer)); });
            operations.Add("query.q30", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q30", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Customer)); });
            operations.Add("query.q31", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q31", new Book.UI.Query.ConditionHChooseForm()); });
            operations.Add("query.q32", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q32", new Book.UI.Query.ConditionAChooseForm()); });
            operations.Add("query.q33", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q33", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Customer)); });
            operations.Add("query.q34", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q34", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Supplier)); });
            operations.Add("query.q35", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q35", new Book.UI.Query.ConditionKChooseForm()); });
            operations.Add("query.q36", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q36", new Book.UI.Query.ConditionIChooseForm(Helper.CompanyKind.Customer)); });
            // operations.Add("query.q37", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q37", new Book.UI.Query.ConditionJChooseForm()); });
            //  operations.Add("query.q38", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q38", new Book.UI.Query.ConditionJChooseForm()); });
            operations.Add("query.q39", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q39", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Supplier)); });
            // operations.Add("query.q40", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q40", new Book.UI.Query.ConditionEChooseForm(Helper.CompanyKind.Supplier)); });
            // operations.Add("query.q41", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q41", new Book.UI.Query.ConditionEChooseForm(Helper.CompanyKind.Customer)); });
            operations.Add("query.q43", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q43", new Book.UI.Query.ConditionGChooseForm()); });
            //  operations.Add("query.q44", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q44", new Book.UI.Query.ConditionJChooseForm()); });
            operations.Add("query.q46", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q46", new Book.UI.Query.DepotPositionListForm()); });
            operations.Add("query.q47", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q47", new Book.UI.Query.ConditionStockByProductForm()); });
            operations.Add("query.q48", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q48", new Book.UI.Query.ConditionMPShooseForm()); });
            operations.Add("query.q49", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q49", new Book.UI.Query.ConditionPronoteHeaderChooseForm()); });
            operations.Add("query.q50", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q50", new Book.UI.Query.ConditionMaterialChooseForm()); });
            //operations.Add("query.q51", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q51", new Book.UI.Query.ConditionMaterialChooseForm()); });
            //生产退料明细
            operations.Add("query.q51", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q51", new Book.UI.produceManager.ProduceMaterialExit.ConditionForList()); });
            operations.Add("query.q59", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q59", new Book.UI.Query.ConditionMRSChooseForm()); });
            operations.Add("query.ROMRSDetailsList", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ROMRSDetailsList", new Book.UI.Query.ConditionMRSChooseForm()); });
            operations.Add("query.conditionStockCheckReport", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.ConditionStockCheckReport", new Book.UI.Query.ConditionStockCheckChooseForm()); });
            operations.Add("query.q52", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q52", new Book.UI.Query.ConditionProInDepotChooseForm()); });
            //厂商交易排行
            operations.Add("query.suppliertransactionsrank", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.SupplierTransactionsRank", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Supplier)); });
            //客户交易排行
            operations.Add("query.customertransactionsrank", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.CustomerTransactionsRank", new Book.UI.Query.ConditionFChooseForm(Helper.CompanyKind.Customer)); });

            //operations.Add("checkDepot", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q45", new Book.UI.Query.ConditionLChooseForm()); });
            operations.Add("query.q53", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q53", new Book.UI.Query.ConditionOtherCompactChooseForm()); });
            operations.Add("query.q54", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q54", new Book.UI.Query.ConditionOtherMaterialChooseForm()); });
            operations.Add("query.q55", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q55", new Book.UI.Query.ConditionOtherExitChooseForm()); });
            operations.Add("query.q56", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q56", new Book.UI.Query.ConditionOtherInDepotChooseForm()); });
            //operations.Add("query.q57", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q57", new Book.UI.Query.ConditionProduceStatisticsChooseForm()); });
            //operations.Add("query.q58", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.Q58", new Book.UI.Query.ConditionProduceStatisticsCheckChooseForm()); });

            //应付、应收结算报表
            operations.Add("query.AcPayment", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.AcPaymentRO", new Book.UI.Query.ConditionAcPaymentChooseForm()); });
            operations.Add("query.AcCollection", (D1)delegate(System.Windows.Forms.Form form) { MM("Query.AcCollectionRO", new Book.UI.Query.ConditionAcCollectionChooseForm()); });

            //会计
            //日记账
            operations.Add("Accounting.Report.XRAdiary", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRAdiary", new Book.UI.Accounting.Report.ConditionAdiaryChooseForm()); });

            operations.Add("Accounting.Report.XRYS1", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRYS1", new Book.UI.Accounting.Report.ConditionYS1ChooseForm()); });
            operations.Add("Accounting.Report.XRYS2", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRYS2", new Book.UI.Accounting.Report.ConditionYS2ChooseForm()); });

            operations.Add("Accounting.Report.XRYF1", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRYF1", new Book.UI.Accounting.Report.ConditionYF1ChooseForm()); });
            operations.Add("Accounting.Report.XRYF2", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRYF2", new Book.UI.Accounting.Report.ConditionYF2ChooseForm()); });
            operations.Add("Accounting.Report.XRNCashYS", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRNCashYS", new Book.UI.Accounting.Report.ConditionNCashYSChooseForm()); });
            operations.Add("Accounting.Report.XRNCashYF", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRNCashYF", new Book.UI.Accounting.Report.ConditionNCashYFChooseForm()); });
            operations.Add("Accounting.Report.XRBankSaveUp", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRBankSaveUp", new Book.UI.Accounting.Report.ConditionBankSaveUpChooseForm()); });
            operations.Add("Accounting.Report.XRSubjectProperty", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRSubjectProperty", new Book.UI.Accounting.Report.ConditionSubjectPropertyChooseForm()); });
            operations.Add("Accounting.Report.XRDateProperty", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRDateProperty", new Book.UI.Accounting.Report.ConditionDatePropertyChooseForm()); });
            operations.Add("Accounting.Report.XRProperty", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRProperty", new Book.UI.Accounting.Report.ConditionPropertyChooseForm()); });
            operations.Add("Accounting.Report.XRCashDebt", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRCashDebt", new Book.UI.Accounting.Report.ConditionCashDebtChooseForm()); });
            operations.Add("Accounting.Report.XRGeneralAccount", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRGeneralAccount", new Book.UI.Accounting.Report.ConditionGeneralAccountChooseForm()); });
            operations.Add("Accounting.Report.XRTayTo", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRTayTo", new Book.UI.Accounting.Report.ConditionTryToChooseForm()); });
            operations.Add("Accounting.Report.XRPeriodTryTo", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRPeriodTryTo", new Book.UI.Accounting.Report.ConditionPeriodTryToChooseForm()); });
            operations.Add("Accounting.Report.XRProfitLoss", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRProfitLoss", new Book.UI.Accounting.Report.ConditionProfitLossChooseForm()); });

            operations.Add("Accounting.Report.XRPropertyDebt", (D1)delegate(System.Windows.Forms.Form form) { MM("Accounting.Report.XRPropertyDebt", new Book.UI.Accounting.Report.ConditionPropertyDebtChooseForm()); });

            operations.Add("FlowChart", (D1)delegate(System.Windows.Forms.Form form) { M1("FlowChartForm", form); });
            operations.Add("exit", (D1)delegate(System.Windows.Forms.Form form) { Application.Exit(); });



            //erp
            operations.Add("Erp.Crm.CRMMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Crm.CRMMainForm", form); });
            operations.Add("Erp.CashBank.CashBankMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Crm.CRMMainForm", form); });
            operations.Add("Erp.ExportStock.ExportStockMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.ExportStock.ExportStockMainForm", form); });
            operations.Add("Erp.FixedProperty.FixedPropertyMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.FixedProperty.FixedPropertyMainForm", form); });
            operations.Add("Erp.ImportStock.ImportStockMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.ImportStock.ImportStockMainForm", form); });
            operations.Add("Erp.Produce.ProduceMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Produce.ProduceMainForm", form); });
            operations.Add("Erp.ProduceCost.ProduceCostMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.ProduceCost.ProduceCostMainForm", form); });
            operations.Add("Erp.ReceivableDue.ReceivableDueMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.ReceivableDue.ReceivableDueMainForm", form); });
            operations.Add("Erp.Salary.SalaryMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Salary.SalaryMainForm", form); });
            operations.Add("Erp.Stock.StockMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Stock.StockMainForm", form); });
            operations.Add("Erp.Technology.TechnologyMainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Technology.TechnologyMainForm", form); });
            operations.Add("Erp.Mainform", (D1)delegate(System.Windows.Forms.Form form) { M1("Erp.Mainform", form); });

            operations.Add("settings.BasicData.company", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Company.EditForm", form); });

            //会计管理
            operations.Add("Accounting.AccountingCategories.ListForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AccountingCategories.ListForm", form); });
            operations.Add("Accounting.AccountingCategory.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AccountingCategory.EditForm", form); });
            operations.Add("Accounting.AtAccountSubject.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtAccountSubject.EditForm", form); });

            operations.Add("Accounting.InvoiceSet.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.InvoiceSet.EditForm", form); });
            operations.Add("Accounting.AtProject.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtProject.EditForm", form); });
            operations.Add("Accounting.AtProperty.ListForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtProperty.ListForm", form); });

            operations.Add("Accounting.AtDepreciationDetail.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtDepreciationDetail.EditForm", form); });

            operations.Add("Accounting.AtBankAccount.ListForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtBankAccount.ListForm", form); });

            operations.Add("Accounting.AtBillsIncome.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtBillsIncome.EditFormo", form); });
            operations.Add("Accounting.AtBillsIncome.EditForm2", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtBillsIncome.EditForm2", form); });

            operations.Add("Accounting.AtBankSaveUp.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtBankSaveUp.EditForm", form); });

            operations.Add("Accounting.AtBankTransfer.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtBankTransfer.EditForm", form); });

            operations.Add("Accounting.AtAccountSubject.TheirBalanceForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtAccountSubject.TheirBalanceForm", form); });

            operations.Add("Accounting.CurrencyCategory.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.CurrencyCategory.EditForm", form); });

            operations.Add("Accounting.AtSummon.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtSummon.EditForm", form); });
            operations.Add("Accounting.BankBill.BillCollectionForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.BankBill.BillCollectionForm", form); });
            operations.Add("Accounting.BankBill.BillStampForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.BankBill.BillStampForm", form); });
            operations.Add("Accounting.BankBill.BillsToCash", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.BankBill.BillsToCash", form); });
            operations.Add("Accounting.BankBill.BackleToCashForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.BankBill.BackleToCashForm", form); });
            operations.Add("Accounting.ProfitLoss.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.ProfitLoss.EditForm", form); });
            operations.Add("Accounting.PropertyDebt.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.PropertyDebt.EditForm", form); });
            operations.Add("Accounting.CurrencyCategory.CategoryEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.CurrencyCategory.CategoryEditForm", form); });

            operations.Add("AccountPayable.AcOtherShouldCollection.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcOtherShouldCollection.EditForm", form); });
            operations.Add("AccountPayable.AcOtherShouldPayment.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcOtherShouldPayment.EditForm", form); });
            operations.Add("AccountPayable.AcOtherShouldPayment.MaintainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcOtherShouldPayment.MaintainForm", form); });
            operations.Add("AccountPayable.AcOtherShouldCollection.MaintainForm", (D1)delegate(System.Windows.Forms.Form form) { M1("AccountPayable.AcOtherShouldCollection.MaintainForm", form); });

            //会计参数设定
            operations.Add("Accounting.AtParameterSet.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Accounting.AtParameterSet.EditForm", form); });


            //客户一览表
            operations.Add("settings.basicdata.ROcustomerlist", (D1)delegate(System.Windows.Forms.Form form) { M6("Settings.BasicData.ROcustomerlist"); });
            //供应商一览表
            operations.Add("settings.basicdata.ROsupplierlist", (D1)delegate(System.Windows.Forms.Form form) { M6("Settings.BasicData.ROsupplierlist"); });


            //出货装箱 
            operations.Add("Invoices.ZX.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZX.EditForm", form); });

            //箱/柜 参数设置
            operations.Add("Invoices.ZX.ZXParameterSet", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZX.ZXParameterSet", form); });

            //订单装箱
            operations.Add("Invoices.ZX.PackingForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZX.PackingForm", form); });

            //箱子装柜
            operations.Add("Invoices.ZG.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZG.EditForm", form); });
            //装柜搜索
            //operations.Add("Invoices.ZG.List", (D1)delegate(System.Windows.Forms.Form form) { M1("Invoices.ZG.List", form); });

            //ANSI外销报告详细
            operations.Add("produceManager.PCExportReportANSI.ListDetail-ANSI", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.ListDetail-ANSI", form); });
            //CSA外销报告详细
            operations.Add("produceManager.PCExportReportANSI.ListDetail-CSA", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.ListDetail-CSA", form); });
            //CEEN外销报告详细
            operations.Add("produceManager.PCExportReportANSI.ListDetail-CEEN", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.ListDetail-CEEN", form); });
            //AS外销报告详细
            operations.Add("produceManager.PCExportReportANSI.ListDetail-AS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.ListDetail-AS", form); });
            //JIS外销报告详细
            operations.Add("produceManager.PCExportReportANSI.ListDetail-JIS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCExportReportANSI.ListDetail-JIS", form); });

            //JIS衝擊測試單
            operations.Add("produceManager.ANSIPCImpactCheck.EditForm-JIS", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.ANSIPCImpactCheck.EditForm-JIS", form); });

            //框脚检验单
            operations.Add("produceManager.PCBoxFootCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCBoxFootCheck.EditForm", form); });

            //设置-产品规格

            operations.Add("produceManager.MouldCategory.ProductMouldSize", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MouldCategory.ProductMouldSize", form); });

            //成品类别
            operations.Add("produceManager.MouldCategory.ProductCategory", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.MouldCategory.ProductCategory", form); });

            //客户产品价格设置
            operations.Add("Settings.BasicData.Customs.CustomerProductPrice", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.CustomerProductPrice", form); });

            //报关系统
            operations.Add("CustomsClearance.MyRecord.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.MyRecord.EditForm", form); });
            operations.Add("CustomsClearance.PassNoteBook.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.PassNoteBook.EditForm", form); });
            //手册归类
            operations.Add("CustomsClearance.HandbookRange.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.HandbookRange.EditForm", form); });

            operations.Add("settings.BasicData.ProductCategories.Material", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.ProductCategories.Material", form); });

            operations.Add("Settings.BasicData.Products.ProductConvertMaterial", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Products.ProductConvertMaterial", form); });

            //库存预警
            operations.Add("StockPrompt", (D1)delegate(System.Windows.Forms.Form form) { M1("StockPrompt", form); });

            operations.Add("CustomsClearance.BGhandbookIdSet", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGhandbookIdSet", form); });

            operations.Add("produceManager.PCClarityCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCClarityCheck.EditForm", form); });
            operations.Add("produceManager.PCEarPressCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCEarPressCheck.EditForm", form); });

            operations.Add("produceManager.PCEarProtectCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCEarProtectCheck.EditForm", form); });

            //AS耳压外销报告
            operations.Add("produceManager.PCExportReportANSI.EarPressEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.EarPressEditForm", form); });
            operations.Add("produceManager.PCEarPressCheck.EditForm-Report", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCEarPressCheck.EditForm-Report", form); });

            operations.Add("produceManager.PCEarProtectCheck.EditForm-Report", (D1)delegate(System.Windows.Forms.Form form) { M7("produceManager.PCEarProtectCheck.EditForm-Report", form); });

            //手册料件进口转入
            operations.Add("CustomsClearance.BGHandbookDepotInForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGHandbookDepotInForm", form); });
            //手册料件转出
            operations.Add("CustomsClearance.BGHandbookDepotOutForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGHandbookDepotOutForm", form); });
            //手册成品出货单
            operations.Add("CustomsClearance.BGProductDepotOutForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGProductDepotOutForm", form); });
            //手册预警
            operations.Add("CustomsClearance.BGPromptForm", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGPromptForm", form); });
            //JIS出货报告
            operations.Add("produceManager.PCExportReportANSI.JISOutPutFile", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.JISOutPutFile", form); });
            //入料检验单
            operations.Add("produceManager.PCIncomingCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCIncomingCheck.EditForm", form); });

            //物料检验单
            operations.Add("produceManager.PCMaterialCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCMaterialCheck.EditForm", form); });

            //品管抽检日报表
            operations.Add("produceManager.PCSampling.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCSampling.EditForm", form); });
            //品管抽检日报表(耳护类)
            operations.Add("produceManager.PCSamplingEar.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCSamplingEar.EditForm", form); });

            //长期未出仓商品
            operations.Add("Settings.StockLimitations.NoDepotOutProducts", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.NoDepotOutProducts", form); });

            //ANSI2015外销报告
            operations.Add("produceManager.PCExportReportANSI.ANSI2015", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.ANSI2015", form); });

            //雪鏡專用ASTM外銷報告
            operations.Add("produceManager.PCExportReportANSI.XuejingASTMForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.XuejingASTMForm", form); });

            //雪鏡專用EN外銷報告
            operations.Add("produceManager.PCExportReportANSI.XuejingENForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.XuejingENForm", form); });

            //商品关键字分类
            operations.Add("Settings.BasicData.Products.ProductClassifyForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Products.ProductClassifyForm", form); });

            //模具上线检验单
            operations.Add("produceManager.PCMouldOnlineCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCMouldOnlineCheck.EditForm", form); });

            //产品上线检验单  对应易达的 模具上线检验单
            operations.Add("produceManager.ProductOnlineCheck.Editform", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProductOnlineCheck.Editform", form); });

            //入料检验单(新)
            operations.Add("produceManager.PCInputCheck.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCInputCheck.EditForm", form); });

            //即时库存(现场版)
            operations.Add("Query.ImmediateStock", (D1)delegate(System.Windows.Forms.Form form) { M1("Query.ImmediateStock", form); });

            //现场库存
            operations.Add("Query.SceneStock", (D1)delegate(System.Windows.Forms.Form form) { M1("Query.SceneStock", form); });

            //区间查询入库单/换算原料
            operations.Add("produceManager.ProduceInDepot.CountMaterialForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.ProduceInDepot.CountMaterialForm", form); });

            //进出仓明细（Excel版）
            operations.Add("Query.OutAndInDepotForExcelForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Query.OutAndInDepotForExcelForm", form); });

            //组装现场盘点录入
            operations.Add("Settings.StockLimitations.AssemblySiteInventoryForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.AssemblySiteInventoryForm", form); });

            //组装现场盘点差异
            operations.Add("Settings.StockLimitations.AssemblySiteDifferenceForm", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.StockLimitations.AssemblySiteDifferenceForm", form); });

            //AS2017外销报告
            operations.Add("produceManager.PCExportReportANSI.ASEditForm2017", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.ASEditForm2017", form); });

            //手册料件出货明细
            operations.Add("CustomsClearance.BGProductOutDetail", (D1)delegate(System.Windows.Forms.Form form) { M1("CustomsClearance.BGProductOutDetail", form); });

            //阻燃性测试表
            operations.Add("produceManager.PCFlameRetardant.EditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCFlameRetardant.EditForm", form); });

            //仓库待转区库存
            operations.Add("Query.PendingAreaStock", (D1)delegate(System.Windows.Forms.Form form) { M1("Query.PendingAreaStock", form); });

            //客戶商品年度出貨查詢
            operations.Add("Book.UI.Settings.BasicData.Customs.AnnualShipment", (D1)delegate(System.Windows.Forms.Form form) { M1("Settings.BasicData.Customs.AnnualShipmentByCustomer", form); });  
            //EN耳护外销报告
            operations.Add("produceManager.PCExportReportANSI.ENEarProtectEditForm", (D1)delegate(System.Windows.Forms.Form form) { M1("produceManager.PCExportReportANSI.ENEarProtectEditForm", form); });

        }

        static Form CreateForm(string formTypeName)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return (Form)assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, formTypeName));
        }

        static Form CreateForm(string formTypeName, object[] args)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return (Form)assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, formTypeName), false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        static Type GetType(string formTypeName)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return assembly.GetType(string.Format("{0}.{1}", assembly.GetName().Name, formTypeName));
        }

        static void M1(string formname, System.Windows.Forms.Form associatedForm)
        {
            Form f = null;
            foreach (Form form in associatedForm.MdiChildren)
            {
                if (form.GetType().FullName.EndsWith(formname))
                {
                    f = form;
                    break;
                }
            }


            if (f == null)
            {
                Type t = GetType(formname);

                MethodInfo method = t.GetMethod("GetChooseSuppliersForm");
                if (method != null)
                {
                    Invoices.CG.ChooseSuppliers supplier = (Invoices.CG.ChooseSuppliers)method.Invoke(null, null);
                    if (supplier == null || supplier.ShowDialog() != DialogResult.OK)
                        return;

                    Invoices.CG.CGForm cgf = supplier.GetCgForm();

                    if (cgf == null || cgf.ShowDialog() != DialogResult.OK) return;

                    f = CreateForm(formname, new object[] { cgf.Invoice });
                    goto Next;
                }

                //method = t.GetMethod("GetChooseCustomerForm");

                //if (method != null)
                //{
                //    Invoices.XS.ChooseCustomerForm customer = (Invoices.XS.ChooseCustomerForm)method.Invoke(null, null);
                //    if (customer == null || customer.ShowDialog() != DialogResult.OK)
                //        return;
                //    Invoices.XS.XSForm xsf = customer.GetXSForm();
                //    if (xsf == null || xsf.ShowDialog() != DialogResult.OK)
                //        return;
                //    f = CreateForm(formname, new object[] { xsf.Invoice });
                //    goto Next;
                //}

                f = CreateForm(formname);
                goto Next;
            }
        Next:
            foreach (Form item in Application.OpenForms)
            {
                if (item.GetType() == f.GetType())
                {
                    item.Activate();
                    item.BringToFront();
                    item.WindowState = FormWindowState.Normal;
                    return;
                }
            }

            f.MdiParent = associatedForm;
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            f.BringToFront();
        }

        static void M2(string formname, System.Windows.Forms.Form form)
        {
            Form f = CreateForm(formname);
            f.ShowDialog(form);
        }

        static void M3(string formname, System.Windows.Forms.Form associatedForm)
        {
            Form f = null;
            foreach (Form form in associatedForm.MdiChildren)
            {
                if (form.GetType().FullName.EndsWith(formname))
                {
                    f = form;
                    break;
                }
            }
            if (f == null)
            {
                Type type = GetType(formname);
                MethodInfo methodInfo = type.GetMethod("GetConditionChooseForm");
                if (methodInfo == null)
                {
                    f = CreateForm(formname);
                }
                else
                {
                    Query.ConditionChooseForm ccf = (Query.ConditionChooseForm)methodInfo.Invoke(null, null);
                    if (ccf == null || ccf.ShowDialog(associatedForm) != DialogResult.OK)
                        return;

                    f = CreateForm(formname, new object[] { ccf.Condition });
                }
                f.MdiParent = associatedForm;
            }

            foreach (Form item in Application.OpenForms)
            {
                if (item.GetType() == f.GetType())
                {
                    item.Activate();
                    item.BringToFront();
                    item.WindowState = FormWindowState.Normal;
                    return;
                }
            }
            f.Show();
            f.BringToFront();
        }

        static void M4(string formname, System.Windows.Forms.Form associatedForm, object[] args)
        {
            Form f = null;
            foreach (Form form in associatedForm.MdiChildren)
            {
                if (form.GetType().FullName.EndsWith(formname))
                {
                    f = form;
                    break;
                }
            }
            if (f == null)
            {
                f = CreateForm(formname, args);
                f.MdiParent = associatedForm;
            }

            foreach (Form item in Application.OpenForms)
            {
                if (item.GetType() == f.GetType())
                {
                    item.Activate();
                    item.BringToFront();
                    item.WindowState = FormWindowState.Normal;
                    return;
                }
            }
            f.Show();
            f.BringToFront();
        }

        static void M5(string formname, System.Windows.Forms.Form associatedForm, object args)
        {
            Form f = null;
            foreach (Form form in associatedForm.MdiChildren)
            {
                if (form.GetType().FullName.EndsWith(formname))
                {
                    f = form;
                    break;
                }
            }
            if (f == null)
            {
                f = CreateForm(formname, new object[] { args });
                f.MdiParent = associatedForm;
            }

            foreach (Form item in Application.OpenForms)
            {
                if (item.GetType() == f.GetType())
                {
                    item.Activate();
                    item.BringToFront();
                    item.WindowState = FormWindowState.Normal;
                    return;
                }
            }
            f.Show();
            f.BringToFront();
        }

        //直接打开报表,没有查询条件窗口
        static void M6(string reportname)
        {
            DevExpress.XtraReports.UI.XtraReport report = null;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            report = assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, reportname), false) as DevExpress.XtraReports.UI.XtraReport;
            if (report != null)
                report.ShowPreviewDialog();
        }

        //直接打开窗口,不检测是否已存在窗口(ANSI外销报告专用)
        static void M7(string formname, System.Windows.Forms.Form associatedForm)
        {
            Form f = CreateForm(formname.Substring(0, formname.IndexOf("-")), new object[] { formname });
            f.MdiParent = associatedForm;
            f.Show();
            f.BringToFront();
        }

        //打开报表,具有查询条件窗口.可以有InvoiceException异常抛出
        static void MM(string reportname, Query.ConditionChooseForm form)
        {
            DevExpress.XtraReports.UI.XtraReport f = null;
            try
            {
                if (form == null || form.ShowDialog() != DialogResult.OK)
                    return;
                f = CreateReport(reportname, new object[] { form.Condition });
                f.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.ToString()))
                {
                    System.Windows.Forms.MessageBox.Show(Properties.Resources.NoRecords, Properties.Resources.Title_NoRecRecord, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, Properties.Resources.Title_NoRecRecord, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
        }

        static DevExpress.XtraReports.UI.XtraReport CreateReport(string reportName, object[] args)
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                return (DevExpress.XtraReports.UI.XtraReport)assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, reportName), false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }

        public static void Open(string operation)
        {
            if (operations.ContainsKey(operation))
                operations[operation].DynamicInvoke();
        }

        public static void Open(string operation, System.Windows.Forms.Form form)
        {
            if (operations.ContainsKey(operation))
                operations[operation].DynamicInvoke(new object[] { form });
        }

        public static void Open(string operation, System.Windows.Forms.Form form, object obj)
        {
            if (operations.ContainsKey(operation))
                operations[operation].DynamicInvoke(new object[] { form, obj });
        }
    }
}
