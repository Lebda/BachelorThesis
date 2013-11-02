using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Interfaces
{
    public interface ICloneAble
    {
        /// <summary>
        /// Create deep copy of object
        /// </summary>
        /// <returns></returns>
        ICloneAble Clone();
    }
}
