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
    public class EvaluationsBesoinsTests
    {
        [Fact]
        public void EvaluationsBesoins_CanBeInstantiated()
        {
            var evaluation = new EvaluationsBesoins();
            Assert.NotNull(evaluation);
        }

        [Fact]
        public void EvaluationsBesoins_Properties_HaveCorrectDefaultValues()
        {
            var evaluation = new EvaluationsBesoins();

            Assert.Equal(0, evaluation.id_evaluation);
            Assert.Equal(0, evaluation.id_caracteristique);
            Assert.Equal(0, evaluation.id_critere);
            Assert.Equal(0, evaluation.id_valeur);

            // RowVersion a un DateTime.Now comme valeur par défaut lors de l'instanciation,
            // donc il ne sera pas DateTime.MinValue.
            // On peut s'assurer qu'il est proche de l'heure actuelle.
            Assert.True(evaluation.RowVersion > DateTime.MinValue);
            Assert.True(evaluation.RowVersion <= DateTime.Now); // Devrait être très proche de Now
        }

        [Fact]
        public void EvaluationsBesoins_CanSetAndGetProperties()
        {
            var evaluation = new EvaluationsBesoins();
            var testDate = new DateTime(2024, 5, 10, 14, 30, 0);

            evaluation.id_evaluation = 1;
            evaluation.id_caracteristique = 101;
            evaluation.id_critere = 202;
            evaluation.id_valeur = 303;
            evaluation.RowVersion = testDate;

            Assert.Equal(1, evaluation.id_evaluation);
            Assert.Equal(101, evaluation.id_caracteristique);
            Assert.Equal(202, evaluation.id_critere);
            Assert.Equal(303, evaluation.id_valeur);
            Assert.Equal(testDate, evaluation.RowVersion);
        }

        [Fact]
        public void EvaluationsBesoins_HasTableAnnotation()
        {
            var tableAttribute = typeof(EvaluationsBesoins)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("evaluation_besoin", tableAttribute.Name);
        }

        [Fact]
        public void EvaluationsBesoins_IdEvaluationHasKeyAndColumnAnnotation()
        {
            var properties = typeof(EvaluationsBesoins).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_evaluation");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("id_evaluation", columnAttribute.Name);
        }

        [Theory]
        [InlineData("id_caracteristique", "id_caracteristique")]
        [InlineData("id_critere", "id_critere")]
        [InlineData("id_valeur", "id_valeur")]
        public void EvaluationsBesoins_ForeignKeyPropertiesHaveColumnAnnotation(string propertyName, string columnName)
        {
            var properties = typeof(EvaluationsBesoins).GetProperties();
            var property = properties.FirstOrDefault(p => p.Name == propertyName);

            Assert.NotNull(property);
            var columnAttribute = property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal(columnName, columnAttribute.Name);
        }

        [Theory]
        [InlineData("id_caracteristique", "caracteristique_besoin")]
        [InlineData("id_critere", "critere")]
        [InlineData("id_valeur", "valeur_evaluation")]
        public void EvaluationsBesoins_ForeignKeyPropertiesHaveForeignKeyAnnotation(string propertyName, string foreignKeyName)
        {
            var properties = typeof(EvaluationsBesoins).GetProperties();
            var property = properties.FirstOrDefault(p => p.Name == propertyName);

            Assert.NotNull(property);
            var foreignKeyAttribute = property.GetCustomAttributes(typeof(ForeignKeyAttribute), false).FirstOrDefault() as ForeignKeyAttribute;
            Assert.NotNull(foreignKeyAttribute);
            Assert.Equal(foreignKeyName, foreignKeyAttribute.Name);
        }

        [Fact]
        public void EvaluationsBesoins_RowVersionHasConcurrencyCheckAndColumnAnnotation()
        {
            var properties = typeof(EvaluationsBesoins).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));

            var columnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("RowVersion", columnAttribute.Name);
        }

        [Fact]
        public void EvaluationsBesoins_ImplementsIHasRowVersion()
        {
            Assert.True(typeof(IHasRowVersion).IsAssignableFrom(typeof(EvaluationsBesoins)));
        }

    }
}