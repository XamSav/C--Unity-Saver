using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Utils
{
    public class MyUtils
    {

        public MyUtils()
        {

        }
        public void Load(GameObject jugador)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("MiPrimerXML.xml");
            XmlNodeList itemNodes = xmlDoc.SelectNodes("//GameObject");
            foreach (XmlNode itemNode in itemNodes)
            {
                XmlNode position = itemNode.SelectSingleNode("Position");
                XmlNode x = position.Attributes.GetNamedItem("X");
                XmlNode y = position.Attributes.GetNamedItem("Y");
                XmlNode z = position.Attributes.GetNamedItem("Z");
                float fx = Convert.ToSingle(x.InnerText);
                float fy =Convert.ToSingle(y.InnerText);
                float fz = Convert.ToSingle(z.InnerText);
                jugador.transform.position = new Vector3(fx,fy,fz);
            }
        }

        public void CreateXML()
        {
            Scene nivel;
            nivel = SceneManager.GetActiveScene();

            GameObject[] g;
            g = nivel.GetRootGameObjects();
            
            CreateXML2(g);
        }




        public void CreateXML2(GameObject[] Player)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode;
            string filename = "MiPrimerXML.xml";

            if (System.IO.File.Exists(filename))
            {
                xmlDoc.Load(filename);
                rootNode = xmlDoc.SelectSingleNode("scene");
            }
            else
            {
                rootNode = xmlDoc.CreateElement("scene");
                xmlDoc.AppendChild(rootNode);
            }

             
            for (int i = 0; i < Player.Length; i++)
            {
                
                XmlNode userNode = xmlDoc.CreateElement("GameObject");
                XmlAttribute name = xmlDoc.CreateAttribute("name");
                name.Value = Player[i].name;
                userNode.Attributes.Append(name);
                rootNode.AppendChild(userNode);
                Component[] componentes = Player[i].GetComponents<Component>();
                for (int s = 0; s < componentes.Length; s++)
                {
                    XmlNode component = xmlDoc.CreateElement("Component");
                    XmlAttribute nameComponent = xmlDoc.CreateAttribute("name");
                    nameComponent.Value = componentes[s].GetType().ToString();
                    component.Attributes.Append(nameComponent);
                    userNode.AppendChild(component);
                }
                //  Position
                XmlNode vector = xmlDoc.CreateElement("Position");
                    XmlAttribute x = xmlDoc.CreateAttribute("X");
                    x.Value = Player[i].transform.position.x.ToString();
                    XmlAttribute y = xmlDoc.CreateAttribute("Y");
                    y.Value = Player[i].transform.position.y.ToString();
                    XmlAttribute z = xmlDoc.CreateAttribute("Z");
                    z.Value = Player[i].transform.position.z.ToString();
                    vector.Attributes.Append(x);
                    vector.Attributes.Append(y);
                    vector.Attributes.Append(z);
                    userNode.AppendChild(vector);

            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false); // Falso = no escribir BOM
            settings.Indent = true;
            XmlWriter writer = XmlTextWriter.Create(filename, settings);
            xmlDoc.Save(writer);
        }

        public void GameObjectSave(GameObject Player, Scene scene)
        {
            scene.GetRootGameObjects();
        }
  
    }
}
