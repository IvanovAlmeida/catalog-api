using Xunit;
using System.Linq;

using Catalog.Domain.Models;
using Catalog.Domain.Models.Validations;

namespace Catalog.Domain.Tests.Validations
{
    public class ItemValidationTest
    {
        [Fact(DisplayName = "Item valid must return true")]
        [Trait("Categoria", "Validation - Item")]
        public void ItemValidation_ItemValid_MustReturnTrue()
        {
            // Arrange
            var itemValidation = new ItemValidation();
            var item = new Item
            {
                Name = "Item 1",
                ItemType = ItemType.Product,
                Value = 25.00m
            };

            // Act
            var result = itemValidation.Validate(item);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "Item invalid must return false")]
        [Trait("Categoria", "Validation - Item")]
        public void ItemValidation_ItemInvalid_MustReturnFalse()
        {
            // Arrange
            var itemValidation = new ItemValidation();
            var item = new Item
            {
                Name = "",
                Value = 0
            };

            // Act
            var result = itemValidation.Validate(item);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(3, result.Errors.Count);
            Assert.Contains(ItemValidation.ErroMsgEmptyName, result.Errors.Select(m => m.ErrorMessage));
            Assert.Contains(ItemValidation.ErroMsgItemTypeInvalid, result.Errors.Select(m => m.ErrorMessage));
            Assert.Contains(ItemValidation.ErroMsgValueGreaterThan, result.Errors.Select(m => m.ErrorMessage));
        }
    }
}
