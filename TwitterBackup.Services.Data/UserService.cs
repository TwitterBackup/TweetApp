using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Remotion.Linq.Clauses;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.User;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class UserService: IUserService

    {
        //private readonly HttpContext httpContext;
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(IRepository<ApplicationUser> userRepository/*, HttpContext httpContext, UserManager<ApplicationUser> userManager*/)
        {
            //this.httpContext = httpContext;
            //this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        public void Update(UserDto user)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserDto user)
        {
            throw new NotImplementedException();
        }

        public string FindUserIdByUserName(string userName)
        {
            var user = userRepository.Find(x => x.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return user.Id;
        }

        //public bool CurrentUserIsAdmin()
        //{
        //    return httpContext.User.IsInRole("Admin");
        //}

        //public bool CurrentUserIsAuthorizedAsync<TResource>(string resourceId, TResource resourceType)
        //{
        //    var currentUser = httpContext.User;
        //    if (currentUser.IsInRole("Admin"))
        //        return true;
        //    else
        //    {
        //        var currentUserId = userManager.GetUserId(currentUser);
        //        var resource = resourceType.TweetById(resourceId, currentUserId);

        //        return resource != null;
        //    }

        //}


    }
}
