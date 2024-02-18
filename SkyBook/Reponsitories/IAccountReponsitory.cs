using Microsoft.AspNetCore.Identity;
using SkyBook.Models;

namespace SkyBook.Reponsitories
{
    public interface IAccountReponsitory
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);

        public Task<IdentityResult> SignUpManagerAsync(SignUpModel model);

    }
}
