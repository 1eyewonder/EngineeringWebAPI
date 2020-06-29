using EngineeringWebAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Models
{
    /// <summary>
    /// Data model for ASME material stress data
    /// </summary>
    public class ASMEMaterial
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Material name
        /// </summary>
        [Required]
        public string Material { get; set; }

        /// <summary>
        /// Temperature (F)
        /// </summary>
        [Required]
        public float Temperature { get; set; }

        /// <summary>
        /// Stress (psi)
        /// </summary>
        [Required]
        public float Stress { get; set; }

        /// <summary>
        /// ASME edition being referenced
        /// </summary>
        [Required]
        public string ASMEYear { get; set; }

        /// <summary>
        /// Class of flange material found in ASME B16.5
        /// </summary>
        [Required]
        public FlangeMaterialClassEnum FlangeMaterialClass { get; set; }

        /// <summary>
        /// Classifies material as carbon, stainless, etc.
        /// </summary>
        [Required]
        public MaterialClassificationEnum MaterialClassification { get; set; }

        /// <summary>
        /// Group material belongs to, i.e. plate, tube, etc.
        /// </summary>
        [Required]
        public MaterialGroupingEnum MaterialGrouping { get; set; }
    }
}