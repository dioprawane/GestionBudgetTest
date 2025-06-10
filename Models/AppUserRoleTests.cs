using Xunit;
using GestionBudgétaire.Data.Entities.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using GestionBudgétaire.Data.Entities; // Pour IHasRowVersion

namespace GestionBudgetaireTest.Models
{
    public class AppUserRoleTests
    {
        [Fact]
        public void AppUserRole_CanBeInstantiated()
        {
            var appUserRole = new AppUserRole();
            Assert.NotNull(appUserRole);
        }

        [Fact]
        public void AppUserRole_Properties_HaveCorrectDefaultValues()
        {
            var appUserRole = new AppUserRole();

            Assert.Equal(0, appUserRole.Id);
            Assert.Equal(string.Empty, appUserRole.CompteAd);
            Assert.Equal(string.Empty, appUserRole.Role);
            Assert.NotEqual(default(DateTime), appUserRole.RowVersion);
        }

        [Fact]
        public void AppUserRole_CanSetAndGetProperties()
        {
            var appUserRole = new AppUserRole();
            var testDate = new DateTime(2025, 6, 5, 13, 0, 0);

            appUserRole.Id = 1;
            appUserRole.CompteAd = "jdupont";
            appUserRole.Role = "Administrateur";
            appUserRole.RowVersion = testDate;

            Assert.Equal(1, appUserRole.Id);
            Assert.Equal("jdupont", appUserRole.CompteAd);
            Assert.Equal("Administrateur", appUserRole.Role);
            Assert.Equal(testDate, appUserRole.RowVersion);
        }

        [Fact]
        public void AppUserRole_HasKeyAnnotationOnId()
        {
            var properties = typeof(AppUserRole).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");

            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));
        }

        [Fact]
        public void AppUserRole_HasTableAnnotation()
        {
            var tableAttribute = typeof(AppUserRole)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            Assert.NotNull(tableAttribute);
            Assert.Equal("appuserroles", tableAttribute.Name);
        }

        [Fact]
        public void AppUserRole_RowVersionHasConcurrencyCheckAnnotation()
        {
            var properties = typeof(AppUserRole).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        [Fact]
        public void AppUserRole_ImplementsIHasRowVersion()
        {
            var appUserRole = new AppUserRole();
            Assert.IsAssignableFrom<IHasRowVersion>(appUserRole);
        }
    }
}