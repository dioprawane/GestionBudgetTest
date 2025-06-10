using Xunit;
using GestionBudgétaire.Data.Entities.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema; // Assurez-vous que cette using est présente
using GestionBudgétaire.Data.Entities;

namespace GestionBudgetaireTest.Models
{
    /// <summary>
    /// Cette classe contient des tests unitaires pour le modèle de données 'Dialogue'.
    /// Elle vise à garantir la robustesse du modèle en vérifiant :
    /// - L'instanciation correcte de l'objet.
    /// - L'initialisation des propriétés avec leurs valeurs par défaut.
    /// - La capacité à définir et récupérer des valeurs pour toutes les propriétés.
    /// - La présence des annotations Entity Framework Core cruciales ([Key], [Table], [ConcurrencyCheck]).
    /// - L'implémentation correcte de l'interface IHasRowVersion.
    /// </summary>
    public class DialogueTests
    {
        /// <summary>
        /// Teste la capacité à instancier un objet Dialogue.
        /// </summary>
        /// <remarks>
        /// Utilité : C'est le test le plus basique. Il s'assure que le constructeur de la classe
        /// n'échoue pas et qu'un objet peut être créé avec succès. Un échec ici indiquerait
        /// un problème fondamental dans la définition de la classe.
        /// Fonctionnement : Crée une nouvelle instance de 'Dialogue' et utilise `Assert.NotNull`
        /// pour vérifier que l'objet créé n'est pas nul.
        /// </remarks>
        [Fact]
        public void Dialogue_CanBeInstantiated()
        {
            // Arrange & Act
            var dialogue = new Dialogue();

            // Assert
            Assert.NotNull(dialogue);
        }

