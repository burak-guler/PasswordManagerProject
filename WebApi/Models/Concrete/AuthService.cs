using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using WebApi.Models.Abstract;

namespace WebApi.Models.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenService tokenService;

        public AuthService(IUserService userService, ITokenService tokenService)
        {
             _userService = userService;
            this.tokenService = tokenService;
        }

        public async Task<UserLoginResponse> LoginUser(User request)
        {
            UserLoginResponse response = new();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
            }

            var user = await _userService.Login(request);
           
                if (request.UserName == user.UserName && request.Password == user.Password)
                {
                    var generatedTokenInformation = await tokenService.GenerateToken(user);

                    response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
                    response.AuthenticateResult = true;
                    response.AuthToken = generatedTokenInformation.Token;
                }
            
            return await Task.FromResult(response);

        }
    }
}
