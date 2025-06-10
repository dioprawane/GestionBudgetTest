using Xunit;
using GestionBudgétaire.Data.Entities.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GestionBudgétaire.Data.Entities;

namespace GestionBudgetaireTest.Models
{
    public class MassesBudgetairesTests
    {
        [Fact]
        public void MassesBudgetaires_CanBeInstantiated()
        {
            var masseBudgetaire = new MassesBudgetaires();
            Assert.NotNull(masseBudgetaire);
        }

        [Fact]
        public void MassesBudgetaires_Properties_HaveCorrectDefaultValues()
        {
            var masseBudgetaire = new MassesBudgetaires();

            Assert.Equal(0, masseBudgetaire.Id);
            Assert.Equal(string.Empty, masseBudgetaire.MASSE_BUDGETAIRE);
            Assert.Equal(0, masseBudgetaire.ID_LIBELLE);
            Assert.Equal(0, masseBudgetaire.ID_VERSION);
            Assert.Equal(default(DateTime), masseBudgetaire.DATE_DE_MODIFICATION);
            Assert.Equal(default(DateTime), masseBudgetaire.DATE_DE_VALIDITE);
            Assert.True(masseBudgetaire.ACTIF);
            Assert.NotEqual(default(DateTime), masseBudgetaire.RowVersion);
            Assert.True(masseBudgetaire.RowVersion <= DateTime.Now);
        }

        [Fact]
        public void MassesBudgetaires_CanSetAndGetProperties()
        {
            var masseBudgetaire = new MassesBudgetaires();
            var testDateModif = new DateTime(2024, 5, 1, 9, 0, 0);
            var testDateValid = new DateTime(2024, 6, 1, 10, 0, 0);
            var testRowVersion = new DateTime(2024, 7, 1, 11, 0, 0);

            masseBudgetaire.Id = 1;
            masseBudgetaire.MASSE_BUDGETAIRE = "Masse Principale";
            masseBudgetaire.ID_LIBELLE = 10;
            masseBudgetaire.ID_VERSION = 1;
            masseBudgetaire.DATE_DE_MODIFICATION = testDateModif;
            masseBudgetaire.DATE_DE_VALIDITE = testDateValid;
            masseBudgetaire.ACTIF = false;
            masseBudgetaire.RowVersion = testRowVersion;

            Assert.Equal(1, masseBudgetaire.Id);
            Assert.Equal("Masse Principale", masseBudgetaire.MASSE_BUDGETAIRE);
            Assert.Equal(10, masseBudgetaire.ID_LIBELLE);
            Assert.Equal(1, masseBudgetaire.ID_VERSION);
            Assert.Equal(testDateModif, masseBudgetaire.DATE_DE_MODIFICATION);
            Assert.Equal(testDateValid, masseBudgetaire.DATE_DE_VALIDITE);
            Assert.False(masseBudgetaire.ACTIF);
            Assert.Equal(testRowVersion, masseBudgetaire.RowVersion);
        }

        [Fact]
        public void MassesBudgetaires_HasTableAnnotation()
        {
            var tableAttribute = typeof(MassesBudgetaires)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("MASSE_BUDGETAIRE", tableAttribute.Name);
        }

        [Fact]
        public void MassesBudgetaires_IdHasKeyAndColumnAnnotation()
        {
            var properties = typeof(MassesBudgetaires).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("ID_MASSE_BUDGETAIRE", columnAttribute.Name);
        }

        [Fact]
        public void MassesBudgetaires_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(MassesBudgetaires).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        [Fact]
        public void MassesBudgetaires_ImplementsIHasRowVersion()
        {
            Assert.True(typeof(IHasRowVersion).IsAssignableFrom(typeof(MassesBudgetaires)));
        }

        [Fact]
        public void MassesBudgetaires_MASSE_BUDGETAIRE_IsNotNullAndAllowEmptyString()
        {
            var masseBudgetaire = new MassesBudgetaires();
            var validationContext = new ValidationContext(masseBudgetaire);
            var validationResults = new List<ValidationResult>();

            Assert.NotNull(masseBudgetaire.MASSE_BUDGETAIRE);
            Assert.Equal(string.Empty, masseBudgetaire.MASSE_BUDGETAIRE);

            var isValidDefault = Validator.TryValidateObject(masseBudgetaire, validationContext, validationResults, true);
            Assert.True(isValidDefault);
            Assert.Empty(validationResults);

            masseBudgetaire.MASSE_BUDGETAIRE = "Nouvelle Masse";
            validationResults.Clear();
            var isValidSet = Validator.TryValidateObject(masseBudgetaire, validationContext, validationResults, true);

            Assert.Equal("Nouvelle Masse", masseBudgetaire.MASSE_BUDGETAIRE);
            Assert.True(isValidSet);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void MassesBudgetaires_DateProperties_AreValidWithDefaultValues()
        {
            var masseBudgetaire = new MassesBudgetaires
            {
                DATE_DE_MODIFICATION = default(DateTime),
                DATE_DE_VALIDITE = default(DateTime)
            };
            var validationContext = new ValidationContext(masseBudgetaire);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(masseBudgetaire, validationContext, validationResults, true);

            Assert.True(isValid);
            Assert.Empty(validationResults);

            masseBudgetaire.DATE_DE_MODIFICATION = DateTime.Now;
            masseBudgetaire.DATE_DE_VALIDITE = DateTime.Now.AddDays(1);
            validationResults.Clear();
            isValid = Validator.TryValidateObject(masseBudgetaire, validationContext, validationResults, true);
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void MassesBudgetaires_ActifProperty_WorksCorrectly()
        {
            var masseBudgetaire = new MassesBudgetaires();

            Assert.True(masseBudgetaire.ACTIF);

            masseBudgetaire.ACTIF = false;

            Assert.False(masseBudgetaire.ACTIF);

            var validationContext = new ValidationContext(masseBudgetaire);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(masseBudgetaire, validationContext, validationResults, true);
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }
    }
}