using System.Collections.Generic;
using UnityEngine;
using ConwaysGameOfLife.Assets.Backend.Scripts;
using ConwaysGameOfLife.Assets.Backend.Scripts.Model;

namespace ConwaysGameOfLife.Assets.Frontend.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject TileTemplate;
        private Dictionary<(int, int), Cell> RawItems;
        private Dictionary<(int, int), GameObjectCell> Items;
        private Core _core;
        private static int ticks = 0;

        public void Start()
        {
            _core = new Core();
            var parser = new MapParser("level_1");
            RawItems = parser.GetRawCellBoard();
            Items = new Dictionary<(int, int), GameObjectCell>();

            foreach (var key in RawItems.Keys)
                Items.Add(key, new GameObjectCell(RawItems[key], TileTemplate, this.transform));
        }

        public void Update()
        {
            if (ticks++ == 200)
            {
                Items = _core.Recalculate(Items);

                foreach (var key in Items.Keys)
                    Items[key].UpdateStateImage();

                ticks = 0;
            }
        }
    }
}
