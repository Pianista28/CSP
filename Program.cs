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
            int n = 4;

            //NQueensFC nQueensFC = new NQueensFC(n, ValueMode.StartEnd, VariableMode.Ascending);
            //RunNQueensFC(nQueensFC);

            //nQueensFC = new NQueensFC(n, ValueMode.Ascending, VariableMode.Ascending);
            //RunNQueensFC(nQueensFC);

            //nQueensFC = new NQueensFC(n, ValueMode.StartEnd, VariableMode.StartEnd);
            //RunNQueensFC(nQueensFC);

            //nQueensFC = new NQueensFC(n, ValueMode.Ascending, VariableMode.StartEnd);
            //RunNQueensFC(nQueensFC);

            LatinSquare latinSquare = new LatinSquare(n, ValueMode.Ascending, VariableMode.Ascending);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n, ValueMode.Ascending, VariableMode.StartEnd);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n, ValueMode.StartEnd, VariableMode.Ascending);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n, ValueMode.StartEnd, VariableMode.StartEnd);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n + 1, ValueMode.Ascending, VariableMode.Ascending);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n + 1, ValueMode.Ascending, VariableMode.StartEnd);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n + 1, ValueMode.StartEnd, VariableMode.Ascending);
            RunLatinSquare(latinSquare);
            latinSquare = new LatinSquare(n + 1, ValueMode.StartEnd, VariableMode.StartEnd);
            RunLatinSquare(latinSquare);

            Console.ReadLine();
        }    
    }
}
