using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm_GeneticSharpLib;
using System.Collections.Generic;

namespace MainUnitTestProject
{
    [TestClass]
    public class MorphAnalysisTests
    {
        


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
