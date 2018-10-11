using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm_GeneticSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace MainUnitTestProject
{
    [TestClass]
    public class MorphAnalysisTests
    {
        ConverterToFromChromosome converter = new ConverterToFromChromosome();

        [TestMethod]
        public void GetSizeBlock_Test()
        {
            //arrange
            int countSol = 8;

            int expected = 4;

            //act
           
            int result = converter.GetSizeBlock(countSol);


            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSizeChromosome_Test()
        {
            //arrange
            int countSol = 8;
            int countFunc = 4;

            int expected = 4 * countFunc;

            //act
            int result = converter.GetSizeChromosome(countSol, countFunc);


            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertFromChromosome_Test()
        {
            //arrange
            string str = "110010101";
            int countFunc = 3;
            int expected = 13;

            //act
            int result = converter.ConvertFromChromosome(str, countFunc).Sum();

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetIdSolutions_Test()
        {
            //arrange
            Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
            dict.Add(5, 0.1m);
            dict.Add(14, 2.3m);
            dict.Add(3, 7.1m);

            int expected = 5 + 14 + 3;

            //act

            int result = converter.GetIdSolutions(new[] { 2, 0, 1 }, dict).Sum();

            //assert
            Assert.AreEqual(expected, result);
        }


        #region Стара версія хромосоми "-f1-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.-f2-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16."
        /*
        //Тестуємо отримання псевдо хромосоми
        [TestMethod]
        public void BuildChromosomeString_Test()
        {
            //arrange
            string[] funcsId = new string[] { "f1", "f2" };
            string[] solsId = new string[] { "s1", "s169", "s4" };
            string[] modsId = new string[] { "m1", "m3", "m16" };

            //-f1-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.-f2-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.
            string expected = "-f1-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.-f2-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.";

            //act
            
            ConverterToFromChromosome c = new ConverterToFromChromosome();
            string result = c.BuildChromosomeString(funcsId, solsId, modsId);

            //assert
            Assert.AreEqual(expected, result);
        }

        //[TestMethod]
        //public void SplitString_Test()
        //{
        //    //arrange
        //    string s = "f1s4m1m16m3s9m1m16m3f2s4m1m16m3s9m1m16m3";
        //
        //    //f1s4m1m16m3s9m1m16m3f2s4m1m16m3s9m1m16m3
        //    string[] expected = new string[] { "f1s4m1m16m3s9m1m16m3","f2s4m1m16m3s9m1m16m3" };
        //
        //    //act
        //
        //    ConverterToFromChromosome c = new ConverterToFromChromosome();
        //    string[] result = c.SplitString(s);
        //
        //    //assert
        //    Assert.AreEqual(expected, result);
        //}

        [TestMethod]
        public void ParseChromosome_Test()
        {
            //arrange
            string[] strList = new string[] { "f1", "m16", "s4", "f3" };
            List<string> expectedList = new List<string>();
            expectedList.Add("f1");
            expectedList.Add("f3");
            string expected = expectedList[0] + expectedList[1];

            //act
            ConverterToFromChromosome c = new ConverterToFromChromosome();
            List<string> resultList = c.ParseChromosome(strList);
            string result = "";
            foreach (var item in resultList)
            {
                result += item;
            }


            //assert
            Assert.AreEqual(expected, result);
        }
        */
        #endregion

    }
}
