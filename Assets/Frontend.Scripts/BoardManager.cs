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
        // private List<GameObjectCell> Items = new List<GameObjectCell>();
        private GameObjectCell[,] Items;
        private static int ticks = 0;
        Stopwatch stopwatch;
        int mapWidth;
        int mapHeight;

        public void Start()
        {
            var mapName = "glider_gun";
            var cells = MapParser.GetMapList<PngMapReader>(mapName);
            mapWidth = MapParser.GetMapWidth();
            mapHeight = MapParser.GetMapHeight();
            stopwatch = Stopwatch.StartNew();
            CreateObjectsFromList(cells);

            //cells.ForEach(cell =>
            //    Items.Add(
            //        new GameObjectCell(cell, TileTemplate, this.transform, TilePixelSize)
            //        ));
        }

        public void Update()
        {
            Items = Core.RecalculateWith2DArrayAlgorithm(Items, mapWidth);
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks++);
        }

        private GameObjectCell[,] CreateObjectsFromList(List<Cell> cells)
        {
            Items = new GameObjectCell[mapHeight, mapWidth];
            foreach (var cell in cells)
            {
                Items[cell.Y, cell.X] = new GameObjectCell(cell, TileTemplate, this.transform, TilePixelSize);
            }
            return Items;
        }
    }
}
