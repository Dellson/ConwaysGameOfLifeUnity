using System.Collections.Generic;
using System.Linq;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class Core
    {
        public static Dictionary<(int, int), GameObjectCell> Recalculate((int x, int y)[] localCoordinates, Dictionary<(int, int), GameObjectCell> originGrid)
        {
            var tempGrid = new Dictionary<(int, int), bool>();

            foreach (var key in originGrid.Keys)
            {
                int neighbours = 0;
                tempGrid.Add(
                    key,
                    originGrid[key].Cell.State);

                foreach (var (x, y) in localCoordinates)
                {
                    var coords = (originGrid[key].Cell.X + x, originGrid[key].Cell.Y + y);
                    if (!originGrid.ContainsKey(coords)) break;
                    if (originGrid[coords].Cell.State) neighbours++;
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
            tempGrid.Keys.ToList().ForEach(key => originGrid[key].Cell.State = tempGrid[key]);
            //foreach (var key in tempGrid.Keys)
            //    originGrid[key].Cell.State = tempGrid[key];

            return originGrid;
        }
    }
}
