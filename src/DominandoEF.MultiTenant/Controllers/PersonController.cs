﻿using DominandoEF.MultiTenant.Data;
using DominandoEF.MultiTenant.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DominandoEF.MultiTenant.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get([FromServices] ApplicationContext db)
        {
            var peaple = db.People.ToArray();

            return peaple;
        }
    }
}
