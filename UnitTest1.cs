using System.ComponentModel;

namespace GestionBudgetaireTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var service = new AddingNewEventArgs();
            var result = 4 + 4;
            Assert.Equal(8, result);
        }

        [Fact]
        public void Test2()
        {
            var service = new AddingNewEventArgs();
            var result = 3+4;
            Assert.Equal(7, result);
        }
    }
}