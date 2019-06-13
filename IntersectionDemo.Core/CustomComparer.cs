using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IntersectionDemo.Core
{
    public class CustomComparer : IEqualityComparer<string[]>
    {
        public bool Equals(string[] x, string[] y)
        {
            return x[0] == y[0];
        }

        public int GetHashCode(string[] obj)
        {
            return Convert.ToInt32(obj[0]);
        }
    }
}
