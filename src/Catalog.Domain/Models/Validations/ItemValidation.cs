using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Models.Validations
{
    public class ItemValidation : AbstractValidator<Item>
    {
        public ItemValidation()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage(ErroMsgEmptyName);
            RuleFor(i => i.ItemType)
                .IsInEnum().WithMessage(ErroMsgItemTypeInvalid);
            RuleFor(i => i.Value)
                .GreaterThan(0).WithMessage(ErroMsgValueGreaterThan);
        }

        public static string ErroMsgEmptyName => "Nome não pode estar vazio.";
        public static string ErroMsgItemTypeInvalid => "Tipo do Item inválido.";
        public static string ErroMsgValueGreaterThan => "O valor deve ser maior que zero.";
    }
}
