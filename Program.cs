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
            Console.WriteLine("  Dla nQeens {3}\nLiczba rozwiązań: {0} \nCzas znalezienia wszystkich rozwiązań: {1}ms \nCzas znalezienia jednego rozwiązania: {2}ms", solutions.Count, watch.ElapsedMilliseconds, nQueens.TimeOfOneSolution, nQueens._numberOfQueens);
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
            //NQueens nQueens = new NQueens(4, ValueMode.Ascending, VariableMode.StartEnd);
            //PrintNQueens(RunNQueens(nQueens));
            //nQueens = new NQueens(5, ValueMode.StartEnd, VariableMode.Ascending);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(5, ValueMode.Ascending, VariableMode.StartEnd);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(5, ValueMode.StartEnd, VariableMode.StartEnd);
            //RunNQueens(nQueens);

            //nQueens = new NQueens(8, ValueMode.Ascending, VariableMode.Ascending);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(8, ValueMode.StartEnd, VariableMode.Ascending);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(8, ValueMode.Ascending, VariableMode.StartEnd);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(8, ValueMode.StartEnd, VariableMode.StartEnd);
            //RunNQueens(nQueens);

            //nQueens = new NQueens(12, ValueMode.Ascending, VariableMode.Ascending);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(12, ValueMode.StartEnd, VariableMode.Ascending);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(12, ValueMode.Ascending, VariableMode.StartEnd);
            //RunNQueens(nQueens);
            //nQueens = new NQueens(12, ValueMode.StartEnd, VariableMode.StartEnd);
            //RunNQueens(nQueens);

            NQueensFC nQueens = new NQueensFC(10, ValueMode.Ascending, VariableMode.StartEnd);
            PrintNQueens(RunNQueensFC(nQueens));

            Console.ReadLine();
        }

        
    }
}
