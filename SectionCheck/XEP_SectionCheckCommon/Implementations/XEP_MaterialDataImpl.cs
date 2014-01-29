using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Implementations
{
//     class XEP_MaterialDataImplXml : XEP_XmlWorkerImpl
//     {
//         readonly XEP_MaterialDataImpl _data = null;
//         public XEP_MaterialDataImplXml(XEP_MaterialDataImpl data)
//         {
//             _data = data;
//         }
//         #region XEP_XmlWorkerImpl Members
//         public override string GetXmlElementName()
//         {
//             return "XEP_MaterialDataImpl";
//         }
//         protected override string GetXmlElementComment()
//         {
//             return "Object represents general material data.";
//         }
//         protected override void AddElements(XElement xmlElement)
//         {
//             foreach (var item in _data.StressStrainDiagram)
//             {
//                 xmlElement.Add(item.XmlWorker.GetXmlElement());
//             }
//         }
//         protected override void LoadElements(XElement xmlElement)
//         {
//             XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
//             var xmlElemColection = xmlElement.Elements(ns + _data.ResolverESDiagramItem.Resolve().XmlWorker.GetXmlElementName());
//             if (xmlElemColection != null && xmlElemColection.Count() > 0)
//             {
//                 for (int counter = 0; counter < xmlElemColection.Count(); ++counter)
//                 {
//                     XElement xmlElemItem = Exceptions.CheckNull<XElement>(xmlElemColection.ElementAt(counter), "Invalid XML file");
//                     XEP_IESDiagramItem item = _data.ResolverESDiagramItem.Resolve();
//                     item.XmlWorker.LoadFromXmlElement(xmlElemItem);
//                     _data.StressStrainDiagram.Add(item);
//                 }
//             }
//         }
//         protected override void AddAtributes(XElement xmlElement)
//         {
//             XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
//             xmlElement.Add(new XAttribute(ns + "DiagramType", XEP_Conventors.ConvertDiagramType(_data.DiagramType, false)));
//             xmlElement.Add(new XAttribute(ns + XEP_MaterialDataImpl.MatFromLibPropertyName, _data.MatFromLib));
//         }
//         protected override void LoadAtributes(XElement xmlElement)
//         {
//             XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
//             _data.DiagramType = XEP_Conventors.ConvertDiagramType((string)xmlElement.Attribute(ns + "DiagramType"), false);
//             _data.MatFromLib = (bool)xmlElement.Attribute(ns + XEP_MaterialDataImpl.MatFromLibPropertyName);
//         }
//         #endregion
//     }

//     [Serializable]
//     public class XEP_MaterialDataImpl : ObservableObject, XEP_IMaterialData
//     {
//         readonly XEP_IResolver<XEP_IESDiagramItem> _resolverESDiagramItem = null;
//         readonly XEP_IResolver<XEP_IMaterialData> _resolver = null;
//         public XEP_IResolver<XEP_IESDiagramItem> ResolverESDiagramItem
//         {
//             get { return _resolverESDiagramItem; }
//         }
//         // ctors
//         public XEP_MaterialDataImpl(XEP_IQuantityManager manager, XEP_IResolver<XEP_IESDiagramItem> resolverESDiagramItem,
//             XEP_IResolver<XEP_IMaterialData> resolver)
//         {
//             _manager = manager;
//             _xmlWorker = new XEP_MaterialDataImplXml(this);
//             _resolver = resolver;
//             _resolverESDiagramItem = resolverESDiagramItem;
//         }
// 
//         #region XEP_IMaterialData Members
//         public void CreatePoints(XEP_ISetupParameters setup)
//         {
//         }
//         public XEP_IMaterialData CopyInstance()
//         {
//             XEP_IMaterialData item = _resolver.Resolve();
//             item.DiagramType = _diagramType;
//             item.StressStrainDiagram = DeepCopy.Make<ObservableCollection<XEP_IESDiagramItem>>(_stressStrainDiagram);
//             item.MatFromLib = _matFromLib;
//             item.Name = _name;
//             return item; 
//         }
//         public static readonly string GammaCPropertyName = "GammaC";
//         public const string StressStrainDiagramPropertyName = "StressStrainDiagram";
//         private ObservableCollection<XEP_IESDiagramItem> _stressStrainDiagram = new ObservableCollection<XEP_IESDiagramItem>();
//         public ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram
//         {
//             get {return  _stressStrainDiagram;}
//             set
//             {
//                 if ( _stressStrainDiagram == value){return;}
//                  _stressStrainDiagram = value;
//                 RaisePropertyChanged(StressStrainDiagramPropertyName);
//             }
//         }
//         public const string DiagramTypePropertyName = "DiagramType";
//         private eEP_MaterialDiagramType _diagramType = eEP_MaterialDiagramType.eBiliUls;
//         public eEP_MaterialDiagramType DiagramType
//         {
//             get {return  _diagramType;}
//             set
//             {
//                 if ( _diagramType == value){return;}
//                  _diagramType = value;
//                 RaisePropertyChanged(DiagramTypePropertyName);
//             }
//         }
//         public const string MatFromLibPropertyName = "MatFromLib";
//         private bool _matFromLib = true;
//         public bool MatFromLib
//         {
//             get { return _matFromLib; }
//             set
//             {
//                 if (_matFromLib == value) { return; }
//                 _matFromLib = value;
//                 RaisePropertyChanged(MatFromLibPropertyName);
//             }
//         }
//         #endregion
//         #region XEP_IDataCacheObjectBase Members
//         XEP_IQuantityManager _manager = null;
//         XEP_IXmlWorker _xmlWorker = null;
//         string _name = String.Empty;
//         public string Name
//         {
//             get { return _name; }
//             set { _name = value; }
//         }
//         public XEP_IQuantityManager Manager
//         {
//             get { return _manager; }
//             set { _manager = value; }
//         }
//         public XEP_IXmlWorker XmlWorker
//         {
//             get { return _xmlWorker; }
//             set { _xmlWorker = value; }
//         }
//         #endregion
//     }
}
