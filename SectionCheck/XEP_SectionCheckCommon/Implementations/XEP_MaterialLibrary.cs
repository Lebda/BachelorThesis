using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
    class XEP_MaterialLibraryXml : XEP_XmlWorkerImpl
    {
        readonly XEP_IMaterialLibrary _data = null;
        public XEP_MaterialLibraryXml(XEP_IMaterialLibrary data)
        {
            _data = data;
        }
        public override string GetXmlElementName()
        {
            return "XEP_MaterialLibrary";
        }
        protected override string GetXmlElementComment()
        {
            return "Objects holds material database.";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.MaterialDataConcrete.Values)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlItems = xmlElement.Elements(ns + _data.ResolverMatConcrete.Resolve().XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IMaterialDataConcrete item = _data.ResolverMatConcrete.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    _data.SaveOneMaterialDataConcrete(item);
                }
            }
        }
    }

    [Serializable]
    public class XEP_MaterialLibrary : XEP_IMaterialLibrary
    {
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = String.Empty;
        Dictionary<string, XEP_IMaterialDataConcrete> _materialDataConcrete = new Dictionary<string, XEP_IMaterialDataConcrete>();
        readonly XEP_IResolver<XEP_IMaterialDataConcrete> _resolverMatConcrete;

        public XEP_MaterialLibrary(XEP_IQuantityManager manager, XEP_IResolver<XEP_IMaterialDataConcrete> resolverMatConcrete)
        {
            _manager = manager;
            _xmlWorker = new XEP_MaterialLibraryXml(this);
            _resolverMatConcrete = resolverMatConcrete;
        }

        public XEP_IResolver<XEP_IMaterialDataConcrete> ResolverMatConcrete
        {
            get { return _resolverMatConcrete; }
        }
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #region XEP_IMaterialLibrary Members
        public List<XEP_IMaterialDataConcrete> GetMaterialConcreteNames
        {
            get
            {
                List<XEP_IMaterialDataConcrete> data = new List<XEP_IMaterialDataConcrete>();
                foreach(var item in _materialDataConcrete)
                {
                    data.Add(item.Value);
                }
                return data;
            }
        }
        public Dictionary<string, XEP_IMaterialDataConcrete> MaterialDataConcrete
        {
            get { return _materialDataConcrete; }
            set { _materialDataConcrete = value; }
        }
        public XEP_IMaterialDataConcrete GetOneMaterialDataConcrete(string matName)
        {
            return Common.GetDataDictionary<string, XEP_IMaterialDataConcrete>(matName, _materialDataConcrete);
        }
        public eDataCacheServiceOperation SaveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData)
        {
            Exceptions.CheckNull(matData);
            if (_materialDataConcrete.ContainsKey(matData.Name))
            {
                matData.Name += "-copy";
            }
            _materialDataConcrete.Add(matData.Name, matData);
            return eDataCacheServiceOperation.eSuccess;
        }
        public eDataCacheServiceOperation RemoveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData)
        {
            Exceptions.CheckNull(matData);
            if (_materialDataConcrete.ContainsKey(matData.Name))
            {
                _materialDataConcrete.Remove(matData.Name);
                return eDataCacheServiceOperation.eSuccess;
            }
            return eDataCacheServiceOperation.eNotFound;
        }
        #endregion
    }
}