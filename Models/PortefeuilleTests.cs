using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Nécessaire pour les annotations [Key], [ConcurrencyCheck]
using GestionBudgétaire.Data.Entities.Database;
using Xunit; // Le framework de test xUnit
using GestionBudgétaire.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema; // Nécessaire pour l'annotation [Table]

namespace GestionBudgetaireTest.Models
{

    /// Cette classe contient des tests unitaires pour le modèle de données 'Portefeuille'.
    /// L'objectif est de s'assurer que le modèle se comporte comme attendu,
    /// notamment en ce qui concerne l'instanciation, les valeurs par défaut des propriétés,
    /// la capacité à définir et récupérer des valeurs et la présence des annotations
    /// Entity Framework Core nécessaires pour le mapping base de données.
    /// Ces tests sont des 'unit tests' purs car ils ne nécessitent pas de base de données réelle
    /// ou en mémoire ; ils testent uniquement la logique et la structure du modèle lui-même.

    public class PortefeuilleTests
    {
        /// <summary>
        /// Teste si une instance du modèle Portefeuille peut être créée avec succès.
        /// </summary>
        [Fact] // L'attribut [Fact] indique à xUnit que cette méthode est un test à exécuter.
        public void Portefeuille_CanBeInstantiated()
        {
            // --- Phase Arrange & Act (Préparation et Exécution) ---
            // On crée simplement une nouvelle instance de la classe Portefeuille.
            // Il n'y a pas de configuration complexe ici, car le but est de vérifier
            // la constructibilité de base.
            var portefeuille = new Portefeuille();

            // --- Phase Assert (Vérification) ---
            // On affirme que l'objet 'portefeuille' n'est pas nul après son instanciation.
            // Si 'portefeuille' était nul, cela signifierait qu'il y a eu un problème
            // lors de la création de l'objet (par exemple, une exception dans le constructeur).
            Assert.NotNull(portefeuille);
        }

        /// <summary>
        /// Teste si les propriétés du modèle Portefeuille sont initialisées avec les valeurs par défaut
        /// définies dans la classe, ou avec leurs valeurs par défaut CLR si aucune n'est spécifiée.
        /// </summary>
        [Fact]
        public void Portefeuille_Properties_HaveCorrectDefaultValues()
        {
            // --- Phase Arrange & Act ---
            // On crée une nouvelle instance de Portefeuille. Les valeurs par défaut seront attribuées
            // automatiquement par le constructeur de la classe et les initialiseurs de propriétés.
            var portefeuille = new Portefeuille();

            // --- Phase Assert ---
            // On vérifie que chaque propriété a bien sa valeur par défaut attendue.
            // Pour les types numériques (int?), si une valeur n'est pas spécifiée, le '0' est souvent le défaut.
            // Pour les chaînes de caractères (string?), un 'string.Empty' est spécifié dans votre modèle.
            // Pour les booléens (bool?) et DateTime (DateTime?), la valeur par défaut est 'null' si elles sont nullables.
            Assert.Equal(0, portefeuille.GP_ID_GROUPE);
            Assert.Equal(string.Empty, portefeuille.LIBELLE);
            Assert.Equal(0, portefeuille.ID_VERSION);
            Assert.Equal(0, portefeuille.ID_PARENT);
            Assert.Equal(string.Empty, portefeuille.NIVEAU);
            Assert.Null(portefeuille.ACTIF); // 'null' car bool?
            Assert.Null(portefeuille.DATE_DE_MODIFICATION); // 'null' car DateTime?
            Assert.Null(portefeuille.DATE_DE_VALIDITE);     // 'null' car DateTime?

            // Test spécifique pour RowVersion :
            // La propriété RowVersion est initialisée avec DateTime.Now.
            // Tester une égalité exacte avec DateTime.Now juste après l'instanciation est risqué
            // à cause de la précision temporelle et du moment d'exécution du test.
            // Une approche plus robuste est de vérifier que la valeur n'est pas la valeur par défaut de DateTime (MinDate),
            // ce qui confirmerait qu'elle a bien été initialisée.
            Assert.NotEqual(default(DateTime), portefeuille.RowVersion); // default(DateTime) est 01/01/0001 00:00:00
        }

        /// <summary>
        /// Teste si les propriétés du modèle Portefeuille peuvent être correctement définies (set)
        /// et récupérées (get) avec des valeurs spécifiques.
        /// </summary>
        [Fact]
        public void Portefeuille_CanSetAndGetProperties()
        {
            // --- Phase Arrange ---
            // On initialise une instance de Portefeuille et une date de test qui sera utilisée
            // pour les propriétés de type DateTime.
            var portefeuille = new Portefeuille();
            var testDate = DateTime.Now;

            // --- Phase Act ---
            // On attribue des valeurs à toutes les propriétés du Portefeuille.
            portefeuille.ID_PORTEFEUILLE = 1;
            portefeuille.GP_ID_GROUPE = 10;
            portefeuille.LIBELLE = "Portefeuille Test";
            portefeuille.ID_VERSION = 2;
            portefeuille.ID_PARENT = 3;
            portefeuille.NIVEAU = "Niveau 1";
            portefeuille.ACTIF = true;
            portefeuille.DATE_DE_MODIFICATION = testDate;
            portefeuille.DATE_DE_VALIDITE = testDate.AddDays(1);
            portefeuille.RowVersion = testDate.AddHours(1); // On lui donne une valeur spécifique pour ce test

            // --- Phase Assert ---
            // On vérifie que les valeurs récupérées de chaque propriété correspondent
            // exactement aux valeurs que nous avons définies à l'étape "Act".
            Assert.Equal(1, portefeuille.ID_PORTEFEUILLE);
            Assert.Equal(10, portefeuille.GP_ID_GROUPE);
            Assert.Equal("Portefeuille Test", portefeuille.LIBELLE);
            Assert.Equal(2, portefeuille.ID_VERSION);
            Assert.Equal(3, portefeuille.ID_PARENT);
            Assert.Equal("Niveau 1", portefeuille.NIVEAU);
            Assert.True(portefeuille.ACTIF);
            Assert.Equal(testDate, portefeuille.DATE_DE_MODIFICATION);
            Assert.Equal(testDate.AddDays(1), portefeuille.DATE_DE_VALIDITE);
            Assert.Equal(testDate.AddHours(1), portefeuille.RowVersion);
        }

