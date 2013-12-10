using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IContainerHolder
    {
        IUnityContainer Container { get; }
    }
}
