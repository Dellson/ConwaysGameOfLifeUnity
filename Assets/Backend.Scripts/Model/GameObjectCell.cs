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

        public GameObjectCell((int x, int y) coords, bool state, GameObject tileTemplate, Transform transformToAttachTo) 
            : this(new Cell(coords, state), tileTemplate, transformToAttachTo) { }

        // TODO consider creating a factory
        public GameObjectCell(Cell cell, GameObject tileTemplate, Transform transformToAttachTo)
        {
            var cellPixelSize = tileTemplate.transform.Find("ImageHolder").GetComponent<RectTransform>().rect.width; // .rect.height will work the same as long as prefab is a square...
            var gameBoardRect = transformToAttachTo.parent.GetComponent<RectTransform>().rect;
            var (width, height) = (gameBoardRect.width, gameBoardRect.height);

            Vector3 Coordinates = new Vector3(
                cell.Y * cellPixelSize / 2 - (height / 2), 
                cell.X * cellPixelSize / 2 - (width / 2), 
                1);

            AssociatedGameObject = Instantiate(tileTemplate, Coordinates, Quaternion.identity);
            AssociatedGameObject.name = $"Cell({cell.Y})-({cell.X})";

            SpriteRenderer = AssociatedGameObject.transform.Find("ImageHolder").gameObject.GetComponent<SpriteRenderer>();
            State = cell.State;

            AssociatedGameObject.transform.SetParent(transformToAttachTo, false);
        }
    }
}
