using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Enums
{
    /// <summary>
    /// Classifies material as carbon, stainless, etc. for code identification
    /// </summary>
    /// Certain codes such as API-661 only apply to carbon steel materials
    /// Using an enum like this makes filtering easier for back end calculations
    public enum MaterialClassificationEnum
    {
        CarbonSteel = 0,
        StainlessSteel = 1
    }
}