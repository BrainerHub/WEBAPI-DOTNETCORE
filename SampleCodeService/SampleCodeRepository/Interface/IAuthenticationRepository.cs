using SampleCodeModel.RequestModel;
using SampleCodeModel.ResponseModel;

namespace SampleCodeService.SampleCodeRepository.Interface
{
    public interface IAuthenticationRepository
    {
        Task RegisterUser(UserRegisterationRequestModel model);

        Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
    }
}
