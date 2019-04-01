﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    public class CampsController : ControllerBase
    {
        public object Get()
        {
            return new { Moniker = "ATL2018", Name = "Atlanta Code Camp" };
        }
    }
}
