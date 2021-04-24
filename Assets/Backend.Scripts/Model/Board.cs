using System.Collections.Generic;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class Board
    {
        public Dictionary<(int, int), Cell> Items;

        public Board()
        {

        }

        public Board(Dictionary<(int, int), Cell> items)
        {
            Items = items;
        }


    }
}
