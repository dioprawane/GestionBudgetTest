using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;
using System.Linq;
using GestionBudgétaire.Data.Entities.Database;
using GestionBudgétaire.Data.Entities;

namespace GestionBudgetaireTest.Models
{
    public class ProgrammationPhotoTests
    {
        [Fact]
        public void ProgrammationPhoto_CanBeInstantiated()
        {
            // Arrange & Act
            var programmationPhoto = new ProgrammationPhoto();

            // Assert
            Assert.NotNull(programmationPhoto);
        }

        [Fact]
        public void ProgrammationPhoto_Properties_HaveCorrectDefaultValues()
        {
            // Arrange & Act
            var programmationPhoto = new ProgrammationPhoto();

            // Assert
            // String? properties (nullable strings default to null)
            Assert.Null(programmationPhoto.JeuDeDonnees);
            Assert.Null(programmationPhoto.MacroDesignation);
            Assert.Null(programmationPhoto.NatureDuBesoin);
            Assert.Null(programmationPhoto.CadreDAchat);
            Assert.Null(programmationPhoto.Fournisseur);
            Assert.Null(programmationPhoto.NumeroTiers);
            Assert.Null(programmationPhoto.LibelleDuMarcheOuDeLaDepense);
            Assert.Null(programmationPhoto.MasseBudgetaireNatureGbcp);
            Assert.Null(programmationPhoto.DaDossierDAnalyse);
            Assert.Null(programmationPhoto.AxeNational1);
            Assert.Null(programmationPhoto.GrandProjet);
            Assert.Null(programmationPhoto.StatutProjetG2pi);
            Assert.Null(programmationPhoto.CodeInitiativeSdsi);
            Assert.Null(programmationPhoto.ComptesBudgetaires);
            Assert.Null(programmationPhoto.ComptesComptables);
            Assert.Null(programmationPhoto.DirectionAdjointe);
            Assert.Null(programmationPhoto.Portefeuille);
            Assert.Null(programmationPhoto.SousPortefeuille);
            Assert.Null(programmationPhoto.NewSousPortefeuille);
            Assert.Null(programmationPhoto.CaracteristiqueDuBesoin);
            Assert.Null(programmationPhoto.CriticiteEnjeuMetiers);
            Assert.Null(programmationPhoto.NumeroAb);
            Assert.Null(programmationPhoto.NumeroDossierBudgetaire);
            Assert.Null(programmationPhoto.NumeroCommande);
            Assert.Null(programmationPhoto.NumeroEngagement);
            Assert.Null(programmationPhoto.Commentaires);

            // Decimal? properties (nullable decimals default to null)
            Assert.Null(programmationPhoto.BudgetInitialDemande);
            Assert.Null(programmationPhoto.BudgetDg1Demande);
            Assert.Null(programmationPhoto.BudgetDg2Demande);
            Assert.Null(programmationPhoto.BudgetDg3Demande);
            Assert.Null(programmationPhoto.BudgetDg4Demande);
            Assert.Null(programmationPhoto.DemandeDeReportIdentifie);
            Assert.Null(programmationPhoto.PlanificationBudgetaireBiJanvier);
            Assert.Null(programmationPhoto.PlanificationBudgetaireBr1Juillet);
            Assert.Null(programmationPhoto.PlanificationBudgetaireBr2Novembre);
            Assert.Null(programmationPhoto.BudgetInitialAutoriseJanvier);
            Assert.Null(programmationPhoto.MontantDeLaCommande);
            Assert.Null(programmationPhoto.MontantEngage);
            Assert.Null(programmationPhoto.Realise);
            Assert.Null(programmationPhoto.Ga486AnneeN1);
            Assert.Null(programmationPhoto.Ga486AnneeN2);
            Assert.Null(programmationPhoto.Ga486AnneeN3);
            Assert.Null(programmationPhoto.Ga486AnneeN4);
            Assert.Null(programmationPhoto.BudgetPrevisionnelN1);
            Assert.Null(programmationPhoto.BudgetPrevisionnelN2);
            Assert.Null(programmationPhoto.BudgetPrevisionnelN3);
            Assert.Null(programmationPhoto.BudgetPrevisionnelN4);

            // DateOnly? properties (nullable DateOnly default to null)
            Assert.Null(programmationPhoto.DateDeDebut);
            Assert.Null(programmationPhoto.DateDeFin);

            // DateTime properties (initialized to DateTime.Now)
            Assert.NotEqual(default(DateTime), programmationPhoto.DatePrisePhoto);
            Assert.NotEqual(default(DateTime), programmationPhoto.RowVersion);
        }

