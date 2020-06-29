using EngineeringWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EngineeringWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base ("name = EngineeringWebAPIDataContext")
        {
        }

        public DbSet<ASMEMaterial> ASMEMaterials { get; set; }
        public DbSet<PipeSchedule> PipeSchedules { get; set; }
    }
}