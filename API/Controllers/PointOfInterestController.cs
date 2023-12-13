using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/cities/{CityId}/pointofinterest")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointOfInterest(int cityId)  //returns the pointeres of a particular city. One city could have multiple pois, hence the ienumerable poi
        {
            var city = CitiesDataStore.Current.Cities  //where Cities is a list of citydto
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterest); //returns citiesdatastore.current.cities.pointofinterest 
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDTO> GetPointOfInterest(int cityId, int pointOfInterestId) //returns the particular poi of a city using ids
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDTO> CreatePointOfInterest(int cityId, PointOfInterestForCreationDTO pointOfInterest) //creating poi for a city
        {

            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(  //selectmany combines the records from a sequence of results and then converts it into one result
                c => c.PointOfInterest).Max(p => p.Id); //it returns the poi in the cities and gets the max id of poi 

            //to map the poiForCreationdto
            var finalPointOfInterest = new PointOfInterestDTO()  //create a new instance of poi
            {
                Id = ++maxPointOfInterestId, //adds 1 to the previously calculated id
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };
            city.PointOfInterest.Add(finalPointOfInterest); //we add the fpoi to the existing poi collection
            return CreatedAtRoute("GetPointOfInterest",  //createdatroute method is used instead of redirect(views) as seen in mvc cause of the endpoints where http response is needed
                new
                {
                    cityId = cityId,
                    pointofInterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest);
        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDTO pointOfInterest)
        {

            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        //Partially updating a resource: if we want to update a specific resource, we use a patchdocument

        [HttpPatch("(pointofinterestid)")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,   //JSON Patch is a format for specifying updates to be applied to a resource. A JSON Patch
            JsonPatchDocument<PointOfInterestForUpdateDTO> patchDocument)                       //document has an array of operations.Each operation identifies a particular type of change
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
             
            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDTO()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description,
                };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            //validate the patch operation as the input model here is the patchDocument
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validate the patched doc - poiForUpdate model itself after applying the patch document
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            //update the poifromstore values to the new patched values
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId) 
        { 
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            { 
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if(pointOfInterest == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(pointOfInterest);
            return NoContent();
        }
    }
}
