using ModelValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ModelValidation.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Posts the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>HttpResponseMessage.</returns>
        public HttpResponseMessage Post(Product product)
        {
            if (ModelState.IsValid)
            {
                // Todo: 用 product 做些事
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
