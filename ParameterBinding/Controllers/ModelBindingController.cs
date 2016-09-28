using ParameterBinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParameterBinding.Controllers
{
    public class ModelBindingController : ApiController
    {
        [HttpGet]
        public string SampleBinding(int id)
        {
            return id.ToString();
        }

        [HttpGet]
        public string OptionParams(int? id = null)
        {
            if (id == null)
            {
                return "id 不得為空！";
            }
            return id.ToString();
        }

        [HttpGet, HttpPost]
        public string TwoMethod(int? id = null)
        {
            if (id == null)
            {
                return "id 不得為空！";
            }
            return id.ToString();
        }


        [HttpPost]
        public string idFromBody([FromBody]int id)
        {
            return id.ToString();
        }

        [HttpGet, HttpPost]
        public string idUriNameBody(int id, [FromBody]string name)
        {
            return $"Id:{id}, name:{name}";
        }

        [HttpPost]
        public string Customer(Customer customer)
        {
            return customer.ToString();
        }

        [HttpPost]
        public string MultiCustomer(Customer customer1, Customer customer2)
        {
            return $"{customer1.ToString()}, {customer2.ToString()}";
        }
    }
}