        /// <summary>
        /// Teste si toutes les propriétés du modèle Dialogue sont initialisées avec leurs valeurs par défaut
        /// (soit celles spécifiées explicitement dans le modèle, soit les valeurs par défaut CLR).
        /// </summary>
        /// <remarks>
        /// Utilité : Garantit que les propriétés ont un état initial prévisible. C'est important
        /// pour éviter les valeurs inattendues si une propriété n'est pas explicitement définie
        /// lors de la création d'une nouvelle instance. Pour les chaînes et les décimals,
        /// cela confirme que les 'string.Empty' et '0' sont bien appliqués.
        /// Fonctionnement : Crée un 'Dialogue' et utilise `Assert.Equal` ou `Assert.Null`
        /// pour comparer chaque propriété à sa valeur par défaut attendue. Pour 'RowVersion',
        /// on vérifie qu'elle n'est pas la valeur par défaut de DateTime (MinDate),
        /// car elle est initialisée avec DateTime.Now.
        /// </remarks>
        [Fact]
        public void Dialogue_Properties_HaveCorrectDefaultValues()
        {
            // Arrange & Act
            var dialogue = new Dialogue();

            // Assert
            Assert.Equal(0, dialogue.ANNEE);
            Assert.Equal(string.Empty, dialogue.SERIES_DE_DONNEES);
            Assert.Null(dialogue.DG_DATE); // DateTime? a pour défaut null
            Assert.Equal(string.Empty, dialogue.SERIE_DATE);
            Assert.Equal(0, dialogue.LINE_TO_USE);
            Assert.Equal(string.Empty, dialogue.MACRO_DESIGNATION);
            Assert.Equal(string.Empty, dialogue.MACRO_DESIGNATION_ORIGINAL);
            Assert.Equal(string.Empty, dialogue.MACRO_DESIGNATION_HDC_TELEPH);
            Assert.Equal(string.Empty, dialogue.CATEGORIE_SDSI);
            Assert.Equal(string.Empty, dialogue.SOUS_CATEGORIE_SDSI);
            Assert.Equal(string.Empty, dialogue.NATURE_DU_BESOIN);
            Assert.Equal(string.Empty, dialogue.CADRE_D_ACHAT);
            Assert.Equal(string.Empty, dialogue.FOURNISSEUR);
            Assert.Equal(string.Empty, dialogue.LIBELLE_DU_MARCHE);
            Assert.Equal(string.Empty, dialogue.MASSE_BUDGETAIRE);
            Assert.Equal(string.Empty, dialogue.BASE_LINE_INITIATIVE);
            Assert.Equal(string.Empty, dialogue.DA);
            Assert.Equal(string.Empty, dialogue.AXE_NATIONAL_1);
            Assert.Equal(string.Empty, dialogue.CTRL_CODE_INITIATIVE_SELON_G2PI);
            Assert.Equal(string.Empty, dialogue.CODE_INITIATIVE_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.LIBELLE_INITIATIVE);
            Assert.Equal(string.Empty, dialogue.LIBELLE_ORIENTATION);
            Assert.Equal(string.Empty, dialogue.COMMENTAIRES_INITIATIVES_SDSI);
            Assert.Equal(string.Empty, dialogue.CODE_PROJET_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.LIBELLE_PROJET);
            Assert.Equal(string.Empty, dialogue.STATUT_PROJET);
            Assert.Equal(string.Empty, dialogue.COMPTES_BUDGETAIRES);
            Assert.Equal(string.Empty, dialogue.COMPTES_COMPTABLES);
            Assert.Equal(string.Empty, dialogue.DA_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.PORTEFEUILLE_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.SOUS_PORTEFEUILLE_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.NEW_SOUS_PORTEFEUILLE_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.CARACTERISTIQUE_DU_BESOIN_STANDARDISE);
            Assert.Equal(string.Empty, dialogue.CRITICITE_STANDARDISE);
            Assert.Equal(0m, dialogue.MONTANT_TTC); // 0m pour decimal
            Assert.Equal(0m, dialogue.DEMANDE_DE_REPORT_IDENTIFIE);
            Assert.Equal(0m, dialogue.BUDGET_AUTORISE);
            Assert.Equal(string.Empty, dialogue.COMMENTAIRES);
            Assert.Equal(string.Empty, dialogue.SOUS_SOUS_PORTEFEUILLE);
            Assert.Equal(string.Empty, dialogue.FICHIER_SOURCE);
            Assert.Equal(string.Empty, dialogue.PRIORITE_BPA);
            Assert.Equal(string.Empty, dialogue.PREVISION_ACTUALISEE_VS_SDSI);
            Assert.Equal(string.Empty, dialogue.CATEGORIE_SDSI_FORMULE);
            Assert.Equal(string.Empty, dialogue.SOUS_CATEGORIE_SDSI_FORMULE);
            Assert.Equal(string.Empty, dialogue.MOTIFS_DR_BC);
            Assert.Equal(string.Empty, dialogue.COMMENTAIRES_DR_BC);
            Assert.NotEqual(default(DateTime), dialogue.RowVersion);
        }

