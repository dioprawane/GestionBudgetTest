using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Nécessaire pour les annotations [Key], [ConcurrencyCheck], [Required]
using GestionBudgétaire.Data.Entities.Database; // Votre classe MacrosDesignations
using Xunit; // Le framework de test xUnit
using GestionBudgétaire.Data.Entities; // Pour IHasRowVersion (assurez-vous du namespace correct)
using System.ComponentModel.DataAnnotations.Schema; // Nécessaire pour les annotations [Table], [Column], [ForeignKey]

namespace GestionBudgetaireTest.Models
{
    /// <summary>
    /// Cette classe contient des tests unitaires pour le modèle de données 'MacrosDesignations'.
    /// L'objectif est de s'assurer que le modèle se comporte comme attendu,
    /// notamment en ce qui concerne l'instanciation, les valeurs par défaut des propriétés,
    /// la capacité à définir et récupérer des valeurs et la présence des annotations
    /// Entity Framework Core nécessaires pour le mapping base de données.
    /// Ces tests sont des 'unit tests' purs car ils ne nécessitent pas de base de données réelle
    /// ou en mémoire ; ils testent uniquement la logique et la structure du modèle lui-même.
    /// </summary>
    public class MacrosDesignationsTests
    {
        /// <summary>
        /// Teste si une instance du modèle MacrosDesignations peut être créée avec succès.
        /// </summary>
        [Fact] // L'attribut [Fact] indique à xUnit que cette méthode est un test à exécuter.
        public void MacrosDesignations_CanBeInstantiated()
        {
            // --- Phase Arrange & Act (Préparation et Exécution) ---
            // On crée simplement une nouvelle instance de la classe MacrosDesignations.
            // Il n'y a pas de configuration complexe ici, car le but est de vérifier
            // la constructibilité de base.
            var macroDesignation = new MacrosDesignations();

            // --- Phase Assert (Vérification) ---
            // On affirme que l'objet 'macroDesignation' n'est pas nul après son instanciation.
            Assert.NotNull(macroDesignation);
        }

        /// <summary>
        /// Teste si les propriétés du modèle MacrosDesignations sont initialisées avec les valeurs par défaut
        /// définies dans la classe, ou avec leurs valeurs par défaut CLR si aucune n'est spécifiée.
        /// </summary>
        [Fact]
        public void MacrosDesignations_Properties_HaveCorrectDefaultValues()
        {
            // --- Phase Arrange & Act ---
            // On crée une nouvelle instance de MacrosDesignations. Les valeurs par défaut seront attribuées
            // automatiquement par le constructeur de la classe et les initialiseurs de propriétés.
            var macroDesignation = new MacrosDesignations();

            // --- Phase Assert ---
            // On vérifie que chaque propriété a bien sa valeur par défaut attendue.
            Assert.Equal(0, macroDesignation.Id);
            Assert.Equal(0, macroDesignation.ID_CATEGORIE_SDSI);
            Assert.Null(macroDesignation.CategorieSDSI); // La propriété de navigation est null par défaut
            Assert.Equal(string.Empty, macroDesignation.CODE_MACRO_DESIGNATION);
            Assert.Equal(string.Empty, macroDesignation.MACRO_DESIGNATION);
            Assert.Equal(string.Empty, macroDesignation.PERIMETRE);
            Assert.Equal(0, macroDesignation.ID_LIBELLE);
            Assert.Equal(0, macroDesignation.ID_VERSION);

            // Pour les DateTime non-nullables sans initialiseur explicite, la valeur par défaut CLR est DateTime.MinValue.
            Assert.Equal(default(DateTime), macroDesignation.DATE_DE_MODIFICATION);
            Assert.Equal(default(DateTime), macroDesignation.DATE_DE_VALIDITE);

            Assert.True(macroDesignation.ACTIF); // Valeur par défaut explicite dans le modèle

            // Test spécifique pour RowVersion :
            // La propriété RowVersion est initialisée avec DateTime.Now.
            // On vérifie qu'elle n'est pas la valeur par défaut de DateTime (MinDate),
            // ce qui confirmerait qu'elle a bien été initialisée.
            Assert.NotEqual(default(DateTime), macroDesignation.RowVersion); // default(DateTime) est 01/01/0001 00:00:00
            Assert.True(macroDesignation.RowVersion <= DateTime.Now); // Vérifie qu'elle est à l'heure actuelle ou avant.
        }

