using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class GameObjectCell : ScriptableObject
    {
        public bool State
        {
            get => SpriteRenderer.enabled;
            set => SpriteRenderer.enabled = value;
        }
        private readonly GameObject AssociatedGameObject;
        private readonly SpriteRenderer SpriteRenderer;

        public GameObjectCell((int x, int y) coords, bool state, GameObject tileTemplate, Transform transformToAttachTo, int cellPixelSize) 
            : this(new Cell(coords, state), tileTemplate, transformToAttachTo, cellPixelSize) { }

        // TODO consider creating a factory
        public GameObjectCell(Cell cell, GameObject tileTemplate, Transform transformToAttachTo, int cellPixelSize)
        {
            Vector3 Coordinates = new Vector3(cell.Y * cellPixelSize / 2, cell.X * cellPixelSize / 2, 1);
            AssociatedGameObject = Instantiate(tileTemplate, Coordinates, Quaternion.identity);
            AssociatedGameObject.name = $"Cell({cell.Y})-({cell.X})";

            SpriteRenderer = AssociatedGameObject.transform.Find("ImageHolder").gameObject.GetComponent<SpriteRenderer>();
            State = cell.State;

            AssociatedGameObject.transform.SetParent(transformToAttachTo, false);
        }
    }
}
