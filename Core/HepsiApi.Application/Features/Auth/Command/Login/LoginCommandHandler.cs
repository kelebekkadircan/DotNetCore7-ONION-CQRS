using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Auth.Exceptions;
using HepsiApi.Application.Features.Auth.Rules;
using HepsiApi.Application.Interfaces.AutoMapper;
using HepsiApi.Application.Interfaces.Tokens;
using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AuthRules _authRules;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(IUnitOfWork unitOfWork,IConfiguration configuration,AuthRules authRules, ITokenService tokenService, IMapper mapper,UserManager<User> userManager , IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authRules = authRules;
            _tokenService = tokenService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) { 
                throw new EmailOrPasswordInvalidException();
            }
            bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            await _authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken jwtToken = await _tokenService.CreateToken(user, roles);

            string refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"] , out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

            string _jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            await _userManager.SetAuthenticationTokenAsync(user , "Default" ,"AccessToken", _jwtToken);


            return new()
            {
                Token = _jwtToken,
                RefreshToken = refreshToken,
                Expiration = jwtToken.ValidTo
            };


            



           


            
        }
    }
}
