using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş geçilemez");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Lütfen en az 5 karakter girişi yapınız");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş geçilemez");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Lütfen en az 10 karakter girişi yapınız");
            RuleFor(x => x.RequestTypeId)
                .NotEqual(RequestType.None).WithMessage("Talep Türü zorunlu!");

            RuleFor(x => x.PriorityId)
                .NotEqual(Priority.None).WithMessage("Öncelik zorunlu!");
        }
    }
}
