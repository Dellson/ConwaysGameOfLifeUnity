using System.Collections.Generic;
using System.Linq;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class Core
    {
        public static List<GameObjectCell> RecalculateWithShiftAlgorithm(List<GameObjectCell> currentGeneration, int mapWidth)
        {
            var count = currentGeneration.Count;
            var currGeneration = new List<bool>(currentGeneration.Select(goc => goc.State));
            var nextGeneration = new List<bool>(currentGeneration.Select(goc => goc.State));

            for (int index = 0; index < count; index++)
            {
                int aliveNeighbours = 0;
                int[] neighbourPositions =
                    new int[]
                    {
                        index - mapWidth - 1,
                        index - mapWidth,
                        index - mapWidth + 1,
                        index - 1,
                        index + 1,
                        index + mapWidth  - 1,
                        index + mapWidth,
                        index + mapWidth + 1
                    };

                foreach (var neighbourPosition in neighbourPositions)
                {
                    if (neighbourPosition >= count || neighbourPosition < 0) continue;
                    if (currGeneration[neighbourPosition]) aliveNeighbours++;
                }

                if (currGeneration[index]
                    && (aliveNeighbours < 2 || aliveNeighbours > 3))
                {
                    nextGeneration[index] = false;
                }
                else if (!currGeneration[index]
                    && aliveNeighbours == 3)
                {
                    nextGeneration[index] = true;
                }
            }

            for (int i = 0; i < count; i++)
            {
                currentGeneration[i].State = nextGeneration[i];
            }

            return currentGeneration;
        }

        public static GameObjectCell[,] RecalculateWith2DArrayAlgorithm(GameObjectCell[,] currentGeneration, int mapWidth)
        {
            var height = currentGeneration.GetLength(0);
            var width = currentGeneration.GetLength(1);
            bool[,] nextGeneration = new bool[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var state = false;
                    int aliveNeighbours = 0;

                    (int y, int x)[] neighbourPositions = new (int, int)[]
                        {
                            (i - 1, j - 1),
                            (i - 1, j),
                            (i - 1, j + 1),
                            (i, j - 1),
                            (i, j + 1),
                            (i + 1, j - 1),
                            (i + 1, j),
                            (i + 1, j + 1)
                        };

                    foreach (var (y, x) in neighbourPositions)
                    {
                        if (x >= width || x < 0 || y >= height || y < 0) continue;
                        if (currentGeneration[y, x].State) aliveNeighbours++;
                    }

                    if (currentGeneration[i, j]
                        && (aliveNeighbours == 2 || aliveNeighbours == 3))
                    {
                        state = true;
                    }
                    else if (!currentGeneration[i, j]
                        && aliveNeighbours == 3)
                    {
                        state = true;
                    }

                    nextGeneration[i, j] = state;
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    currentGeneration[i, j].State = nextGeneration[i, j];
                }
            }

            return currentGeneration;
        }
    }
}
