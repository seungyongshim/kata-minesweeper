using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class Cell
    {
        public Cell(int x, int y) : this(x, y, Enumerable.Empty<Cell>()) { }
        public Cell(int x, int y, IEnumerable<Cell> nearCells)
        {
            X = x;
            Y = y;
            NearCells = nearCells;
        }

        public bool IsBomb { get; private set; }
        public int NearBombsCount { get; set; }
        public bool IsCovered { get; private set; } = true;
        public int X { get; }
        public int Y { get; }
        public IEnumerable<Cell> NearCells { get; }

        public void SetBomb()
        {
            IsBomb = true;

            NearCells.ForEach(x => x.NearBombsCount++);
        }

        public void Click()
        {
            if (!IsCovered) return;

            IsCovered = false;

            if (NearBombsCount != 0) return;

            NearCells.ForEach(x => x.Click());
        }

        public override string ToString() => (IsCovered, IsBomb, NearBombsCount) switch
        {
            (true, _, _) => ".",
            (false, true, _) => "*",
            (false, false, var c) => c.ToString()
        };
    }
}
