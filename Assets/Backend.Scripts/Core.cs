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
            var xLen = currentGeneration.GetLength(0);
            var yLen = currentGeneration.GetLength(1);
            bool[,] nextGeneration = new bool[xLen, yLen];

            for (int x = 0; x < xLen; x++)
            {
                for (int y = 0; y < yLen; y++)
                {
                    int aliveNeighbours = 0;
                    (int xpos, int ypos)[] neighbourPositions = new (int, int)[]
                        {
                            (x - 1, y - 1),
                            (x - 1, y),
                            (x - 1, y + 1),
                            (x, y - 1),
                            (x, y + 1),
                            (x + 1, y - 1),
                            (x + 1, y),
                            (x + 1, y + 1)
                        };

                    foreach (var neighbourPosition in neighbourPositions)
                    {
                        if (neighbourPosition.xpos >= xLen || neighbourPosition.xpos < 0
                            || neighbourPosition.ypos >= yLen || neighbourPosition.ypos < 0) continue;
                        if (currentGeneration[neighbourPosition.xpos, neighbourPosition.ypos].State) aliveNeighbours++;
                    }

                    if (currentGeneration[x, y]
                        && (aliveNeighbours < 2 || aliveNeighbours > 3))
                    {
                        nextGeneration[x, y] = false;
                    }
                    else if (!currentGeneration[x, y]
                        && aliveNeighbours == 3)
                    {
                        nextGeneration[x, y] = true;
                    }
                }
            }

            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    currentGeneration[i, j].State = nextGeneration[i, j];
                }
            }

            return currentGeneration;
        }
    }
}
