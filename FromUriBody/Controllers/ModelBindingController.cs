using FromUriBody.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FromUriBody.Controllers
{
    public class ModelBindingController : ApiController
    {
        /// <summary>
        /// 複雜型別指定[FromUri]
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Get([FromUri] GeoPoint location)
        {
            if (location.Latitude == 0 || location.Longitude == 0)
            {
                return BadRequest("Location is empty!");
            }
            var geo = $"Latitude:{location.Latitude},Longitude:{location.Longitude}";
            return Ok(geo);
        }

        // POST: api/ModelBinding
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ModelBinding/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ModelBinding/5
        public void Delete(int id)
        {
        }
    }
}
