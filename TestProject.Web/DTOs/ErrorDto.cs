﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Web.DTOs
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<String>();
        }

        public List<String> Errors { get; set; }
        public int Status { get; set; }

    }
}
