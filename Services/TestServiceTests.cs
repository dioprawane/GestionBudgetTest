using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionBudgétaire.Data.Entities.Database;
using GestionBudgétaire.Data.Services.ToBeRemoved;
using GestionBudgétaire.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionBudgetaireTest.Services
{
    /*internal class TestServiceTests
    {
    }*/
    public class TestServiceTests
    {
        private async Task<ApplicationDbContext> GetInMemoryDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            var context = new ApplicationDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task AddDialogueAsync_ShouldAddDialogue()
        {
            var context = await GetInMemoryDbContextAsync();
            var service = new TestService(context);

            var dialogue = new Dialogue { DG_CODE = 1, COMMENTAIRES = "Test Libelle" };
            await service.AddDialogueAsync(dialogue);

            var result = await context.Dialogue!.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Test Libelle", result!.COMMENTAIRES);
        }

        [Fact]
        public async Task GetDialoguesAsync_ShouldReturnOrderedList()
        {
            var context = await GetInMemoryDbContextAsync();
            context.Dialogue!.AddRange(
                new Dialogue { DG_CODE = 2, COMMENTAIRES = "Deuxième" },
                new Dialogue { DG_CODE = 1, COMMENTAIRES = "Premier" }
            );
            await context.SaveChangesAsync();

            var service = new TestService(context);
            var result = await service.GetDialoguesAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.First().DG_CODE);
        }

        [Fact]
        public async Task GetDialogueByIdAsync_ShouldReturnCorrectDialogue()
        {
            var context = await GetInMemoryDbContextAsync();
            var dialogue = new Dialogue { DG_CODE = 3, COMMENTAIRES = "Dialogue à trouver" };
            context.Dialogue!.Add(dialogue);
            await context.SaveChangesAsync();

            var service = new TestService(context);
            var result = await service.GetDialogueByIdAsync(3);

            Assert.NotNull(result);
            Assert.Equal("Dialogue à trouver", result!.COMMENTAIRES);
        }

        [Fact]
        public async Task UpdateDialogueAsync_ShouldUpdateDialogue()
        {
            var context = await GetInMemoryDbContextAsync();
            var dialogue = new Dialogue { DG_CODE = 4, COMMENTAIRES = "Ancien Libelle" };
            context.Dialogue!.Add(dialogue);
            await context.SaveChangesAsync();

            var service = new TestService(context);
            dialogue.COMMENTAIRES = "Nouveau Libelle";
            await service.UpdateDialogueAsync(dialogue);

            var updated = await context.Dialogue.FindAsync(4);
            Assert.Equal("Nouveau Libelle", updated!.COMMENTAIRES);
        }

        [Fact]
        public async Task DeleteDialogueAsync_ShouldRemoveDialogue()
        {
            var context = await GetInMemoryDbContextAsync();
            var dialogue = new Dialogue { DG_CODE = 5, COMMENTAIRES = "À supprimer" };
            context.Dialogue!.Add(dialogue);
            await context.SaveChangesAsync();

            var service = new TestService(context);
            await service.DeleteDialogueAsync(5);

            var deleted = await context.Dialogue.FindAsync(5);
            Assert.Null(deleted);
        }
    }
}