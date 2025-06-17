using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using Xunit;
using Radzen;
using GestionBudgétaire.Data.Entities.Database;

namespace GestionBudgetaireTest.Models
{
    public class UserPreferenceTests
    {
        [Fact]
        public void UserPreference_CanBeInstantiated()
        {
            // Arrange & Act
            var userPreference = new UserPreference();

            // Assert
            Assert.NotNull(userPreference);
        }

        [Fact]
        public void UserPreference_DirectProperties_HaveCorrectDefaultValues()
        {
            // Arrange & Act
            var userPreference = new UserPreference();

            // Assert
            Assert.Equal(0, userPreference.Id); // Default for int
            Assert.Null(userPreference.UserId); // Default for string?
            Assert.Null(userPreference.PreferencesJson); // Default for string?
        }

        [Fact]
        public void UserPreference_CanSetAndGetDirectProperties()
        {
            // Arrange
            var userPreference = new UserPreference();
            var testId = 123;
            var testUserId = "user456";
            var testJson = "{\"key\":\"value\"}";

            // Act
            userPreference.Id = testId;
            userPreference.UserId = testUserId;
            userPreference.PreferencesJson = testJson;

            // Assert
            Assert.Equal(testId, userPreference.Id);
            Assert.Equal(testUserId, userPreference.UserId);
            Assert.Equal(testJson, userPreference.PreferencesJson);
        }

        [Fact]
        public void UserPreference_Preferences_ReturnsNewContainerWhenJsonIsNullOrEmpty()
        {
            // Arrange
            var userPreference1 = new UserPreference { PreferencesJson = null };
            var userPreference2 = new UserPreference { PreferencesJson = string.Empty };

            // Act
            var preferences1 = userPreference1.Preferences;
            var preferences2 = userPreference2.Preferences;

            // Assert
            Assert.NotNull(preferences1);
            Assert.Empty(preferences1.Pages);
            Assert.NotNull(preferences2);
            Assert.Empty(preferences2.Pages);
        }

        [Fact]
        public void UserPreference_Preferences_SetsPreferencesJsonCorrectly()
        {
            // Arrange
            var userPreference = new UserPreference();
            var container = new UserPreferenceContainer();
            container.Pages.Add("HomePage", new UserPagePreferences { Background = "Dark" });

            // Act
            userPreference.Preferences = container;

            // Assert
            Assert.NotNull(userPreference.PreferencesJson);
            var deserializedContainer = JsonSerializer.Deserialize<UserPreferenceContainer>(userPreference.PreferencesJson);
            Assert.NotNull(deserializedContainer);
            Assert.Single(deserializedContainer.Pages);
            Assert.Equal("Dark", deserializedContainer.Pages["HomePage"].Background);
        }

        [Fact]
        public void UserPreference_Preferences_GetsPreferencesJsonCorrectly()
        {
            // Arrange
            var userPreference = new UserPreference();
            var originalContainer = new UserPreferenceContainer();
            originalContainer.Pages.Add("SettingsPage", new UserPagePreferences { ColumnVisibility = { { "col1", true } } });
            userPreference.PreferencesJson = JsonSerializer.Serialize(originalContainer);

            // Act
            var retrievedPreferences = userPreference.Preferences;

            // Assert
            Assert.NotNull(retrievedPreferences);
            Assert.Single(retrievedPreferences.Pages);
            Assert.True(retrievedPreferences.Pages.ContainsKey("SettingsPage"));
            Assert.Single(retrievedPreferences.Pages["SettingsPage"].ColumnVisibility);
            Assert.True(retrievedPreferences.Pages["SettingsPage"].ColumnVisibility["col1"]);
        }

        [Fact]
        public void UserPreferenceContainer_CanBeInstantiatedAndHasDefaultValues()
        {
            // Arrange & Act
            var container = new UserPreferenceContainer();

            // Assert
            Assert.NotNull(container);
            Assert.NotNull(container.Pages);
            Assert.Empty(container.Pages);
        }

        [Fact]
        public void UserPagePreferences_CanBeInstantiatedAndHasDefaultValues()
        {
            // Arrange & Act
            var pagePreferences = new UserPagePreferences();

            // Assert
            Assert.NotNull(pagePreferences);
            Assert.NotNull(pagePreferences.ColumnVisibility);
            Assert.Empty(pagePreferences.ColumnVisibility);
            Assert.NotNull(pagePreferences.dialogueDataGridSettings);
            Assert.NotNull(pagePreferences.ProgrammationDataGridSettings);
            Assert.Equal("Light", pagePreferences.Background);
        }

        [Fact]
        public void UserPagePreferences_CanSetAndGetProperties()
        {
            // Arrange
            var pagePreferences = new UserPagePreferences();
            var colVisibility = new Dictionary<string, bool> { { "Name", true }, { "Age", false } };
            var dialogueSettings = new DataGridSettings { /* populate with test values if DataGridSettings has public properties */ };
            var progSettings = new DataGridSettings { /* populate with test values */ };
            var background = "Dark";

            // Act
            pagePreferences.ColumnVisibility = colVisibility;
            pagePreferences.dialogueDataGridSettings = dialogueSettings;
            pagePreferences.ProgrammationDataGridSettings = progSettings;
            pagePreferences.Background = background;

            // Assert
            Assert.Equal(colVisibility, pagePreferences.ColumnVisibility);
            Assert.Equal(dialogueSettings, pagePreferences.dialogueDataGridSettings);
            Assert.Equal(progSettings, pagePreferences.ProgrammationDataGridSettings);
            Assert.Equal(background, pagePreferences.Background);
        }

        [Fact]
        public void UserPreference_Preferences_HasNotMappedAttribute()
        {
            // Arrange
            var property = typeof(UserPreference).GetProperty("Preferences");

            // Assert
            Assert.NotNull(property);
            Assert.True(property.IsDefined(typeof(NotMappedAttribute), false));
        }
    }
}