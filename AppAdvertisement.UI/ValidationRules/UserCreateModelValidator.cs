using AppAdvertisement.UI.Models;
using FluentValidation;
using System;

namespace AppAdvertisement.UI.ValidationRules
{
    public class UserCreateModelValidator:AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x => x.PassWord).NotEmpty().MinimumLength(6);
            RuleFor(x => x.PassWord).Equal(x => x.ConfirmPassword).WithMessage("Passwords not match");
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(6);
            RuleFor(x => x.UserName).NotNull().Must(CanNotSwear).WithMessage("Username can not contains f*ck or b*stard");
            RuleFor(x => new
            {
                x.Firstname,
                x.UserName
            }).NotNull().Must(x=>CanNotContains(x.UserName,x.Firstname)).WithMessage("Username can not contains firstname");
            RuleFor(x => x.GenderId).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }

        private bool CanNotContains(string userName, string firstname)
        {
            if (userName != null && firstname != null)
            {
                return !userName.Contains(firstname);
            }
            return true;
            
        }

        private bool CanNotSwear(string arg)
        {
            if (arg!=null && (arg.Contains("fucker")||arg.Contains("bustard")))
            {
                return false;
            }
            return true;
        }
    }
}
