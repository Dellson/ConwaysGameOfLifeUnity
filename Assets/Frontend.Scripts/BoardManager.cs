using System.Collections.Generic;
using UnityEngine;
using ConwaysGameOfLife.Assets.Backend.Scripts;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;
using ConwaysGameOfLife.Assets.Backend.Scripts.MapReader;
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
            stopwatch  = Stopwatch.StartNew();
            var mapName = "puffer_train";
            var RawItems = MapParser.GetMapList(mapName);
            mapWidth = MapParser.GetMapWidth(mapName);

            foreach (var item in RawItems)
                Items.Add(
                    new GameObjectCell(new Cell(item), TileTemplate, this.transform, TilePixelSize)
                    );
        }

        public void Update()
        {
            Items = Core.Recalculate(Items, mapWidth);            
            ticks++;
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks);
        }
    }
}
