using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Models
{
    /// <summary>
    /// Data model for pipe schedule data
    /// </summary>
    public class PipeSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// Nominal pipe size
        /// </summary>
        public string NPS { get; set; }

        [Required]
        /// <summary>
        /// Pipe outer diameter
        /// </summary>
        public float OD { get; set; }

        [Required]
        [StringLength(3)]
        /// <summary>
        /// Pipe schedule
        /// </summary>
        public string Schedule { get; set; }

        [Required]
        /// <summary>
        /// Pipe thickness
        /// </summary>
        public float WallThickness { get; set; }

        [Required]
        /// <summary>
        /// Boolean for if pipe thickness is acceptable to API 661 standards
        /// </summary>
        /// API 661 pipe thicknesses only apply to carbon steel for the current edition
        public bool API661Approved { get; set; }
    }
}