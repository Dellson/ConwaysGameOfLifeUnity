using System.Collections.Generic;
using System.Linq;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class Core
    {
        public static List<GameObjectCell> Recalculate(int[] neighboursOffsets, List<GameObjectCell> currentGeneration, int mapWidth, int mapHeight)
        {
            var count = currentGeneration.Count;
            var nextGeneration = new List<bool>(currentGeneration.Select(goc => goc.Cell.State));

            for (int index = 0; index < count; index++)
            {
                int aliveNeighbours = 0;
                neighboursOffsets =
                    new int[]
                    {
                        index - mapWidth - 1,
                        index - mapWidth,
                        index - mapWidth + 1,
                        index - 1,
                        index + 1,
                        index + mapHeight  - 1,
                        index + mapHeight,
                        index + mapHeight + 1
                    };

                foreach (var offset in neighboursOffsets)
                {
                    if (offset >= count || offset < 0) continue;
                    if (currentGeneration[offset].Cell.State) aliveNeighbours++;
                }

                if (currentGeneration[index].Cell.State
                    && (aliveNeighbours < 2 || aliveNeighbours > 3))
                {
                    nextGeneration[index] = false;
                }
                else if (!currentGeneration[index].Cell.State
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
