using System.Collections.Generic;
using UnityEngine;
using ConwaysGameOfLife.Assets.Backend.Scripts;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;
using ConwaysGameOfLife.Assets.Backend.Scripts.MapReader;

namespace ConwaysGameOfLife.Assets.Frontend.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject TileTemplate;
        private Dictionary<(int, int), GameObjectCell> Items = new Dictionary<(int, int), GameObjectCell>();
        private static int ticks = 0;

        public void Start()
        {
            IMapReader mapReader = new ReadPngMap();
            //IMapReader mapReader = new ReadCgolMap();
            var mapName = "puffer_train";
            var RawItems = MapParser.GetRawCellBoard(mapName, mapReader);

            foreach (var key in RawItems.Keys)
                Items.Add(key, new GameObjectCell(RawItems[key], TileTemplate, this.transform));
        }

        public void Update()
        {
            if (ticks++ == 10)
            {
                Items = Core.Recalculate(Items);

                foreach (var key in Items.Keys)
                    Items[key].UpdateStateImage();

                ticks = 0;
            }
        }
    }
}
