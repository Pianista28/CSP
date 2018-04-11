using System;
using System.Collections.Generic;

namespace CSP
{

    public enum ValueMode
    {
        Ascending,
        StartEnd
    }

    public enum VariableMode
    {
        Ascending,
        StartEnd
    }

    public class Program
    {
        static List<int[]> RunNQueens(NQueens nQueens)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var solutions = nQueens.FindSolution();
            watch.Stop();
            Console.WriteLine("  Dla nQeens {3}\nLiczba rozwiązań: {0} \nCzas znalezienia wszystkich rozwiązań: {1}ms \nCzas znalezienia jednego rozwiązania: {2}ms", solutions.Count, watch.ElapsedMilliseconds, nQueens.TimeOfOneSolution, nQueens._numberOfQueens);
            return solutions;
        }

        private static List<int[]> RunNQueensFC(NQueensFC nQueens)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var solutions = nQueens.FindSolution();
            watch.Stop();
            Console.WriteLine("  Dla nQeensFC {3}\nLiczba rozwiązań: {0} \nCzas znalezienia wszystkich rozwiązań: {1}ms \nCzas znalezienia jednego rozwiązania: {2}ms", solutions.Count, watch.ElapsedMilliseconds, nQueens.TimeOfOneSolution, nQueens._numberOfQueens);
            return solutions;
        }

        private static List<int[,]> RunLatinSquare(LatinSquare latinSquare)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var solutions = latinSquare.FindSolution();
            watch.Stop();
            Console.WriteLine("  Dla LatinSquare {3}\nLiczba rozwiązań: {0} \nCzas znalezienia wszystkich rozwiązań: {1}ms \nCzas znalezienia jednego rozwiązania: {2}ms", solutions.Count, watch.ElapsedMilliseconds, latinSquare.TimeOfOneSolution, latinSquare._size);
            return solutions;
        }

        private static List<int[,]> RunLatinSquareFC(LatinSquareFC latinSquare)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var solutions = latinSquare.FindSolution();
            watch.Stop();
            Console.WriteLine("  Dla LatinSquareFC {3}\nLiczba rozwiązań: {0} \nCzas znalezienia wszystkich rozwiązań: {1}ms \nCzas znalezienia jednego rozwiązania: {2}ms", solutions.Count, watch.ElapsedMilliseconds, latinSquare.TimeOfOneSolution, latinSquare._size);
            return solutions;
        }

        static void PrintNQueens(List<int[]> solutions)
        {
            foreach (var solution in solutions)
            {
                foreach (var position in solution)
                {
                    Console.Write(position + ", ");
                }
                Console.WriteLine();
            }
            
        }

        static void Main(string[] args)
        {
            int n = 2;

            LatinSquareFC latinSquare = new LatinSquareFC(n, ValueMode.Ascending, VariableMode.Ascending);
            RunLatinSquareFC(latinSquare);
            latinSquare = new LatinSquareFC(n + 1, ValueMode.Ascending, VariableMode.Ascending);
            RunLatinSquareFC(latinSquare);
            latinSquare = new LatinSquareFC(n + 2, ValueMode.Ascending, VariableMode.Ascending);
            RunLatinSquareFC(latinSquare);
            latinSquare = new LatinSquareFC(n + 3, ValueMode.Ascending, VariableMode.Ascending);
            RunLatinSquareFC(latinSquare);

            Console.ReadLine();
        }    
    }
}
