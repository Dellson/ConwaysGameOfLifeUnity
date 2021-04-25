using System.Collections.Generic;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class Core
    {
        public static Dictionary<(int, int), GameObjectCell> Recalculate(Dictionary<(int, int), GameObjectCell> originGrid)
        {
            var tempGrid = new Dictionary<(int, int), bool>();
            foreach (var key in originGrid.Keys)
            {
                int neighbours = 0;
                tempGrid.Add(
                    key,
                    originGrid[key].Cell.State);

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;

                        var coords = (originGrid[key].Cell.X + i, originGrid[key].Cell.Y + j);
                        if (originGrid.ContainsKey(coords) && originGrid[coords].Cell.State) neighbours++;
                    }
                }

                if (originGrid[key].Cell.State
                    && (neighbours < 2 || neighbours > 3))
                {
                        tempGrid[key] = false;
                }
                else if (!originGrid[key].Cell.State
                    && neighbours == 3)
                {
                        tempGrid[key] = true;
                }
            }

            foreach (var key in tempGrid.Keys)
                originGrid[key].Cell.State = tempGrid[key];

            return originGrid;
        }
    }
}
