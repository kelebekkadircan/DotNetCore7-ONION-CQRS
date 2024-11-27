using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator() {
            RuleFor(p => p.FullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .WithName("İsim Soyisim")
                .MinimumLength(2);
            

            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .EmailAddress()
                .WithName("E-Posta Adresi")
                .MinimumLength(8);


            RuleFor(p => p.Password)
                .NotEmpty()
                .WithName("Şifre")
                .NotNull()
                .MinimumLength(6);

            RuleFor(p => p.ConfirmPassword)
                .Equal(p => p.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6)
                .WithName("Şifre Tekrarı");



        
        }
    }
}
