using Xunit;
using GestionBudgétaire.Data.Entities.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionBudgetaireTest.Models
{
    public class DemoEntityValueTests
    {
        [Fact]
        public void DemoEntityValue_CanBeInstantiated()
        {
            var demoEntity = new DemoEntityValue();
            Assert.NotNull(demoEntity);
        }

        [Fact]
        public void DemoEntityValue_Properties_HaveCorrectDefaultValues()
        {
            var demoEntity = new DemoEntityValue();

            Assert.Equal(0, demoEntity.Id);
            // Name est string? mais Required, donc le défaut CLR est null.
            Assert.Null(demoEntity.Name);
            Assert.True(demoEntity.IsActive); // Default value set to true
            Assert.Null(demoEntity.CreatedDate); // CHANGEMENT ICI: DateTime? est null par défaut
            Assert.Null(demoEntity.Amount);      // CHANGEMENT ICI: decimal? est null par défaut
            Assert.Null(demoEntity.Score); // double? default is null
            Assert.Null(demoEntity.ExternalId); // Guid? default is null
            Assert.Null(demoEntity.Description); // string? default is null
        }

        [Fact]
        public void DemoEntityValue_CanSetAndGetProperties()
        {
            var demoEntity = new DemoEntityValue();
            var testDate = new DateTime(2023, 7, 20, 9, 0, 0);
            var testGuid = Guid.NewGuid();

            demoEntity.Id = 5;
            demoEntity.Name = "Test Name";
            demoEntity.IsActive = false;
            demoEntity.CreatedDate = testDate;
            demoEntity.Amount = 150.75m;
            demoEntity.Score = 99.9;
            demoEntity.ExternalId = testGuid;
            demoEntity.Description = "This is a test description.";

            Assert.Equal(5, demoEntity.Id);
            Assert.Equal("Test Name", demoEntity.Name);
            Assert.False(demoEntity.IsActive);
            Assert.Equal(testDate, demoEntity.CreatedDate);
            Assert.Equal(150.75m, demoEntity.Amount);
            Assert.Equal(99.9, demoEntity.Score);
            Assert.Equal(testGuid, demoEntity.ExternalId);
            Assert.Equal("This is a test description.", demoEntity.Description);
        }

        [Fact]
        public void DemoEntityValue_HasKeyAnnotationOnId()
        {
            var properties = typeof(DemoEntityValue).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));
        }

        [Fact]
        public void DemoEntityValue_HasTableAnnotation()
        {
            var tableAttribute = typeof(DemoEntityValue)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("DemoEntityValues", tableAttribute.Name);
        }

        [Fact]
        public void DemoEntityValue_AmountHasColumnTypeAnnotation()
        {
            var properties = typeof(DemoEntityValue).GetProperties();
            var amountProperty = properties.FirstOrDefault(p => p.Name == "Amount");

            Assert.NotNull(amountProperty);
            var columnAttribute = amountProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("decimal(10,2)", columnAttribute.TypeName);
        }

        // --- Validation Tests ---

        [Theory]
        [InlineData(null, false, "The Name field is required.")]
        [InlineData("", false, "The Name field is required.")]
        [InlineData("Short Name", true, null)]
        public void DemoEntityValue_Name_RequiredValidation(string? name, bool expectedIsValid, string? expectedErrorMessage)
        {
            var demoEntity = new DemoEntityValue
            {
                Name = name!, // Use ! operator for non-null Name
                // Ajoutez des valeurs valides pour les autres propriétés [Required]
                IsActive = true,
                CreatedDate = DateTime.Now,
                Amount = 100m
            };
            var validationContext = new ValidationContext(demoEntity);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(demoEntity, validationContext, validationResults, true);

            Assert.Equal(expectedIsValid, isValid);
            if (!expectedIsValid)
            {
                Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedErrorMessage);
            }
            else
            {
                Assert.Empty(validationResults);
            }
        }

        [Fact]
        public void DemoEntityValue_Name_StringLengthValidation()
        {
            // Test case for name exceeding max length
            var longName = new string('A', 101); // 101 characters
            var demoEntityLong = new DemoEntityValue
            {
                Name = longName,
                // Ajoutez des valeurs valides pour les autres propriétés [Required]
                IsActive = true,
                CreatedDate = DateTime.Now,
                Amount = 100m
            };
            var validationContextLong = new ValidationContext(demoEntityLong);
            var validationResultsLong = new List<ValidationResult>();

            var isValidLong = Validator.TryValidateObject(demoEntityLong, validationContextLong, validationResultsLong, true);

            Assert.False(isValidLong);
            Assert.Contains(validationResultsLong, vr => vr.ErrorMessage!.Contains("maximum length of 100")); // Default message


            // Test case for name within max length
            var validName = new string('B', 100); // 100 characters
            var demoEntityValid = new DemoEntityValue
            {
                Name = validName,
                // Ajoutez des valeurs valides pour les autres propriétés [Required]
                IsActive = true,
                CreatedDate = DateTime.Now,
                Amount = 100m
            };
            var validationContextValid = new ValidationContext(demoEntityValid);
            var validationResultsValid = new List<ValidationResult>();

            var isValidValid = Validator.TryValidateObject(demoEntityValid, validationContextValid, validationResultsValid, true);

            Assert.True(isValidValid);
            Assert.Empty(validationResultsValid);
        }

        [Fact]
        public void DemoEntityValue_CreatedDate_RequiredValidation()
        {
            // Cas de test 1 : CreatedDate est null
            var demoEntityNullDate = new DemoEntityValue
            {
                Name = "Valid Name",
                IsActive = true,
                Amount = 100m,
                CreatedDate = null // Test de la propriété nullable
            };

            var validationContextNull = new ValidationContext(demoEntityNullDate);
            var validationResultsNull = new List<ValidationResult>();

            // Act
            var isValidNull = Validator.TryValidateObject(demoEntityNullDate, validationContextNull, validationResultsNull, true);

            // Assert : On s'attend à ce que la validation ÉCHOUE pour un CreatedDate null
            Assert.False(isValidNull);
            Assert.Contains(validationResultsNull, vr => vr.MemberNames.Contains(nameof(DemoEntityValue.CreatedDate)));
            Assert.Contains(validationResultsNull, vr => vr.ErrorMessage == "The CreatedDate field is required.");


            // Cas de test 2 : CreatedDate est une date explicitement définie (valide)
            var demoEntitySetDate = new DemoEntityValue
            {
                Name = "Valid Name",
                IsActive = true,
                Amount = 100m,
                CreatedDate = DateTime.Now
            };
            var validationContextSet = new ValidationContext(demoEntitySetDate);
            var validationResultsSet = new List<ValidationResult>();

            // Act
            var isValidSet = Validator.TryValidateObject(demoEntitySetDate, validationContextSet, validationResultsSet, true);

            // Assert : On s'attend à ce que la validation RÉUSSISSE pour une date définie
            Assert.True(isValidSet);
            Assert.Empty(validationResultsSet);
        }

        [Fact]
        public void DemoEntityValue_Amount_RequiredValidation()
        {
            // Cas de test 1 : Amount est null
            var demoEntityNullAmount = new DemoEntityValue
            {
                Name = "Valid Name",
                IsActive = true,
                CreatedDate = DateTime.Now,
                Amount = null // Test de la propriété nullable
            };

            var validationContextNull = new ValidationContext(demoEntityNullAmount);
            var validationResultsNull = new List<ValidationResult>();

            // Act
            var isValidNull = Validator.TryValidateObject(demoEntityNullAmount, validationContextNull, validationResultsNull, true);

            // Assert : On s'attend à ce que la validation ÉCHOUE pour un Amount null
            Assert.False(isValidNull);
            Assert.Contains(validationResultsNull, vr => vr.MemberNames.Contains(nameof(DemoEntityValue.Amount)));
            Assert.Contains(validationResultsNull, vr => vr.ErrorMessage == "The Amount field is required.");

            // Cas de test 2 : Amount est une valeur explicitement définie (valide)
            var demoEntitySetAmount = new DemoEntityValue
            {
                Name = "Valid Name",
                IsActive = true,
                CreatedDate = DateTime.Now,
                Amount = 123.45m
            };
            var validationContextSet = new ValidationContext(demoEntitySetAmount);
            var validationResultsSet = new List<ValidationResult>();

            // Act
            var isValidSet = Validator.TryValidateObject(demoEntitySetAmount, validationContextSet, validationResultsSet, true);

            // Assert : On s'attend à ce que la validation RÉUSSISSE pour une valeur définie
            Assert.True(isValidSet);
            Assert.Empty(validationResultsSet);
        }
    }
}