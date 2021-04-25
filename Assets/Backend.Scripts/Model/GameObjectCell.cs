using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class GameObjectCell : ScriptableObject
    {
        public Cell Cell { get; private set; }
        private Vector3 Coordinates;
        private readonly GameObject AssociatedGameObject;
        private const int CellPixelSize = 32;

        public GameObjectCell((int x, int y) coords, bool state, GameObject tileTemplate, Transform transformToAttachTo) 
            : this(new Cell(coords, state), tileTemplate, transformToAttachTo) { }

        public GameObjectCell(Cell cell, GameObject tileTemplate, Transform transformToAttachTo)
        {
            Cell = cell;
            Coordinates = new Vector3(Cell.X * CellPixelSize / 2 - 500, Cell.Y * CellPixelSize / 2 - 500, 1);

            AssociatedGameObject = Instantiate(tileTemplate, Coordinates, Quaternion.identity);
            AssociatedGameObject.name = $"Cell({Cell.X})-({Cell.Y})";

            UpdateStateImage();
            AssociatedGameObject.transform.SetParent(transformToAttachTo, false);
        }

        public void UpdateStateImage() =>
            SpriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/{Cell.State}");

        /// <summary>
        /// "ImageHolder" is a prefab's child GameObject holding SpriteRenderer
        /// </summary>
        private SpriteRenderer SpriteRenderer => 
            AssociatedGameObject.transform.Find("ImageHolder").gameObject.GetComponent<SpriteRenderer>();
    }
}
