using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Enums
{
    /// <summary>
    /// Clarifies material group. Different groups potentially have different stresses.
    /// </summary>
    /// This enum also makes front end grouping easier when creating combo boxes for users
    /// and other such instances
    public enum MaterialGroupingEnum
    {
        Plate = 0,
        Pipe = 1,
        Tube = 2,
        Forging = 3,
    }
}