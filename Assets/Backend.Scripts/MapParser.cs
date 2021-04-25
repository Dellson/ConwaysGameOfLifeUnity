using System.Collections.Generic;
using ConwaysGameOfLife.Assets.Backend.Scripts.MapReader;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class MapParser
    {
        public static Dictionary<(int, int), Cell> GetRawCellBoard(string mapName, IMapReader mapReader)
        {
            var alive = true;
            var dead = false;
            var aliveChar = 'x';
            var deadChar = '.';
            var DataToParse = mapReader.ReadMapFile(mapName, aliveChar, deadChar);
            var Items = new Dictionary<(int, int), Cell>();

            for (int i = 0; i < DataToParse.Length; i++)
            {
                for (int j = 0; j < DataToParse[0].Length; j++)
                {
                    bool state = false;

                    if (DataToParse[i][j] == aliveChar)
                        state = alive;
                    else if (DataToParse[i][j] == deadChar)
                        state = dead;

                    var cell = new Cell((i, j), state);
                    Items.Add((i, j), cell);
                }
            }

            return Items;
        }
    }
}
