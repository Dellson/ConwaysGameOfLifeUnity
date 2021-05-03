using System.Collections.Generic;
using UnityEngine;
using ConwaysGameOfLife.Assets.Backend.Scripts;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;
using System.Diagnostics;
using ConwaysGameOfLife.Assets.Backend.Scripts.MapReader;

namespace ConwaysGameOfLife.Assets.Frontend.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject TileTemplate;
        public int TilePixelSize = 8;
        private GameObjectCell[,] Items;
        private static int ticks = 0;
        Stopwatch stopwatch;
        int mapWidth;
        int mapHeight;

        public void Start()
        {
            var mapName = "puffer_train";
            var cells = MapParser.GetMapList<PngMapReader>(mapName);
            mapWidth = MapParser.GetMapWidth();
            mapHeight = MapParser.GetMapHeight();
            stopwatch = Stopwatch.StartNew();
            CreateObjectsFromList(cells);
        }

        public void Update()
        {
            Items = Core.RecalculateWith2DArrayAlgorithm(Items);
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks++);
            cells.ForEach(cell => Items.Add(
                new GameObjectCell(cell, TileTemplate, this.transform)));
        }

        private GameObjectCell[,] CreateObjectsFromList(List<Cell> cells)
        {
            Items = new GameObjectCell[mapHeight,mapWidth];

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    var match = cells.Find(cell => cell.Y == j && cell.X == i);
                    Items[i,j] = new GameObjectCell(match, TileTemplate, this.transform, TilePixelSize);
                }
            }

            return Items;
        }
    }
}
