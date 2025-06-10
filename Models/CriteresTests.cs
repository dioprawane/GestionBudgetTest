using Xunit;
using GestionBudgétaire.Data.Entities.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using GestionBudgétaire.Data.Entities;

namespace GestionBudgetaireTest.Models
{
    public class CriteresTests
    {
        [Fact]
        public void Criteres_CanBeInstantiated()
        {
            var criteres = new Criteres();
            Assert.NotNull(criteres);
        }

        [Fact]
        public void Criteres_Properties_HaveCorrectDefaultValues()
        {
            var criteres = new Criteres();

            Assert.Equal(0, criteres.id_critere);
            Assert.Equal(string.Empty, criteres.nom);
            Assert.Equal(0f, criteres.poids); // Default for float is 0f
            Assert.NotEqual(default(DateTime), criteres.RowVersion);
        }

        [Fact]
        public void Criteres_CanSetAndGetProperties()
        {
            var criteres = new Criteres();
            var testRowVersion = new DateTime(2025, 6, 5, 14, 15, 0);

            criteres.id_critere = 1;
            criteres.nom = "Impact Économique";
            criteres.poids = 0.5f;
            criteres.RowVersion = testRowVersion;

            Assert.Equal(1, criteres.id_critere);
            Assert.Equal("Impact Économique", criteres.nom);
            Assert.Equal(0.5f, criteres.poids);
            Assert.Equal(testRowVersion, criteres.RowVersion);
        }

        [Fact]
        public void Criteres_HasKeyAnnotationOnIdCritere()
        {
            var properties = typeof(Criteres).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_critere");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));
        }

        [Fact]
        public void Criteres_HasTableAnnotation()
        {
            var tableAttribute = typeof(Criteres)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("critere", tableAttribute.Name);
        }

        [Fact]
        public void Criteres_IdCritereHasColumnAnnotation()
        {
            var properties = typeof(Criteres).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_critere");

            Assert.NotNull(idProperty);
            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("id_critere", columnAttribute.Name);
        }

        [Fact]
        public void Criteres_NomHasColumnAnnotation()
        {
            var properties = typeof(Criteres).GetProperties();
            var nomProperty = properties.FirstOrDefault(p => p.Name == "nom");

            Assert.NotNull(nomProperty);
            var columnAttribute = nomProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("nom", columnAttribute.Name);
        }

        [Fact]
        public void Criteres_PoidsHasColumnAnnotation()
        {
            var properties = typeof(Criteres).GetProperties();
            var poidsProperty = properties.FirstOrDefault(p => p.Name == "poids");

            Assert.NotNull(poidsProperty);
            var columnAttribute = poidsProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("poids", columnAttribute.Name);
        }

        [Fact]
        public void Criteres_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(Criteres).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        [Fact]
        public void Criteres_RowVersionHasColumnAnnotation()
        {
            var properties = typeof(Criteres).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            var columnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("RowVersion", columnAttribute.Name);
        }


        [Fact]
        public void Criteres_ImplementsIHasRowVersion()
        {
            var criteres = new Criteres();
            Assert.IsAssignableFrom<IHasRowVersion>(criteres);
        }

        // Tests de validation pour 'nom' [Required]
        [Theory]
        [InlineData(null, false, "The nom field is required.")]
        [InlineData("", false, "The nom field is required.")]
        [InlineData("Critère valide", true, null)]
        public void Criteres_Nom_Validation(string? nom, bool expectedIsValid, string? expectedErrorMessage)
        {
            var criteres = new Criteres { nom = nom! };
            var validationContext = new ValidationContext(criteres);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(criteres, validationContext, validationResults, true);

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
    }
}