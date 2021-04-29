using System.Collections.Generic;
using System.Linq;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class Core
    {
        public static List<GameObjectCell> Recalculate(int[] neighbourPositions, List<GameObjectCell> currentGeneration, int mapWidth, int mapHeight)
        {
            var count = currentGeneration.Count;
            var currGeneration = new List<bool>(currentGeneration.Select(goc => goc.Cell.State));
            var nextGeneration = new List<bool>(currentGeneration.Select(goc => goc.Cell.State));

            for (int index = 0; index < count; index++)
            {
                int aliveNeighbours = 0;
                neighbourPositions =
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
                currentGeneration[i].Cell.State = nextGeneration[i];
            }

            return currentGeneration;
        }
    }
}