        /// <summary>
        /// Teste si les propriétés du modèle MacrosDesignations peuvent être correctement définies (set)
        /// et récupérées (get) avec des valeurs spécifiques.
        /// </summary>
        [Fact]
        public void MacrosDesignations_CanSetAndGetProperties()
        {
            // --- Phase Arrange ---
            // On initialise une instance de MacrosDesignations et des dates de test.
            var macroDesignation = new MacrosDesignations();
            var testDateModif = new DateTime(2024, 5, 1, 9, 0, 0);
            var testDateValid = new DateTime(2024, 6, 1, 10, 0, 0);
            var testRowVersion = new DateTime(2024, 7, 1, 11, 0, 0);
            // Assurez-vous que CategorieSDSI a la propriété 'CATEGORIE_SDSI' ou 'Nom'
            // en fonction de la définition réelle de votre classe CategorieSDSI.
            // J'utilise CATEGORIE_SDSI ici pour correspondre à la ligne de code que vous avez fournie.
            var testCategorieSDSI = new CategorieSDSI { Id = 100, CATEGORIE_SDSI = "Catégorie Test" };

            // --- Phase Act ---
            // On attribue des valeurs à toutes les propriétés de MacrosDesignations.
            macroDesignation.Id = 10;
            macroDesignation.ID_CATEGORIE_SDSI = 100;
            macroDesignation.CategorieSDSI = testCategorieSDSI; // Affecte l'objet de navigation
            macroDesignation.CODE_MACRO_DESIGNATION = "CODE_MACRO_ABC";
            macroDesignation.MACRO_DESIGNATION = "Désignation Macro Test";
            macroDesignation.PERIMETRE = "Perimètre A";
            macroDesignation.ID_LIBELLE = 1;
            macroDesignation.ID_VERSION = 2;
            macroDesignation.DATE_DE_MODIFICATION = testDateModif;
            macroDesignation.DATE_DE_VALIDITE = testDateValid;
            macroDesignation.ACTIF = false;
            macroDesignation.RowVersion = testRowVersion;

            // --- Phase Assert ---
            // On vérifie que les valeurs récupérées de chaque propriété correspondent
            // exactement aux valeurs que nous avons définies à l'étape "Act".
            Assert.Equal(10, macroDesignation.Id);
            Assert.Equal(100, macroDesignation.ID_CATEGORIE_SDSI);
            Assert.Equal(testCategorieSDSI, macroDesignation.CategorieSDSI); // Vérifie la référence de l'objet
            // Assurez-vous que la propriété accédée est correcte pour votre CategorieSDSI
            Assert.Equal("Catégorie Test", macroDesignation.CategorieSDSI.CATEGORIE_SDSI);
            Assert.Equal("CODE_MACRO_ABC", macroDesignation.CODE_MACRO_DESIGNATION);
            Assert.Equal("Désignation Macro Test", macroDesignation.MACRO_DESIGNATION);
            Assert.Equal("Perimètre A", macroDesignation.PERIMETRE);
            Assert.Equal(1, macroDesignation.ID_LIBELLE);
            Assert.Equal(2, macroDesignation.ID_VERSION);
            Assert.Equal(testDateModif, macroDesignation.DATE_DE_MODIFICATION);
            Assert.Equal(testDateValid, macroDesignation.DATE_DE_VALIDITE);
            Assert.False(macroDesignation.ACTIF);
            Assert.Equal(testRowVersion, macroDesignation.RowVersion);
        }

        /// <summary>
        /// Teste si la classe MacrosDesignations elle-même possède l'annotation '[Table("MACRO_DESIGNATION")]'.
        /// </summary>
        [Fact]
        public void MacrosDesignations_HasTableAnnotation()
        {
            // --- Phase Arrange ---
            var tableAttribute = typeof(MacrosDesignations)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            // --- Phase Assert ---
            Assert.NotNull(tableAttribute);
            Assert.Equal("MACRO_DESIGNATION", tableAttribute.Name);
        }

        /// <summary>
        /// Teste si la propriété 'Id' (ID_MACRO_DESIGNATION) du modèle MacrosDesignations
        /// possède l'annotation '[Key]' et '[Column("ID_MACRO_DESIGNATION")]'.
        /// </summary>
        [Fact]
        public void MacrosDesignations_IdHasKeyAndColumnAnnotation()
        {
            // --- Phase Arrange ---
            var properties = typeof(MacrosDesignations).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");

            // --- Phase Assert ---
            Assert.NotNull(idProperty);
            Assert.True(idProperty.IsDefined(typeof(KeyAttribute), false));

            var columnAttribute = idProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("ID_MACRO_DESIGNATION", columnAttribute.Name);
        }

