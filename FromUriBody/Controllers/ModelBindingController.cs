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

        /// <summary>
        /// 簡單型別指定[FromBody]
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Post([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name is empty!");
            }
            var hello = "Hello " + name;
            return Ok(hello);
        }

        /// <summary>
        /// 錯誤示範 FromBody 不能在參數內使用兩次
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>IHttpActionResult.</returns>
        //public IHttpActionResult PostError([FromBody]int id,[FromBody] string name)
        //{
        //    return InternalServerError();
        //}

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
