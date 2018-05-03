using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.User;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;
using TwitterBackup.Web.Areas.Admin.Models;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly ITweeterApiService tweeterApiService;
        private readonly IUserService userService;
        private readonly IUserService userDbService;
        private readonly ITweeterService tweeterService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mappingProvider;

        public UsersController(UserManager<ApplicationUser> userManager, IMappingProvider mappingProvider,
            ITweeterService tweeterService, IUserService userService, ITweeterApiService tweeterApiService)
        {
            this.tweeterApiService = tweeterApiService ?? throw new ArgumentNullException(nameof(tweeterApiService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<UserDto> userDtos;
            IEnumerable<UserViewModel> userViewModels;

            if (searchString == null)
            {
                userDtos = await userService.GetAllUsersAsync();
                userViewModels = mappingProvider.ProjectTo<UserDto, UserViewModel>(userDtos);
            }
            else
            {
                userDtos = await userService.SearchUserAsync(searchString);
                userViewModels = mappingProvider.ProjectTo<UserDto, UserViewModel>(userDtos);
            }

            return View(userViewModels);

        }
    }
}