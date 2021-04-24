using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class GameObjectCell : ScriptableObject
    {
        public Cell Cell { get; private set; }
        private Vector3 Coordinates;
        private readonly GameObject AssociatedGameObject;
        private const int CellPixelSize = 64;

        public GameObjectCell((int x, int y) coords, bool state, GameObject tileTemplate, Transform transformToAttachTo) 
            : this(new Cell(coords, state), tileTemplate, transformToAttachTo) { }

        public GameObjectCell(Cell cell, GameObject tileTemplate, Transform transformToAttachTo)
        {
            Cell = cell;
            Coordinates = new Vector3(Cell.X * CellPixelSize / 2, Cell.Y * CellPixelSize / 2, 1);

            AssociatedGameObject = Instantiate(tileTemplate, Coordinates, Quaternion.identity);
            AssociatedGameObject.name = $"Cell({Cell.X})-({Cell.Y})";

            UpdateStateImage(Cell.State, true);
            AssociatedGameObject.transform.SetParent(transformToAttachTo, false);
        }

        public void UpdateStateImage(bool newState, bool forceUpdate = false)
        {
            if (forceUpdate || Cell.State != newState)
                SpriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/{newState}");
        }

        public void UpdateStateImage() =>
            SpriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/{Cell.State}");

        public Cell GetCellByValue() => 
            new Cell((Cell.X, Cell.Y), Cell.State);

        private SpriteRenderer SpriteRenderer => 
            AssociatedGameObject.transform.Find("ImageHolder").gameObject.GetComponent<SpriteRenderer>();
    }
}
