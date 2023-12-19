using FleetCommandAPI.Core.Entity.User;
using FleetCommandAPI.Core.Entity.User.DTO;
using Microsoft.AspNetCore.Identity;


namespace FleetCommandAPI.Core.Services
{
    public class UserService
    {

        private UserManager<UserModel> _userManager;
        private TokenService _tokenService;
        private SignInManager<UserModel> _signInManager;

        public UserService(UserManager<UserModel> userManager, TokenService tokenService, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUser(CreatedUserDto dto)
        {
            UserModel userModel = new UserModel
            {
                UserName = dto.UserName,
            };

            IdentityResult result = await _userManager.CreateAsync(userModel, dto.Password);
            return result;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.userName, loginDto.Password, false, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {

                    throw new ApplicationException("Conta bloqueada");
                }
                else if (result.IsNotAllowed)
                {
                    
                    throw new ApplicationException("Login não permitido");
                }
                else if (result.RequiresTwoFactor)
                {
                    
                    throw new ApplicationException("Autenticação em duas etapas necessária");
                }
                else
                {
                    
                    throw new ApplicationException("Erro durante o login");
                }
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == loginDto.userName.ToUpper());
            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}