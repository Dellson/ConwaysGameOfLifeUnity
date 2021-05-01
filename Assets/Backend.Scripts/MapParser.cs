﻿using System;
using System.Collections.Generic;
using ConwaysGameOfLife.Assets.Backend.Scripts.MapReader;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Backend.Scripts
{
    public class MapParser
    {
        public static IMapReader mapReader;
        public static Dictionary<(int, int), Cell> GetDictMap<T>(string mapName) where T : IMapReader
        {
            mapReader = (T)Activator.CreateInstance(typeof(T), mapName);
            var alive = true;
            var dead = false;
            var aliveChar = 'x';
            var deadChar = '.';
            var DataToParse = mapReader.ReadMapFile(mapName, aliveChar, deadChar);
            var Items = new Dictionary<(int, int), Cell>();

            for (int i = 0; i < mapReader.GetMapHeight(); i++)
            {
                for (int j = 0; j < mapReader.GetMapWidth(); j++)
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

        public static List<Cell> GetMapList<T>(string mapName) where T : IMapReader
        {
            mapReader = (T)Activator.CreateInstance(typeof(T), mapName);
            var aliveChar = 'x';
            var deadChar = '.';
            var DataToParse = mapReader.ReadMapFile(mapName, aliveChar, deadChar);
            var Items = new List<Cell>();

            for (int row = 0; row < mapReader.GetMapHeight(); row++)
            {
                for (int column = 0; column < mapReader.GetMapWidth(); column++)
                {
                    if (DataToParse[row][column] == aliveChar)
                        Items.Add(new Cell(column, mapReader.GetMapHeight() - row - 1, true));
                    else if (DataToParse[row][column] == deadChar)
                        Items.Add(new Cell(column, mapReader.GetMapHeight() - row - 1, false));
                    else
                        throw new ArgumentException();
                }
            }

            return Items;
        }

        public static int GetMapWidth() => mapReader.GetMapWidth();
    }
}