        /// <summary>
        /// Teste si la propriété 'ID_CATEGORIE_SDSI' possède l'annotation '[Column("ID_CATEGORIE_SDSI")]'
        /// et que la propriété de navigation 'CategorieSDSI' possède l'annotation '[ForeignKey(nameof(ID_CATEGORIE_SDSI))]'.
        /// </summary>
        [Fact]
        public void MacrosDesignations_IDCATEGORIESDSIHasColumnAndForeignKeyAnnotation()
        {
            // --- Phase Arrange ---
            var properties = typeof(MacrosDesignations).GetProperties();
            // Test de l'annotation [Column] sur la clé étrangère primitive
            var idCategorieSdsiProperty = properties.FirstOrDefault(p => p.Name == "ID_CATEGORIE_SDSI");

            // --- Phase Assert (Column Annotation) ---
            Assert.NotNull(idCategorieSdsiProperty);
            var columnAttribute = idCategorieSdsiProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
            Assert.NotNull(columnAttribute);
            Assert.Equal("ID_CATEGORIE_SDSI", columnAttribute.Name);

            // --- Phase Arrange (pour ForeignKey) ---
            // Test de l'annotation [ForeignKey] sur la PROPRIÉTÉ DE NAVIGATION
            var categorieSdsiNavigationProperty = properties.FirstOrDefault(p => p.Name == "CategorieSDSI");

            // --- Phase Assert (ForeignKey Annotation) ---
            Assert.NotNull(categorieSdsiNavigationProperty); // Cette ligne était la source de l'erreur précédente si le nom de la propriété était mal tapé
            var foreignKeyAttribute = categorieSdsiNavigationProperty.GetCustomAttributes(typeof(ForeignKeyAttribute), false).FirstOrDefault() as ForeignKeyAttribute;
            Assert.NotNull(foreignKeyAttribute); // La ligne 181 de votre erreur était ici, car foreignKeyAttribute était null.
            Assert.Equal(nameof(MacrosDesignations.ID_CATEGORIE_SDSI), foreignKeyAttribute.Name);
        }

