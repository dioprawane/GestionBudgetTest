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
    public class CaracteristiqueDuBesoinTests
    {
        [Fact]
        public void CaracteristiqueDuBesoin_CanBeInstantiated()
        {
            var caracteristique = new CaracteristiqueDuBesoin();
            Assert.NotNull(caracteristique);
        }

        [Fact]
        public void CaracteristiqueDuBesoin_Properties_HaveCorrectDefaultValues()
        {
            var caracteristique = new CaracteristiqueDuBesoin();

            Assert.Equal(0, caracteristique.id_caracteristique);
            Assert.Equal(string.Empty, caracteristique.nom);
            Assert.NotEqual(default(DateTime), caracteristique.RowVersion);
        }

        [Fact]
        public void CaracteristiqueDuBesoin_CanSetAndGetProperties()
        {
            var caracteristique = new CaracteristiqueDuBesoin();
            var testDate = new DateTime(2025, 6, 5, 13, 5, 0);

            caracteristique.id_caracteristique = 10;
            caracteristique.nom = "Critique";
            caracteristique.RowVersion = testDate;

            Assert.Equal(10, caracteristique.id_caracteristique);
            Assert.Equal("Critique", caracteristique.nom);
            Assert.Equal(testDate, caracteristique.RowVersion);
        }

        [Fact]
        public void CaracteristiqueDuBesoin_HasKeyAnnotationOnIdCaracteristique()
        {
            var properties = typeof(CaracteristiqueDuBesoin).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_caracteristique");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));
        }

        [Fact]
        public void CaracteristiqueDuBesoin_HasTableAnnotation()
        {
            var tableAttribute = typeof(CaracteristiqueDuBesoin)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("caracteristique_besoin", tableAttribute.Name);
        }

        [Fact]
        public void CaracteristiqueDuBesoin_IdCaracteristiqueHasColumnAnnotation()
        {
            var properties = typeof(CaracteristiqueDuBesoin).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_caracteristique");

            Assert.NotNull(idProperty);
            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("id_caracteristique", columnAttribute.Name);
        }

        [Fact]
        public void CaracteristiqueDuBesoin_NomHasColumnAnnotation()
        {
            var properties = typeof(CaracteristiqueDuBesoin).GetProperties();
            var nomProperty = properties.FirstOrDefault(p => p.Name == "nom");

            Assert.NotNull(nomProperty);
            var columnAttribute = nomProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("nom", columnAttribute.Name);
        }


        [Fact]
        public void CaracteristiqueDuBesoin_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(CaracteristiqueDuBesoin).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        [Fact]
        public void CaracteristiqueDuBesoin_RowVersionHasColumnAnnotation()
        {
            var properties = typeof(CaracteristiqueDuBesoin).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            var columnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("RowVersion", columnAttribute.Name);
        }


        [Fact]
        public void CaracteristiqueDuBesoin_ImplementsIHasRowVersion()
        {
            var caracteristique = new CaracteristiqueDuBesoin();
            Assert.IsAssignableFrom<IHasRowVersion>(caracteristique);
        }

        // Test pour l'annotation [Required] sur 'nom'
        // Test pour l'annotation [Required] sur 'nom'
        [Theory]
        [InlineData(null, false, "The nom field is required.")] // Maintenant correct
        [InlineData("", false, "The nom field is required.")]   // Correct, "" est une chaîne vide, pas null
        [InlineData("Valeur Valide", true, null)]             // Maintenant correct
        public void CaracteristiqueDuBesoin_Nom_Validation(string? nom, bool expectedIsValid, string? expectedErrorMessage)
        {
            var caracteristique = new CaracteristiqueDuBesoin { nom = nom! }; // Utilisez l'opérateur "!" ici pour dire que c'est intentionnel
            var validationContext = new ValidationContext(caracteristique);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(caracteristique, validationContext, validationResults, true);

            Assert.Equal(expectedIsValid, isValid);
            if (!expectedIsValid)
            {
                // Dans le cas d'un message d'erreur null, vérifiez si la liste de résultats est vide.
                // Sinon, vérifiez si le message d'erreur attendu est contenu.
                if (expectedErrorMessage == null)
                {
                    Assert.Empty(validationResults); // Si vous attendez l'absence d'erreur même si isValid est false, ce cas est étrange
                                                     // Mais si `isValid` est false, il DOIT y avoir une erreur.
                                                     // Donc, ce 'if' est plus pertinent si `expectedErrorMessage` est non-null
                }
                else
                {
                    Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedErrorMessage);
                }
            }
            else
            {
                Assert.Empty(validationResults);
            }
        }
    }
}