        [Fact]
        public void ProgrammationPhoto_CanSetAndGetProperties()
        {
            // Arrange
            var programmationPhoto = new ProgrammationPhoto();
            var testDateTime = new DateTime(2025, 6, 17, 10, 30, 0);
            var testDateOnly = new DateOnly(2025, 6, 17);

            // Act
            programmationPhoto.Prog_Id = 98765;
            programmationPhoto.JeuDeDonnees = "PhotoData";
            programmationPhoto.DatePrisePhoto = testDateTime.AddDays(-1);
            programmationPhoto.MacroDesignation = "MacroPhoto";
            programmationPhoto.NatureDuBesoin = "BesoinPhoto";
            programmationPhoto.CadreDAchat = "CadrePhoto";
            programmationPhoto.Fournisseur = "FournisseurPhoto";
            programmationPhoto.NumeroTiers = "TiersPhoto";
            programmationPhoto.LibelleDuMarcheOuDeLaDepense = "LibellePhoto";
            programmationPhoto.MasseBudgetaireNatureGbcp = "MassePhoto";
            programmationPhoto.DaDossierDAnalyse = "DossierPhoto";
            programmationPhoto.AxeNational1 = "AxePhoto";
            programmationPhoto.GrandProjet = "GrandProjetPhoto";
            programmationPhoto.StatutProjetG2pi = "StatutPhoto";
            programmationPhoto.CodeInitiativeSdsi = "CodeSDSIPhoto";
            programmationPhoto.ComptesBudgetaires = "ComptesBudgetairesPhoto";
            programmationPhoto.ComptesComptables = "ComptesComptablesPhoto";
            programmationPhoto.DirectionAdjointe = "DirectionAdjointePhoto";
            programmationPhoto.Portefeuille = "PortefeuillePhoto";
            programmationPhoto.SousPortefeuille = "SousPortefeuillePhoto";
            programmationPhoto.NewSousPortefeuille = "NewSousPortefeuillePhoto";
            programmationPhoto.CaracteristiqueDuBesoin = "CaractPhoto";
            programmationPhoto.CriticiteEnjeuMetiers = "CriticitePhoto";
            programmationPhoto.BudgetInitialDemande = 2000.00m;
            programmationPhoto.BudgetDg1Demande = 2100.00m;
            programmationPhoto.BudgetDg2Demande = 2200.00m;
            programmationPhoto.BudgetDg3Demande = 2300.00m;
            programmationPhoto.BudgetDg4Demande = 2400.00m;
            programmationPhoto.DemandeDeReportIdentifie = 100.00m;
            programmationPhoto.PlanificationBudgetaireBiJanvier = 500.00m;
            programmationPhoto.PlanificationBudgetaireBr1Juillet = 600.00m;
            programmationPhoto.PlanificationBudgetaireBr2Novembre = 700.00m;
            programmationPhoto.NumeroAb = "AB_Photo_01";
            programmationPhoto.NumeroDossierBudgetaire = "DB_Photo_01";
            programmationPhoto.BudgetInitialAutoriseJanvier = 1900.00m;
            programmationPhoto.NumeroCommande = "CMD_Photo_01";
            programmationPhoto.DateDeDebut = testDateOnly;
            programmationPhoto.DateDeFin = testDateOnly.AddMonths(6);
            programmationPhoto.MontantDeLaCommande = 2500.00m;
            programmationPhoto.NumeroEngagement = "ENG_Photo_01";
            programmationPhoto.MontantEngage = 2400.00m;
            programmationPhoto.Realise = 2300.00m;
            programmationPhoto.Ga486AnneeN1 = 500.00m;
            programmationPhoto.Ga486AnneeN2 = 550.00m;
            programmationPhoto.Ga486AnneeN3 = 600.00m;
            programmationPhoto.Ga486AnneeN4 = 650.00m;
            programmationPhoto.BudgetPrevisionnelN1 = 1500.00m;
            programmationPhoto.BudgetPrevisionnelN2 = 1600.00m;
            programmationPhoto.BudgetPrevisionnelN3 = 1700.00m;
            programmationPhoto.BudgetPrevisionnelN4 = 1800.00m;
            programmationPhoto.Commentaires = "Commentaires photo test.";
            programmationPhoto.RowVersion = testDateTime;

            // Assert
            Assert.Equal(98765, programmationPhoto.Prog_Id);
            Assert.Equal("PhotoData", programmationPhoto.JeuDeDonnees);
            Assert.Equal(testDateTime.AddDays(-1), programmationPhoto.DatePrisePhoto);
            Assert.Equal("MacroPhoto", programmationPhoto.MacroDesignation);
            Assert.Equal("BesoinPhoto", programmationPhoto.NatureDuBesoin);
            Assert.Equal("CadrePhoto", programmationPhoto.CadreDAchat);
            Assert.Equal("FournisseurPhoto", programmationPhoto.Fournisseur);
            Assert.Equal("TiersPhoto", programmationPhoto.NumeroTiers);
            Assert.Equal("LibellePhoto", programmationPhoto.LibelleDuMarcheOuDeLaDepense);
            Assert.Equal("MassePhoto", programmationPhoto.MasseBudgetaireNatureGbcp);
            Assert.Equal("DossierPhoto", programmationPhoto.DaDossierDAnalyse);
            Assert.Equal("AxePhoto", programmationPhoto.AxeNational1);
            Assert.Equal("GrandProjetPhoto", programmationPhoto.GrandProjet);
            Assert.Equal("StatutPhoto", programmationPhoto.StatutProjetG2pi);
            Assert.Equal("CodeSDSIPhoto", programmationPhoto.CodeInitiativeSdsi);
            Assert.Equal("ComptesBudgetairesPhoto", programmationPhoto.ComptesBudgetaires);
            Assert.Equal("ComptesComptablesPhoto", programmationPhoto.ComptesComptables);
            Assert.Equal("DirectionAdjointePhoto", programmationPhoto.DirectionAdjointe);
            Assert.Equal("PortefeuillePhoto", programmationPhoto.Portefeuille);
            Assert.Equal("SousPortefeuillePhoto", programmationPhoto.SousPortefeuille);
            Assert.Equal("NewSousPortefeuillePhoto", programmationPhoto.NewSousPortefeuille);
            Assert.Equal("CaractPhoto", programmationPhoto.CaracteristiqueDuBesoin);
            Assert.Equal("CriticitePhoto", programmationPhoto.CriticiteEnjeuMetiers);
            Assert.Equal(2000.00m, programmationPhoto.BudgetInitialDemande);
            Assert.Equal(2100.00m, programmationPhoto.BudgetDg1Demande);
            Assert.Equal(2200.00m, programmationPhoto.BudgetDg2Demande);
            Assert.Equal(2300.00m, programmationPhoto.BudgetDg3Demande);
            Assert.Equal(2400.00m, programmationPhoto.BudgetDg4Demande);
            Assert.Equal(100.00m, programmationPhoto.DemandeDeReportIdentifie);
            Assert.Equal(500.00m, programmationPhoto.PlanificationBudgetaireBiJanvier);
            Assert.Equal(600.00m, programmationPhoto.PlanificationBudgetaireBr1Juillet);
            Assert.Equal(700.00m, programmationPhoto.PlanificationBudgetaireBr2Novembre);
            Assert.Equal("AB_Photo_01", programmationPhoto.NumeroAb);
            Assert.Equal("DB_Photo_01", programmationPhoto.NumeroDossierBudgetaire);
            Assert.Equal(1900.00m, programmationPhoto.BudgetInitialAutoriseJanvier);
            Assert.Equal("CMD_Photo_01", programmationPhoto.NumeroCommande);
            Assert.Equal(testDateOnly, programmationPhoto.DateDeDebut);
            Assert.Equal(testDateOnly.AddMonths(6), programmationPhoto.DateDeFin);
            Assert.Equal(2500.00m, programmationPhoto.MontantDeLaCommande);
            Assert.Equal("ENG_Photo_01", programmationPhoto.NumeroEngagement);
            Assert.Equal(2400.00m, programmationPhoto.MontantEngage);
            Assert.Equal(2300.00m, programmationPhoto.Realise);
            Assert.Equal(500.00m, programmationPhoto.Ga486AnneeN1);
            Assert.Equal(550.00m, programmationPhoto.Ga486AnneeN2);
            Assert.Equal(600.00m, programmationPhoto.Ga486AnneeN3);
            Assert.Equal(650.00m, programmationPhoto.Ga486AnneeN4);
            Assert.Equal(1500.00m, programmationPhoto.BudgetPrevisionnelN1);
            Assert.Equal(1600.00m, programmationPhoto.BudgetPrevisionnelN2);
            Assert.Equal(1700.00m, programmationPhoto.BudgetPrevisionnelN3);
            Assert.Equal(1800.00m, programmationPhoto.BudgetPrevisionnelN4);
            Assert.Equal("Commentaires photo test.", programmationPhoto.Commentaires);
            Assert.Equal(testDateTime, programmationPhoto.RowVersion);
        }

