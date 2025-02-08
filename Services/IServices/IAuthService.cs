using E_Commerce2.Models;
using E_Commerce2.ViewModels.UserVM_s;

namespace E_Commerce2.Services.IServices
{
    public interface IAuthService
    {
        AppUser MapVMtoModel(UserRegisterVM model);
		ProfileVM MapModelToVM(AppUser model);
	}
}
