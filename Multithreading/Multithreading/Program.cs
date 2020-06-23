using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        public static async Task<List<int>> MergeSortAsync(List<int> x, int h)
        {
            var result = await Task.Run(() => MergeSort(x, h));
            return result;
        }
        static List<int> MergeSort(List<int> x, int h = 0)
        {
            if (x.Count == 1)
            {
                return x;
            }
            int mid = x.Count / 2;
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            for (int i = 0; i < x.Count; ++i)
            {
                if (i < x.Count / 2)
                {
                    a.Add(x[i]);
                }
                else
                {
                    b.Add(x[i]);
                }
            }
            if (h < 2)
            {
                var asa = MergeSortAsync(a, h + 1);
                var asb = MergeSortAsync(b, h + 1);
                a = asa.Result;
                b = asb.Result;
            }
            else
            {
                a = MergeSort(a, h + 1);
                b = MergeSort(b, h + 1);
            }
            int p = 0;
            int q = 0;
            int k = 0;
            while (p < a.Count && q < b.Count)
            {
                if (a[p] < b[q])
                {
                    x[k++] = a[p++];
                }
                else
                {
                    x[k++] = b[q++];
                }
            }
            while (p < a.Count)
            {
                x[k++] = a[p++];
            }
            while (q < b.Count)
            {
                x[k++] = b[q++];
            }
            return x;
        }
        static void Main()
        {
            int n = 10000000;
            List<int> x = new List<int>();
            Random random = new Random();
            for (int i = 0; i < n; ++i)
            {
                x.Add(random.Next(1, 474747474));
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            x = MergeSort(x);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs + "ms");
            Console.ReadKey();
        }
    }
}
