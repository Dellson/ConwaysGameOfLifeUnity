using System;

namespace ConwaysGameOfLife.Backend.Actions
{
    internal class ConsoleDraw : IDraw
    {
        public void Draw(bool[][] array)
        {
            (int x, int y) axesLengths = (array.Length, array[0].Length);

            for (int i = 0; i < axesLengths.x; i++)
            {
                Console.Write('\n');
                for (int j = 0; j < axesLengths.y; j++)
                {
                    Console.Write(array[i][j] ? '█' : ' ');
                }
            }
        }
    }
}
