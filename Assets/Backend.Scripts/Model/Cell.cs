namespace ConwaysGameOfLife.Assets.Backend.Scripts.Model
{
    public class Cell
    {
        public int Y { get; set; }
        public int X { get; set; }
        public bool State { get; set; } = false;
        public bool PreviousState { get; set; } = false;


        public Cell((int y, int x) coord, bool state)
        {
            Y = coord.y;
            X = coord.x;
            State = state;
            PreviousState = !state;
        }

        public Cell(int y, int x, bool state)
        {
            Y = y;
            X = x;
            State = state;
            PreviousState = !state;
        }

        public Cell(Cell cellToCopy)
        {
            Y = cellToCopy.Y;
            X = cellToCopy.X;
            State = cellToCopy.State;
            PreviousState = cellToCopy.PreviousState;
        }
    }
}