        /// <summary>
        /// Teste si la propriété 'ID_PORTEFEUILLE' du modèle Portefeuille possède l'annotation '[Key]'.
        /// Cette annotation est cruciale pour Entity Framework Core car elle désigne la clé primaire de l'entité
        /// et par conséquent, de la table correspondante dans la base de données.
        /// </summary>
        [Fact]
        public void Portefeuille_HasKeyAnnotationOnIdPortefeuille()
        {
            // --- Phase Arrange ---
            // On utilise la réflexion (System.Reflection) pour obtenir toutes les propriétés
            // de la classe Portefeuille.
            var properties = typeof(Portefeuille).GetProperties();
            // Ensuite, on cherche la propriété spécifique nommée "ID_PORTEFEUILLE".
            var idPortefeuilleProperty = properties.FirstOrDefault(p => p.Name == "ID_PORTEFEUILLE");

            // --- Phase Assert ---
            // 1. On s'assure que la propriété "ID_PORTEFEUILLE" existe bien dans la classe.
            Assert.NotNull(idPortefeuilleProperty);
            // 2. On vérifie que cette propriété est annotée avec l'attribut '[Key]'.
            // `IsDefined(typeof(KeyAttribute), false)` : vérifie si l'attribut KeyAttribute est appliqué.
            // Le 'false' indique de ne pas rechercher dans les classes de base.
            Assert.True(idPortefeuilleProperty.IsDefined(typeof(KeyAttribute), false));
        }

        /// <summary>
        /// Teste si la classe Portefeuille elle-même possède l'annotation '[Table("PORTEFEUILLES")]'.
        /// Cette annotation est utilisée par Entity Framework Core pour spécifier le nom de la table
        /// correspondante dans la base de données, si celui-ci diffère du nom de la classe.
        /// </summary>
        [Fact]
        public void Portefeuille_HasTableAnnotation()
        {
            // --- Phase Arrange ---
            // On utilise la réflexion pour obtenir tous les attributs personnalisés (annotations)
            // appliqués à la classe Portefeuille.
            // On filtre pour trouver l'attribut de type 'TableAttribute'.
            var tableAttribute = typeof(Portefeuille)
                .GetCustomAttributes(typeof(TableAttribute), false) // Récupère les attributs de type TableAttribute
                .FirstOrDefault() as TableAttribute; // Prend le premier et le caste en TableAttribute

            // --- Phase Assert ---
            // 1. On s'assure que l'attribut '[Table]' est bien présent sur la classe.
            Assert.NotNull(tableAttribute);
            // 2. On vérifie que le nom de la table spécifié dans l'annotation est bien "PORTEFEUILLES".
            Assert.Equal("PORTEFEUILLES", tableAttribute.Name);
        }

        /// <summary>
        /// Teste si la propriété 'RowVersion' du modèle Portefeuille possède l'annotation '[ConcurrencyCheck]'.
        /// Cette annotation est utilisée par Entity Framework Core pour implémenter le contrôle de concurrence optimiste.
        /// Quand cette annotation est présente, EF Core inclut la colonne dans les clauses WHERE
        /// des commandes UPDATE et DELETE pour s'assurer qu'aucune modification n'a eu lieu
        /// entre le moment où l'entité a été chargée et le moment où elle est mise à jour/supprimée.
        /// </summary>
        [Fact]
        public void Portefeuille_RowVersionHasConcurrencyCheckAnnotation()
        {
            // --- Phase Arrange ---
            // On obtient toutes les propriétés de la classe Portefeuille via la réflexion.
            var properties = typeof(Portefeuille).GetProperties();
            // On cherche la propriété spécifique nommée "RowVersion".
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            // --- Phase Assert ---
            // 1. On s'assure que la propriété "RowVersion" existe.
            Assert.NotNull(rowVersionProperty);
            // 2. On vérifie que cette propriété est annotée avec l'attribut '[ConcurrencyCheck]'.
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        /// <summary>
        /// Teste si le modèle Portefeuille implémente correctement l'interface 'IHasRowVersion'.
        /// L'implémentation d'interfaces est une bonne pratique pour promouvoir la réutilisabilité du code
        /// et permettre l'application de logiques génériques (par exemple, un service générique de persistance
        /// qui gère la 'RowVersion' pour toutes les entités implémentant cette interface).
        /// </summary>
        [Fact]
        public void Portefeuille_ImplementsIHasRowVersion()
        {
            // --- Phase Arrange & Act ---
            // On crée une instance de Portefeuille. Il n'y a pas d'action spécifique à réaliser sur l'objet
            // pour ce test, juste sa création.
            var portefeuille = new Portefeuille();

            // --- Phase Assert ---
            // On utilise `Assert.IsAssignableFrom<T>(object)` pour vérifier que l'objet 'portefeuille'
            // peut être assigné à une variable de type `IHasRowVersion`.
            // Cela confirme que la classe Portefeuille implémente bien cette interface.
            Assert.IsAssignableFrom<IHasRowVersion>(portefeuille);
        }
    }
}