        /// <summary>
        /// Teste la capacité à définir des valeurs sur toutes les propriétés du modèle Dialogue
        /// et à les récupérer correctement.
        /// </summary>
        /// <remarks>
        /// Utilité : Valide que les "getters" et "setters" de toutes les propriétés fonctionnent
        /// comme prévu. C'est essentiel pour s'assurer que les données peuvent être lues et écrites
        /// dans l'objet sans corruption.
        /// Fonctionnement : Crée une instance de 'Dialogue', attribue des valeurs de test à chaque propriété,
        /// puis utilise `Assert.Equal` pour vérifier que les valeurs récupérées correspondent
        /// aux valeurs définies.
        /// </remarks>
        [Fact]
        public void Dialogue_CanSetAndGetProperties()
        {
            // Arrange
            var dialogue = new Dialogue();
            var testDate = new DateTime(2025, 6, 5, 10, 30, 0); // Date fixe pour la reproductibilité

            // Act
            dialogue.DG_CODE = 101;
            dialogue.ANNEE = 2024;
            dialogue.SERIES_DE_DONNEES = "Données A";
            dialogue.DG_DATE = testDate;
            dialogue.SERIE_DATE = "2024-01-01";
            dialogue.LINE_TO_USE = 5;
            dialogue.MACRO_DESIGNATION = "Designation Test";
            dialogue.MACRO_DESIGNATION_ORIGINAL = "Original Test";
            dialogue.MACRO_DESIGNATION_HDC_TELEPH = "HDC Teleph Test";
            dialogue.CATEGORIE_SDSI = "Catégorie Test";
            dialogue.SOUS_CATEGORIE_SDSI = "Sous-Catégorie Test";
            dialogue.NATURE_DU_BESOIN = "Besoin Test";
            dialogue.CADRE_D_ACHAT = "Cadre Test";
            dialogue.FOURNISSEUR = "Fournisseur Test";
            dialogue.LIBELLE_DU_MARCHE = "Marché Test";
            dialogue.MASSE_BUDGETAIRE = "Masse Test";
            dialogue.BASE_LINE_INITIATIVE = "Base Line Test";
            dialogue.DA = "DA Test";
            dialogue.AXE_NATIONAL_1 = "Axe Test";
            dialogue.CTRL_CODE_INITIATIVE_SELON_G2PI = "CTRL Code Test";
            dialogue.CODE_INITIATIVE_STANDARDISE = "Code Init Test";
            dialogue.LIBELLE_INITIATIVE = "Libelle Init Test";
            dialogue.LIBELLE_ORIENTATION = "Libelle Orient Test";
            dialogue.COMMENTAIRES_INITIATIVES_SDSI = "Commentaires Init Test";
            dialogue.CODE_PROJET_STANDARDISE = "Code Projet Test";
            dialogue.LIBELLE_PROJET = "Libelle Projet Test";
            dialogue.STATUT_PROJET = "Statut Test";
            dialogue.COMPTES_BUDGETAIRES = "Comptes Budget Test";
            dialogue.COMPTES_COMPTABLES = "Comptes Comptable Test";
            dialogue.DA_STANDARDISE = "DA Std Test";
            dialogue.PORTEFEUILLE_STANDARDISE = "Portefeuille Std Test";
            dialogue.SOUS_PORTEFEUILLE_STANDARDISE = "Sous Portefeuille Std Test";
            dialogue.NEW_SOUS_PORTEFEUILLE_STANDARDISE = "New Sous Portefeuille Std Test";
            dialogue.CARACTERISTIQUE_DU_BESOIN_STANDARDISE = "Caracteristique Besoin Std Test";
            dialogue.CRITICITE_STANDARDISE = "Criticite Std Test";
            dialogue.MONTANT_TTC = 123.45m;
            dialogue.DEMANDE_DE_REPORT_IDENTIFIE = 67.89m;
            dialogue.BUDGET_AUTORISE = 100.00m;
            dialogue.COMMENTAIRES = "Commentaires Test";
            dialogue.SOUS_SOUS_PORTEFEUILLE = "Sous Sous Portefeuille Test";
            dialogue.FICHIER_SOURCE = "Fichier Source Test";
            dialogue.PRIORITE_BPA = "Priorite BPA Test";
            dialogue.PREVISION_ACTUALISEE_VS_SDSI = "Prevision Actualisee Test";
            dialogue.CATEGORIE_SDSI_FORMULE = "Categorie SDSI Formule Test";
            dialogue.SOUS_CATEGORIE_SDSI_FORMULE = "Sous Categorie SDSI Formule Test";
            dialogue.MOTIFS_DR_BC = "Motifs DR BC Test";
            dialogue.COMMENTAIRES_DR_BC = "Commentaires DR BC Test";
            dialogue.RowVersion = testDate.AddHours(1);

            // Assert
            Assert.Equal(101, dialogue.DG_CODE);
            Assert.Equal(2024, dialogue.ANNEE);
            Assert.Equal("Données A", dialogue.SERIES_DE_DONNEES);
            Assert.Equal(testDate, dialogue.DG_DATE);
            Assert.Equal("2024-01-01", dialogue.SERIE_DATE);
            Assert.Equal(5, dialogue.LINE_TO_USE);
            Assert.Equal("Designation Test", dialogue.MACRO_DESIGNATION);
            Assert.Equal("Original Test", dialogue.MACRO_DESIGNATION_ORIGINAL);
            Assert.Equal("HDC Teleph Test", dialogue.MACRO_DESIGNATION_HDC_TELEPH);
            Assert.Equal("Catégorie Test", dialogue.CATEGORIE_SDSI);
            Assert.Equal("Sous-Catégorie Test", dialogue.SOUS_CATEGORIE_SDSI);
            Assert.Equal("Besoin Test", dialogue.NATURE_DU_BESOIN);
            Assert.Equal("Cadre Test", dialogue.CADRE_D_ACHAT);
            Assert.Equal("Fournisseur Test", dialogue.FOURNISSEUR);
            Assert.Equal("Marché Test", dialogue.LIBELLE_DU_MARCHE);
            Assert.Equal("Masse Test", dialogue.MASSE_BUDGETAIRE);
            Assert.Equal("Base Line Test", dialogue.BASE_LINE_INITIATIVE);
            Assert.Equal("DA Test", dialogue.DA);
            Assert.Equal("Axe Test", dialogue.AXE_NATIONAL_1);
            Assert.Equal("CTRL Code Test", dialogue.CTRL_CODE_INITIATIVE_SELON_G2PI);
            Assert.Equal("Code Init Test", dialogue.CODE_INITIATIVE_STANDARDISE);
            Assert.Equal("Libelle Init Test", dialogue.LIBELLE_INITIATIVE);
            Assert.Equal("Libelle Orient Test", dialogue.LIBELLE_ORIENTATION);
            Assert.Equal("Commentaires Init Test", dialogue.COMMENTAIRES_INITIATIVES_SDSI);
            Assert.Equal("Code Projet Test", dialogue.CODE_PROJET_STANDARDISE);
            Assert.Equal("Libelle Projet Test", dialogue.LIBELLE_PROJET);
            Assert.Equal("Statut Test", dialogue.STATUT_PROJET);
            Assert.Equal("Comptes Budget Test", dialogue.COMPTES_BUDGETAIRES);
            Assert.Equal("Comptes Comptable Test", dialogue.COMPTES_COMPTABLES);
            Assert.Equal("DA Std Test", dialogue.DA_STANDARDISE);
            Assert.Equal("Portefeuille Std Test", dialogue.PORTEFEUILLE_STANDARDISE);
            Assert.Equal("Sous Portefeuille Std Test", dialogue.SOUS_PORTEFEUILLE_STANDARDISE);
            Assert.Equal("New Sous Portefeuille Std Test", dialogue.NEW_SOUS_PORTEFEUILLE_STANDARDISE);
            Assert.Equal("Caracteristique Besoin Std Test", dialogue.CARACTERISTIQUE_DU_BESOIN_STANDARDISE);
            Assert.Equal("Criticite Std Test", dialogue.CRITICITE_STANDARDISE);
            Assert.Equal(123.45m, dialogue.MONTANT_TTC);
            Assert.Equal(67.89m, dialogue.DEMANDE_DE_REPORT_IDENTIFIE);
            Assert.Equal(100.00m, dialogue.BUDGET_AUTORISE);
            Assert.Equal("Commentaires Test", dialogue.COMMENTAIRES);
            Assert.Equal("Sous Sous Portefeuille Test", dialogue.SOUS_SOUS_PORTEFEUILLE);
            Assert.Equal("Fichier Source Test", dialogue.FICHIER_SOURCE);
            Assert.Equal("Priorite BPA Test", dialogue.PRIORITE_BPA);
            Assert.Equal("Prevision Actualisee Test", dialogue.PREVISION_ACTUALISEE_VS_SDSI);
            Assert.Equal("Categorie SDSI Formule Test", dialogue.CATEGORIE_SDSI_FORMULE);
            Assert.Equal("Sous Categorie SDSI Formule Test", dialogue.SOUS_CATEGORIE_SDSI_FORMULE);
            Assert.Equal("Motifs DR BC Test", dialogue.MOTIFS_DR_BC);
            Assert.Equal("Commentaires DR BC Test", dialogue.COMMENTAIRES_DR_BC);
            Assert.Equal(testDate.AddHours(1), dialogue.RowVersion);
        }

