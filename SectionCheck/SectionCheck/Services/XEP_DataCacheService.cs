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

            //////////////////////////////////////////////////////////////////////////
//             string loadInfo = "OK";
//             try
//             {
//                 if ((fileName == null) || (fileName.Count() <= 0))
//                 {
//                     loadInfo = "Invalid file name !";
//                     throw new ArgumentException(loadInfo);
//                 }
//                 string absolutePath = null;
//                 if (path == null)
//                 {
//                     absolutePath = @"c:\CheckerGame";
//                 }
//                 else
//                 {
//                     absolutePath = path;
//                 }
//                 DirectoryInfo myDirectory = new DirectoryInfo(absolutePath);
//                 if (myDirectory.Exists == false)
//                 {
//                     loadInfo = "No xml file for creating Checker game";
//                     throw new ApplicationException(loadInfo);
//                 }
//                 if (adExtension == true)
//                 {
//                     string extension = ".xml";
//                     fileName += extension;
//                 }
//                 fileName = Path.Combine(absolutePath, fileName);
//                 FileInfo myFile = new FileInfo(fileName);
//                 if (myFile.Exists == false)
//                 {
//                     loadInfo = "No xml file for creating Checker game";
//                     throw new ApplicationException(loadInfo);
//                 }
//                 XNamespace ns = CParachutingCheckerUtils.sc_xml_ns;
//                 XDocument documentXML = XDocument.Load(fileName);
//                 //////////////////////////////////////////////////////////////////////////
//                 // 22.4.13 validate document with help of xsd
//                 if (!adExtension)
//                 { // has to be in this way, else problem in UT
//                     // READING FROM FILE
//                     // string file = "XmlXsdTest.xsd";
//                     // FileStream fs = new FileStream(file, FileMode.Open);
//                     // XmlTextReader r = new XmlTextReader(fs);
//                     // XmlSchemaSet schemas = new XmlSchemaSet();
//                     //schemas.Add(ns.ToString(), r);
//                     //
//                     XmlSchemaSet schemas = new XmlSchemaSet();
//                     schemas.Add(ns.ToString(), XmlReader.Create(new StringReader(GetXsdValidation())));
//                     bool errors = false;
//                     documentXML.Validate(schemas, (o, e) =>
//                     {
//                         loadInfo = e.Message.ToString();
//                         Console.WriteLine("{0}", loadInfo);
//                         errors = true;
//                     }, true);
//                     if (!isWPFDebug)
//                     {
//                         if (errors)
//                         { // Do not provide LOAD action
//                             return loadInfo;
//                         }
//                     }
//                 }
//                 //////////////////////////////////////////////////////////////////////////
//                 XElement elementXML = documentXML.Element(ns + CCheckerGame.sc_xmlElementName);
//                 if (elementXML == null)
//                 {
//                     loadInfo = "Invalid XML file !";
//                     throw new ApplicationException(loadInfo);
//                 }
//                 GameState.LoadFromXmlElement(elementXML.Element(ns + CCheckerGameState.sc_xmlElementName));
//                 Board.LoadFromXmlElement(elementXML.Element(ns + CCheckerBoard.sc_xmlElementName));
//                 Players.LoadFromXmlElement(elementXML.Element(ns + CCheckerGamePlayers.sc_xmlElementName));
//                 GameCounters.LoadFromXmlElement(elementXML.Element(ns + CCheckerGameCounters.sc_xmlElementName));
//                 GameSteps.LoadFromXmlElement(elementXML.Element(ns + CCheckerGameSteps.sc_xmlElementName));
//                 Counters.Update(Board);
//                 if (!isWPFDebug)
//                 {
//                     GeneratePossibleMovesAndBestMoves();
//                 }
//                 return loadInfo;
//             }
//             catch (System.Exception ex)
//             {
//                 CatchError4Console(ref ex);
//                 return loadInfo;
//             }
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
