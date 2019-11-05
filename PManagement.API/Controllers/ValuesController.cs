using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PManagement.Core.Entities;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Core.Interfaces.Services;

namespace PManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITokenInfoRepository tokenRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyRepository companyRepository;
        private readonly IAuthenticationService authenticationService;

        public ValuesController(ITokenInfoRepository tokenRepository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository, IAuthenticationService authenticationService)
        {
            this.tokenRepository = tokenRepository;
            this.unitOfWork = unitOfWork;
            this.companyRepository = companyRepository;
            this.authenticationService = authenticationService;
        }

        // GET api/values
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            string tst = "tst";
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return Ok("test");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
