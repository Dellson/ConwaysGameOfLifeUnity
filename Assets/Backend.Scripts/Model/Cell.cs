namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool State { get; set; } = false;
        public bool PreviousState { get; set; } = false;


        public Cell((int x, int y) coord, bool state)
        {
            X = coord.x;
            Y = coord.y;
            State = state;
            PreviousState = !state;
        }

        public Cell(int x, int y, bool state)
        {
            X = x;
            Y = y;
            State = state;
            PreviousState = !state;
        }

        public Cell(Cell cellToCopy)
        {
            X = cellToCopy.X;
            Y = cellToCopy.Y;
            State = cellToCopy.State;
            PreviousState = cellToCopy.PreviousState;
        }
    }
}
