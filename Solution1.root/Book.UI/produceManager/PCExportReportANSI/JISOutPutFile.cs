using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.IO;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class JISOutPutFile : DevExpress.XtraEditors.XtraForm
    {
        Model.PCExportReportANSIDetail detail = new Book.Model.PCExportReportANSIDetail();
        BL.PCExportReportANSIDetailManager manager = new Book.BL.PCExportReportANSIDetailManager();
        DataTable dt = new DataTable();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        BL.InvoiceCOManager invoiceCOManager = new Book.BL.InvoiceCOManager();
        BL.MRSHeaderManager mrsHeaderManager = new Book.BL.MRSHeaderManager();
        BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        BL.ProduceOtherMaterialManager produceOtherMaterialManager = new Book.BL.ProduceOtherMaterialManager();
        BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();
        BL.DepotOutManager depotOutManager = new Book.BL.DepotOutManager();
        BL.InvoiceXSManager invoiceXSManager = new Book.BL.InvoiceXSManager();
        BL.ProduceInDepotDetailManager produceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.ProductOnlineCheckManager productOnlineCheckManager = new Book.BL.ProductOnlineCheckManager();
        BL.PCFinishCheckManager pCFinishCheckManager = new Book.BL.PCFinishCheckManager();
        BL.PCPGOnlineCheckDetailManager pCPGOnlineCheckDetailManager = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.PCOpticsCheckManager pCOpticsCheckManager = new Book.BL.PCOpticsCheckManager();
        BL.ANSIPCImpactCheckManager aNSIPCImpactCheckManager = new Book.BL.ANSIPCImpactCheckManager();
        BL.PCOtherCheckDetailManager pCOtherCheckDetailManager = new Book.BL.PCOtherCheckDetailManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();

        public JISOutPutFile()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dt.Columns.Add("Type", typeof(string));   //单据类型
            dt.Columns.Add("Id", typeof(string));     //单据编号
            dt.Columns.Add("Annex", typeof(string));  //附件
            dt.Columns.Add("Address", typeof(string)); //附件地址

            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "订单通知";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "采购单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "生产加工单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "领料单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "物料采购订单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "入料检验单";
            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "材质证明";
            //dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "模具上线检查表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "生产日报表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "委外加工单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "光学/厚度表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "光谱测试";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "强化/防雾工作日报表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "冲击测试表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "品检日报表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "委外加工检验单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "组装半成品日报表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "成品组装日报表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "组装检验日报表";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "成品检验单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "出货报告";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "出货通知单";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "出库单";
            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "JIS光学棱镜度制程测试换算表";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "雾度测试";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "品管线上检查表";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "物料检验单";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "品管抽检日报表";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "成品测试报告";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr[0] = "出货验货单";
            //dt.Rows.Add(dr);

        }

        private void JISOutPutFile_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = dt;
            this.gridControl1.RefreshDataSource();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            string invoiceCusId = this.txt_InvoiceCusXoId.Text;
            if (string.IsNullOrEmpty(invoiceCusId))
            {
                MessageBox.Show("客户订单编号不能为空！", this.Text, MessageBoxButtons.OK);
                return;
            }
            if (this.bindingSource1.Current != null)
            {
                DataRowView dr = this.bindingSource1.Current as DataRowView;
                if (dr != null)
                {
                    string name = dr[0].ToString();


                    if (name.Contains("订单通知") || name.Contains("采购单"))
                    {
                        Invoices.XO.ListForm listform = new Book.UI.Invoices.XO.ListForm(invoiceCusId);
                        listform.Show(this);
                    }

                    //else if (name.Contains("入料检验单") || name.Contains("材质证明") || name.Contains("原物料采购订单"))
                    else if (name.Contains("入料检验单"))
                    {
                        PCIncomingCheck.RelationXOForm f = new Book.UI.produceManager.PCIncomingCheck.RelationXOForm(invoiceCusId);
                        if (f._relationXO == null)
                        {
                            MessageBox.Show("无记录！", this.Text, MessageBoxButtons.OK);
                            return;
                        }
                        else
                            f.Show(this);
                    }
                    else if (name.Contains("物料采购订单"))
                    {
                        //Invoices.CO.EditForm co = new Book.UI.Invoices.CO.EditForm(str);
                        //co.Show(this);
                        Invoices.CO.ListForm listform = new Book.UI.Invoices.CO.ListForm(invoiceCusId);
                        listform.Show(this);
                        PCOtherCheck.ListForm listform2 = new Book.UI.produceManager.PCOtherCheck.ListForm(invoiceCusId);
                        listform2.Show(this);
                    }
                    else if (name.Contains("领料单"))
                    {
                        ProduceMaterial.ListForm listform = new Book.UI.produceManager.ProduceMaterial.ListForm(invoiceCusId);
                        listform.Show(this);

                        ProduceOtherMaterial.ListForm listform2 = new Book.UI.produceManager.ProduceOtherMaterial.ListForm(invoiceCusId);
                        listform2.Show(this);

                        Settings.StockLimitations.ChooseOutStockDepot listform3 = new Book.UI.Settings.StockLimitations.ChooseOutStockDepot(invoiceCusId);
                        listform3.Show(this);
                    }

                    else if (name.Contains("生产加工单"))
                    {
                        //PronoteHeader.EditForm pnt = new Book.UI.produceManager.PronoteHeader.EditForm(str);
                        //pnt.Show(this);
                        PronoteHeader.ListForm listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId);
                        listform.Show(this);
                    }

                    else if (name.Contains("模具上线检查表"))
                    {
                        ProductOnlineCheck.List listform = new Book.UI.produceManager.ProductOnlineCheck.List(invoiceCusId);
                        listform.Show(this);
                    }

                    else if (name.Contains("生产日报表") || name.Contains("强化/防雾工作日报表") || name.Contains("品检日报表") || name.Contains("成品组装日报表") || name.Contains("组装半成品日报表"))
                    {
                        ProduceInDepot.ListForm listform = new Book.UI.produceManager.ProduceInDepot.ListForm(invoiceCusId);
                        listform.Show(this);
                    }

                    else if (name.Contains("委外加工单"))            //委外合同或者产品上线检验单
                    {
                        //ProduceOtherCompact.EditForm p = new Book.UI.produceManager.ProduceOtherCompact.EditForm(str);
                        //p.Show(this);
                        ProduceOtherCompact.ListForm listform = new Book.UI.produceManager.ProduceOtherCompact.ListForm(invoiceCusId);
                        listform.Show(this);

                    }

                    //else if (name.Contains("品管线上检查表"))
                    //{
                    //    PCBoxFootCheck.ListForm listform = new Book.UI.produceManager.PCBoxFootCheck.ListForm(invoiceCusId);
                    //    listform.Show(this);
                    //}

                    else if (name.Contains("光学/厚度表"))
                    {
                        PCPGOnlineCheck.ListForm listform = new Book.UI.produceManager.PCPGOnlineCheck.ListForm(invoiceCusId);
                        listform.Show(this);
                    }

                    //else if (name.Contains("JIS光学棱镜度制程测试换算表"))
                    //{
                    //    Invoices.XO.ListForm listform = new Book.UI.Invoices.XO.ListForm(invoiceCusId);
                    //    listform.Show(this);
                    //}

                    //else if (name.Contains("雾度测试"))
                    //{
                    //    PCFogCheck.ListForm listform = new Book.UI.produceManager.PCFogCheck.ListForm(invoiceCusId);
                    //    listform.Show(this);
                    //}
                    else if (name.Contains("光谱测试"))
                    {
                        //PCFinishCheck.ListForm listform = new Book.UI.produceManager.PCFinishCheck.ListForm(invoiceCusId);
                        //listform.Show(this);
                        PronoteHeader.ListForm listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId);
                        listform.Show(this);
                    }

                    else if (name.Contains("成品检验单"))
                    {
                        PCFinishCheck.ListForm listform = new Book.UI.produceManager.PCFinishCheck.ListForm(invoiceCusId);
                        listform.Show(this);
                        //PronoteHeader.ListForm listform = new Book.UI.produceManager.PronoteHeader.ListForm(invoiceCusId);
                        //listform.Show(this);
                    }

                    else if (name.Contains("出货报告"))
                    {
                        ListForm listform = new ListForm(invoiceCusId, "JIS");
                        listform.Show(this);
                    }

                    else if (name.Contains("组装检验日报表"))
                    {
                        PCSampling.ListForm listform1 = new Book.UI.produceManager.PCSampling.ListForm(invoiceCusId);
                        listform1.Show(this);
                        PCSamplingEar.ListForm listform2 = new Book.UI.produceManager.PCSamplingEar.ListForm(invoiceCusId);
                        listform2.Show(this);
                    }

                    else if (name.Contains("委外加工检验单"))
                    {
                        //PCOtherCheck.EditForm pcoc = new Book.UI.produceManager.PCOtherCheck.EditForm(str);
                        //pcoc.Show(this);
                        PCOtherCheck.ListForm listform = new Book.UI.produceManager.PCOtherCheck.ListForm(invoiceCusId);
                        listform.Show(this);

                        PCPGOnlineCheck.ListForm listform2 = new Book.UI.produceManager.PCPGOnlineCheck.ListForm(invoiceCusId);
                        listform2.Show(this);

                        PCImpactCheck.ListForm listform3 = new Book.UI.produceManager.PCImpactCheck.ListForm(invoiceCusId);
                        listform3.Show(this);
                    }

                    else if (name.Contains("冲击测试表"))
                    {
                        //PCImpactCheck.ListForm listform = new Book.UI.produceManager.PCImpactCheck.ListForm(invoiceCusId);
                        //listform.Show(this);

                        //ANSIPCImpactCheck.ListForm listform2 = new Book.UI.produceManager.ANSIPCImpactCheck.ListForm(invoiceCusId, 0);
                        //listform2.Show(this);

                        ANSIPCImpactCheck.ListForm listform3 = new Book.UI.produceManager.ANSIPCImpactCheck.ListForm(invoiceCusId, 1);
                        listform3.Show(this);

                        //PCDoubleImpactCheck.ListForm listform4 = new Book.UI.produceManager.PCDoubleImpactCheck.ListForm(invoiceCusId, 0);
                        //listform4.Show(this);

                        //PCDoubleImpactCheck.ListForm listform5 = new Book.UI.produceManager.PCDoubleImpactCheck.ListForm(invoiceCusId, 1);
                        //listform5.Show(this);

                        //PCDoubleImpactCheck.ListForm listform6 = new Book.UI.produceManager.PCDoubleImpactCheck.ListForm(invoiceCusId, 2);
                        //listform6.Show(this);

                    }

                    //else if (name.Contains("品管抽检日报表"))
                    //{
                    //    PCSampling.ListForm listform1 = new Book.UI.produceManager.PCSampling.ListForm(invoiceCusId);
                    //    listform1.Show(this);
                    //    PCSamplingEar.ListForm listform2 = new Book.UI.produceManager.PCSamplingEar.ListForm(invoiceCusId);
                    //    listform2.Show(this);
                    //}

                    //else if (name.Contains("成品测试报告"))
                    //{
                    //    ListForm listform = new ListForm(invoiceCusId, "JIS");
                    //    listform.Show(this);
                    //}

                    //else if (name.Contains("出货验货单"))
                    //{
                    //    //Invoices.XO.ListForm listform = new Book.UI.Invoices.XO.ListForm(invoiceCusId);
                    //    //listform.Show(this);

                    //    PCFinishCheck.ListForm listform2 = new Book.UI.produceManager.PCFinishCheck.ListForm(invoiceCusId);
                    //    listform2.Show(this);
                    //}

                    else if (name.Contains("出货通知单") || name.Contains("出库单"))
                    {
                        //Invoices.XS.EditForm xc = new Book.UI.Invoices.XS.EditForm(str);
                        //xc.Show(this);
                        Invoices.XS.ListForm listform = new Book.UI.Invoices.XS.ListForm(invoiceCusId);
                        listform.Show(this);
                    }

                    //else if (name.Contains("出库单"))
                    //{
                    //    //Settings.StockLimitations.OutStockEditForm cc = new Book.UI.Settings.StockLimitations.OutStockEditForm(str);
                    //    //cc.Show(this);
                    //    Settings.StockLimitations.ChooseOutStockDepot f = new Book.UI.Settings.StockLimitations.ChooseOutStockDepot(invoiceCusId);
                    //    f.Show(this);
                    //}

                    //else if (name.Contains("物料检验单"))
                    //{
                    //    //PCMaterialCheck.ListForm listform = new Book.UI.produceManager.PCMaterialCheck.ListForm(invoiceCusId);
                    //    //listform.Show(this);

                    //    //ProduceOtherMaterial.ListForm listform2 = new Book.UI.produceManager.ProduceOtherMaterial.ListForm(invoiceCusId);
                    //    //listform2.Show(this);
                    //    ProduceMaterial.ListForm listform4 = new Book.UI.produceManager.ProduceMaterial.ListForm(invoiceCusId);
                    //    listform4.BringToFront();

                    //    Invoices.CG.ListForm listform3 = new Book.UI.Invoices.CG.ListForm(invoiceCusId);
                    //    listform3.BringToFront();

                    //    PCOtherCheck.ListForm listform2 = new Book.UI.produceManager.PCOtherCheck.ListForm(invoiceCusId);

                    //    Invoices.CO.ListForm listform1 = new Book.UI.Invoices.CO.ListForm(invoiceCusId);

                    //    listform4.Show(this);
                    //    listform3.Show(this);
                    //    listform2.Show(this);
                    //    listform1.Show(this);
                    //}

                }
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == this.gridColumn2)
                e.DisplayText = "查詢";
        }
    }

}