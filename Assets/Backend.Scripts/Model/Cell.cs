using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Backend.Scripts.Model
{
    public class Cell : ScriptableObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// 0 stands for DEAD
        /// 1 stands for ALIVE
        /// </summary>
        public bool State { get; set; } = false;

        private const int cellPixelSize = 64;
        public Sprite Image;
        public Vector3 Coordinates;
        public GameObject thisObject;


        public Cell((int x, int y) coord, bool state)
        {
            X = coord.x;
            Y = coord.y;
            State = state;

            Coordinates = new Vector3(coord.x * cellPixelSize / 2, coord.y * cellPixelSize / 2, 1);

            Image = Resources.Load<Sprite>($"Sprites/{state}");
        }

        /// <summary>
        /// resolve Spriete image!
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="imageHolder"></param>
        /// <param name="image"></param>
        public void ChangeImage(bool state)//Sprite image = null)
        {
            if (true)
            {
                string imageHolder = "ImageHolder";
                GameObject changeImageObject = thisObject.transform.Find(imageHolder).gameObject;
                changeImageObject.GetComponent<SpriteRenderer>().sprite = 
                    Resources.Load<Sprite>($"Sprites/{state}"); ;
            }
        }
    }
}
