using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm_GeneticSharpLib;

namespace MainUnitTestProject
{
    [TestClass]
    public class MorphAnalysisTests
    {
        //Тестуємо отримання псевдо хромосоми
        [TestMethod]
        public void BuildChromosomeString_Test()
        {
            //arrange
            string[] funcsId = new string[] { "f1", "f2" };
            string[] solsId = new string[] { "s4", "s9" };
            string[] modsId = new string[] { "m1", "m16", "m3" };

            //f1s4m1m16m3s9m1m16m3f2s4m1m16m3s9m1m16m3
            string expected = "f1s4m1m16m3s9m1m16m3f2s4m1m16m3s9m1m16m3";

            //act
            
            ConverterToFromChromosome c = new ConverterToFromChromosome();
            string result = c.BuildChromosomeString(funcsId, solsId, modsId);

            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
