using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ShopBridge.Models
{
    public class XMLCast
    {
        public string CreateXML(dynamic entity)
        {
            //Create a xml object
            XmlDocument xmlDoc = new XmlDocument();
            //Create a XmlSerializer object from entity
            XmlSerializer xmlSerializer = new XmlSerializer(entity.GetType());
            //Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, entity);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public XmlNode CreateXMLNode(dynamic entity)
        {
            //Create a xml object
            XmlDocument xmlDoc = new XmlDocument();
            //Create a XmlSerializer object from entity
            XmlSerializer xmlSerializer = new XmlSerializer(entity.GetType());
            //Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, entity);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.DocumentElement;
            }
        }
    }
}
