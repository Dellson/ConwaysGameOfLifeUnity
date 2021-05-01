using System.Collections.Generic;
using UnityEngine;
using ConwaysGameOfLife.Assets.Backend.Scripts;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;
using System.Diagnostics;

namespace ConwaysGameOfLife.Assets.Frontend.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject TileTemplate;
        public int TilePixelSize = 8;
        private List<GameObjectCell> Items = new List<GameObjectCell>();
        private static int ticks = 0;
        Stopwatch stopwatch;
        int mapWidth;

        public void Start()
        {
            var mapName = "puffer_train";
            var cells = MapParser.GetMapList(mapName);
            mapWidth = MapParser.GetMapWidth(mapName);
            stopwatch = Stopwatch.StartNew();

            cells.ForEach(item =>
                Items.Add(
                    new GameObjectCell(new Cell(item), TileTemplate, this.transform, TilePixelSize)
                    ));
        }

        public void Update()
        {
            Items = Core.RecalculateWithShiftAlgorithm(Items, mapWidth);            
            ticks++;
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks);
        }
    }
}
