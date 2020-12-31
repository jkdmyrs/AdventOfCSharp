namespace AoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Day10
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day10input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day10input.txt");

        private static IEnumerable<Adapter> Flatten(this IEnumerable<Adapter> a) => a.SelectMany(x => x.PossibleConnections.Flatten()).Concat(a);


        public static long Part1()
        {
            List<int> adapters = lines.Select(int.Parse).ToList();
            adapters.Add(adapters.Max() + 3);
            Adapter mainAdapter = new Adapter(0, null, null);

            mainAdapter.PossibleConnections = adapters
                .Where(x => x - mainAdapter.Jolts <= 3)
                .Select(x => new Adapter(x, mainAdapter, adapters))
                .ToList();

            Adapter finalAdapter = mainAdapter
                .PossibleConnections
                .Flatten()
                .First(x => x.PossibleConnections.Count == 0 
                    && x.IncompatableConnections.Count == 0);

            return finalAdapter.ChainLengthDiff1 * finalAdapter.ChainLengthDiff3;
        }

        public static long Part2()
        {
            throw new NotImplementedException();
        }

        private class Adapter
        {
            public Adapter(int jolts, Adapter parent, List<int> adapters)
            {
                Jolts = jolts;
                Parent = parent;
                if (Parent == null)
                {
                    PossibleConnections = new List<Adapter>();
                }
                else
                {
                    int[] availAdapArr = new int[adapters.Count];
                    adapters.CopyTo(availAdapArr);
                    List<int> availableAdapters = availAdapArr.ToList();
                    availableAdapters.Remove(this.Jolts);
                    PossibleConnections = (from adapterJolts in availableAdapters
                                           let diff = adapterJolts - this.Jolts
                                           where diff == 1
                                           select new Adapter(adapterJolts, this, availableAdapters.ToList())).ToList();
                    if (PossibleConnections.Count == 0)
                    {
                        PossibleConnections = (from adapterJolts in availableAdapters
                                               let diff = adapterJolts - this.Jolts
                                               where diff == 3
                                               select new Adapter(adapterJolts, this, availableAdapters.ToList())).ToList();
                    }
                    IncompatableConnections = (from adapterJolts in availableAdapters
                                               let diff = adapterJolts - this.Jolts
                                           where diff != 1 && diff != 3
                                           select new Adapter(adapterJolts, this, new List<int>())).ToList();
                }
            }

            public int Jolts { get; set; }
            public List<Adapter> PossibleConnections { get; set; }
            public List<Adapter> IncompatableConnections { get; set; }
            public Adapter Parent { get; set; }
            public int ChainLengthDiff3
            {
                get
                {
                    int diff3 = 0;
                    Adapter currAdapter = this;
                    do
                    {
                        int diff = currAdapter.Jolts - currAdapter.Parent.Jolts;
                        if (diff == 3) diff3++;
                        currAdapter = currAdapter.Parent;
                    } while (currAdapter.Parent != null);
                    return diff3;
                }
            }
            public int ChainLengthDiff1
            {
                get
                {
                    int diff1 = 0;
                    Adapter currAdapter = this;
                    while (currAdapter.Parent != null)
                    {
                        int diff = currAdapter.Jolts - currAdapter.Parent.Jolts;
                        if (diff == 1) diff1++;
                        currAdapter = currAdapter.Parent;
                    }
                    return diff1;
                }
            }
        }
    }
}
