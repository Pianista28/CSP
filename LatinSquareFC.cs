using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CSP
{
    internal struct Position2
    {
        internal int X;
        internal int Y;
        internal SortedSet<int> Domain;
        internal List<Position2> Removed;
    }
    public class LatinSquareFC
    {
        public long TimeOfOneSolution { get; set; }
        public List<int[,]> Solutions { get; set; }
        private bool first = true;
        private int[,] board;
        private Stopwatch watch;
        private List<Position2> processed;
        private List<Position2> notProcessed;
        public int _size { get; set; }
        private int _domain;
        private readonly ValueMode _valueMode;
        private readonly VariableMode _variableMode;
        private readonly bool _firstOnly;


        public LatinSquareFC(int size, ValueMode valueMode, VariableMode variableMode, bool firstOnly = false)
        {
            _size = size;
            _valueMode = valueMode;
            _variableMode = variableMode;
            _firstOnly = firstOnly;
            board = new int[_size, _size];
            _domain = _size;
            processed = new List<Position2>();
            notProcessed = new List<Position2>();
            Solutions = new List<int[,]>();
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                var pos = new Position2() {X = i, Y = j, Domain = new SortedSet<int>()};
                for (int x = 1; x <= _domain; x++)
                    pos.Domain.Add(x);
                pos.Removed = new List<Position2>();
                notProcessed.Add(pos);
            }
        }

        public List<int[,]> FindSolution()
        {
            ProcessFirst();
            return Solutions;
        }

        private bool ProcessFirst()
        {
            watch = Stopwatch.StartNew();
            var firstVariable = GetNextVariable();

            int nextValue = GetNextValue(firstVariable);
            while (nextValue != (int)Value.NoValue)
            {
                board[firstVariable.X, firstVariable.Y] = nextValue;
                Process(firstVariable);
                nextValue = GetNextValue(firstVariable);
            }

            return false;
        }


        private bool Process(Position2 variable)
        {
            notProcessed.Remove(variable);
            processed.Add(variable);
            if (!notProcessed.Any())
            {
                if (first)
                {
                    watch.Stop();
                    TimeOfOneSolution = watch.ElapsedMilliseconds;
                    first = false;
                }
                SaveSolution();
                notProcessed.Insert(0,variable);
                processed.Remove(variable);
                //board[variable.X, variable.Y] = 0;
                return true;
            }
            
            if (!FilterRemainingVariables(variable))
            {
                Unfilter(variable);
                notProcessed.Insert(0, variable);
                processed.Remove(variable);
                return false;
            }



            var nextVariable = GetNextVariable();
            int nextValue = GetNextValue(nextVariable);
            while (nextValue != (int)Value.NoValue)
            {
                board[nextVariable.X, nextVariable.Y] = nextValue;
                Process(nextVariable);

                nextValue = GetNextValue(nextVariable);
            }
            Unfilter(variable);
            notProcessed.Insert(0,variable);
            processed.Remove(variable);
            board[nextVariable.X, nextVariable.Y] = 0;
            return false;
        }

        private void Unfilter(Position2 variable)
        {
            var value = board[variable.X, variable.Y];
            //foreach (var position2 in notProcessed.Where(np => np.X == variable.X || np.Y == variable.Y))
            //{
            //    position2.Domain.Add(value);
            //}
            foreach (var position2 in variable.Removed)
            {
                position2.Domain.Add(value);
            }
            variable.Removed.Clear();
        }

        private bool FilterRemainingVariables(Position2 variable)
        {
            var value = board[variable.X, variable.Y];
            foreach (var position2 in notProcessed.Where(np => np.X == variable.X || np.Y == variable.Y))
            {
                var b = position2.Domain.Remove(value);
                if (b)
                    variable.Removed.Add(position2);
                if (!position2.Domain.Any())
                    return false;
            }
            return true;
        }

        private bool IsVariableInConflict(Position2 currentVariable, int currentValue)
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

        private void StepBack(Position2 currentVariable)
        {


                    notProcessed.Insert(0, currentVariable);
                    processed.Remove(processed.Last());

        }

        private Position2 GetPreviousVariable(Position2 curentVaiable)
        {
            board[curentVaiable.X, curentVaiable.Y] = 0;
            return processed.Last();
        }

        private int GetNextValue(Position2 currentVariable)
        {

            var next = currentVariable.Domain.FirstOrDefault(x => x > board[currentVariable.X, currentVariable.Y]);
            return next == 0 ? (int) Value.NoValue : next;

        }

        private Position2 GetNextVariable()
        {
            return notProcessed.Any() ? notProcessed.First() : new Position2() { X = -1, Y = -1 };
        }

        private void SaveSolution()
        {
            // Console.WriteLine(Solutions.Count);
            Solutions.Add(board.Clone() as int[,]);
        }
    }
}
