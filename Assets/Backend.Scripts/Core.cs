using System.Collections.Generic;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class Core
    {
        public static Dictionary<(int, int), GameObjectCell> Recalculate(Dictionary<(int, int), GameObjectCell> originGrid)
        {
            var tempGrid = new Dictionary<(int, int), bool>();
            (int x, int y)[] localCoords = {
                    (-1, -1),
                    (-1, 0),
                    (-1, 1),
                    (0, -1),
                    (0, 1),
                    (1, -1),
                    (1, 0),
                    (1, 1)
                };

            foreach (var key in originGrid.Keys)
            {
                int neighbours = 0;
                tempGrid.Add(
                    key,
                    originGrid[key].Cell.State);

                foreach (var (x, y) in localCoords)
                {
                    var coords = (originGrid[key].Cell.X + x, originGrid[key].Cell.Y + y);
                    if (originGrid.ContainsKey(coords) && originGrid[coords].Cell.State) neighbours++;
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
