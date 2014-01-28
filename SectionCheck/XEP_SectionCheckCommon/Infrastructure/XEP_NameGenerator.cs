using System;
using System.Linq;
using XEP_SectionCheckCommon.Res;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public class XEP_NameGenerator : XEP_INameGenerator
    {
        public XEP_NameGenerator()
        {
        }

        string XEP_INameGenerator.GetResourceMarkString(string source)
        {
            return Resources.ResourceManager.GetString(source + "_MARK");
        }
    }
}
