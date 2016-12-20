using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace Book.UI
{
    public class ConnDB
    { 
        
        string file = "connections.xml";

        public string getconnectionstring(string gname)
        {
            string s = "";
            string host = "";
            string dbname = "";
            string uid = "";
            string pwd = "";
            XmlDocument document = new XmlDocument();
            document.Load(file);
            XmlNodeReader reader = new XmlNodeReader(document);
            XmlNode x = document.SelectSingleNode("connections");
            XmlNodeList xnl = x.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement  xe = (XmlElement)xnf;
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {

                    if ((xn2.Name == "datafile"))
                    {
                        host = xn2.InnerText;
                     
                    }
                    else if ((xn2.Name == "initialcatalog"))
                    {
                        dbname = xn2.InnerText;

                    }
                    else if ((xn2.Name == "username"))
                    {
                        uid = xn2.InnerText;

                    }
                    else if ((xn2.Name == "password"))
                    {
                        pwd = xn2.InnerText;

                    }


                }

               

            
            }

            s = "data source=" + host + ";initial catalog=" + dbname + ";user id=" + uid + ";password="+pwd;

            return s; 
        }
    
         

    }
}
