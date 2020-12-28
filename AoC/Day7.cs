namespace AoC
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Day7
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day7input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day7input.txt");

        private static HashSet<string> colors = new HashSet<string>();

        private static int bagTotal = 0;

        public static int Part1()
        {
            List<LuggageRule> masterRules = new List<LuggageRule>();
            foreach (var line in lines)
            {
                var rule = new LuggageRule(line);
                masterRules.Add(rule);
            }

            foreach (LuggageRule rule in masterRules)
            {
                if (rule.Contents.Any(x => x.Color == "shiny gold"))
                {
                    colors.Add(rule.Color);
                }
                else
                {
                    foreach (var contents in rule.Contents)
                    {
                        TraverseRule(rule, contents, masterRules);
                    }
                }
            }
            return colors.Count;
        }

        public static int Part2()
        {
            List<LuggageRule> masterRules = new List<LuggageRule>();
            foreach (var line in lines)
            {
                var rule = new LuggageRule(line);
                masterRules.Add(rule);
            }

            var shinyGoldRule = masterRules.First(x => x.Color == "shiny gold");
            foreach(var contents in shinyGoldRule.Contents)
            {
                bagTotal += contents.Quantity;
                TraverseRule(contents.Quantity, contents, masterRules);
            }
            return bagTotal;
        }

        private static void TraverseRule(int parentTotalBagCount, LuggageContentsRule rule, List<LuggageRule> masterRuleList)
        {
            var contents = rule.GetContents(masterRuleList);
            foreach (var innerRule in contents)
            {
                bagTotal += innerRule.Quantity * parentTotalBagCount;
                TraverseRule(innerRule.Quantity * parentTotalBagCount, innerRule, masterRuleList);
            }
        }

        private static void TraverseRule(LuggageRule topLevelRule, LuggageContentsRule rule, List<LuggageRule> masterRuleList)
        {
            var contents = rule.GetContents(masterRuleList);
            foreach (var innerRule in contents)
            {
                if (innerRule.Color == "shiny gold")
                {
                    colors.Add(topLevelRule.Color);
                }
                else
                {
                    TraverseRule(topLevelRule, innerRule, masterRuleList);
                }
            }
        }

        private class LuggageRule
        {
            public LuggageRule(string line)
            {
                this.Color = line.Substring(0, line.IndexOf("contain")).Replace("bags", string.Empty).Replace("bag", string.Empty).Trim();
                this.Contents = new List<LuggageContentsRule>();
                string contents = line.Substring(line.IndexOf("contain") + "contain".Length);
                contents = contents.Trim('.');
                if (!contents.Contains("no other bags"))
                {
                    var contentsArr = contents.Split(',');
                    foreach (var item in contentsArr)
                    {
                        var splitItem = item.Replace("bags", string.Empty).Replace("bag", string.Empty).Trim().Split(' ', 2);
                        Contents.Add(new LuggageContentsRule
                        {
                            Color = splitItem[1],
                            Quantity = int.Parse(splitItem[0])
                        });
                    }
                }
            }

            public string Color { get; set; }
            public List<LuggageContentsRule> Contents { get; set; }
        }

        private class LuggageContentsRule
        {
            public int Quantity { get; set; }
            public string Color { get; set; }
            public List<LuggageContentsRule> GetContents(List<LuggageRule> masterRuleList)
            {
                return masterRuleList.First(x => x.Color == this.Color).Contents;
            }
        }
    }
}
