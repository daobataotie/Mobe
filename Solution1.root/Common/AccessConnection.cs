using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.OleDb;

namespace Common
{
    public class AccessConnection : Connection
    {
        private string datafile;
    
        public string DataFile
        {
            get
            {
                return this.datafile;
            }
            set
            {
                this.datafile = value;
            }
        }

        public override string ToString(string note)
        {
            string result = "";
            switch (note)
            {
                case "s":
                    XmlDocument document = new XmlDocument();
                    XmlNode connectionNode = document.CreateElement("connection");
                    XmlNode typeNode = document.CreateElement("type");
                    XmlNode nameNode = document.CreateElement("name");
                    XmlNode datafileNode = document.CreateElement("datafile");

                    typeNode.InnerText = this.GetType().FullName;
                    nameNode.InnerText = this.Name;
                    datafileNode.InnerText = this.DataFile;

                    connectionNode.AppendChild(typeNode);
                    connectionNode.AppendChild(nameNode);
                    connectionNode.AppendChild(datafileNode);

                    document.AppendChild(connectionNode);

                    result = document.FirstChild.InnerXml;
                    break;

                case "d":
                    result = string.Format("provider=Microsoft.Jet.OLEDB.4.0;data source={0}", this.DataFile);
                    break;

                case "r":
                    result = string.Format("provider=Microsoft.Jet.OLEDB.4.0;data source={0}", this.DataFile);
                    break;
            }
            return result;
        }

        public static AccessConnection Parse(XmlNode connectionNode)
        {
            AccessConnection conn = new AccessConnection();

            XmlNode nameNode = connectionNode.SelectSingleNode("name");
            XmlNode datafileNode = connectionNode.SelectSingleNode("datafile");

            if (nameNode == null || datafileNode == null)
                throw new Exception();

            if (nameNode.InnerText == "" || datafileNode.InnerText == "")
                throw new Exception();

            conn.Name = nameNode.InnerText;
            conn.DataFile = datafileNode.InnerText;

            return conn;
        }


        public override string Type
        {
            get 
            {
                return "Access2003";
            }
        }

        public override bool Awailable
        {
            get 
            {
                bool result = true;

                OleDbConnection conn = new OleDbConnection(this.ToString("r"));
                try
                {
                    conn.Open();
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
    }
}
