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
    public class CategorieSDSITests
    {
        [Fact]
        public void CategorieSDSI_CanBeInstantiated()
        {
            var categorieSDSI = new CategorieSDSI();
            Assert.NotNull(categorieSDSI);
        }

        [Fact]
        public void CategorieSDSI_Properties_HaveCorrectDefaultValues()
        {
            var categorieSDSI = new CategorieSDSI();

            Assert.Equal(0, categorieSDSI.Id);
            Assert.Equal(string.Empty, categorieSDSI.CATEGORIE_MD);
            Assert.Equal(string.Empty, categorieSDSI.CATEGORIE_SDSI);
            Assert.Equal(string.Empty, categorieSDSI.SOUS_CATEGORIE_SDSI);
            Assert.Equal(string.Empty, categorieSDSI.ACTIVITE_MODELE_DE_COUT);
            Assert.Equal(string.Empty, categorieSDSI.SERVICE_MODELE_DE_COUT_V2);
            Assert.Equal(string.Empty, categorieSDSI.SERVICE_MODELE_DE_COUT_V2_ABD);
            Assert.Equal(0, categorieSDSI.ID_LIBELLE);
            Assert.Equal(0, categorieSDSI.ID_VERSION);
            // Pour DateTime non-nullable, la valeur par défaut est DateTime.MinValue (01/01/0001)
            Assert.Equal(default(DateTime), categorieSDSI.DATE_DE_MODIFICATION);
            Assert.Equal(default(DateTime), categorieSDSI.DATE_DE_VALIDITE);
            Assert.True(categorieSDSI.ACTIF); // Default value set to true
            Assert.Equal(string.Empty, categorieSDSI.COMMENTAIRES);
            Assert.NotEqual(default(DateTime), categorieSDSI.RowVersion);
        }

        [Fact]
        public void CategorieSDSI_CanSetAndGetProperties()
        {
            var categorieSDSI = new CategorieSDSI();
            var testDateModif = new DateTime(2023, 1, 15);
            var testDateValid = new DateTime(2024, 12, 31);
            var testRowVersion = new DateTime(2025, 6, 5, 14, 0, 0);


            categorieSDSI.Id = 1;
            categorieSDSI.CATEGORIE_MD = "MD_Test";
            categorieSDSI.CATEGORIE_SDSI = "SDSI_Test";
            categorieSDSI.SOUS_CATEGORIE_SDSI = "Sous_SDSI_Test";
            categorieSDSI.ACTIVITE_MODELE_DE_COUT = "Activite_Test";
            categorieSDSI.SERVICE_MODELE_DE_COUT_V2 = "Service_V2_Test";
            categorieSDSI.SERVICE_MODELE_DE_COUT_V2_ABD = "Service_V2_ABD_Test";
            categorieSDSI.ID_LIBELLE = 100;
            categorieSDSI.ID_VERSION = 2;
            categorieSDSI.DATE_DE_MODIFICATION = testDateModif;
            categorieSDSI.DATE_DE_VALIDITE = testDateValid;
            categorieSDSI.ACTIF = false;
            categorieSDSI.COMMENTAIRES = "Commentaires Test";
            categorieSDSI.RowVersion = testRowVersion;

            Assert.Equal(1, categorieSDSI.Id);
            Assert.Equal("MD_Test", categorieSDSI.CATEGORIE_MD);
            Assert.Equal("SDSI_Test", categorieSDSI.CATEGORIE_SDSI);
            Assert.Equal("Sous_SDSI_Test", categorieSDSI.SOUS_CATEGORIE_SDSI);
            Assert.Equal("Activite_Test", categorieSDSI.ACTIVITE_MODELE_DE_COUT);
            Assert.Equal("Service_V2_Test", categorieSDSI.SERVICE_MODELE_DE_COUT_V2);
            Assert.Equal("Service_V2_ABD_Test", categorieSDSI.SERVICE_MODELE_DE_COUT_V2_ABD);
            Assert.Equal(100, categorieSDSI.ID_LIBELLE);
            Assert.Equal(2, categorieSDSI.ID_VERSION);
            Assert.Equal(testDateModif, categorieSDSI.DATE_DE_MODIFICATION);
            Assert.Equal(testDateValid, categorieSDSI.DATE_DE_VALIDITE);
            Assert.False(categorieSDSI.ACTIF);
            Assert.Equal("Commentaires Test", categorieSDSI.COMMENTAIRES);
            Assert.Equal(testRowVersion, categorieSDSI.RowVersion);
        }

        [Fact]
        public void CategorieSDSI_HasKeyAnnotationOnId()
        {
            var properties = typeof(CategorieSDSI).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));
        }

        [Fact]
        public void CategorieSDSI_HasTableAnnotation()
        {
            var tableAttribute = typeof(CategorieSDSI)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("CATEGORIE_SDSI", tableAttribute.Name);
        }

        [Fact]
        public void CategorieSDSI_IdHasColumnAnnotation()
        {
            var properties = typeof(CategorieSDSI).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");

            Assert.NotNull(idProperty);
            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            Assert.NotNull(columnAttribute);
            Assert.Equal("CSDSI_ID_CATEGORIE_SDSI", columnAttribute.Name);
        }

        [Fact]
        public void CategorieSDSI_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(CategorieSDSI).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        [Fact]
        public void CategorieSDSI_ImplementsIHasRowVersion()
        {
            var categorieSDSI = new CategorieSDSI();
            Assert.IsAssignableFrom<IHasRowVersion>(categorieSDSI);
        }
    }
}