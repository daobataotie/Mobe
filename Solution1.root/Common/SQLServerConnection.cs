using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class SQLServerConnection : Connection
    {
        #region Data

        private SQLServerAuthentication authentication;
        private string datasource;
        private string initialcatalog;
        private string password;
        private string username;

        #endregion

        #region Properties

        public string DataSource
        {
            get
            {
                return this.datasource;
            }
            set
            {
                this.datasource = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string InitialCatalog
        {
            get
            {
                return this.initialcatalog;
            }
            set
            {
                this.initialcatalog = value;
            }
        }

        public SQLServerAuthentication Authentication
        {
            get
            {
                return this.authentication;
            }
            set
            {
                this.authentication = value;
            }
        }

        #endregion

        #region ToString

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
                    XmlNode datasourceNode = document.CreateElement("datafile");
                    XmlNode initialcatalogNode = document.CreateElement("initialcatalog");
                    XmlNode usernameNode = document.CreateElement("username");
                    XmlNode passwordNode = document.CreateElement("password");
                    XmlNode authenticationNode = document.CreateElement("authentication");

                    typeNode.InnerText = this.GetType().FullName;
                    nameNode.InnerText = this.Name;
                    datasourceNode.InnerText = this.datasource;
                    initialcatalogNode.InnerText = this.InitialCatalog;
                    usernameNode.InnerText = this.UserName;
                    passwordNode.InnerText = this.Password;
                    authenticationNode.InnerText = this.Authentication.ToString();

                    connectionNode.AppendChild(typeNode);
                    connectionNode.AppendChild(nameNode);
                    connectionNode.AppendChild(datasourceNode);
                    connectionNode.AppendChild(initialcatalogNode);
                    connectionNode.AppendChild(usernameNode);
                    connectionNode.AppendChild(passwordNode);
                    connectionNode.AppendChild(authenticationNode);

                    document.AppendChild(connectionNode);

                    result = document.FirstChild.InnerXml;
                    break;

                case "d":
                    if (this.authentication == SQLServerAuthentication.Windows)
                        result = string.Format("data source={0};initial catalog={1};integrated security=sspi", this.datasource, this.initialcatalog);
                    else
                        result = string.Format("data source={0};initial catalog={1};user id={2};password={3}", this.datasource, this.initialcatalog, this.username, "***");
                    break;

                case "r":
                    if (this.authentication == SQLServerAuthentication.Windows)
                        result = string.Format("data source={0};initial catalog={1};integrated security=sspi", this.datasource, this.initialcatalog);
                    else
                        result = string.Format("data source={0};initial catalog={1};user id={2};password={3}", this.datasource, this.initialcatalog, this.username, this.password);
                    break;
            }
            return result;
        }

        #endregion

        #region Parse

        public static SQLServerConnection Parse(XmlNode connectionNode)
        {
            SQLServerConnection conn = new SQLServerConnection();

            XmlNode nameNode = connectionNode.SelectSingleNode("name");
            XmlNode datasourceNode = connectionNode.SelectSingleNode("datafile");
            XmlNode initialcatalogNode = connectionNode.SelectSingleNode("initialcatalog");
            XmlNode usernameNode = connectionNode.SelectSingleNode("username");
            XmlNode passwordNode = connectionNode.SelectSingleNode("password");
            XmlNode authenticationNode = connectionNode.SelectSingleNode("authentication");

            if (nameNode == null || datasourceNode == null || initialcatalogNode == null || authenticationNode == null)
                throw new Exception();

            if (nameNode.InnerText == "" || datasourceNode.InnerText == "" || initialcatalogNode.InnerText == "" || authenticationNode.InnerText == "")
                throw new Exception();

            SQLServerAuthentication authentication = (SQLServerAuthentication)System.Enum.Parse(typeof(SQLServerAuthentication), authenticationNode.InnerText);
            if (authentication == SQLServerAuthentication.SQLServer && (usernameNode == null || usernameNode.InnerText == ""))
                throw new Exception();

            conn.Name = nameNode.InnerText;
            conn.DataSource = datasourceNode.InnerText;
            conn.InitialCatalog = initialcatalogNode.InnerText;
            conn.Authentication = authentication;
            conn.UserName = usernameNode.InnerText;
            conn.Password = passwordNode.InnerText;

            return conn;
        }

        #endregion

        public override string Type
        {
            get 
            {
                return "SQLServer";
            }
        }

        public override bool Awailable
        {
            get 
            {
                bool result = true;

                SqlConnection conn = new SqlConnection(this.ToString("r"));
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
