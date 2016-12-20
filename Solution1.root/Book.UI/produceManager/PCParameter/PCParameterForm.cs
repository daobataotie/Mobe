using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Book.UI.produceManager.PCParameter
{
    public partial class PCParameterForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        XmlDocument xDoc = new XmlDocument();
        string filePath = Application.StartupPath + "\\QualityCheck.config";
        public PCParameterForm()
        {
            InitializeComponent();
            this.action = "insert";
        }

        private void PCParameterForm_Load(object sender, EventArgs e)
        {
            this.xDoc.Load(filePath);
            //=-ANSI-=
            Hashtable ansiHT = new Hashtable();
            XmlNodeList ANSINodeList = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck").ChildNodes;
            //冲击力度
            ansiHT.Add(this.ListBoxCJLD, ANSINodeList.Item(0).ChildNodes);
            //坠球重量
            ansiHT.Add(this.ListBoxZQZL, ANSINodeList.Item(1).ChildNodes);
            //列印表头信息
            ansiHT.Add(this.ListBoxLYBTXX, ANSINodeList.Item(2).ChildNodes);

            BindAllListBox(ansiHT);
        }

        private void BindAllListBox(Hashtable mHt)
        {
            foreach (DictionaryEntry de in mHt)
            {
                DevExpress.XtraEditors.ListBoxControl mListBox = de.Key as DevExpress.XtraEditors.ListBoxControl;
                XmlNodeList mNodes = de.Value as XmlNodeList;
                mListBox.Items.Clear();
                foreach (XmlNode nd in mNodes)
                {
                    mListBox.Items.Add(nd.Attributes["SortNo"].Value + "---" + nd.Attributes["value"].Value);
                }
            }
        }

        private void bindSingleList(DevExpress.XtraEditors.ListBoxControl lb, XmlNodeList nodes)
        {
            lb.Items.Clear();
            foreach (XmlNode nd in nodes)
            {
                lb.Items.Add(nd.Attributes["SortNo"].Value + "---" + nd.Attributes["value"].Value);
            }
        }

        #region XML Operation
        //插入节点
        private void AddNode(XmlNode Node, XmlElement xe)
        {
            bool inserFlag = true;
            foreach (XmlNode nd in Node.ChildNodes)
            {
                XmlElement e1 = nd as XmlElement;       //将子节点 类型转化为XmlElement元素类型
                if (inserFlag)
                {
                    if (int.Parse(xe.GetAttribute("SortNo")) == int.Parse(e1.GetAttribute("SortNo")))  //判断插入位置
                    {
                        Node.InsertBefore((XmlNode)xe, nd);       //插入本节点之前
                        e1.SetAttribute("SortNo", (int.Parse(e1.GetAttribute("SortNo")) + 1).ToString());   //将本节点向后移动
                        inserFlag = false;      //判定为flase,标识插入动作已完成,开始执行移位操作
                    }
                }
                else
                {
                    e1.SetAttribute("SortNo", (int.Parse(e1.GetAttribute("SortNo")) + 1).ToString());
                }
            }
            if (inserFlag)      //如果已经插入,就不必再次插入
            {
                Node.AppendChild(xe);
            }
            #region 注释
            //if (int.Parse(xe.GetAttribute("SortNo")) == Node.ChildNodes.Count + 1)
            //{
            //    Node.AppendChild(xe);
            //    return;
            //}
            //for (int i = Node.ChildNodes.Count - 1; i >= 0; i--)
            //{
            //    XmlElement e1 = Node.ChildNodes[i] as XmlElement;
            //    e1.SetAttribute("SortNo", (int.Parse(e1.GetAttribute("SortNo")) + 1).ToString());
            //    if (int.Parse(xe.GetAttribute("SortNo")) == int.Parse(e1.GetAttribute("SortNo")))
            //    {
            //        Node.InsertBefore((XmlNode)xe, Node.ChildNodes[i]);       //插入本节点之前
            //        break;
            //    }
            //}
            #endregion
        }
        //删除节点
        private void DelNode(XmlNode Node, XmlElement xe)
        {
            for (int i = Node.ChildNodes.Count - 1; i >= 0; i--)
            {
                XmlElement e1 = Node.ChildNodes[i] as XmlElement;
                if (e1.GetAttribute("SortNo") == xe.GetAttribute("SortNo") && e1.GetAttribute("value") == xe.GetAttribute("value"))
                {
                    Node.RemoveChild(e1);
                    break;                      //删除之后就不用再次循环.
                }
                e1.SetAttribute("SortNo", (int.Parse(e1.GetAttribute("SortNo")) - 1).ToString());
            }
        }
        #endregion

        protected override void Save()
        {
            this.xDoc.Save(this.filePath);
        }

        #region 冲击力道
        private void btnAddCJLD_Click(object sender, EventArgs e)
        {
            XmlNode node = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck/ChongJiLiDao");
            XmlElement xe = xDoc.CreateElement("add");
            try
            {
                xe.SetAttribute("SortNo", this.txtANSI_CJLD.Text.Split(',')[0] != null && this.txtANSI_CJLD.Text.Split(',')[0] != "" ? this.txtANSI_CJLD.Text.Split(',')[0].ToString() : "0");
                xe.SetAttribute("value", this.txtANSI_CJLD.Text.Split(',')[1] != null && this.txtANSI_CJLD.Text.Split(',')[1] != "" ? this.txtANSI_CJLD.Text.Split(',')[1].ToString() : "0");
                if (xe.GetAttribute("SortNo") != "0" && xe.GetAttribute("value") != "0")
                {
                    this.AddNode(node, xe);
                    bindSingleList(this.ListBoxCJLD, node.ChildNodes);
                }
                else
                {
                    MessageBox.Show("插入数据格式有误.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入数据格式有误." + ex.Message);
            }
        }

        private void btnDelCJLD_Click(object sender, EventArgs e)
        {
            XmlNode node = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck/ChongJiLiDao");
            XmlElement xe = xDoc.CreateElement("add");
            string xeStr = this.ListBoxCJLD.SelectedValue != null ? this.ListBoxCJLD.SelectedValue.ToString() : "";
            if (xeStr != null && xeStr != "")
            {
                xe.SetAttribute("SortNo", xeStr.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries)[0].ToString());
                xe.SetAttribute("value", xeStr.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries)[1].ToString());
                this.DelNode(node, xe);
                bindSingleList(this.ListBoxCJLD, node.ChildNodes);
            }
            else
            {
                MessageBox.Show("沒有更多記錄可供操作");
            }
        }
        #endregion

        #region 坠球重量
        private void btnAddZQZL_Click(object sender, EventArgs e)
        {
            XmlNode node = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck/ZhuiQiuZhongLiang");
            XmlElement xe = xDoc.CreateElement("add");
            try
            {
                xe.SetAttribute("SortNo", this.txtANSI_ZQZL.Text.Split(',')[0] != null && this.txtANSI_ZQZL.Text.Split(',')[0] != "" ? this.txtANSI_ZQZL.Text.Split(',')[0].ToString() : "0");
                xe.SetAttribute("value", this.txtANSI_ZQZL.Text.Split(',')[1] != null && this.txtANSI_ZQZL.Text.Split(',')[1] != "" ? this.txtANSI_ZQZL.Text.Split(',')[1].ToString() : "0");
                if (xe.GetAttribute("SortNo") != "0" && xe.GetAttribute("value") != "0")
                {
                    this.AddNode(node, xe);
                    bindSingleList(this.ListBoxZQZL, node.ChildNodes);
                }
                else
                {
                    MessageBox.Show("插入数据格式有误.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入数据格式有误." + ex.Message);
            }
        }

        private void btnDelZQZL_Click(object sender, EventArgs e)
        {
            XmlNode node = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck/ZhuiQiuZhongLiang");
            XmlElement xe = xDoc.CreateElement("add");
            string xeStr = this.ListBoxZQZL.SelectedValue != null ? this.ListBoxZQZL.SelectedValue.ToString() : "";
            if (xeStr != null && xeStr != "")
            {
                xe.SetAttribute("SortNo", xeStr.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries)[0].ToString());
                xe.SetAttribute("value", xeStr.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries)[1].ToString());
                this.DelNode(node, xe);
                bindSingleList(this.ListBoxZQZL, node.ChildNodes);
            }
            else
            {
                MessageBox.Show("沒有更多記錄可供操作");
            }
        }
        #endregion

        #region 列印表头信息
        private void btnAddLYBTXX_Click(object sender, EventArgs e)
        {
            XmlNode node = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck/LieYinBiaoTouXinXi");
            XmlElement xe = xDoc.CreateElement("add");
            try
            {
                xe.SetAttribute("SortNo", this.txtANSI_BTXX.Text.Split(',')[0] != null && this.txtANSI_BTXX.Text.Split(',')[0] != "" ? this.txtANSI_BTXX.Text.Split(',')[0].ToString() : "0");
                xe.SetAttribute("value", this.txtANSI_BTXX.Text.Split(',')[1] != null && this.txtANSI_BTXX.Text.Split(',')[1] != "" ? this.txtANSI_BTXX.Text.Split(',')[1].ToString() : "0");
                if (xe.GetAttribute("SortNo") != "0" && xe.GetAttribute("value") != "0")
                {
                    this.AddNode(node, xe);
                    bindSingleList(this.ListBoxLYBTXX, node.ChildNodes);
                }
                else
                {
                    MessageBox.Show("插入数据格式有误.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入数据格式有误." + ex.Message);
            }

        }

        private void btnDelLYBTXX_Click(object sender, EventArgs e)
        {
            XmlNode node = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck/LieYinBiaoTouXinXi");
            XmlElement xe = xDoc.CreateElement("add");
            string xeStr = this.ListBoxLYBTXX.SelectedValue != null ? this.ListBoxLYBTXX.SelectedValue.ToString() : "";
            if (xeStr != null && xeStr != "")
            {
                xe.SetAttribute("SortNo", xeStr.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries)[0].ToString());
                xe.SetAttribute("value", xeStr.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries)[1].ToString());
                this.DelNode(node, xe);
                bindSingleList(this.ListBoxLYBTXX, node.ChildNodes);
            }
            else
            {
                MessageBox.Show("沒有更多記錄可供操作");
            }
        }
        #endregion
    }
}
