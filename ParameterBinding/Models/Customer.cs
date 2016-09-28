using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParameterBinding.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}";
        }
    }
}