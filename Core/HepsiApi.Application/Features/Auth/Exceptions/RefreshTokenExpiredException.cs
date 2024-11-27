using HepsiApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Exceptions
{
    public class RefreshTokenExpiredException : BaseExceptions
    {
        public RefreshTokenExpiredException() : base("Oturumunuzun Süresi Doldu Tekrar Giriş Yapınız")
        {
        }
    }
}
