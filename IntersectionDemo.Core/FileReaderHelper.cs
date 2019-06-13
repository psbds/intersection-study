using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntersectionDemo.Core
{
    public static class FileReaderHelper
    {
        public static IEnumerable<string[]> ReadArrayFromCsv(string csvContent, char delimiter)
        {
            var csvRows = csvContent.Replace("\r", "").Split('\n');

            return csvRows.Select(row => row.Split(delimiter));
        }

    }
}
