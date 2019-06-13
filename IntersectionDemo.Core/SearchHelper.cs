using System;
using System.Collections.Generic;
using System.Text;

namespace IntersectionDemo.Core
{
    public static class SearchHelper
    {
        public static string[] BinarySearch(this string[][] array, int key)
        {
            return BinarySearch(array, key, 0, array.Length - 1);
        }
        private static string[] BinarySearch(this string[][] array, int key, int start, int end)
        {
            int lowest = start;
            int highest = end;
            while (lowest <= highest)
            {
                int middle = (lowest + highest) / 2;
                if (key == Convert.ToInt32(array[middle][0]))
                {
                    return array[middle];
                }
                else if (key < Convert.ToInt32(array[middle][0]))
                {
                    return BinarySearch(array, key, lowest, middle - 1);
                }
                else
                {
                    return BinarySearch(array, key, middle + 1, highest);
                }
            }

            return null;
        }

    }
}
