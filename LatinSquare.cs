using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSP
{
    internal struct Position
    {
        internal int X;
        internal int Y;
    }

    public class LatinSquare
    {
        public long TimeOfOneSolution { get; set; }
        public List<int[,]> Solutions { get; set; }

        private int[,] board;

        private List<Position> processed;
        private List<Position> notProcessed;
        public int _size { get; set; }
        private int _domain;
        private readonly ValueMode _valueMode;
        private readonly VariableMode _variableMode;
        private readonly bool _firstOnly;


        public LatinSquare(int size, ValueMode valueMode, VariableMode variableMode, bool firstOnly = false)
        {
            _size = size;
            _valueMode = valueMode;
            _variableMode = variableMode;
            _firstOnly = firstOnly;
            board = new int[_size,_size];
            _domain = _size;
            processed = new List<Position>();
            notProcessed = new List<Position>();
            Solutions = new List<int[,]>();
            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                    notProcessed.Add(new Position(){X = i, Y = j});
        }


        public List<int[,]> FindSolution()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool first = true;
            var currentVariable = new Position();
            var noMoreSolutions = false;
            while (!noMoreSolutions)
            {
                if (processed.Count == _size * _size)
                {
                    watch.Stop();
                    if (first)
                    {
                        TimeOfOneSolution = watch.ElapsedMilliseconds;
                        first = false;
                    }

                    SaveSolution();
                    if (_firstOnly)
                        return Solutions;
                    StepBack(currentVariable);
                    
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
                        if (currentVariable.X == 0 && currentVariable.Y == 0) 
                        {
                            return Solutions;
                        }

                        currentVariable = GetPreviousVariable(currentVariable);
                        StepBack(currentVariable);                
                        currentValue = GetNextValueForVariable(currentVariable);
                    }
                } while (IsVariableInConflict(currentVariable, currentValue));

                board[currentVariable.X, currentVariable.Y] = currentValue;
                processed.Add(currentVariable);
                notProcessed.Remove(currentVariable);
            }
            return Solutions;
        }

        private bool IsVariableInConflict(Position currentVariable, int currentValue)
        {
            for (int i = 0; i < _size; i++)
            {
                if (currentVariable.Y != i && board[currentVariable.X, i] == currentValue || currentVariable.X != i && board[i, currentVariable.Y] == currentValue)
                {
                    return true;
                }
            }

            return false;
        }

        private void StepBack(Position currentVariable)
        {
            switch (_variableMode)
            {
                case VariableMode.Ascending:
                    notProcessed.Insert(0, currentVariable);
                    processed.Remove(processed.Last());
                    break;
                case VariableMode.StartEnd:
                    processed.Remove(processed.Last());
                    if (processed.Count % 2 == 0)
                        notProcessed.Insert(0, currentVariable);
                    else
                        notProcessed.Add(currentVariable);
                    
                    break;
            }
        }

        private Position GetPreviousVariable(Position curentVaiable)
        {
            board[curentVaiable.X, curentVaiable.Y] = 0;
            return processed.Last();
        }

        private int GetNextValueForVariable(Position currentVariable)
        {
            switch (_valueMode)
            {
                case ValueMode.Ascending:
                    return GetNextValueForVariableAscending(currentVariable);
                case ValueMode.StartEnd:
                    return GetNextValueForVariableStartEnd(currentVariable);
            }
            return GetNextValueForVariableAscending(currentVariable);
        }

        private int GetNextValueForVariableStartEnd(Position currentVariable)
        {
            if(processed.Count % 2 == 0)
                return board[currentVariable.X, currentVariable.Y] += 1;

            if (board[currentVariable.X, currentVariable.Y] == 0)
            {
                return board[currentVariable.X, currentVariable.Y] = _size;
            }

            if (board[currentVariable.X, currentVariable.Y] == 1)
            {
                return board[currentVariable.X, currentVariable.Y] = _size + 1;
            }

            return board[currentVariable.X, currentVariable.Y] -= 1;


        }

        private int GetNextValueForVariableAscending(Position currentVariable)
        {
            return board[currentVariable.X, currentVariable.Y] += 1;
        }


        private Position GetNextVariable()
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

        private Position GetNextVariableStartEnd()
        {
            if (processed.Count % 2 == 0)
                return notProcessed.First();
            return notProcessed.Last();
        }

        private Position GetNextVariableAscending()
        {
            return notProcessed.Any() ? notProcessed.First() : new Position(){X = -1, Y = -1};
        }

        private void SaveSolution()
        {
           // Console.WriteLine(Solutions.Count);
            Solutions.Add(board.Clone() as int[,]);
        }
    }
}
