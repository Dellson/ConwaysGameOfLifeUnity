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
        private List<GameObjectCell> Items = new List<GameObjectCell>();
        private static int ticks = 0;
        Stopwatch stopwatch;
        int mapWidth;

        public void Start()
        {
            var mapName = "puffer_train";
            stopwatch = Stopwatch.StartNew();

            var cells = MapParser.GetMapList<PngMapReader>(mapName);
            mapWidth = MapParser.GetMapWidth();

            cells.ForEach(cell => Items.Add(
                new GameObjectCell(cell, TileTemplate, this.transform)));
        }

        public void Update()
        {
            Items = Core.RecalculateWithShiftAlgorithm(Items, mapWidth);            
            ticks++;
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks);
        }
    }
}
