using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Auth.Rules;
using HepsiApi.Application.Interfaces.AutoMapper;
using HepsiApi.Application.Interfaces.Tokens;
using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly AuthRules _authRules;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork,AuthRules authRules,UserManager<User> userManager,ITokenService tokenService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _authRules = authRules;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principle = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

            string email = principle.FindFirstValue(ClaimTypes.Email);

            User? user = await _userManager.FindByEmailAsync(email);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            

            await _authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpireTime);

            JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);

            string newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };





            
            

        }
    }
}
