using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace IntersectionDemo.Core
{
    // TODO Refactor to use generics
    public static class Intersector
    {

        #region [ Public ] 
        public static IEnumerable<string[]> IntersectionWithLinearSearch(
          string[][] firstArray,
          string[][] secondArray,
          Func<string[], string[], string[]> formatFunction)
        {
            (var smallest, var largest) = CompareSize(firstArray, secondArray);
            var intersections = new List<string[]>();

            foreach (var smallestItem in smallest)
            {
                foreach (var largestItem in largest)
                {
                    if (largestItem[0] == smallestItem[0])
                    {
                        intersections.Add(HandleFormatOrder(smallest, firstArray, smallestItem, largestItem, formatFunction));
                        break;
                    }
                }
            }
            return intersections;
        }

        public static IEnumerable<string[]> IntersectionWithBinarySearch(
           string[][] firstArray,
           string[][] secondArray,
           Func<string[], string[], string[]> formatFunction)
        {
            (var smallest, var largest) = CompareSize(firstArray, secondArray);
            var intersections = new List<string[]>();

            foreach (var smallestItem in smallest)
            {
                var item = SearchHelper.BinarySearch(largest, Convert.ToInt32(smallestItem[0]));

                if (item != null)
                {
                    intersections.Add(HandleFormatOrder(smallest, firstArray, smallestItem, item, formatFunction));
                }
            }
            return intersections;
        }

        public static IEnumerable<string[]> IntersectionWithHashSet(
            string[][] firstArray,
            string[][] secondArray,
          IEqualityComparer<string[]> comparer,
          Func<string[], string[], string[]> formatFunction)
        {
            (var smallest, var largest) = CompareSize(firstArray, secondArray);

            var intersections = new List<string[]>();
            var hashSet = new HashSet<string[]>(comparer);

            foreach (var smallestItem in smallest)
            {
                hashSet.Add(smallestItem);
                foreach (var largestItem in largest)
                {
                    if (hashSet.Contains(largestItem))
                    {
                        intersections.Add(HandleFormatOrder(smallest, firstArray, smallestItem, largestItem, formatFunction));
                        break;
                    }
                }
            }
            return intersections;
        }

        #endregion

        #region [ Private ]
        // Having n = firstArray and m = secondArray
        // This method is made to make sure that the order of properties to be return keep the same on both 
        // n.length > m.lenght and m.length > n.length
        private static string[] HandleFormatOrder(string[][] smallestArray, string[][] fistArray, string[] firstItem, string[] secondItem, Func<string[], string[], string[]> formatFunction)
        {
            return smallestArray == fistArray ? formatFunction(firstItem, secondItem) : formatFunction(secondItem, firstItem);
        }

        private static (string[][] smallest, string[][] largest) CompareSize(string[][] firstArray, string[][] secondArray)
        {
            return firstArray.Length > secondArray.Length ? (secondArray, firstArray) : (firstArray, secondArray);
        }

        #endregion

    }
}
