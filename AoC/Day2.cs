namespace AoC
{
    using System.Linq;

    public static class Day2
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day2input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day2input.txt");

        public static int Part1()
        {
            int validCount = 0;
            foreach (var line in lines.Select(l => ParseLine(l)))
            {
                var ruleLetterCount = line.password.Where(x => x == line.rule.letter).Count();
                if (ruleLetterCount >= line.rule.min && ruleLetterCount <= line.rule.max)
                {
                    validCount++;
                }
            }
            return validCount;
        }

        public static int Part2()
        {
            int validCount = 0;
            foreach (var line in lines.Select(l => ParseLine(l)))
            {
                bool check1 = line.password[line.rule.min - 1] == line.rule.letter;
                bool check2 = line.password[line.rule.max - 1] == line.rule.letter;
                if ((check1 && !check2) || (check2 && !check1))
                {
                    validCount++;
                }
            }
            return validCount;
        }

        private static ((int min, int max, char letter) rule, string password) ParseLine(string line)
        {
            int spaceIndex = line.IndexOf(" ");
            int hyphIndex = line.IndexOf('-');
            int maxIndex = hyphIndex + 1;
            
            string password = line.Substring(line.IndexOf(':') + 2);
            int min = int.Parse(line.Substring(0, hyphIndex));
            int max = int.Parse(line.Substring(maxIndex, spaceIndex - maxIndex));
            char letter = line[spaceIndex + 1];

            return ((min, max, letter), password);
        }
    }
}
