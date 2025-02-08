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
            newUser.Address = userVM.Address;

            return newUser;
        }

		public ProfileVM MapModelToVM(AppUser appUser)
		{
			var profileVM = new ProfileVM();

			profileVM.FirstName = appUser.FirstName;
			profileVM.LastName = appUser.LastName;
			profileVM.PhoneNumber = appUser.PhoneNumber;
			profileVM.Email = appUser.Email;

            return profileVM;
		}


        public AppUser MapVMtoModel(AppUser user, ProfileVM userVM)
        {
            user.Email = userVM.Email;
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.UserName = userVM.Email;
            user.PhoneNumber = userVM.PhoneNumber;
            user.CreatedAt = DateTime.Now;
            user.Address = userVM.Address;

            return user;
        }

    }
}
