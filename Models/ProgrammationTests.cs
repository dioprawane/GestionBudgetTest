using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit; // Le framework de test xUnit
using System.Linq; // Pour FirstOrDefault
using GestionBudgétaire.Data.Entities.Database; // Assurez-vous que le namespace de votre modèle est correct
using GestionBudgétaire.Data.Entities; // Si IHasRowVersion est dans ce namespace

namespace GestionBudgetaireTest.Models
{
    /// <summary>
    /// Cette classe contient des tests unitaires pour le modèle de données 'Programmation'.
    /// L'objectif est de s'assurer que le modèle se comporte comme attendu,
    /// notamment en ce qui concerne l'instanciation, les valeurs par défaut des propriétés,
    /// la capacité à définir et récupérer des valeurs et la présence des annotations
    /// Entity Framework Core nécessaires pour le mapping base de données.
    /// Ces tests sont des 'unit tests' purs car ils ne nécessitent pas de base de données réelle
    /// ou en mémoire ; ils testent uniquement la logique et la structure du modèle lui-même.
    /// </summary>
    public class ProgrammationTests
    {
        /// <summary>
        /// Teste si une instance du modèle Programmation peut être créée avec succès.
        /// </summary>
        [Fact]
        public void Programmation_CanBeInstantiated()
        {
            // Arrange & Act
            var programmation = new Programmation();

            // Assert
            Assert.NotNull(programmation);
        }

        /// <summary>
        /// Teste si les propriétés de type string? ont la valeur par défaut null ou string.Empty
        /// et les decimal? ont la valeur par défaut null.
        /// Les propriétés DateOnly? doivent être null par défaut.
        /// La propriété RowVersion doit être initialisée à DateTime.Now.
        /// </summary>
        [Fact]
        public void Programmation_Properties_HaveCorrectDefaultValues()
        {
            // Arrange & Act
            var programmation = new Programmation();

            // Assert
            // Propriétés string?
            Assert.Null(programmation.JeuDeDonnees); // Editable(false) n'impacte pas la valeur par défaut CLR
            Assert.Null(programmation.MacroDesignation);
            Assert.Null(programmation.NatureDuBesoin);
            Assert.Null(programmation.CadreDAchat);
            Assert.Null(programmation.Fournisseur);
            Assert.Null(programmation.NumeroTiers);
            Assert.Null(programmation.LibelleDuMarcheOuDeLaDepense);
            Assert.Null(programmation.MasseBudgetaireNatureGbcp);
            Assert.Null(programmation.DaDossierDAnalyse);
            Assert.Null(programmation.AxeNational1);
            Assert.Null(programmation.GrandProjet);
            Assert.Null(programmation.StatutProjetG2pi);
            Assert.Null(programmation.CodeInitiativeSdsi);
            Assert.Null(programmation.ComptesBudgetaires);
            Assert.Null(programmation.ComptesComptables);
            Assert.Null(programmation.DirectionAdjointe);
            Assert.Null(programmation.Portefeuille);
            Assert.Null(programmation.SousPortefeuille);
            Assert.Null(programmation.NewSousPortefeuille);
            Assert.Null(programmation.CaracteristiqueDuBesoin);
            Assert.Null(programmation.CriticiteEnjeuMetiers);
            Assert.Null(programmation.NumeroAb);
            Assert.Null(programmation.NumeroDossierBudgetaire);
            Assert.Null(programmation.NumeroCommande);
            Assert.Null(programmation.NumeroEngagement);
            Assert.Null(programmation.Commentaires);

            // Propriétés decimal?
            Assert.Null(programmation.BudgetInitialDemande);
            Assert.Null(programmation.BudgetDg1Demande);
            Assert.Null(programmation.BudgetDg2Demande);
            Assert.Null(programmation.BudgetDg3Demande);
            Assert.Null(programmation.BudgetDg4Demande);
            Assert.Null(programmation.DemandeDeReportIdentifie);
            Assert.Null(programmation.PlanificationBudgetaireBiJanvier);
            Assert.Null(programmation.PlanificationBudgetaireBr1Juillet);
            Assert.Null(programmation.PlanificationBudgetaireBr2Novembre);
            Assert.Null(programmation.BudgetInitialAutoriseJanvier);
            Assert.Null(programmation.MontantDeLaCommande);
            Assert.Null(programmation.MontantEngage);
            Assert.Null(programmation.Realise);
            Assert.Null(programmation.Ga486AnneeN1);
            Assert.Null(programmation.Ga486AnneeN2);
            Assert.Null(programmation.Ga486AnneeN3);
            Assert.Null(programmation.Ga486AnneeN4);
            Assert.Null(programmation.BudgetPrevisionnelN1);
            Assert.Null(programmation.BudgetPrevisionnelN2);
            Assert.Null(programmation.BudgetPrevisionnelN3);
            Assert.Null(programmation.BudgetPrevisionnelN4);

            // Propriétés DateOnly?
            Assert.Null(programmation.DateDeDebut);
            Assert.Null(programmation.DateDeFin);

            // Propriété RowVersion
            Assert.NotEqual(default(DateTime), programmation.RowVersion);
        }

