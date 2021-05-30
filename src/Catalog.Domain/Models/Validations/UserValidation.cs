using Catalog.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Domain.Models.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;
        public UserValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage(MsgErrorEmptyName)
                .MinimumLength(MinLenName).WithMessage(MsgErrorMinLenName);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(MsgErrorEmptyEmail)
                .EmailAddress().WithMessage(MsgErrorInvalidEmail)
                .MustAsync(VerifyEmailAsync).WithMessage(MsgErrorAlreadyExistEmail);

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(MsgErrorEmptyPassword)
                .MinimumLength(MinLenPassword).WithMessage(MsgErrorMinLenPassword)
                .Matches(@"[a-z]").WithMessage(MsgErrorPasswordDoesNotContainLowerChars)
                .Matches(@"[A-Z]").WithMessage(MsgErrorPasswordDoesNotContainUpperChars)
                .Matches(@"[0-9]").WithMessage(MsgErrorPasswordHasNoNumbers)
                .Matches(@"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\] ]").WithMessage(MsgErrorPasswordHasNoSymbols);
        }

        #region ErrorsName
        private static int MinLenName => 5;
        public static string MsgErrorEmptyName => "O nome deve ser preenchido.";
        public static string MsgErrorMinLenName => $"O nome deve conter no mínimo {MinLenName} caracteres;";
        #endregion

        #region ErrorsEmail
        public static string MsgErrorEmptyEmail => "O email deve ser preenchido.";
        public static string MsgErrorInvalidEmail => "Email inválido.";
        public static string MsgErrorAlreadyExistEmail => "O email informado já está cadastrado.";
        #endregion

        #region ErrorsPassword
        private static int MinLenPassword => 6;
        public static string MsgErrorEmptyPassword => "A senha não foi preenchida.";
        public static string MsgErrorMinLenPassword => $"A senha deve conter no mínimo {MinLenPassword} caracteres.";
        public static string MsgErrorPasswordDoesNotContainLowerChars => "A senha deve conter caracteres minúsculos.";
        public static string MsgErrorPasswordDoesNotContainUpperChars => "A senha deve conter caracteres maiúsculos.";
        public static string MsgErrorPasswordHasNoNumbers => "A senha deve conter números";
        public static string MsgErrorPasswordHasNoSymbols => "A senha deve conter caracteres especiais";
        #endregion

        private async Task<bool> VerifyEmailAsync(string email, CancellationToken cancellationToken)
        {
            var users = await _userRepository.Find(u => u.Email.Equals(email));

            return !users.Any();
        }
    }
}
