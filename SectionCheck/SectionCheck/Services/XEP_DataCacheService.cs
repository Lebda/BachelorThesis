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
        string _aplicationFolderPathFullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string _folderName = "XEP_SectionCheck";
        string _fileName = "XEP_DataCache";
        string _materialLibraryName = "XEP_SectionCheck_MaterialLibrary";

        #region XEP_IDataCacheService Members
        public string AplicationFolderPathFullName
        {
            get { return _aplicationFolderPathFullName; }
            set { _aplicationFolderPathFullName = value; }
        }
        public string FolderName
        {
            get { return _folderName; }
            set { _folderName = value; }
        }
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public string MaterialLibraryName
        {
            get { return _materialLibraryName; }
            set { _materialLibraryName = value; }
        }
        public virtual eDataCacheServiceOperation Load(XEP_IDataCache dataCache)
        {
            try
            {
                DirectoryInfo ducumentsDirectory = new DirectoryInfo(_aplicationFolderPathFullName);
                DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(ducumentsDirectory.FullName, _folderName));
                if (myDirectory.Exists == false)
                {
                    throw new ApplicationException("No xml file for creating data cache !");
                }
                FileInfo myFile = new FileInfo(Path.Combine(myDirectory.FullName, _fileName + ".xml"));
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
                // read material library
                FileInfo myFileMatLib = new FileInfo(Path.Combine(myDirectory.FullName, _materialLibraryName + ".xml"));
                if (myFileMatLib.Exists == false)
                {
                    throw new ApplicationException("No xml file for creating material library exists !");
                }
                XDocument documentXmlMatLib = XDocument.Load(myFileMatLib.FullName);
                XElement elementXmlMatLib = documentXmlMatLib.Element(ns + dataCache.MaterialLibrary.XmlWorker.GetXmlElementName());
                if (elementXmlMatLib == null)
                {
                    throw new ApplicationException("Invalid XML file for material library !");
                }
                dataCache.MaterialLibrary.XmlWorker.LoadFromXmlElement(elementXmlMatLib);
                return eDataCacheServiceOperation.eSuccess;
            }
            catch (System.Exception ex)
            {
                return eDataCacheServiceOperation.eFailed;
            }
        }
        eDataCacheServiceOperation XEP_IDataCacheService.Save(XEP_IDataCache dataCache)
        {
            DirectoryInfo ducumentsDirectory = new DirectoryInfo(_aplicationFolderPathFullName);
            DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(ducumentsDirectory.FullName, _folderName));
            if (myDirectory.Exists == false)
            {
                myDirectory.Create();
            }
            XDocument documentXml = new XDocument(
                new XDeclaration("1.0", null, "yes"),
                new XComment(Resources.ResourceManager.GetString("COMMENT_XML")));
            SaveOneXmlFile(documentXml, dataCache.XmlWorker.GetXmlElement(), Path.Combine(myDirectory.FullName, _fileName + ".xml"));
            // Save material library
            XDocument documentXmlMatLib = new XDocument(
                new XDeclaration("1.0", null, "yes"),
                new XComment(Resources.ResourceManager.GetString("COMMENT_XML")));
            SaveOneXmlFile(documentXmlMatLib, dataCache.MaterialLibrary.XmlWorker.GetXmlElement(), Path.Combine(myDirectory.FullName, _materialLibraryName + ".xml"));
            return eDataCacheServiceOperation.eSuccess;
        }
        #endregion

        #region METHODS 
        private void SaveOneXmlFile(XDocument documentXml, XElement xmlElement, string xmlFileFullName)
        {
            documentXml.Add(xmlElement);
            documentXml.Save(xmlFileFullName);
        }
        #endregion
    }
}
