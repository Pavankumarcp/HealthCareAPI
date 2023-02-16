﻿using System;
using System.Reflection.Metadata;

namespace HealthCare.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Spec { get; set; }
        public DateTime SlotDate { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
