namespace AoC
{
    using System;
    using System.Collections.Generic;

    public static class Day5
    {
        //private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day5input.txt");
        private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day5input.txt");

        public static int Part1()
        {
            int max = 0;
            foreach (var line in lines)
            {
                var seatId = CalculateSeatId(line);
                if (seatId > max) max = seatId;
            }
            return max;
        }

        public static int Part2()
        {
            List<int> seats = new List<int>();
            foreach (var line in lines)
            {
                seats.Add(CalculateSeatId(line));
            }
            seats.Sort();

            for (int i = 0; i < seats.Count; i++)
            {
                var currentSeatId = seats[i] + 1;
                var nextSeatId = seats[i + 1];
                if (currentSeatId != nextSeatId)
                {
                    return seats[i] + 1;
                }
            }
            return 0;
        }

        private static int CalculateSeatId(string seatCode)
        {
            (int lower, int upper) rowSpace = (0, 127);
            (int lower, int upper) seatSpace = (0, 7);

            for (int i = 0; i < 10; i++)
            {
                if (seatCode[i] == 'F')
                {
                    int range = rowSpace.upper - rowSpace.lower;
                    rowSpace.upper = rowSpace.upper - (int)Math.Ceiling((decimal)range / 2);
                }
                else if (seatCode[i] == 'B')
                {
                    int range = rowSpace.upper - rowSpace.lower;
                    rowSpace.lower = rowSpace.lower + (int)Math.Ceiling((decimal)range / 2);
                }
                else if (seatCode[i] == 'L')
                {
                    int range = seatSpace.upper - seatSpace.lower;
                    seatSpace.upper = seatSpace.upper - (int)Math.Ceiling((decimal)range / 2);
                }
                else if (seatCode[i] == 'R')
                {
                    int range = seatSpace.upper - seatSpace.lower;
                    seatSpace.lower = seatSpace.lower + (int)Math.Ceiling((decimal)range / 2);
                }
            }
            return rowSpace.upper * 8 + seatSpace.upper;
        }
    }
}
