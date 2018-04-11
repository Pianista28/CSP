using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace CSP
{
    internal enum Value
    {
        NoValue = -1,
        NotSetValue = -2
    };

    /// <summary>
    /// Variables - indexed from 0
    /// Domains = indexed from 1
    /// in currentBoard 1 means unavailable (attacked) position
    /// </summary>
    public class NQueensFC
    {
        private bool first = true;
        private Stopwatch watch;
        public int _numberOfQueens { get; set; }
        private static int[] board;
        private static List<int> notProcessed;
        private static List<int> processed;
        private int _domain;
        public long TimeOfOneSolution { get; set; }

        private ValueMode _valueMode;
        private VariableMode _variableMode;

        public List<int[]> Solutions { get; set; }

        public NQueensFC(int numberOfQueens, ValueMode valueMode, VariableMode variableMode)
        {
            _numberOfQueens = numberOfQueens;
            _domain = numberOfQueens;
            board = new int[numberOfQueens];
            notProcessed = new List<int>();
            for (int i = 0; i < _numberOfQueens; i++)
            {
                notProcessed.Add(i);
            }
            Solutions = new List<int[]>();
            processed = new List<int>();
            _valueMode = valueMode;
            _variableMode = variableMode;
        }

        public List<int[]> FindSolution()
        {
            ProcessFirst();
            return Solutions;
        }

        private bool ProcessFirst()
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
            var firstVariable = GetNextVariable();

            int nextValue = GetNextValue(firstVariable, new int[_numberOfQueens]);
            while (nextValue != (int)Value.NoValue)
            {             
                board[firstVariable] = nextValue;
                Process(firstVariable);
                nextValue = GetNextValue(firstVariable, new int[_numberOfQueens]);
            }

            return false;
        }


        private bool Process(int variable)
        {
            var currentBoard = CreateBoard();
            if (board.All(x => x != 0))
            {
                if (first)
                {
                    watch.Stop();
                    TimeOfOneSolution = watch.ElapsedMilliseconds;
                    first = false;
                }
                SaveSolution();
                return true;
            }
            notProcessed.Remove(variable);
            processed.Add(variable);
            if (!FilterRemainingVariables(currentBoard))
            {
                notProcessed.Add(variable);
                processed.Remove(variable);
                return false;
            }
           

            var nextVariable = GetNextVariable();
            int nextValue = GetNextValue(nextVariable, currentBoard[nextVariable]);
            while (nextValue != (int) Value.NoValue)
            {
                board[nextVariable] = nextValue;
                Process(nextVariable);
                nextValue = GetNextValue(nextVariable, currentBoard[nextVariable]);
            }

            notProcessed.Add(variable);
            processed.Remove(variable);
            board[nextVariable] = 0;
            return false;
        }

        private bool FilterRemainingVariables(int[][] currentBoard)
        {
            foreach (var variable in notProcessed)
            {
                for (int i = 0; i < _numberOfQueens; i++)
                {
                    if (isAttacked(variable, i))
                        currentBoard[variable][i] = 1;
                    if (currentBoard[variable].Sum() == _numberOfQueens)
                        return false;
                }
            }

            return true;
        }

        private bool isAttacked(int variable, int position)
        {
            foreach (var i in processed)
            {
                if (board[i] == position + 1 || Math.Abs(variable - i) == Math.Abs(position + 1 - board[i]))
                    return true;
            }

            return false;
        }

        private int GetNextValue(int variable, int[] domain)
        {
            switch (_valueMode)
            {
                case ValueMode.Ascending:
                    return GetNextValueAscending(variable, domain);
                case ValueMode.StartEnd:
                    return GetNextValueStartEnd(variable, domain);
            }

            return -1;
        }

        private int GetNextVariable()
        {
            switch (_variableMode)
            {
                case VariableMode.Ascending:
                    return GetNextVariableAscending();
                case VariableMode.StartEnd:
                    return GetNextVariableStartEnd();
            }
            return GetNextVariableAscending();
        }

        private int GetNextVariableAscending()
        {
            return notProcessed.Min();
        }

        private int GetNextVariableStartEnd()
        {
            if (processed.Count() % 2 == 0)
                return notProcessed.Min();
            return notProcessed.Max();

        }

        private int GetNextValueAscending(int variable, int[] domain)
        {
            if(domain.Sum() == _numberOfQueens)
                return (int) Value.NoValue;
            for (int i = board[variable]; i < _numberOfQueens; i++)
            {
                if (domain[i] == 0)
                    return i + 1;
            }

            return (int) Value.NoValue;
        }

        private int GetNextValueStartEnd(int variable, int[] domain)
        {
            if (domain.Sum() == _numberOfQueens)
                return (int)Value.NoValue;
            if (processed.Count() % 2 == 0)
            {
                for (int i = board[variable]; i < _numberOfQueens; i++)
                {
                    if (domain[i] == 0)
                        return i + 1;
                }
            }
            else
            {

                for (int i = board[variable] == 0 ? _numberOfQueens - 1 : board[variable] - 2 ; i >= 0; i--)
                {
                    if (domain[i] == 0)
                        return i + 1;
                }
            }

            return (int)Value.NoValue;
        }
        

        private void SaveSolution()
        {
            var solution = new int[_numberOfQueens];
            board.CopyTo(solution, 0);
            Solutions.Add(solution);
        }

        private int[][] CreateBoard()
        {
            var currentBoard = new int[_numberOfQueens][];
            for (var i = 0; i < currentBoard.Length; i++)
            {
                currentBoard[i] = new int[_numberOfQueens];
            }

            return currentBoard;
        }
    }
}
