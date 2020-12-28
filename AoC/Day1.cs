namespace AoC
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Day1
    {
        private static List<int> list = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day1input.txt").Select(x => int.Parse(x)).ToList();

        public static int Part1()
        {
            for (int j = 0; j < list.Count - 1; j++)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[j] + list[i] == 2020)
                    {
                        return list[j] * list[i];
                    }
                }
            }
            return -1;
        }

        public static int Part2()
        {
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    for (int k = 2; k < list.Count; k++)
                    {
                        if (list[j] + list[i] + list[k] == 2020)
                        {
                            return list[j] * list[i] * list[k];
                        }
                    }
                }
            }
            return -1;
        }
    }
}
