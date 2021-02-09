using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entites.Concrete
{
    public class Customer:IEntity
    {
        public string CustomerId { get; set; }
        public string ContactId { get; set; }

        public string CompanyNAme { get; set; }
        public string City { get; set; }

    }
}
