namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool State { get; set; } = false;


        public Cell((int x, int y) coord, bool state)
        {
            X = coord.x;
            Y = coord.y;
            State = state;
        }
    }
}
