using SmartEdu.Api.Models.Foundations.Users;
using SmartEdu.Api.Models.Foundations.Users.Exceptions;

namespace SmartEdu.Api.Services.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateUserOnAdd(User user)
        {
            ValidateUserNotNull(user);

            Validate(
                (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.FirstName), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName), Parameter: nameof(User.LastName)),
                (Rule: IsInvalid(user.Email), Parameter: nameof(User.Email)),
                (Rule: IsInvalid(user.Address), Parameter: nameof(User.Address)),
                (Rule: IsInvalid(user.PasswordHash), Parameter: nameof(User.PasswordHash)),
                (Rule: IsInvalid(user.Role), Parameter: nameof(User.Role)),
                (Rule: IsInvalid(user.CreatedDate), Parameter: nameof(User.CreatedDate)),
                (Rule: IsInvalid(user.UpdatedDate), Parameter: nameof(User.UpdatedDate)),
                (Rule: IsInvalid(user.IsActive), Parameter: nameof(User.IsActive)));
        }

        private void ValidateAccountOnModify(User user)
        {
            ValidateUserNotNull(user);
            Validate(
                (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.FirstName), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName), Parameter: nameof(User.LastName)),
                (Rule: IsInvalid(user.Email), Parameter: nameof(User.Email)),
                (Rule: IsInvalid(user.Address), Parameter: nameof(User.Address)),
                (Rule: IsInvalid(user.PasswordHash), Parameter: nameof(User.PasswordHash)),
                (Rule: IsInvalid(user.Role), Parameter: nameof(User.Role)),
                (Rule: IsInvalid(user.CreatedDate), Parameter: nameof(User.CreatedDate)),
                (Rule: IsInvalid(user.UpdatedDate), Parameter: nameof(User.UpdatedDate)),
                (Rule: IsInvalid(user.IsActive), Parameter: nameof(User.IsActive)));
        }

        private void ValidateUserNotNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private void ValidateUserId(Guid userId) =>
            Validate((Rule: IsInvalid(userId), Parameter: nameof(User.Id)));

        private void ValidateStorageUser(User storageUser, Guid Id)
        {
            if (storageUser is null)
            {
                throw new NotFoundUserException(Id);
            }
        }

        private static dynamic IsInvalid(Guid Id) => new
        {
            Condition = Id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(Role role) => new
        {
            Condition = Enum.IsDefined(role) is false,
            Message = "Role is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(bool isActive) => new
        {
            Condition = isActive is false,
            Message = "IsActive must be true"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserException = new InvalidUserException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidUserException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidUserException.ThrowIfContainsErrors();
        }
    }
}
