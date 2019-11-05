using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PManagement.Core.Entities;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Entity.Company;
using PManagement.Utils.Cryptography;

namespace PManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository CompanyRepository;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IUserRepository UserRepository;

        public CompanyController(ICompanyRepository companyRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.CompanyRepository = companyRepository;
            this.UserRepository = userRepository;
            this.UnitOfWork = unitOfWork;
        }

        // GET: api/company
        [HttpGet("GetCompany")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/company
        [HttpPost]
        //public int Post([FromBody] CompanyRequest companyRequest)
        public async Task<int> Post([FromBody] CompanyRequest companyRequest)
        {
            var existUser = await UserRepository.GetByEmailAsync(companyRequest.UserInfo.Email.Trim());
            if (existUser != null)
                throw new ValidationException("User Email already exists");

            var company = new Company()
            {
                Email = companyRequest.CompanyInfo.Email,
                Name = companyRequest.CompanyInfo.Name,
                IsEnabled = true
            };

            string salt = Cryptography.GenerateSalt();
            var user = new User()
            {
                Address = new Core.ValueObjects.Address(companyRequest.UserInfo.Address, companyRequest.UserInfo.PostCode, companyRequest.UserInfo.City, companyRequest.UserInfo.Country),
                FirstName = companyRequest.UserInfo.FirstName,
                Email = companyRequest.UserInfo.Email,
                BirthDate = companyRequest.UserInfo.BirthDate,
                LastName = companyRequest.UserInfo.LastName,
                IsEnabled = true,
                Salt = salt,
                Password = Cryptography.GenerateHash(companyRequest.UserInfo.Password, salt)
            };

            company.Users.Add(user);
            CompanyRepository.Insert(company);
            await UnitOfWork.Commit();

            return company.Id;
        }

        // PUT: api/company/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}