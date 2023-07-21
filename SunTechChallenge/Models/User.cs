﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTechChallenge.Models
{
    public class User
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BirthdayInEpoch { get; set; }
        public string Email { get; set; }
    }
}