        /// <summary>
        /// Teste si la propriété 'DG_CODE' du modèle Dialogue possède l'annotation '[Key]'.
        /// </summary>
        /// <remarks>
        /// Utilité : C'est un test fondamental pour Entity Framework Core. Il assure que la clé primaire
        /// de l'entité est correctement identifiée, ce qui est essentiel pour toutes les opérations
        /// de base de données (insertion, mise à jour, suppression, récupération par ID).
        /// Fonctionnement : Utilise la réflexion pour obtenir la propriété 'DG_CODE' et vérifie
        /// qu'elle est décorée avec le `KeyAttribute`.
        /// </remarks>
        [Fact]
        public void Dialogue_HasKeyAnnotationOnDgCode()
        {
            // Arrange
            var properties = typeof(Dialogue).GetProperties();
            var dgCodeProperty = properties.FirstOrDefault(p => p.Name == "DG_CODE");

            // Assert
            Assert.NotNull(dgCodeProperty);
            Assert.True(dgCodeProperty.IsDefined(typeof(KeyAttribute), false));
        }

        /// <summary>
        /// Teste si la classe Dialogue elle-même possède l'annotation '[Table("DIALOGUE_GESTION")]'.
        /// </summary>
        /// <remarks>
        /// Utilité : Confirme que le modèle est correctement mappé à la table "DIALOGUE_GESTION"
        /// dans la base de données. Si cette annotation est absente ou incorrecte, EF Core
        /// utilisera le nom de la classe par défaut ("Dialogues"), ce qui pourrait entraîner
        /// des erreurs si le nom de la table réelle est différent.
        /// Fonctionnement : Récupère les attributs de la classe 'Dialogue' et vérifie
        /// la présence et le nom du `TableAttribute`.
        /// </remarks>
        [Fact]
        public void Dialogue_HasTableAnnotation()
        {
            // Arrange
            var tableAttribute = typeof(Dialogue)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .FirstOrDefault() as TableAttribute;

            // Assert
            Assert.NotNull(tableAttribute);
            Assert.Equal("DIALOGUE_GESTION", tableAttribute.Name);
        }

