namespace AoC
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Day6
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day6input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day6input.txt");

        public static int Part1()
        {
            int sum = 0;
            var parsed = ParseQuestions();
            foreach (var group in parsed)
            {
                sum += group.chars.Distinct().Count();
            }
            return sum;
        }

        public static int Part2()
        {
            int sum = 0;
            var parsed = ParseQuestions();
            foreach (var group in parsed)
            {
                foreach (var character in group.chars.Distinct())
                {
                    if (group.chars.Where(x => x == character).Count() == group.numPeople)
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        public static List<(List<char> chars, int numPeople)> ParseQuestions()
        {
            List<(List<char> chars, int numPeople)> parsed = new List<(List<char> chars, int numPeople)>();

            List<char> groupChar = new List<char>();
            int lineCount = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    parsed.Add((groupChar, lineCount));
                    groupChar = new List<char>();
                    lineCount = 0;
                }
                else
                {
                    lineCount++;
                    groupChar.AddRange(line.ToCharArray());
                }
            }
            parsed.Add((groupChar, lineCount));
            return parsed;
        }
    }
}
