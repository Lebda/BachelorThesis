using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using Microsoft.Practices.Unity;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Implementations;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheck.ResTrans;

namespace SectionCheck.Services
{
    public class XEP_DataCacheService : XEP_IDataCacheService, XEP_IQuantityManagerHolder
    {
        IUnityContainer _container = null;
        XEP_IQuantityManager _manager = null;
        public XEP_DataCacheService(IUnityContainer container)
        {
            _container = Exceptions.CheckNull(container);
            _manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
        }
        #region XEP_IQuantityManagerHolder Members
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        #endregion

        #region XEP_IDataCacheService Members
        eDataCacheServiceOperation XEP_IDataCacheService.Load(XEP_IDataCache dataCache)
        {
            XEP_IOneSectionData newSectionData = Exceptions.CheckNull(UnityContainerExtensions.Resolve<XEP_IOneSectionData>(_container));
            newSectionData.InternalForces = GetInternalForces();
            newSectionData.Name = "Section 1";
            XEP_IOneMemberData newMemberData = Exceptions.CheckNull(UnityContainerExtensions.Resolve<XEP_IOneMemberData>(_container));
            newMemberData.Name = "Member 1";
            newMemberData.SaveOneSectionData(newSectionData);
            XEP_IStructure newStructure = Exceptions.CheckNull(UnityContainerExtensions.Resolve<XEP_IStructure>(_container));
            newStructure.SaveOneMemberData(newMemberData);
            dataCache.Structure = newStructure;
            return eDataCacheServiceOperation.eSuccess;
        }
        eDataCacheServiceOperation XEP_IDataCacheService.Save(XEP_IXmlWorker dataCache)
        {
            DirectoryInfo ducumentsDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(ducumentsDirectory.FullName, "XEP_SectionCheck"));
            if (myDirectory.Exists == false)
            {
                myDirectory.Create();
            }
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XDocument documentXML = new XDocument(
            new XDeclaration("1.0", null, "yes"),
            new XComment(Resources.ResourceManager.GetString("COMMENT_XML")));
            XElement xmlElement = dataCache.GetXmlElement();
            documentXML.Add(xmlElement);
            documentXML.Save(Path.Combine(myDirectory.FullName, "XEP_DataCache.xml"));
            return eDataCacheServiceOperation.eSuccess;
        }
        #endregion

        #region METHODS 
        // load from XML !!!!!!!!!!!!!
        XEP_IInternalForceItem CreateForce()
        {
            XEP_IInternalForceItem item = new XEP_InternalForceItem(Manager);
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = false;
            item.Name = "New force";
            return item;
        }
        ObservableCollection<XEP_IInternalForceItem> GetInternalForces()
        {
            ObservableCollection<XEP_IInternalForceItem> collection = new ObservableCollection<XEP_IInternalForceItem>();
            XEP_IInternalForceItem item = new XEP_InternalForceItem(Manager);
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = true;
            item.Name = "RS2-C01.1-1";
            item.N.Value = 150000.0;
            item.Vy.Value = 65000.0;
            item.Vz.Value = 0.0;
            item.Mx.Value = 113000;
            item.My.Value = 32000;
            item.Mz.Value = 68000;
            collection.Add(item);
            item = new XEP_InternalForceItem(Manager);
            item.Name = "RS2-C02.2-4";
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = false;
            item.N.Value = 178000.0;
            item.Vy.Value = 52000.0;
            item.Vz.Value = 0.0;
            item.Mx.Value = 98000;
            item.My.Value = 42000;
            item.Mz.Value = 75000;
            collection.Add(item);
            return collection;
        }
        #endregion

    }
}
