using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSP
{
    /// <summary>
    /// Variables - indexed from 0
    /// Domains = indexed from 1
    /// </summary>
    public class NQueens
    {
        public int _numberOfQueens { get; set; }
        private int[] board;
        private List<int> processed;
        private int _domain;
        private int[][] valuesHistory;
        public long TimeOfOneSolution{ get; set; }

        private ValueMode _valueMode;
        private VariableMode _variableMode;

        public List<int[]> Solutions { get; set; }

        public NQueens(int numberOfQueens, ValueMode valueMode, VariableMode variableMode)
        {
            _numberOfQueens = numberOfQueens;
            _domain = numberOfQueens;
            board = new int[numberOfQueens];
            processed = new List<int>();
            Solutions = new List<int[]>();
            valuesHistory = new int[numberOfQueens][];
            for (int i =0; i < valuesHistory.Length;  i++)
            {
                valuesHistory[i] = new int[numberOfQueens];
            }
            _valueMode = valueMode;
            _variableMode = variableMode;
        }

        private int GetNextValueForVariable(int variable)
        {
            switch (_valueMode)
            {
                case ValueMode.Ascending:
                    return GetNextValueForVariableAscending(variable);
                case ValueMode.StartEnd:
                    return GetNextValueForVariableStartEnd(variable);
                default:
                    return -1;
            }
        }

        private int GetNextVariable()
        {
            switch (_variableMode)
            {
                case VariableMode.Ascending:
                    return GetNextVariableAscending();
                case VariableMode.StartEnd:
                    return GetNextVariableStartEnd();
                default:
                    return -1;
            }
        }

        private bool AreInConfilct(int var1, int var2)
        {
            return board[var1] == board[var2] || (Math.Abs(var1 - var2) == Math.Abs(board[var1] - board[var2]));
        }

        private bool IsVariableInConflict(int variable)
        {
            foreach (var processedVariable in processed)
            {
                if (AreInConfilct(variable, processedVariable))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetNextVariableAscending()
        {
            if (!processed.Any())
                return 0;
            return processed.Max() + 1;
        }

        private int GetNextVariableStartEnd()
        {

            if (!processed.Any())
                return 0;
            if (processed.Count() == _numberOfQueens )
                return processed.Count() + 1;
            if (processed.Count() % 2 == 0)
            {
                return processed.Count() / 2;
            }
            else
            {
                return _numberOfQueens - 1 - processed.Count / 2;
            }
        }

        private int GetPreviousVariable(int currentVariable)
        {
            board[currentVariable] = 0;
            if (_valueMode == ValueMode.StartEnd)
            {
                for (int i = 0; i < _numberOfQueens; i++)
                    valuesHistory[currentVariable][i] = 0;
            }
            if (!processed.Any())
                return -1;
            var previousVariable =  processed.Last();
            processed.Remove(processed.Last());
            return previousVariable;
        }

        private int GetNextValueForVariableAscending(int variable)
        {
            if (variable == -1)
                return -1;
            board[variable]  += 1;
            return board[variable] ;
        }

        private int GetNextValueForVariableStartEnd(int variable)
        {
            if (variable == -1)
                return -1;

            if (valuesHistory[variable].Sum() % 2 == 0)
                board[variable] = Array.FindIndex(valuesHistory[variable], x => x == 0) + 1;
            else
                board[variable] = Array.FindLastIndex(valuesHistory[variable], x => x == 0) + 1;
            if (board[variable] == -1 + 1)
                return _numberOfQueens + 1;
            valuesHistory[variable][ board[variable] - 1] = 1;
            return board[variable];
        }

        public List<int[]> FindSolution()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool first = true;
            var currentVariable = -1;
            var noMoreSolutions = false;
            bool clear = false;
            while (!noMoreSolutions)
            {
                if (processed.Count == _numberOfQueens && !clear)
                {
                    watch.Stop();
                    if (first)
                    {
                        TimeOfOneSolution = watch.ElapsedMilliseconds;
                        first = false;
                    }

                    SaveSolution();
                    processed.Remove(processed.Last());
                }
                else
                {
                    currentVariable = GetNextVariable();
                }

                var currentValue = -1;
                do
                {
                    currentValue = GetNextValueForVariable(currentVariable);
                    while (currentValue > _domain)
                    {
                        if (currentVariable == 0)
                        {
                            return Solutions;
                        }

                        currentVariable = GetPreviousVariable(currentVariable);
                        currentValue = GetNextValueForVariable(currentVariable);
                    }
                } while (IsVariableInConflict(currentVariable));

                board[currentVariable] = currentValue;
                processed.Add(currentVariable);
            }
            return Solutions;
        }

        private void SaveSolution()
        {
            var solution = new int[_numberOfQueens];
            board.CopyTo(solution, 0);
            Solutions.Add(solution);
        }
    }
}
