using FluentValidation;
using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Application.User.Queries
{
    public class GetSingleUserQueryValidator : AbstractValidator<GetSingleUserQuery>
    {
        public GetSingleUserQueryValidator(IConfigConstants constant)
        {
            this.RuleFor(v => v.UserID).GreaterThan(0).WithMessage(constant.MSG_USER_NULLUSERID);
        }
    }
}