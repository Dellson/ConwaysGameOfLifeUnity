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
        private List<GameObjectCell> Items = new List<GameObjectCell>();
        private static int ticks = 0;
        private int[] neighboursOffets;
        Stopwatch stopwatch;
        int mapWidth;
        int mapHeight;
        static int tick = 0;

        public void Start()
        {
            stopwatch  = Stopwatch.StartNew();
            var mapName = "puffer_train";
            IMapReader mapReader = new ReadPngMap(mapName);
            //IMapReader mapReader = new ReadCgolMap(mapName);
            this.mapWidth = mapReader.GetMapWidth();
            mapHeight = mapReader.GetMapHeight();
            var RawItems = MapParser.GetMapList(mapName, mapReader);

            foreach (var item in RawItems)
                Items.Add(new GameObjectCell(new Cell(item), TileTemplate, this.transform));
        }

        public void Update()
        {
            Items = Core.Recalculate(neighboursOffets, Items, mapWidth, mapHeight);

            Items.ForEach(i => i.UpdateStateImage());
            
            ticks++;
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks);
        }
    }
}
