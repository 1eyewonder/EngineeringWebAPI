using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EngineeringWebAPI.Data;
using EngineeringWebAPI.Enums;
using EngineeringWebAPI.Helpers;
using EngineeringWebAPI.Models;

namespace EngineeringWebAPI.Controllers
{
    public class ASMEMaterialsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ASMEMaterials/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ASMEMaterial aSMEMaterial = db.ASMEMaterials.Find(id);
            if (aSMEMaterial == null)
            {
                return NotFound();
            }

            return Ok(aSMEMaterial);
        }

        // GET: api/ASMEMaterials
        [HttpGet]
        public IHttpActionResult GetASMEMaterials()
        {
            return Ok(db.ASMEMaterials.OrderBy(m =>m.Material));
        }

        /// <summary>
        /// Returns the flange material class of the requested material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ASMEMaterials/FlangeClass/{material=}")]
        public IHttpActionResult GetFlangeClass(string material)
        {
            FlangeMaterialClassEnum flangeClass;

            try
            {
                flangeClass = db.ASMEMaterials.Where(m => m.Material == material).FirstOrDefault().FlangeMaterialClass;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }          

            return Ok(flangeClass);
        }

        /// <summary>
        /// Returns the material classification of the requested material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ASMEMaterials/Classification/{material=}")]
        public IHttpActionResult GetClassification(string material)
        {
            MaterialClassificationEnum materialClassification;

            try
            {
                materialClassification = db.ASMEMaterials.Where(m => m.Material == material).FirstOrDefault().MaterialClassification;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }

            return Ok(materialClassification);
        }

        /// <summary>
        /// Returns the stress for the requested material at the given temperature and ASME code edition year
        /// </summary>
        /// <param name="material">Material name</param>
        /// <param name="temp">Temperature</param>
        /// <param name="year">ASME code edition year</param>
        /// <returns></returns>
        /// Go to linear interpolation method called near bottom for explanation and link on what it is
        [HttpGet]
        [Route("api/ASMEMaterials/Stress/{material=}/{temp=}/{year=}")]
        public IHttpActionResult GetStress(string material, float temp, string year)
        {
            //Part of bounds for linear interpolation
            float? maxTemp = db.ASMEMaterials.Where(m => m.Material == material).Where(y => y.ASMEYear == year).Max(t => t.Temperature);
            float? minTemp = db.ASMEMaterials.Where(m => m.Material == material).Where(y => y.ASMEYear == year).Min(t => t.Temperature);

            //Bad request catches
            if (maxTemp == null)
            {
                return BadRequest("The material, year, or combination of the two does not exist");
            }

            if (temp > maxTemp || temp < minTemp)
            {
                return BadRequest("Temperature is out of range for this material. Temperature range is between {minTemp} and {maxTemp}");
            }

            //Attempts to grab data with the requested parameters
            ASMEMaterial aSMEMaterial = db.ASMEMaterials.Where(m => m.Material == material).Where(t => t.Temperature == temp).Where(y => y.ASMEYear == year).FirstOrDefault();

            //Temperature requested matches one in the database. No interpolation is required
            if (aSMEMaterial != null)
            {
                return Ok(aSMEMaterial.Stress);
            }

            //>>>>>>>>>>>
            //Linear interpolation is required
            //>>>>>>>>>>>

            //Orders by entries with temperatures closest to the requested temperature
            var result = db.ASMEMaterials.Where(m=>m.Material == material).Where(y =>y.ASMEYear == year).OrderBy(n => Math.Abs(n.Temperature - temp));

            //First data point for linear interpolation
            var point1 = result.FirstOrDefault();

            //Second data point for linear interpolation
            ASMEMaterial point2;

            //Find t2. Need to set t2 as opposite bound so that design temperature is between t1 and t2
            if (temp <= point1.Temperature)
            {
                point2 = result.FirstOrDefault(x => x.Temperature < temp);
            }
            else
            {
                point2 = result.FirstOrDefault(x => x.Temperature > temp);
            }

            //Calculates stress using linear interpolation
            var returnStress = MathExtension.LinearInterpolation(temp, point1.Temperature, point2.Temperature, point1.Stress, point2.Stress);

            return Ok(returnStress);
        }

        /// <summary>
        /// Returns distinct materials for the requested material group
        /// </summary>
        /// <param name="materialGrouping"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ASMEMaterials/GroupMaterials/{materialGrouping=}")]
        public IHttpActionResult GetGroupMaterials(MaterialGroupingEnum materialGrouping)
        {
            var groupMaterials = db.ASMEMaterials.Where(m => m.MaterialGrouping == materialGrouping).OrderBy(m => m.Material).Select(m => m.Material).Distinct();

            //Not found catch
            if(groupMaterials == null)
            {
                return NotFound();
            }

            return Ok(groupMaterials);
        }

        // PUT: api/ASMEMaterials/5
        public IHttpActionResult Put(int id, [FromBody]ASMEMaterial aSMEMaterial)
        {
            //Bad request catches
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = db.ASMEMaterials.FirstOrDefault(q => q.Id == id);

            if (entity == null)
            {
                return BadRequest("No record found against this id");
            }

            //Automapper can be implemented later. DTO to be used to pass data in for security purposes
            entity.Material = aSMEMaterial.Material;
            entity.Temperature = aSMEMaterial.Temperature;
            entity.Stress = aSMEMaterial.Stress;
            entity.ASMEYear = aSMEMaterial.ASMEYear;
            entity.FlangeMaterialClass = aSMEMaterial.FlangeMaterialClass;
            entity.MaterialClassification = aSMEMaterial.MaterialClassification;

            db.SaveChanges();
            return Ok("Record updated successfully");
            
        }

        // POST: api/ASMEMaterials
        public IHttpActionResult Post([FromBody]ASMEMaterial aSMEMaterial)
        {
            //Bad request catch
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Adds entry and saves database
            db.ASMEMaterials.Add(aSMEMaterial);
            db.SaveChanges();

            return Ok("Record added successfully");
        }

        // DELETE: api/ASMEMaterials/5
        [ResponseType(typeof(ASMEMaterial))]
        public async Task<IHttpActionResult> DeleteASMEMaterial(int id)
        {
            //Not found catch
            ASMEMaterial aSMEMaterial = await db.ASMEMaterials.FindAsync(id);
            if (aSMEMaterial == null)
            {
                return NotFound();
            }

            //Removes entry and saves database
            db.ASMEMaterials.Remove(aSMEMaterial);
            await db.SaveChangesAsync();

            return Ok(aSMEMaterial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}