using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Gzh.Template.Core.IdentityServer.Models;
using System.Linq;

namespace Gzh.Template.Core.IdentityServer.Quickstart
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private UserDbContext _userDbContext { get; set; }
        public ResourceOwnerPasswordValidator(UserDbContext userContext)
        {
            _userDbContext = userContext;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //var user = await _myUserManager.FindByNameAsync(context.UserName);

            User u = new User
            {
                Name = context.UserName,
                Password = context.Password

            };
            var user = _userDbContext.Users.Where(x => x.Name == context.UserName && x.Password == context.Password).FirstOrDefault();
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    subject: user.Name,
                    authenticationMethod: "custom",
                    claims: null);
            }
            else
            {
                context.Result = new GrantValidationResult(
                       TokenRequestErrors.InvalidGrant,
                       "invalid custom credential");
            }


            return;

        }
    }
}
