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
        private Dictionary<(int, int), GameObjectCell> Items = new Dictionary<(int, int), GameObjectCell>();
        private static int ticks = 0;
        private (int x, int y)[] localCoordinates;
        Stopwatch stopwatch;

        public void Start()
        {
            stopwatch  = Stopwatch.StartNew();
            IMapReader mapReader = new ReadPngMap();
            //IMapReader mapReader = new ReadCgolMap();
            var mapName = "puffer_train";
            var RawItems = MapParser.GetRawCellBoard(mapName, mapReader);

            foreach (var key in RawItems.Keys)
                Items.Add(key, new GameObjectCell(RawItems[key], TileTemplate, this.transform)); 
            
            localCoordinates = 
                new (int x, int y)[]
                 {
                    (-1, -1),
                    (-1, 0),
                    (-1, 1),
                    (0, -1),
                    (0, 1),
                    (1, -1),
                    (1, 0),
                    (1, 1)
                 };
        }

        public void Update()
        {
            Items = Core.Recalculate(localCoordinates, Items);

            foreach (var key in Items.Keys)
                Items[key].UpdateStateImage();
            
            ticks++;
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "\t\t" + ticks);
        }
    }
}
