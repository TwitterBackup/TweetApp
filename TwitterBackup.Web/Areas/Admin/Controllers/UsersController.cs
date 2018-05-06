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
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mappingProvider;

        public UsersController(UserManager<ApplicationUser> userManager, IMappingProvider mappingProvider, IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<UserDto> userDtos;
            IEnumerable<UserViewModel> userViewModels;

            if (searchString == null)
            {
                userDtos = await userService.GetAllActiveUsersAsync();
                userViewModels = mappingProvider.ProjectTo<UserDto, UserViewModel>(userDtos);
            }
            else
            {
                userDtos = await userService.SearchUserAsync(searchString);
                userViewModels = mappingProvider.ProjectTo<UserDto, UserViewModel>(userDtos);
            }

           return View(userViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userName)
        {

            if (userName == null)
            {
                TempData["Result"] = "Such tweeter does not exist. ";
                return RedirectToAction(nameof(Index));
            }

            UserDto userDto;

            try
            {
                userDto = await userService.FindUserByNameAsync(userName);
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                if (currentUser.UserName != userDto.UserName)
                {
                    await userService.RemoveAsync(userDto);
                }
                else
                {
                    return Json("Warning! You cannot delete yourself!");
                }
            }
            catch (Exception)
            {
                return Json("Something went wrong! Please, try again or call developer!");
            }

            return Json("success");
        }




        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Json("UserName cannot be null or whitespace.");
            }

            if (ModelState.IsValid)
            {
                var userDto = await userService.FindUserByNameAsync(userName);
                if (userDto == null)
                {
                    return Json("Such tweet is not found. Please check and try again!");
                }

                var editUserViewModel = mappingProvider.MapTo<EditUserViewModel>(userDto);

                return PartialView("_Edit", editUserViewModel);
            }
            else
            {
                return Json("Something went wrong. No tweet was deleted");
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public IActionResult Edit(EditUserViewModel userForEditViewModel)
        {
            if (userForEditViewModel == null)
            {
                return Json("Such user is not found. Please check and try again!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editUserDto = mappingProvider.MapTo<EditUserDto>(userForEditViewModel);
                    userService.Update(editUserDto);
                    return Json("success");
                }
                catch (ArgumentException)
                {
                    return Json("There's no such User.");
                }
            }

            return Json("Something went wrong. No user was edited");
        }
    }
}