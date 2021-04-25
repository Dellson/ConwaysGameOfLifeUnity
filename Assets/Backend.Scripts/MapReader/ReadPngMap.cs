using System.Text;
using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.MapReader
{
    public class ReadPngMap : IMapReader
    {
        public string[] ReadMapFile(string mapName, char trueVal, char falseVal)
        {
            var map = Resources.Load<Texture2D>($"Maps\\{mapName}");
            var Output = new string[map.width];

            for (int i = 0; i < map.width; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < map.height; j++)
                {
                    var color = map.GetPixel(i, j);
                    sb.Append(
                        color.maxColorComponent == 0f ? trueVal : falseVal);
                }
                Output[i] = sb.ToString();
            }

            return Output;
        }
    }
}
