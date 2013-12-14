using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheck.ResTrans;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;

namespace SectionCheck.Services
{
    public class XEP_DataCacheService : XEP_IDataCacheService
    {
        #region XEP_IDataCacheService Members
        eDataCacheServiceOperation XEP_IDataCacheService.Load(XEP_IDataCache dataCache)
        {
            try
            {
                DirectoryInfo ducumentsDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(ducumentsDirectory.FullName, "XEP_SectionCheck"));
                if (myDirectory.Exists == false)
                {
                    throw new ApplicationException("No xml file for creating data cache !");
                }
                FileInfo myFile = new FileInfo(Path.Combine(myDirectory.FullName, "XEP_DataCache.xml"));
                if (myFile.Exists == false)
                {
                    throw new ApplicationException("No xml file for creating data cache !");
                }
                XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
                XDocument documentXml = XDocument.Load(myFile.FullName);
                XElement elementXml = documentXml.Element(ns + dataCache.XmlWorker.GetXmlElementName());
                if (elementXml == null)
                {
                    throw new ApplicationException("Invalid XML file !");
                }
                dataCache.XmlWorker.LoadFromXmlElement(elementXml);
                return eDataCacheServiceOperation.eSuccess;
            }
            catch (System.Exception ex)
            {
                return eDataCacheServiceOperation.eFailed;
            }
        }
        eDataCacheServiceOperation XEP_IDataCacheService.Save(XEP_IDataCache dataCache)
        {
            DirectoryInfo ducumentsDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(ducumentsDirectory.FullName, "XEP_SectionCheck"));
            if (myDirectory.Exists == false)
            {
                myDirectory.Create();
            }
            XDocument documentXml = new XDocument(
            new XDeclaration("1.0", null, "yes"),
            new XComment(Resources.ResourceManager.GetString("COMMENT_XML")));
            XElement xmlElement = dataCache.XmlWorker.GetXmlElement();
            documentXml.Add(xmlElement);
            documentXml.Save(Path.Combine(myDirectory.FullName, "XEP_DataCache.xml"));
            return eDataCacheServiceOperation.eSuccess;
        }
        #endregion

        #region METHODS 

        #endregion
    }
}
