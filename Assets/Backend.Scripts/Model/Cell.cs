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
        public bool State { get; set; }

        private const int cellPixelSize = 64;
        public readonly Sprite Image;
        public readonly Vector3 Coordinates;


        public Cell((int x, int y) coord, bool state)
        {
            X = coord.x;
            Y = coord.y;
            State = state;

            Coordinates = new Vector3(coord.x * cellPixelSize, coord.y * cellPixelSize, 1);

            Image = Resources.Load<Sprite>($"Sprites/{state}");
        }

        /// <summary>
        /// resolve Spriete image!
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="imageHolder"></param>
        /// <param name="image"></param>
        public void ChangeImage(GameObject instance, string imageHolder, Sprite image = null)
        {
            GameObject changeImageObject = instance.transform.Find(imageHolder).gameObject;
            changeImageObject.GetComponent<SpriteRenderer>().sprite = image;
        }
    }
}
