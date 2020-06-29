using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Helpers
{
    /// <summary>
    /// Static class containing mathematical extension methods
    /// </summary>
    public class MathExtension
    {
        /// <summary>
        /// Returns y0 value using linear interpolation
        /// </summary>
        /// <param name="x0">Given value</param>
        /// <param name="x1">Lower bounds</param>
        /// <param name="x2">Upper bounds</param>
        /// <param name="y1">Lower bounds output</param>
        /// <param name="y2">Upper bounds output</param>
        /// <returns></returns>
        /// https://en.wikipedia.org/wiki/Linear_interpolation
        public static double LinearInterpolation(double x0, double x1, double x2, double y1, double y2)
        {
            if (x0 == x1 || x0 == x2)
            {
                return (y1 + y2) / 2;
            }

            else
            {
                return y1 + (x0 - x1) * (y2 - y1) / (x2 - x1);
            }
        }
    }
}