        /// <summary>
        /// Teste si la propriété 'RowVersion' du modèle MacrosDesignations possède l'annotation '[ConcurrencyCheck]'.
        /// </summary>
        [Fact]
        public void MacrosDesignations_RowVersionHasConcurrencyCheckAnnotation()
        {
            // --- Phase Arrange ---
            var properties = typeof(MacrosDesignations).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            // --- Phase Assert ---
            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        /// <summary>
        /// Teste si le modèle MacrosDesignations implémente correctement l'interface 'IHasRowVersion'.
        /// </summary>
        [Fact]
        public void MacrosDesignations_ImplementsIHasRowVersion()
        {
            // --- Phase Assert ---
            Assert.True(typeof(IHasRowVersion).IsAssignableFrom(typeof(MacrosDesignations)));
        }

        // --- Tests de validation des propriétés (sans attributs [Required] explicites) ---

        /// <summary>
        /// Teste que les propriétés de type string non-nullables (CODE_MACRO_DESIGNATION, MACRO_DESIGNATION, PERIMETRE)
        /// sont correctement initialisées à string.Empty et acceptent des valeurs non vides.
        /// Comme elles n'ont pas d'attribut [Required] explicite (mais ont un initialiseur string.Empty),
        /// elles seront toujours considérées comme "valides" par Validator.TryValidateObject car elles ne sont jamais null.
        /// </summary>
        [Fact]
        public void MacrosDesignations_StringProperties_AreNotNullAndAllowEmptyString()
        {
            // --- Phase Arrange ---
            var macroDesignation = new MacrosDesignations();
            var validationContext = new ValidationContext(macroDesignation);
            var validationResults = new List<ValidationResult>();

            // --- Phase Act ---
            // L'objet est créé, les strings sont déjà string.Empty par défaut.
            var isValidDefault = Validator.TryValidateObject(macroDesignation, validationContext, validationResults, true);

            // --- Phase Assert (Valeurs par défaut) ---
            Assert.NotNull(macroDesignation.CODE_MACRO_DESIGNATION);
            Assert.Equal(string.Empty, macroDesignation.CODE_MACRO_DESIGNATION);
            Assert.NotNull(macroDesignation.MACRO_DESIGNATION);
            Assert.Equal(string.Empty, macroDesignation.MACRO_DESIGNATION);
            Assert.NotNull(macroDesignation.PERIMETRE);
            Assert.Equal(string.Empty, macroDesignation.PERIMETRE);

            // Vérifie que l'objet est valide même avec des chaînes vides
            Assert.True(isValidDefault);
            Assert.Empty(validationResults);

            // --- Phase Act (Définition de valeurs) ---
            macroDesignation.CODE_MACRO_DESIGNATION = "ABC";
            macroDesignation.MACRO_DESIGNATION = "XYZ";
            macroDesignation.PERIMETRE = "DEF";
            validationResults.Clear(); // Nettoie les résultats de validation précédents
            var isValidSet = Validator.TryValidateObject(macroDesignation, validationContext, validationResults, true);

            // --- Phase Assert (Valeurs définies) ---
            Assert.Equal("ABC", macroDesignation.CODE_MACRO_DESIGNATION);
            Assert.Equal("XYZ", macroDesignation.MACRO_DESIGNATION);
            Assert.Equal("DEF", macroDesignation.PERIMETRE);
            Assert.True(isValidSet);
            Assert.Empty(validationResults);
        }

        /// <summary>
        /// Teste que les propriétés DateTime non-nullables (DATE_DE_MODIFICATION, DATE_DE_VALIDITE)
        /// sont considérées comme valides par Validator.TryValidateObject même si elles ont la valeur par défaut CLR (DateTime.MinValue),
        /// car elles ne sont pas marquées avec [Required].
        /// </summary>
        [Fact]
        public void MacrosDesignations_DateProperties_AreValidWithDefaultValues()
        {
            // --- Phase Arrange ---
            var macroDesignation = new MacrosDesignations
            {
                // Ces propriétés auront DateTime.MinValue par défaut à l'instanciation
                DATE_DE_MODIFICATION = default(DateTime),
                DATE_DE_VALIDITE = default(DateTime)
            };
            var validationContext = new ValidationContext(macroDesignation);
            var validationResults = new List<ValidationResult>();

            // --- Phase Act ---
            var isValid = Validator.TryValidateObject(macroDesignation, validationContext, validationResults, true);

            // --- Phase Assert ---
            // On s'attend à ce que l'objet soit valide car les propriétés DateTime ne sont pas nullables
            // et ne sont pas marquées [Required]. Validator.TryValidateObject ne considère pas DateTime.MinValue comme invalide.
            Assert.True(isValid);
            Assert.Empty(validationResults);

            // Test avec des dates valides pour s'assurer que cela fonctionne aussi
            macroDesignation.DATE_DE_MODIFICATION = DateTime.Now;
            macroDesignation.DATE_DE_VALIDITE = DateTime.Now.AddDays(1);
            validationResults.Clear();
            isValid = Validator.TryValidateObject(macroDesignation, validationContext, validationResults, true);
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        /// <summary>
        /// Teste que la propriété bool (ACTIF) est initialisée correctement et peut être modifiée.
        /// Comme elle n'est pas nullable et n'a pas [Required], elle est toujours valide.
        /// </summary>
        [Fact]
        public void MacrosDesignations_ActifProperty_WorksCorrectly()
        {
            // --- Phase Arrange ---
            var macroDesignation = new MacrosDesignations();

            // --- Phase Assert (Valeur par défaut) ---
            Assert.True(macroDesignation.ACTIF); // Vérifie la valeur par défaut

            // --- Phase Act ---
            macroDesignation.ACTIF = false;

            // --- Phase Assert (Valeur modifiée) ---
            Assert.False(macroDesignation.ACTIF);

            // Vérifier la validation pour s'assurer qu'elle est toujours valide
            var validationContext = new ValidationContext(macroDesignation);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(macroDesignation, validationContext, validationResults, true);
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        // Les propriétés int (ID_CATEGORIE_SDSI, ID_LIBELLE, ID_VERSION) sont non-nullables
        // et n'ont pas d'attribut [Required] explicite. Par défaut, Validator.TryValidateObject
        // considérera 0 comme une valeur valide pour ces champs. Si vous voulez interdire
        // la valeur 0 ou d'autres plages, vous devrez ajouter des attributs [Range] ou
        // implémenter une validation personnalisée dans votre modèle.
        // Aucun test spécifique pour la validation de ces 'int' n'est ajouté ici pour l'instant,
        // car le modèle ne contient pas d'annotations de validation pour ces cas.
    }
}