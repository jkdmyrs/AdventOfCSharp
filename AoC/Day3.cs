namespace AoC
{
    using System.Collections.Generic;

    public static class Day3
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day3input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day3input.txt");

        public static int Part1()
        {
            int treeCount = 0;
            int lineIndex = 0;
            int positionIndex = 0;
            while (lineIndex < lines.Length)
            {
                if (lines[lineIndex][positionIndex % lines[lineIndex].Length] == '#')
                {
                    treeCount++;
                }
                lineIndex++;
                positionIndex += 3;
            }
            return treeCount;
        }

        public static long Part2()
        {
            List<(int over, int down)> tests = new List<(int over, int down)> { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            long result = 1;
            foreach (var test in tests)
            {
                int downInc = test.down;
                int overInc = test.over;

                int treeCount = 0;
                int lineIndex = 0;
                int positionIndex = 0;
                while (lineIndex < lines.Length)
                {
                    if (lines[lineIndex][positionIndex % lines[lineIndex].Length] == '#')
                    {
                        treeCount++;
                    }
                    lineIndex += downInc;
                    positionIndex += overInc;
                }
                result *= treeCount;
            }
            return result;
        }
    }
}
