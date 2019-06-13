using IntersectionDemo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IntersectionDemo.Tests
{
    [TestClass]
    public class IntersectionTests
    {
        int COMPARISON_LOOP = 20;
        string colorsCsv, namesCsv, expectedResult;

        string[][] colorsArray, namesArray;

        [TestInitialize]
        public void Initialize()
        {
            colorsCsv = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/array1.csv");
            namesCsv = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/array2.csv");

            // ==> array1.csv x array2.csv
            expectedResult = @"0,Bisque,Xander
46,Violet,Germaine
56,Lime,Silas
66,Dark Goldenrod,Kibo
76,Light Green,Jeanette
126,Sienna,Odette
136,Light Sea Green,Dillon
146,Olive Drab,Damian
156,Dodger Blue,Vivian
166,Antique White,Kane
246,Fire Brick,Odysseus";

            // => array1-v2.csv x array2-v2.csv
            //            expectedResult = @"176,Navajo White,Alexa
            //186,Deep Sky Blue,Ryan
            //196,Aqua,Simon
            //206,Pink,Malachi
            //216,Light Slate Gray,Sacha
            //226,Light Steel Blue,Hayfa
            //236,Light Cyan,Mariam
            //246,Fire Brick,Odysseus
            //256,Papaya Whip,Cruz
            //266,Dark Salmon,Urielle
            //276,Dark Slate Blue,Jessamine";
            colorsArray = FileReaderHelper.ReadArrayFromCsv(colorsCsv, ',').OrderBy(x => Convert.ToInt32(x[0])).ToArray();
            namesArray = FileReaderHelper.ReadArrayFromCsv(namesCsv, ',').OrderBy(x => Convert.ToInt32(x[0])).ToArray();
        }

        [TestMethod]
        public void Test_Intersect_Between_Arrays_Hash()
        {
            var intersection = Intersector.IntersectionWithHashSet(colorsArray, namesArray,
                comparer: new CustomComparer(),
                formatFunction: (i1, i2) => new string[] { i1[0], i1[1], i2[1] });

            var result = string.Join("\r\n", intersection.Select(x => $"{x[0]},{x[1]},{x[2]}"));

     //       Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void Test_Intersect_Between_Arrays_Binary_Search()
        {
            var intersection = Intersector.IntersectionWithBinarySearch(colorsArray, namesArray,
                formatFunction: (i1, i2) => new string[] { i1[0], i1[1], i2[1] });

            var result = string.Join("\r\n", intersection.Select(x => $"{x[0]},{x[1]},{x[2]}"));

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_Intersect_Between_Arrays_Linear_Search()
        {
            var intersection = Intersector.IntersectionWithLinearSearch(colorsArray, namesArray,
                formatFunction: (i1, i2) => new string[] { i1[0], i1[1], i2[1] });

            var result = string.Join("\r\n", intersection.Select(x => $"{x[0]},{x[1]},{x[2]}"));

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_Intersect_Between_Arrays_Comparison_Hash()
        {
            long averageMs = 0;
            long averageTicks = 0;
            int count = 0;
            Console.WriteLine("Test_Intersect_Between_Arrays_Hash_Average");
            for (var i = 0; i < COMPARISON_LOOP; i++)
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                var intersection = Intersector.IntersectionWithHashSet(colorsArray, namesArray,
                    comparer: new CustomComparer(),
                    formatFunction: (i1, i2) => new string[] { i1[0], i1[1], i2[1] });
                stopWatch.Stop();

                Console.WriteLine($"Ran in {stopWatch.ElapsedTicks} ticks,{stopWatch.ElapsedMilliseconds}ms");
                if (i != 0)
                {
                    averageTicks += stopWatch.ElapsedTicks;
                    averageMs += stopWatch.ElapsedMilliseconds;
                    count++;
                }
            }

            averageMs = averageMs / count;
            averageTicks = averageTicks / count;

            Console.WriteLine($"Average in {averageTicks} ticks,{averageMs}ms");
        }

        [TestMethod]
        public void Test_Intersect_Between_Arrays_Comparison_Binary_Search()
        {
            long averageMs = 0;
            long averageTicks = 0;
            int count = 0;
            Console.WriteLine("Test_Intersect_Between_Arrays_Binary_Search_Average");
            for (var i = 0; i < COMPARISON_LOOP; i++)
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                var intersection = Intersector.IntersectionWithBinarySearch(colorsArray, namesArray,
                    formatFunction: (i1, i2) => new string[] { i1[0], i1[1], i2[1] });
                stopWatch.Stop();
                Console.WriteLine($"Ran in {stopWatch.ElapsedTicks} ticks,{stopWatch.ElapsedMilliseconds}ms");
                if (i != 0)
                {
                    averageTicks += stopWatch.ElapsedTicks;
                    averageMs += stopWatch.ElapsedMilliseconds;
                    count++;
                }
            }

            averageMs = averageMs / count;
            averageTicks = averageTicks / count;

            Console.WriteLine($"Average in {averageTicks} ticks,{averageMs}ms");
        }

        [TestMethod]
        public void Test_Intersect_Between_Arrays_Comparison_Linear_Search()
        {
            long averageMs = 0;
            long averageTicks = 0;
            int count = 0;
            Console.WriteLine("Test_Intersect_Between_Arrays_Linear_Search_Average");

            for (var i = 0; i < COMPARISON_LOOP; i++)
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                var intersection = Intersector.IntersectionWithLinearSearch(colorsArray, namesArray,
                    formatFunction: (i1, i2) => new string[] { i1[0], i1[1], i2[1] });
                stopWatch.Stop();

                Console.WriteLine($"Ran in {stopWatch.ElapsedTicks} ticks,{stopWatch.ElapsedMilliseconds}ms");
                if (i != 0)
                {
                    averageTicks += stopWatch.ElapsedTicks;
                    averageMs += stopWatch.ElapsedMilliseconds;
                    count++;
                }
            }

            averageMs = averageMs / count;
            averageTicks = averageTicks / count;

            Console.WriteLine($"Average in {averageTicks} ticks,{averageMs}ms");
        }
    }
}
