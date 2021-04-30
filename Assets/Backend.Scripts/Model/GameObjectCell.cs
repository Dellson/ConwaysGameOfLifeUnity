using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class GameObjectCell : ScriptableObject
    {
        public Cell Cell { get; private set; }
        private Vector3 Coordinates;
        private readonly GameObject AssociatedGameObject;
        private readonly SpriteRenderer SpriteRenderer;
        private const int CellPixelSize = 8;

        public GameObjectCell((int x, int y) coords, bool state, GameObject tileTemplate, Transform transformToAttachTo) 
            : this(new Cell(coords, state), tileTemplate, transformToAttachTo) { }

        public GameObjectCell(Cell cell, GameObject tileTemplate, Transform transformToAttachTo)
        {
            Cell = cell;
            Coordinates = new Vector3(Cell.X * CellPixelSize / 2, Cell.Y * CellPixelSize / 2, 1);

            AssociatedGameObject = Instantiate(tileTemplate, Coordinates, Quaternion.identity);
            AssociatedGameObject.name = $"Cell({Cell.X})-({Cell.Y})";

            SpriteRenderer = AssociatedGameObject.transform.Find("ImageHolder").gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/true");
            UpdateStateImage();

            AssociatedGameObject.transform.SetParent(transformToAttachTo, false);
        }

        public void UpdateStateImage()
        {
            if (Cell.State != Cell.PreviousState)
            {
                SpriteRenderer.enabled = Cell.State;
                Cell.PreviousState = Cell.State;
            }
        }            
    }
}
