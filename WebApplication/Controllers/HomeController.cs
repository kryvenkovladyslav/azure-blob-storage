using AzureBlobStorage.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models.RequestModels;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    [Authorize(Policy = ClaimTypes.Role)]
    public class HomeController : Controller
    {
        private readonly IAzureBlobStorageService blobService;

        public HomeController(IAzureBlobStorageService blobService)
        {
            this.blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roleClaim = this.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            var containerName = string.Concat(roleClaim.ToLower(), "-images");

            var requestModel = new AzureFileRequestModel
            {
                UserRolePolicy = roleClaim,
                ContainerName = containerName
            };

            var images = (await this.blobService.GetBlobsAsync(requestModel))
                .Select(img => new FileViewModel
                {
                    ContainerName = containerName,
                    FullyQualifiedUri = img.FullyQualifiedUri,
                    Name = img.Name,
                });

            return this.View(nameof(this.Index), images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleFilesView(ContainerSelectionViewModel viewModel)
        {
            var roleClaim = this.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            var containerName = string.Concat(viewModel.ContainerName.ToLower(), "-images");

            var requestModel = new AzureFileRequestModel
            {
                UserRolePolicy = roleClaim,
                ContainerName = string.Concat(viewModel.ContainerName.ToLower(), "-images")
            };

            var images = (await this.blobService.GetBlobsAsync(requestModel))
                .Select(img => new FileViewModel
                {
                    ContainerName = containerName,
                    FullyQualifiedUri = img.FullyQualifiedUri,
                    Name = img.Name,
                });


            return this.View(nameof(this.RoleFilesView), images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadFileViewModel fileViewModel)
        {
            var file = new AzureFile
            {
                Name = fileViewModel.File.FileName,
                Stream = fileViewModel.File.OpenReadStream()
            };
            var roleClaim = this.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

            var uri = await this.blobService.UploadAsync(new AzureFileUploadRequestModel
            {
                File = file,
                ContainerName = string.Concat(roleClaim.ToLower(), "-images"),
                OverrideExistingNamesFiles = true,
                UserRolePolicy = this.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault()
            });

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string containerName, string imageName)
        {
            await this.blobService.DeleteAsync(new AzureRemoveFileRequestModel
            {
                ContainerName = containerName,
                FileName = imageName
            });

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}