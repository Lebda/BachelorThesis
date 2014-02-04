using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheck.ResTrans;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace SectionCheck.Services
{
    public class XEP_DataCacheService : XEP_IDataCacheService
    {
        string _aplicationFolderPathFullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string _folderName = "XEP_SectionCheck";
        string _fileName = "XEP_DataCache";
        string _materialLibraryName = "XEP_SectionCheck_MaterialLibrary";
        string _setupParametersName = "XEP_SectionCheck_SetupParameters";
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
        public string SetupParametersName
        {
            get { return _setupParametersName; }
            set { _setupParametersName = value; }
        }
        public virtual eDataCacheServiceOperation Load(XEP_IDataCache dataCache)
        {
            try
            {
                DirectoryInfo ducumentsDirectory = new DirectoryInfo(_aplicationFolderPathFullName);
                DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(ducumentsDirectory.FullName, _folderName));
                if (myDirectory.Exists == false)
                {
                    throw new ApplicationException("No directory for xml file load !");
                }
                XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
                SaveOneFile(myDirectory, _fileName, ns, dataCache.XmlWorker);
                SaveOneFile(myDirectory, _materialLibraryName, ns, dataCache.MaterialLibrary.XmlWorker);
                SaveOneFile(myDirectory, _setupParametersName, ns, dataCache.SetupParameters.XmlWorker);
                return eDataCacheServiceOperation.eSuccess;
            }
            catch (System.Exception ex)
            {
                string error = ex.ToString();
                return eDataCacheServiceOperation.eFailed;
            }
        }

        private void SaveOneFile(DirectoryInfo myDirectory, string fileName, XNamespace ns, XEP_IXmlWorker xmlWorker)
        {
            FileInfo myFileSetup = new FileInfo(Path.Combine(myDirectory.FullName, fileName + ".xml"));
            if (myFileSetup.Exists == false)
            {
                throw new ApplicationException("No xml file for load exists !");
            }
            XDocument documentXmlSetup = XDocument.Load(myFileSetup.FullName);
            XElement elementXmlSetup = documentXmlSetup.Element(ns + xmlWorker.GetXmlElementName());
            if (elementXmlSetup == null)
            {
                throw new ApplicationException("Invalid XML file for load !");
            }
            xmlWorker.LoadFromXmlElement(elementXmlSetup);
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
            // Save material library
            XDocument documentXmlSetup = new XDocument(
                new XDeclaration("1.0", null, "yes"),
                new XComment(Resources.ResourceManager.GetString("COMMENT_XML")));
            SaveOneXmlFile(documentXmlSetup, dataCache.SetupParameters.XmlWorker.GetXmlElement(), Path.Combine(myDirectory.FullName, _setupParametersName + ".xml"));
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
