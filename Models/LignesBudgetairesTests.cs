using Xunit;
using GestionBudgétaire.Data.Entities.Database; // Assurez-vous que le namespace est correct
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GestionBudgétaire.Data.Entities;

namespace GestionBudgetaireTest.Models
{
    public class LignesBudgetairesTests
    {
        [Fact]
        public void LignesBudgetaires_CanBeInstantiated()
        {
            var ligneBudgetaire = new LignesBudgetaires();
            Assert.NotNull(ligneBudgetaire);
        }

        [Fact]
        public void LignesBudgetaires_Properties_HaveCorrectDefaultValues()
        {
            var ligneBudgetaire = new LignesBudgetaires();

            Assert.Equal(0, ligneBudgetaire.id_ligne_budget);
            Assert.Equal(0, ligneBudgetaire.ref_macro_designation);
            Assert.Equal(0, ligneBudgetaire.ref_masse_budgetaire);
            Assert.Equal(string.Empty, ligneBudgetaire.nature_du_besoin); // Default is string.Empty
            Assert.Equal(0L, ligneBudgetaire.ref_portefeuille);
            Assert.Equal(0, ligneBudgetaire.ref_caracteristique_besoin);
            Assert.Null(ligneBudgetaire.date_realisation_cible); // DateTime? is null by default
            Assert.Null(ligneBudgetaire.date_creation);          // DateTime? is null by default
            Assert.Null(ligneBudgetaire.date_modification);       // DateTime? is null by default
            Assert.Equal(default(DateTime), ligneBudgetaire.RowVersion); // DateTime non-nullable, default is MinValue if not explicitly set
        }

        [Fact]
        public void LignesBudgetaires_CanSetAndGetProperties()
        {
            var ligneBudgetaire = new LignesBudgetaires();
            var testDate1 = new DateTime(2024, 1, 15, 10, 0, 0);
            var testDate2 = new DateTime(2024, 2, 20, 11, 30, 0);
            var testDate3 = new DateTime(2024, 3, 25, 12, 45, 0);
            var testRowVersion = new DateTime(2024, 4, 30, 13, 0, 0);

            ligneBudgetaire.id_ligne_budget = 1;
            ligneBudgetaire.ref_macro_designation = 10;
            ligneBudgetaire.ref_masse_budgetaire = 20;
            ligneBudgetaire.nature_du_besoin = "Achat de fournitures";
            ligneBudgetaire.ref_portefeuille = 300L;
            ligneBudgetaire.ref_caracteristique_besoin = 40;
            ligneBudgetaire.date_realisation_cible = testDate1;
            ligneBudgetaire.date_creation = testDate2;
            ligneBudgetaire.date_modification = testDate3;
            ligneBudgetaire.RowVersion = testRowVersion;

            Assert.Equal(1, ligneBudgetaire.id_ligne_budget);
            Assert.Equal(10, ligneBudgetaire.ref_macro_designation);
            Assert.Equal(20, ligneBudgetaire.ref_masse_budgetaire);
            Assert.Equal("Achat de fournitures", ligneBudgetaire.nature_du_besoin);
            Assert.Equal(300L, ligneBudgetaire.ref_portefeuille);
            Assert.Equal(40, ligneBudgetaire.ref_caracteristique_besoin);
            Assert.Equal(testDate1, ligneBudgetaire.date_realisation_cible);
            Assert.Equal(testDate2, ligneBudgetaire.date_creation);
            Assert.Equal(testDate3, ligneBudgetaire.date_modification);
            Assert.Equal(testRowVersion, ligneBudgetaire.RowVersion);
        }

        [Fact]
        public void LignesBudgetaires_HasTableAnnotation()
        {
            var tableAttribute = typeof(LignesBudgetaires)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("LignesBudgetaires", tableAttribute.Name);
        }

        [Fact]
        public void LignesBudgetaires_IdLigneBudgetHasKeyAndColumnAnnotation()
        {
            var properties = typeof(LignesBudgetaires).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_ligne_budget");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("id_ligne_budget", columnAttribute.Name);
        }

        [Fact]
        public void LignesBudgetaires_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(LignesBudgetaires).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
            // Note: L'attribut [Column("RowVersion")] n'est pas explicitement défini dans votre modèle,
            // mais ce test vérifie seulement [ConcurrencyCheck]. Si vous l'ajoutez dans le modèle,
            // vous pourriez vouloir tester aussi l'annotation Column.
        }

        [Fact]
        public void LignesBudgetaires_ImplementsIHasRowVersion()
        {
            Assert.True(typeof(IHasRowVersion).IsAssignableFrom(typeof(LignesBudgetaires)));
        }

        // --- Validation Tests ---

        [Fact]
        public void LignesBudgetaires_NatureDuBesoin_IsNotNullAfterInstantiation()
        {
            // Vérifie que nature_du_besoin est initialisé à string.Empty et non null
            var ligneBudgetaire = new LignesBudgetaires();
            Assert.NotNull(ligneBudgetaire.nature_du_besoin);
            Assert.Equal(string.Empty, ligneBudgetaire.nature_du_besoin);
        }

        [Fact]
        public void LignesBudgetaires_NatureDuBesoin_AllowsEmptyString()
        {
            var ligneBudgetaire = new LignesBudgetaires
            {
                nature_du_besoin = string.Empty
                // Les autres propriétés sont des types valeur ou nullables.
                // Elles auront leurs valeurs par défaut ou null, ce qui est considéré comme valide
                // car il n'y a pas d'attribut [Required] sur elles.
            };
            var validationContext = new ValidationContext(ligneBudgetaire);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(ligneBudgetaire, validationContext, validationResults, true);

            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

    }
}