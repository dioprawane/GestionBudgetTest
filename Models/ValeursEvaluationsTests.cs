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
    public class ValeursEvaluationsTests
    {
        [Fact]
        public void ValeursEvaluations_CanBeInstantiated()
        {
            var valeurEvaluation = new ValeursEvaluations();
            Assert.NotNull(valeurEvaluation);
        }

        [Fact]
        public void ValeursEvaluations_Properties_HaveCorrectDefaultValues()
        {
            var valeurEvaluation = new ValeursEvaluations();

            Assert.Equal(0, valeurEvaluation.id_valeur);
            Assert.Equal(0, valeurEvaluation.id_critere);
            Assert.Null(valeurEvaluation.Critere); // Ajouter ce Assert si vous avez ajouté la propriété de navigation
            Assert.Equal(string.Empty, valeurEvaluation.label);
            Assert.Equal(0, valeurEvaluation.score);
            Assert.NotEqual(default(DateTime), valeurEvaluation.RowVersion);
            Assert.True(valeurEvaluation.RowVersion <= DateTime.Now);
        }

        [Fact]
        public void ValeursEvaluations_CanSetAndGetProperties()
        {
            var valeurEvaluation = new ValeursEvaluations();
            var testRowVersion = new DateTime(2024, 7, 1, 11, 0, 0);

            // Créer une instance de Criteres pour la propriété de navigation
            var testCritere = new Criteres { id_critere = 10, nom = "Critere Test", poids = 1.0f };

            valeurEvaluation.id_valeur = 1;
            valeurEvaluation.id_critere = 10;
            valeurEvaluation.Critere = testCritere; // Assigner la propriété de navigation
            valeurEvaluation.label = "Très bien";
            valeurEvaluation.score = 5;
            valeurEvaluation.RowVersion = testRowVersion;

            Assert.Equal(1, valeurEvaluation.id_valeur);
            Assert.Equal(10, valeurEvaluation.id_critere);
            Assert.Equal(testCritere, valeurEvaluation.Critere); // Vérifier la propriété de navigation
            Assert.Equal("Très bien", valeurEvaluation.label);
            Assert.Equal(5, valeurEvaluation.score);
            Assert.Equal(testRowVersion, valeurEvaluation.RowVersion);
        }

        [Fact]
        public void ValeursEvaluations_HasTableAnnotation()
        {
            var tableAttribute = typeof(ValeursEvaluations)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("valeur_evaluation", tableAttribute.Name);
        }

        [Fact]
        public void ValeursEvaluations_IdValeurHasKeyAndColumnAnnotation()
        {
            var properties = typeof(ValeursEvaluations).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "id_valeur");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("id_valeur", columnAttribute.Name);
        }

        // Test modifié : id_critere n'a plus l'attribut ForeignKey, seulement Column
        [Fact]
        public void ValeursEvaluations_IdCritereHasColumnAnnotation()
        {
            var properties = typeof(ValeursEvaluations).GetProperties();
            var idCritereProperty = properties.FirstOrDefault(p => p.Name == "id_critere");

            Assert.NotNull(idCritereProperty);

            var columnAttribute = idCritereProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("id_critere", columnAttribute.Name);

            // Vérifier qu'il n'y a PAS d'attribut ForeignKey sur la propriété de clé étrangère primitive
            Assert.False(idCritereProperty.IsDefined(typeof(ForeignKeyAttribute), false));
        }

        // Nouveau test pour la propriété de navigation Critere
        [Fact]
        public void ValeursEvaluations_CritereNavigationPropertyHasForeignKeyAnnotation()
        {
            var properties = typeof(ValeursEvaluations).GetProperties();
            var critereNavigationProperty = properties.FirstOrDefault(p => p.Name == "Critere"); // Nom de la propriété de navigation

            Assert.NotNull(critereNavigationProperty); // S'assure que la propriété de navigation existe

            var foreignKeyAttribute = critereNavigationProperty.GetCustomAttributes(typeof(ForeignKeyAttribute), false).FirstOrDefault() as ForeignKeyAttribute;
            Assert.NotNull(foreignKeyAttribute);
            Assert.Equal(nameof(ValeursEvaluations.id_critere), foreignKeyAttribute.Name); // Vérifie que cela pointe vers la bonne FK
        }


        [Fact]
        public void ValeursEvaluations_LabelHasRequiredAndColumnAnnotation()
        {
            var properties = typeof(ValeursEvaluations).GetProperties();
            var labelProperty = properties.FirstOrDefault(p => p.Name == "label");

            Assert.NotNull(labelProperty);
            Assert.True(labelProperty.IsDefined(typeof(RequiredAttribute), false));

            var columnAttribute = labelProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("label", columnAttribute.Name);
        }

        [Fact]
        public void ValeursEvaluations_ScoreHasRequiredAndColumnAnnotation()
        {
            var properties = typeof(ValeursEvaluations).GetProperties();
            var scoreProperty = properties.FirstOrDefault(p => p.Name == "score");

            Assert.NotNull(scoreProperty);
            Assert.True(scoreProperty.IsDefined(typeof(RequiredAttribute), false));

            var columnAttribute = scoreProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("score", columnAttribute.Name);
        }

        [Fact]
        public void ValeursEvaluations_RowVersionHasConcurrencyCheckAndColumnAnnotation()
        {
            var properties = typeof(ValeursEvaluations).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));

            var columnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("RowVersion", columnAttribute.Name);
        }

        [Fact]
        public void ValeursEvaluations_ImplementsIHasRowVersion()
        {
            Assert.True(typeof(IHasRowVersion).IsAssignableFrom(typeof(ValeursEvaluations)));
        }

        [Fact]
        public void ValeursEvaluations_Label_RequiredValidation()
        {
            var testCritere = new Criteres { id_critere = 1, nom = "Critere bidon", poids = 0.5f };

            // --- Test pour le cas où le label est vide (devrait échouer la validation) ---
            var valeurEvaluationEmptyLabel = new ValeursEvaluations { id_critere = 1, score = 1, label = string.Empty, Critere = testCritere };
            var validationContextEmptyLabel = new ValidationContext(valeurEvaluationEmptyLabel);
            var validationResultsEmptyLabel = new List<ValidationResult>();

            var isValidEmptyLabel = Validator.TryValidateObject(valeurEvaluationEmptyLabel, validationContextEmptyLabel, validationResultsEmptyLabel, true);

            // Nous nous attendons maintenant à ce que la validation ÉCHOUE pour une chaîne vide
            Assert.False(isValidEmptyLabel, $"La validation a réussi inattendument pour un label vide. Erreurs: {string.Join(", ", validationResultsEmptyLabel.Select(r => r.ErrorMessage))}");
            Assert.Contains(validationResultsEmptyLabel, r => r.MemberNames.Contains("label") && r.ErrorMessage!.Contains("The label field is required."));


            // --- Test pour le cas où le label est valide (devrait réussir la validation) ---
            var valeurEvaluationValidLabel = new ValeursEvaluations { id_critere = 1, score = 1, label = "Valid Label", Critere = testCritere };
            var validationContextValidLabel = new ValidationContext(valeurEvaluationValidLabel);
            var validationResultsValidLabel = new List<ValidationResult>();

            var isValidValidLabel = Validator.TryValidateObject(valeurEvaluationValidLabel, validationContextValidLabel, validationResultsValidLabel, true);

            Assert.True(isValidValidLabel);
            Assert.Empty(validationResultsValidLabel);
        }

        // Ajout d'un test pour vérifier le cas où le label serait null (ce qui ne devrait pas arriver avec l'initialiseur)
        [Fact]
        public void ValeursEvaluations_Label_RequiredValidation_FailsWhenNull()
        {
            // Pour résoudre CS8625, utilisez l'opérateur d'annulation de null (!) si vous savez que c'est intentionnel pour le test.
            // Cependant, la meilleure approche est de rendre la propriété nullable dans le modèle si null est un état valide,
            // ou de s'assurer que votre logique d'application ne produit jamais de null pour cette propriété [Required].
            // Ici, pour le test, on force le null.
            var valeurEvaluation = new ValeursEvaluations { id_critere = 1, score = 1, label = null! }; // Utiliser null! pour supprimer le warning CS8625
            var validationContext = new ValidationContext(valeurEvaluation);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(valeurEvaluation, validationContext, validationResults, true);

            Assert.False(isValid); // Doit être faux car label est null et [Required] est présent
            // Pour résoudre CS8602, utilisez l'opérateur d'annulation de null (!) sur ErrorMessage.
            // Ou vérifiez d'abord si ErrorMessage n'est pas null.
            Assert.Contains(validationResults, r => r.MemberNames.Contains("label") && r.ErrorMessage!.Contains("The label field is required."));
        }


        [Fact]
        public void ValeursEvaluations_Score_RequiredValidation()
        {
            // CRÉER UNE INSTANCE DE CRITERES POUR SATISFAIRE LA RELATION
            var testCritere = new Criteres { id_critere = 1, nom = "Critere bidon", poids = 0.5f };

            var valeurEvaluation = new ValeursEvaluations { id_critere = 1, label = "Test", score = 0, Critere = testCritere }; // Score peut être 0
            var validationContext = new ValidationContext(valeurEvaluation);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(valeurEvaluation, validationContext, validationResults, true);

            Assert.True(isValid);
            Assert.Empty(validationResults);

            valeurEvaluation.score = 10;
            validationResults.Clear();
            isValid = Validator.TryValidateObject(valeurEvaluation, validationContext, validationResults, true);
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }
    }
}