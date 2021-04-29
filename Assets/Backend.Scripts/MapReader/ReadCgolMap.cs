using System.IO;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.MapReader
{
    public class ReadCgolMap : IMapReader
    {
        private string[] map;
        public ReadCgolMap(string mapName)
        {
            map = File.ReadAllLines(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Assets\\Resources\\Maps",
                    $"{mapName}.cgol"));
        }

        public int GetMapHeight() => map.Length;

        public int GetMapWidth() => map[0].Length;

        public string[] ReadMapFile(string mapName, char trueVal, char falseVal) => map;
    }
}
