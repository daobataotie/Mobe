using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Common
{
    public class ConnectionInfoAccessor
    {
        public static void Save(string file, IList<Connection> connections)
        {
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateElement("connections"));

            foreach (Connection c in connections)
            {
                XmlNode connectionNode = document.CreateElement("connection");
                connectionNode.InnerXml = c.ToString("s");

                document.FirstChild.AppendChild(connectionNode);
            }
            
            document.Save(file);
        }

        public static IList<Connection> Load(string file)
        {
            IList<Connection> connections = new List<Connection>();

            IDictionary<string, Type> types = new Dictionary<string, Type>();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            XmlDocument document = new XmlDocument();
            document.Load(file);

            XmlNodeList connectionNodes = document.SelectNodes("/connections/connection");
            foreach (XmlNode node in connectionNodes)
            {
                XmlNode typeNode = node.SelectSingleNode("type");
                if (typeNode == null || typeNode.InnerText == "")
                    throw new Exception();

                Type type;
                if (types.ContainsKey(typeNode.InnerText))
                    type = types[typeNode.InnerText];
                else
                {
                    type = assembly.GetType(typeNode.InnerText);
                    types.Add(typeNode.InnerText, type);
                }
                if (type == null)
                    throw new Exception();

                Connection connection = (Connection)type.InvokeMember("Parse", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod, null, null, new object[] { node });
                connections.Add(connection);
            }
            return connections;
        }
    }
}
