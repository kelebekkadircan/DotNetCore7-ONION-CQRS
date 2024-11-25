using HepsiApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Exceptions
{
    public class ProductTitleMustNotBeSameException : BaseExceptions
    {
        
        public ProductTitleMustNotBeSameException () : base("Ürün Başlığı Zaten Var")
        {
        }
    }
}
