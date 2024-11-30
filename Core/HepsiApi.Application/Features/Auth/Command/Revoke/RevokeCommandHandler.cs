using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Auth.Rules;
using HepsiApi.Application.Interfaces.AutoMapper;
using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Command.Revoke
{
    public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthRules _authRules;
        public RevokeCommandHandler(IUnitOfWork unitOfWork,UserManager<User> userManager , AuthRules authRules, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
            _authRules = authRules;
        }

        //public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        //{
        //    User? user = await _userManager.FindByEmailAsync(request?.Email);
        //    await _authRules.EmailAddressShouldBeValid(user);
        //    if (user.RefreshToken is null) return Unit.Value;

        //    user.RefreshToken = null;
        //    await _userManager.UpdateAsync(user);



        //    return Unit.Value;
        //}

        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı e-posta ile getir.
            User? user = await _userManager.FindByEmailAsync(request.Email);

            // E-posta adresinin geçerliliğini doğrula.
            await _authRules.EmailAddressShouldBeValid(user);

            // Kullanıcının RefreshToken'ı null ise güncelleme yapmaya gerek yok.
            if (string.IsNullOrEmpty(user?.RefreshToken))
                return Unit.Value;

            // RefreshToken'ı kaldır ve kullanıcıyı güncelle.
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
