using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
                RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adını boş geçemezsiniz");
                RuleFor(x => x.Username).MinimumLength(6).WithMessage("Lütfen en az 6 karakter girişi yapınız");
                RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Parola boş geçilemez");
                RuleFor(x => x.PasswordHash).MinimumLength(6).WithMessage("Lütfen en az 6 karakter girişi yapınız");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi boş geçilemez");
                RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
                RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad boş geçilemez");
                RuleFor(x => x.FullName).MinimumLength(5).WithMessage("Lütfen en az 5 karakter girişi yapınız");
                RuleFor(x => x.RoleId).NotEmpty().WithMessage("Rol boş geçilemez");
                RuleFor(x => x.Email).Must(BeUniqueEmail).WithMessage("Bu e-posta zaten kullanımda.");
        }

        public bool BeUniqueEmail(string email)
        {
            UserManager userManager = new UserManager(new EFUserDal());
            return !userManager.List().Any(x => x.Email == email);
        }
    }
}
