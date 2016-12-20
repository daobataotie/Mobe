using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.ProduceOtherInDepot
{
    public partial class ChooseProduceOtherInDepotForPCO : DevExpress.XtraEditors.XtraForm
    {
        private BL.ProduceOtherInDepotManager ProduceOtherInDepotManager = new Book.BL.ProduceOtherInDepotManager();
        private BL.ProduceOtherInDepotDetailManager ProduceOtherInDepotDetailManager = new Book.BL.ProduceOtherInDepotDetailManager();

        private IList<Model.ProduceOtherInDepot> ListProduceOtherInDepot = new List<Model.ProduceOtherInDepot>();
        public IList<Model.ProduceOtherInDepotDetail> SelectItems = new List<Model.ProduceOtherInDepotDetail>();

        public ChooseProduceOtherInDepotForPCO()
        {
            InitializeComponent();
            this.dateStart.DateTime = DateTime.Now.AddDays(-1);
            this.dateEnd.DateTime = DateTime.Now.AddDays(1).AddSeconds(-1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.loadData();
        }

        private void ChooseProduceOtherInDepotForPCO_Load(object sender, EventArgs e)
        {
            this.loadData();
        }

        //获取数据进行绑定
        private void loadData()
        {
            //绑定头
            DateTime ds = this.dateStart.EditValue == null ? DateTime.Now.AddDays(-1) : this.dateStart.DateTime;
            DateTime de = this.dateEnd.EditValue == null ? DateTime.Now.AddDays(1).AddSeconds(-1) : this.dateEnd.DateTime;
            this.ListProduceOtherInDepot = this.ProduceOtherInDepotManager.SelectByDateRange(ds, de);
            this.bsGCHeader.DataSource = this.ListProduceOtherInDepot;

            //绑定详细
            if (this.bsGCHeader.Current != null)
            {
                Model.ProduceOtherInDepot proInDepot = this.bsGCHeader.Current as Model.ProduceOtherInDepot;
                IList<Model.ProduceOtherInDepotDetail> poidd = ProduceOtherInDepotDetailManager.SelectByProduceotherInDepotId(proInDepot.ProduceOtherInDepotId);
                (this.bsGCHeader.Current as Model.ProduceOtherInDepot).Details = poidd;
                this.bsDetail.DataSource = (this.bsGCHeader.Current as Model.ProduceOtherInDepot).Details;
            }
        }

        //确认选择
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.bsGCHeader.Current != null || (this.bsGCHeader.Current as Model.ProduceOtherInDepot) != null)
            {
                this.SelectItems = (from i in (this.bsGCHeader.Current as Model.ProduceOtherInDepot).Details
                                    where i.Checked == true
                                    select i).ToList<Model.ProduceOtherInDepotDetail>();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.SelectItems.Clear();
            this.Close();
            this.Dispose();
        }

        //头更改
        private void bsGCHeader_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bsGCHeader.Current != null)
            {
                if ((this.bsGCHeader.Current as Model.ProduceOtherInDepot).Details == null)
                {
                    Model.ProduceOtherInDepot proInDepot = this.bsGCHeader.Current as Model.ProduceOtherInDepot;
                    IList<Model.ProduceOtherInDepotDetail> poidd = ProduceOtherInDepotDetailManager.SelectByProduceotherInDepotId(proInDepot.ProduceOtherInDepotId);
                    (this.bsGCHeader.Current as Model.ProduceOtherInDepot).Details = poidd;
                }
                this.bsDetail.DataSource = (this.bsGCHeader.Current as Model.ProduceOtherInDepot).Details;
            }
        }

    }
}