        [Fact]
        public void ProgrammationPhoto_HasKeyAndColumnAndDatabaseGeneratedIdentityAnnotationsOnProgId()
        {
            // Arrange
            var properties = typeof(ProgrammationPhoto).GetProperties();
            var progIdProperty = properties.FirstOrDefault(p => p.Name == "Prog_Id");

            // Assert
            Assert.NotNull(progIdProperty);
            Assert.True(progIdProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = progIdProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("Prog_Photos_Id", columnAttribute.Name);

            var databaseGeneratedAttribute = progIdProperty.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false).FirstOrDefault() as DatabaseGeneratedAttribute;
            Assert.NotNull(databaseGeneratedAttribute);
            Assert.Equal(DatabaseGeneratedOption.Identity, databaseGeneratedAttribute.DatabaseGeneratedOption);
        }

        [Fact]
        public void ProgrammationPhoto_HasTableAnnotation()
        {
            // Arrange
            var tableAttribute = typeof(ProgrammationPhoto)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            // Assert
            Assert.NotNull(tableAttribute);
            Assert.Equal("programmation_photos", tableAttribute.Name);
            Assert.Equal("gestion_budgetaire", tableAttribute.Schema);
        }

        [Fact]
        public void ProgrammationPhoto_RowVersionHasConcurrencyCheckAndColumnAnnotations()
        {
            // Arrange
            var properties = typeof(ProgrammationPhoto).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            // Assert
            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));

            var columnAttribute = rowVersionProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("RowVersion", columnAttribute.Name);
        }

        [Fact]
        public void ProgrammationPhoto_DatePrisePhotoHasColumnAnnotation()
        {
            // Arrange
            var properties = typeof(ProgrammationPhoto).GetProperties();
            var datePrisePhotoProperty = properties.FirstOrDefault(p => p.Name == "DatePrisePhoto");

            // Assert
            Assert.NotNull(datePrisePhotoProperty);
            var columnAttribute = datePrisePhotoProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("DatePrisePhoto", columnAttribute.Name);
        }

        [Fact]
        public void ProgrammationPhoto_ImplementsIHasRowVersion()
        {
            // Arrange & Act
            var programmationPhoto = new ProgrammationPhoto();

            // Assert
            Assert.IsAssignableFrom<IHasRowVersion>(programmationPhoto);
        }
    }
}