        /// <summary>
        /// Teste si les propriétés du modèle Programmation peuvent être correctement définies (set)
        /// et récupérées (get) avec des valeurs spécifiques.
        /// </summary>
        [Fact]
        public void Programmation_CanSetAndGetProperties()
        {
            // Arrange
            var programmation = new Programmation();
            var testDate = DateTime.Now;
            var testDateOnly = DateOnly.FromDateTime(DateTime.Now);

            // Act
            programmation.Prog_Id = 12345; // ID auto-généré, mais on peut le définir pour le test unitaire
            programmation.JeuDeDonnees = "JeuDeDonneesTest";
            programmation.MacroDesignation = "MacroDesignationTest";
            programmation.NatureDuBesoin = "NatureDuBesoinTest";
            programmation.CadreDAchat = "CadreDAchatTest";
            programmation.Fournisseur = "FournisseurTest";
            programmation.NumeroTiers = "NTiers123";
            programmation.LibelleDuMarcheOuDeLaDepense = "LibelleDuMarcheTest";
            programmation.MasseBudgetaireNatureGbcp = "MasseBudgetaireTest";
            programmation.DaDossierDAnalyse = "DossierAnalyseTest";
            programmation.AxeNational1 = "AxeNational1Test";
            programmation.GrandProjet = "GrandProjetTest";
            programmation.StatutProjetG2pi = "StatutProjetG2piTest";
            programmation.CodeInitiativeSdsi = "CodeInitiativeSdsiTest";
            programmation.ComptesBudgetaires = "ComptesBudgetairesTest";
            programmation.ComptesComptables = "ComptesComptablesTest";
            programmation.DirectionAdjointe = "DirectionAdjointeTest";
            programmation.Portefeuille = "PortefeuilleTest";
            programmation.SousPortefeuille = "SousPortefeuilleTest";
            programmation.NewSousPortefeuille = "NewSousPortefeuilleTest";
            programmation.CaracteristiqueDuBesoin = "CaracteristiqueDuBesoinTest";
            programmation.CriticiteEnjeuMetiers = "CriticiteTest";
            programmation.BudgetInitialDemande = 1000.50m;
            programmation.BudgetDg1Demande = 1100.50m;
            programmation.BudgetDg2Demande = 1200.50m;
            programmation.BudgetDg3Demande = 1300.50m;
            programmation.BudgetDg4Demande = 1400.50m;
            programmation.DemandeDeReportIdentifie = 50.75m;
            programmation.PlanificationBudgetaireBiJanvier = 100m;
            programmation.PlanificationBudgetaireBr1Juillet = 200m;
            programmation.PlanificationBudgetaireBr2Novembre = 300m;
            programmation.NumeroAb = "AB001";
            programmation.NumeroDossierBudgetaire = "DB001";
            programmation.BudgetInitialAutoriseJanvier = 950.25m;
            programmation.NumeroCommande = "CMD001";
            programmation.DateDeDebut = testDateOnly;
            programmation.DateDeFin = testDateOnly.AddDays(30);
            programmation.MontantDeLaCommande = 2000.00m;
            programmation.NumeroEngagement = "ENG001";
            programmation.MontantEngage = 1900.00m;
            programmation.Realise = 1800.00m;
            programmation.Ga486AnneeN1 = 486.1m;
            programmation.Ga486AnneeN2 = 486.2m;
            programmation.Ga486AnneeN3 = 486.3m;
            programmation.Ga486AnneeN4 = 486.4m;
            programmation.BudgetPrevisionnelN1 = 2025.1m;
            programmation.BudgetPrevisionnelN2 = 2026.2m;
            programmation.BudgetPrevisionnelN3 = 2027.3m;
            programmation.BudgetPrevisionnelN4 = 2028.4m;
            programmation.Commentaires = "Ceci est un commentaire de test.";
            programmation.RowVersion = testDate.AddHours(2);

            // Assert
            Assert.Equal(12345, programmation.Prog_Id);
            Assert.Equal("JeuDeDonneesTest", programmation.JeuDeDonnees);
            Assert.Equal("MacroDesignationTest", programmation.MacroDesignation);
            Assert.Equal("NatureDuBesoinTest", programmation.NatureDuBesoin);
            Assert.Equal("CadreDAchatTest", programmation.CadreDAchat);
            Assert.Equal("FournisseurTest", programmation.Fournisseur);
            Assert.Equal("NTiers123", programmation.NumeroTiers);
            Assert.Equal("LibelleDuMarcheTest", programmation.LibelleDuMarcheOuDeLaDepense);
            Assert.Equal("MasseBudgetaireTest", programmation.MasseBudgetaireNatureGbcp);
            Assert.Equal("DossierAnalyseTest", programmation.DaDossierDAnalyse);
            Assert.Equal("AxeNational1Test", programmation.AxeNational1);
            Assert.Equal("GrandProjetTest", programmation.GrandProjet);
            Assert.Equal("StatutProjetG2piTest", programmation.StatutProjetG2pi);
            Assert.Equal("CodeInitiativeSdsiTest", programmation.CodeInitiativeSdsi);
            Assert.Equal("ComptesBudgetairesTest", programmation.ComptesBudgetaires);
            Assert.Equal("ComptesComptablesTest", programmation.ComptesComptables);
            Assert.Equal("DirectionAdjointeTest", programmation.DirectionAdjointe);
            Assert.Equal("PortefeuilleTest", programmation.Portefeuille);
            Assert.Equal("SousPortefeuilleTest", programmation.SousPortefeuille);
            Assert.Equal("NewSousPortefeuilleTest", programmation.NewSousPortefeuille);
            Assert.Equal("CaracteristiqueDuBesoinTest", programmation.CaracteristiqueDuBesoin);
            Assert.Equal("CriticiteTest", programmation.CriticiteEnjeuMetiers);
            Assert.Equal(1000.50m, programmation.BudgetInitialDemande);
            Assert.Equal(1100.50m, programmation.BudgetDg1Demande);
            Assert.Equal(1200.50m, programmation.BudgetDg2Demande);
            Assert.Equal(1300.50m, programmation.BudgetDg3Demande);
            Assert.Equal(1400.50m, programmation.BudgetDg4Demande);
            Assert.Equal(50.75m, programmation.DemandeDeReportIdentifie);
            Assert.Equal(100m, programmation.PlanificationBudgetaireBiJanvier);
            Assert.Equal(200m, programmation.PlanificationBudgetaireBr1Juillet);
            Assert.Equal(300m, programmation.PlanificationBudgetaireBr2Novembre);
            Assert.Equal("AB001", programmation.NumeroAb);
            Assert.Equal("DB001", programmation.NumeroDossierBudgetaire);
            Assert.Equal(950.25m, programmation.BudgetInitialAutoriseJanvier);
            Assert.Equal("CMD001", programmation.NumeroCommande);
            Assert.Equal(testDateOnly, programmation.DateDeDebut);
            Assert.Equal(testDateOnly.AddDays(30), programmation.DateDeFin);
            Assert.Equal(2000.00m, programmation.MontantDeLaCommande);
            Assert.Equal("ENG001", programmation.NumeroEngagement);
            Assert.Equal(1900.00m, programmation.MontantEngage);
            Assert.Equal(1800.00m, programmation.Realise);
            Assert.Equal(486.1m, programmation.Ga486AnneeN1);
            Assert.Equal(486.2m, programmation.Ga486AnneeN2);
            Assert.Equal(486.3m, programmation.Ga486AnneeN3);
            Assert.Equal(486.4m, programmation.Ga486AnneeN4);
            Assert.Equal(2025.1m, programmation.BudgetPrevisionnelN1);
            Assert.Equal(2026.2m, programmation.BudgetPrevisionnelN2);
            Assert.Equal(2027.3m, programmation.BudgetPrevisionnelN3);
            Assert.Equal(2028.4m, programmation.BudgetPrevisionnelN4);
            Assert.Equal("Ceci est un commentaire de test.", programmation.Commentaires);
            Assert.Equal(testDate.AddHours(2), programmation.RowVersion);
        }

