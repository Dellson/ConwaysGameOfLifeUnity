using System;
using System.Collections.Generic;
using ConwaysGameOfLife.Assets.Backend.Scripts.MapReader;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class MapParser
    {
        public static Dictionary<(int, int), Cell> GetDictMap(string mapName)
        {
            IMapReader mapReader = new ReadPngMap(mapName);
            //IMapReader mapReader = new ReadCgolMap(mapName);
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

        public static List<Cell> GetMapList(string mapName)
        {
            IMapReader mapReader = new ReadPngMap(mapName);
            //IMapReader mapReader = new ReadCgolMap(mapName);
            var aliveChar = 'x';
            var deadChar = '.';
            var DataToParse = mapReader.ReadMapFile(mapName, aliveChar, deadChar);
            var Items = new List<Cell>();

            for (int row = 0; row < mapReader.GetMapHeight(); row++)
            {
                for (int column = 0; column < mapReader.GetMapWidth(); column++)
                {
                    if (DataToParse[row][column] == aliveChar)
                        Items.Add(new Cell(row, column, true));
                    else if (DataToParse[row][column] == deadChar)
                        Items.Add(new Cell(row, column, false));
                    else
                        throw new ArgumentException();
                }
            }

            return Items;
        }

        public static int GetMapWidth(string mapName) => (new ReadPngMap(mapName)).GetMapWidth();
    }
}
