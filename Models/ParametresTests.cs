using Xunit;
using GestionBudgétaire.Data.Entities.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GestionBudgétaire.Data.Entities; // Assurez-vous que IHasRowVersion est ici

namespace GestionBudgetaireTest.Models
{
    public class ParametresTests
    {
        [Fact]
        public void Parametres_CanBeInstantiated()
        {
            var parametre = new Parametres();
            Assert.NotNull(parametre);
        }

        [Fact]
        public void Parametres_Properties_HaveCorrectDefaultValues()
        {
            var parametre = new Parametres();

            Assert.Equal(0, parametre.id_parametre);
            Assert.Equal(0, parametre.annee);
            Assert.Equal(0, parametre.exercice);
            Assert.Null(parametre.date_param);
            Assert.Null(parametre.statut); // string? is null by default
            Assert.Null(parametre.date_creation);
            Assert.Equal(string.Empty, parametre.cree_par);
            Assert.Equal(string.Empty, parametre.commentaires);
            Assert.Equal(default(DateTime), parametre.RowVersion); // DateTime non-nullable, default is MinValue
        }

        [Fact]
        public void Parametres_CanSetAndGetProperties()
        {
            var parametre = new Parametres();
            var testDateParam = new DateTime(2024, 1, 1);
            var testDateCreation = new DateTime(2024, 1, 10);
            var testRowVersion = new DateTime(2024, 1, 15);

            parametre.id_parametre = 1;
            parametre.annee = 2024;
            parametre.exercice = 1;
            parametre.date_param = testDateParam;
            parametre.statut = "Actif";
            parametre.date_creation = testDateCreation;
            parametre.cree_par = "Admin";
            parametre.commentaires = "Commentaires de test";
            parametre.RowVersion = testRowVersion;

            Assert.Equal(1, parametre.id_parametre);
            Assert.Equal(2024, parametre.annee);
            Assert.Equal(1, parametre.exercice);
            Assert.Equal(testDateParam, parametre.date_param);
            Assert.Equal("Actif", parametre.statut);
            Assert.Equal(testDateCreation, parametre.date_creation);
            Assert.Equal("Admin", parametre.cree_par);
            Assert.Equal("Commentaires de test", parametre.commentaires);
            Assert.Equal(testRowVersion, parametre.RowVersion);
        }

        [Fact]
        public void Parametres_HasTableAnnotation()
        {
            var tableAttribute = typeof(Parametres)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("Parametres", tableAttribute.Name);
        }

        [Fact]
        public void Parametres_IdParametreHasKeyAndColumnAnnotation()
        {
            var properties = typeof(Parametres).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_parametre");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("id_parametre", columnAttribute.Name);
        }

        [Fact]
        public void Parametres_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(Parametres).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        [Fact]
        public void Parametres_ImplementsIHasRowVersion()
        {
            Assert.True(typeof(IHasRowVersion).IsAssignableFrom(typeof(Parametres)));
        }

        [Fact]
        public void Parametres_NullableDateProperties_AllowNullAndSetValues()
        {
            var parametre = new Parametres();
            var testDate = new DateTime(2024, 6, 5);

            Assert.Null(parametre.date_param);
            Assert.Null(parametre.date_creation);

            parametre.date_param = testDate;
            parametre.date_creation = testDate.AddDays(1);

            Assert.Equal(testDate, parametre.date_param);
            Assert.Equal(testDate.AddDays(1), parametre.date_creation);

            parametre.date_param = null;
            parametre.date_creation = null;

            Assert.Null(parametre.date_param);
            Assert.Null(parametre.date_creation);

            var validationContext = new ValidationContext(parametre);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parametre, validationContext, validationResults, true);

            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Parametres_NullableStringStatut_AllowsNullAndEmptyAndSetValues()
        {
            var parametre = new Parametres();

            Assert.Null(parametre.statut);

            parametre.statut = string.Empty;
            Assert.Equal(string.Empty, parametre.statut);

            parametre.statut = "En Cours";
            Assert.Equal("En Cours", parametre.statut);

            parametre.statut = null;
            Assert.Null(parametre.statut);

            var validationContext = new ValidationContext(parametre);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parametre, validationContext, validationResults, true);

            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Parametres_StringProperties_AreNotNullAndAllowEmptyString()
        {
            var parametre = new Parametres();
            var validationContext = new ValidationContext(parametre);
            var validationResults = new List<ValidationResult>();

            Assert.NotNull(parametre.cree_par);
            Assert.Equal(string.Empty, parametre.cree_par);
            Assert.NotNull(parametre.commentaires);
            Assert.Equal(string.Empty, parametre.commentaires);

            var isValidDefault = Validator.TryValidateObject(parametre, validationContext, validationResults, true);
            Assert.True(isValidDefault);
            Assert.Empty(validationResults);

            parametre.cree_par = "User A";
            parametre.commentaires = "Divers commentaires.";
            validationResults.Clear();
            var isValidSet = Validator.TryValidateObject(parametre, validationContext, validationResults, true);

            Assert.Equal("User A", parametre.cree_par);
            Assert.Equal("Divers commentaires.", parametre.commentaires);
            Assert.True(isValidSet);
            Assert.Empty(validationResults);
        }
    }
}