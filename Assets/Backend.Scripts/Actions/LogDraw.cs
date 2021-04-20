using UnityEngine;

namespace ConwaysGameOfLife.Backend.Actions
{
    internal class LogDraw : MonoBehaviour, IDraw
    {
        public void Draw(bool[][] array)
        {
            (int x, int y) axesLengths = (array.Length, array[0].Length);

            for (int i = 0; i < axesLengths.x; i++)
            {
                Debug.Log('\n');
                for (int j = 0; j < axesLengths.y; j++)
                {
                    Debug.Log(array[i][j] ? '█' : '_');
                }
            }
        }
    }
}
