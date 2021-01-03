using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class MineField
    {
        public MineField(int width, int height, int bombCount = 0)
        {
            Width = width;
            Height = height;
            BombCount = bombCount;
            Cells = Enumerable.Range(0, Width * Height)
                              .Select(x => new Cell())
                              .ToArray();
        }

        public int Width { get; }
        public int Height { get; }
        public int BombCount { get; }
        public Cell[] Cells { get; }

        public void SetBombs()
        {
            var rand = new Random();

            (from i in IndexGenerator(rand)
             select Cells[i]).Distinct()
                             .Take(BombCount)
                             .ForEach(x => x.SetBomb());
        }

        private IEnumerable<int> IndexGenerator(Random rand)
        {
            while (true)
                yield return rand.Next(Width * Height);
        }

        public void SetNearBombsCounts()
        {
        }

        public IEnumerable<Cell> GetNearCells(int x, int y)
        {
            return from i in NearIndexGenerator(x, y)
                   let c = GetCell(i)
                   where c is not null
                   select c;
        }

        private Cell GetCell((int X, int Y) pos) => pos switch
        {
            (var x, _) when x < 0 => null,
            (var x, _) when x >= Width => null,
            (_, var y) when y < 0 => null,
            (_, var y) when y >= Height => null,
            (var x, var y) => Cells[x + (y * Width)],
        };


        private IEnumerable<(int, int)> NearIndexGenerator(int x, int y)
        {
            yield return (x - 1, y - 1);
            yield return (x, y - 1);
            yield return (x + 1, y - 1);
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x - 1, y + 1);
            yield return (x, y + 1);
            yield return (x + 1, y + 1);
        }
    }
}
