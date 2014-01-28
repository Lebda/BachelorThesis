using System;
using System.Linq;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public interface XEP_INameGenerator
    {
        string GetResourceMarkString(string source);
    }
}
