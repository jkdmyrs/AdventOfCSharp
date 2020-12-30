namespace AoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Day9
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day9input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day9input.txt");

        public static long Part1()
        {
            var xmasCert = new XmasCertificate(lines, 25);
            return xmasCert.PrivateKey;
        }

        public static long Part2()
        {
            var xmasCert = new XmasCertificate(lines, 25);
            return xmasCert.Weakness;
        }

        private class XmasCertificate
        {
            private long _searchIndex;

            public XmasCertificate(string[] lines, long preambleLength)
            {
                _searchIndex = preambleLength;
                PreambleLength = preambleLength;
                Certificate = lines.Select(x => long.Parse(x)).ToArray();
            }

            public long[] Certificate { get; private set; }

            private long PreambleLength { get; set; }

            private long[] SearchSpace
            {
                get
                {
                    long diff = _searchIndex - PreambleLength;
                    int skipLength = (int)diff;
                    return Certificate.Skip(skipLength).Take((int)PreambleLength).ToArray();
                }
            }

            public long PrivateKey
            {
                get
                {
                    return SolvePrivateKey();
                }
            }

            public long Weakness
            {
                get
                {
                    return SolveWeakness();
                }
            }

            private bool IsNext
            {
                get
                {
                    return _searchIndex < Certificate.Length;
                }
            }

            private long SolvePrivateKey()
            {
                bool foundPreviousSum = true;
                while (foundPreviousSum && IsNext)
                {
                    long searchSum = Certificate[_searchIndex];
                    if (SearchSpace.Any(x => x >= searchSum/2))
                    {
                        List<(long x, long y)> answers = (from x in SearchSpace
                                from y in SearchSpace
                                let z = x + y
                                where z == searchSum
                                select (x, y)).ToList();
                        answers.RemoveAll(x => x.x == x.y && answers.Where(y => y.x == x.x).Count() < 2);
                        foundPreviousSum = answers.Any();
                        if (foundPreviousSum)
                        {
                            _searchIndex++;
                        }
                    }
                    else
                    {
                        foundPreviousSum = false;
                    }
                }
                return Certificate[_searchIndex];
            }

            private long SolveWeakness()
            {
                long searchSum = PrivateKey;
                long sum = 0;
                long min = 0;
                long max = 0;
                for (int i = 0; i < Certificate.Length; i++)
                {
                    if (sum == 0)
                    {
                        min = Certificate[i];
                    }
                    sum += Certificate[i];
                    for (int j = i + 1; j < Certificate.Length; j++)
                    {
                        min = Math.Min(min, Certificate[j]);
                        max = Math.Max(max, Certificate[j]);
                        sum += Certificate[j];
                        if (sum == searchSum)
                        {
                            return min + max;
                        }
                        if (sum > searchSum) break;
                    }
                    sum = 0;
                    min = 0;
                    max = 0;
                }
                throw new Exception("Answer not found");
            }
        }
    }
}