        /// <summary>
        /// Teste si la propriété 'Prog_Id' du modèle Programmation possède l'annotation '[Key]'
        /// et '[Column("Prog_Id")]' et '[DatabaseGenerated(DatabaseGeneratedOption.Identity)]'.
        /// </summary>
        [Fact]
        public void Programmation_HasKeyAndColumnAndDatabaseGeneratedIdentityAnnotationsOnProgId()
        {
            // Arrange
            var properties = typeof(Programmation).GetProperties();
            var progIdProperty = properties.FirstOrDefault(p => p.Name == "Prog_Id");

            // Assert
            Assert.NotNull(progIdProperty);
            Assert.True(progIdProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = progIdProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("Prog_Id", columnAttribute.Name);

            var databaseGeneratedAttribute = progIdProperty.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false).FirstOrDefault() as DatabaseGeneratedAttribute;
            Assert.NotNull(databaseGeneratedAttribute);
            Assert.Equal(DatabaseGeneratedOption.Identity, databaseGeneratedAttribute.DatabaseGeneratedOption);
        }

        /// <summary>
        /// Teste si la classe Programmation elle-même possède l'annotation '[Table("programmation_budgetaire", Schema = "gestion_budgetaire")]'.
        /// </summary>
        [Fact]
        public void Programmation_HasTableAnnotation()
        {
            // Arrange
            var tableAttribute = typeof(Programmation)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            // Assert
            Assert.NotNull(tableAttribute);
            Assert.Equal("programmation_budgetaire", tableAttribute.Name);
            Assert.Equal("gestion_budgetaire", tableAttribute.Schema);
        }

