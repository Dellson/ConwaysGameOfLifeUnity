using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Backend.Scripts.Model
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