        /// <summary>
        /// Teste si la propriété 'RowVersion' du modèle Dialogue possède l'annotation '[ConcurrencyCheck]'.
        /// </summary>
        /// <remarks>
        /// Utilité : Cette annotation est vitale pour la gestion de la concurrence optimiste avec EF Core.
        /// Elle assure que si deux utilisateurs tentent de modifier la même entité simultanément,
        /// seule la première modification réussira et la seconde sera rejetée, empêchant ainsi
        /// la perte de données due à des écrasements involontaires.
        /// Fonctionnement : Utilise la réflexion pour trouver la propriété 'RowVersion' et vérifie
        /// qu'elle est décorée avec le `ConcurrencyCheckAttribute`.
        /// </remarks>
        [Fact]
        public void Dialogue_RowVersionHasConcurrencyCheckAnnotation()
        {
            // Arrange
            var properties = typeof(Dialogue).GetProperties();
            var rowVersionProperty = properties.FirstOrDefault(p => p.Name == "RowVersion");

            // Assert
            Assert.NotNull(rowVersionProperty);
            Assert.True(rowVersionProperty.IsDefined(typeof(ConcurrencyCheckAttribute), false));
        }

        /// <summary>
        /// Teste si le modèle Dialogue implémente correctement l'interface 'IHasRowVersion'.
        /// </summary>
        /// <remarks>
        /// Utilité : Si vous avez une interface comme `IHasRowVersion` pour regrouper des fonctionnalités
        /// communes (comme la gestion d'une version de ligne pour la concurrence), ce test garantit
        /// que votre modèle adhère à ce contrat. Cela permet d'écrire du code générique qui peut
        /// opérer sur n'importe quelle entité implémentant `IHasRowVersion`.
        /// Fonctionnement : Crée une instance de 'Dialogue' et utilise `Assert.IsAssignableFrom`
        /// pour confirmer qu'elle peut être assignée à une variable de type `IHasRowVersion`.
        /// </remarks>
        [Fact]
        public void Dialogue_ImplementsIHasRowVersion()
        {
            // Arrange & Act
            var dialogue = new Dialogue();

            // Assert
            Assert.IsAssignableFrom<IHasRowVersion>(dialogue);
        }
    }
}