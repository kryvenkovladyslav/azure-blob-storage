using AzureBlobStorage.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Common;
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
            var roleClaim = this.GetUserRoleClaim();
            var containerName = string.Concat(roleClaim.ToLower(), UrlConstants.ImagePostfix);

            var requestModel = new AzureFileRequestModel
            {
                UserRolePolicy = roleClaim,
                ContainerName = containerName
            };
            var images = (await this.blobService.GetBlobsAsync(requestModel))
                .Select(img => new FileViewModel
                {
                    FullyQualifiedUri = img.FullyQualifiedUri,
                    Name = img.Name,
                });

            var containerFiles = new ContainerFilesViewModel
            {
                ContainerName = containerName,
                Files = images
            };


            return this.View(nameof(this.Index), containerFiles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleFilesView(ContainerSelectionViewModel viewModel)
        {
            var containerName = string.Concat(viewModel.ContainerName.ToLower(), UrlConstants.ImagePostfix);

            var requestModel = new AzureFileRequestModel
            {
                UserRolePolicy = this.GetUserRoleClaim(),
                ContainerName = containerName
            };

            var images = (await this.blobService.GetBlobsAsync(requestModel))
                .Select(img => new FileViewModel
                {
                    FullyQualifiedUri = img.FullyQualifiedUri,
                    Name = img.Name,
                });

            var containerFiles = new ContainerFilesViewModel
            {
                ContainerName = containerName,
                Files = images
            };


            return this.View(nameof(this.RoleFilesView), containerFiles);
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
            var roleClaim = this.GetUserRoleClaim();

            var uri = await this.blobService.UploadAsync(new AzureFileUploadRequestModel
            {
                File = file,
                ContainerName = fileViewModel.RoleContainer ?? string.Concat(roleClaim.ToLower(), UrlConstants.ImagePostfix),
                OverrideExistingNamesFiles = true,
                UserRolePolicy = roleClaim
            });

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string containerName, string imageName)
        {
            await this.blobService.DeleteAsync(new AzureRemoveFileRequestModel
            {
                UserRolePolicy = this.GetUserRoleClaim(),
                ContainerName = containerName,
                FileName = imageName
            });

            return this.RedirectToAction(nameof(this.Index));
        }

        private string GetUserRoleClaim()
        {
            return this.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .FirstOrDefault();
        }
    }
}