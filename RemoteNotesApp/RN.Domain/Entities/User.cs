using RN.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RN.Domain.Entities
{
    public class User
    {
        public User()
        {
         //   Roles = new List<Role>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> Roles { get; set; }

        //public string Address { get; set; }
        //public string City { get; set; }
        //public string Region { get; set; }
        //public string PostalCode { get; set; }
        //public string Country { get; set; }
        //public string Phone { get; set; }
        //public string Fax { get; set; }
    }
}
