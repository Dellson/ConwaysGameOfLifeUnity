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
        int mapLength;

        public void Start()
        {
            var mapName = "glider_gun";
            var cells = MapParser.GetMapList<PngMapReader>(mapName);
            mapWidth = MapParser.GetMapWidth();
            mapLength = MapParser.GetMapHeight();
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
            ticks++;
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks);
        }

        private GameObjectCell[,] CreateObjectsFromList(List<Cell> cells)
        {
            Items = new GameObjectCell[mapWidth, mapLength];
            foreach (var cell in cells)
            {
                Items[cell.X, cell.Y] = new GameObjectCell(cell, TileTemplate, this.transform, TilePixelSize);
            }
            return Items;
        }
    }
}
