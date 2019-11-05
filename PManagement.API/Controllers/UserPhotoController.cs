using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PManagement.API.Controllers.Base;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Core.Interfaces.Services;

namespace PManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotoController : BaseController
    {
        private readonly IHostingEnvironment host;
        private readonly IUserRepository userRepository;
        private readonly IStorageService storageService;
        private readonly IUnitOfWork unitOfWork;

        public UserPhotoController(IHostingEnvironment host, IAuthenticationService authenticationService, IUserRepository userRepository, IStorageService storageService, IUnitOfWork unitOfWork)
            : base(authenticationService)
        {
            this.host = host;
            this.userRepository = userRepository;
            this.storageService = storageService;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Null file");

            var tokenDetails = this.GetTokenInfo();
            if (tokenDetails == null)
                return BadRequest("Invalid User");

            //if (file.Length > photoSettings.MaxBytes)
            //    return BadRequest("Maximum size exceeded (1Mb)");

            //if (!photoSettings.IsSupported(file.FileName))
            //    return BadRequest("Invalid File Type");

            //var folderPath = Path.Combine(host.WebRootPath, "uploads");
            //if (!Directory.Exists(folderPath))
            //    Directory.CreateDirectory(folderPath);

            //var filePath = Path.Combine(folderPath, fileName);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string imgUrl = storageService.Updoad(fileName, file.OpenReadStream());

            var userDetails = await this.userRepository.GetAsync(tokenDetails.UserId);
            if (userDetails != null)
            {
                userDetails.ProfileImg = fileName;
                await unitOfWork.Commit();
            }

            //string url = Request.HttpContext.Request..VirtualPathRoot;

            return Ok(new
            {
                Url = imgUrl,
                FileName = fileName
            });
        }

    }
}