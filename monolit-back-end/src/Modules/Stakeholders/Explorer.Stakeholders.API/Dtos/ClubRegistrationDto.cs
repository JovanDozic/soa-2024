﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubRegistrationDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL {  get; set; }
        public int OwnerId { get; set; }
        
    }
}
