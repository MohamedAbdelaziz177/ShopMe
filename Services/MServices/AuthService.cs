using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.ViewModels.UserVM_s;

namespace E_Commerce2.Services.MServices
{
    public class AuthService : IAuthService
    {
        public AppUser MapVMtoModel(UserRegisterVM userVM)
        {
            AppUser newUser = new AppUser();

            newUser.Email = userVM.Email;
            newUser.FirstName = userVM.FirstName;
            newUser.LastName = userVM.LastName;
            newUser.UserName = userVM.Email;
            newUser.PhoneNumber = userVM.PhoneNumber;
            newUser.CreatedAt = DateTime.Now;

            return newUser;
        }
    }
}