        /// <summary>
        /// Teste si la propriété 'RowVersion' du modèle Programmation possède l'annotation '[ConcurrencyCheck]'
        /// et '[Column("RowVersion")]' et '[ScaffoldColumn(false)]'.
        /// </summary>
        [Fact]
        public void Programmation_RowVersionHasConcurrencyCheckAndColumnAndScaffoldColumnAnnotations()
        {
            // Arrange
            var properties = typeof(Programmation).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            // Assert
            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));

            var columnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("RowVersion", columnAttribute.Name);

            Assert.True(rowVersionProperty.IsDefined(typeof(ScaffoldColumnAttribute), false));
            var scaffoldColumnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ScaffoldColumnAttribute), false).FirstOrDefault() as ScaffoldColumnAttribute;
            Assert.NotNull(scaffoldColumnAttribute);
            Assert.False(scaffoldColumnAttribute.Scaffold);
        }

        /// <summary>
        /// Teste si la propriété 'JeuDeDonnees' du modèle Programmation possède l'annotation '[Editable(false)]'.
        /// </summary>
        [Fact]
        public void Programmation_JeuDeDonneesHasEditableFalseAnnotation()
        {
            // Arrange
            var properties = typeof(Programmation).GetProperties();
            var jeuDeDonneesProperty = properties.FirstOrDefault(p => p.Name == "JeuDeDonnees");

            // Assert
            Assert.NotNull(jeuDeDonneesProperty);
            Assert.True(jeuDeDonneesProperty.IsDefined(typeof(EditableAttribute), false));
            var editableAttribute = jeuDeDonneesProperty.GetCustomAttributes(typeof(EditableAttribute), false).FirstOrDefault() as EditableAttribute;
            Assert.NotNull(editableAttribute);
            Assert.False(editableAttribute.AllowEdit);
        }

        /// <summary>
        /// Teste si le modèle Programmation implémente correctement l'interface 'IHasRowVersion'.
        /// </summary>
        [Fact]
        public void Programmation_ImplementsIHasRowVersion()
        {
            // Arrange & Act
            var programmation = new Programmation();

            // Assert
            Assert.IsAssignableFrom<IHasRowVersion>(programmation);
        }
    }
}