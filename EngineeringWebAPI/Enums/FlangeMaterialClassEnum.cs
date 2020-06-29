using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Enums
{
    /// <summary>
    /// Class of material for flange rating found in ASME B16.5
    /// </summary>
    /// Used for pressure temperature rating look ups
    /// Just two enums used to keep things simple
    public enum FlangeMaterialClassEnum
    {
        //Carbon steel
        Class1 = 0,

        //Stainless steel
        Class5 = 0
    }
}