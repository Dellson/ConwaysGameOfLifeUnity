using System.Text;
using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.MapReader
{
    public class ReadPngMap : IMapReader
    {
        private readonly Texture2D map;

        public ReadPngMap(string mapName)
        {
            map = Resources.Load<Texture2D>($"Maps\\{mapName}");
        }
        public int GetMapHeight() => map.height;
        public int GetMapWidth() => map.width;

        public string[] ReadMapFile(string mapName, char trueVal, char falseVal)
        {
            var Output = new string[map.height];

            for (int row = 0; row < map.height; row++)
            {
                var sb = new StringBuilder();
                for (int column = 0; column < map.width; column++)
                {
                    var color = map.GetPixel(column, row);
                    sb.Append(
                        color.maxColorComponent == 0f ? trueVal : falseVal);
                }
                Output[row] = sb.ToString();
            }

            return Output;
        }
    }
}
