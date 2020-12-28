using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public static class Day4
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day4input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day4input.txt");

        public static int Part1()
        {
            List<Dictionary<string, string>> passports = ParsePassports();
            int validPassports = 0;
            foreach (var passport in passports)
            {
                bool valid = VerifyPassport(passport);
                if (valid)
                {
                    validPassports++;
                }
            }
            return validPassports;
        }

        public static int Part2()
        {
            List<Dictionary<string, string>> passports = ParsePassports();
            int validPassports = 0;
            foreach (var passport in passports)
            {
                bool valid = VerifyPassport2(passport);
                if (valid)
                {
                    validPassports++;
                }
                else
                {
                    VerifyPassport2(passport);
                }
            }
            return validPassports;
        }

        public static bool VerifyPassport2(Dictionary<string, string> passport)
        {
            string value;

            if (!passport.TryGetValue("byr", out value)) return false;
            if (!VerifyYear(value, 1920, 2002)) return false;

            if (!passport.TryGetValue("iyr", out value)) return false;
            if (!VerifyYear(value, 2010, 2020)) return false;

            if (!passport.TryGetValue("eyr", out value)) return false;
            if (!VerifyYear(value, 2020, 2030)) return false;

            if (!passport.TryGetValue("hgt", out value)) return false;
            bool hasLabel = false;
            if (value.EndsWith("in"))
            {
                hasLabel = true;
                if (int.TryParse(value.Substring(0, value.Length - 2), out int hgt))
                {
                    if (hgt < 59 || hgt > 76) return false;
                }
            }
            if (value.EndsWith("cm"))
            {
                hasLabel = true;
                if (int.TryParse(value.Substring(0, value.Length - 2), out int hgt))
                {
                    if (hgt < 150 || hgt > 193) return false;
                }
            }
            if (!hasLabel) return false;

            if (!passport.TryGetValue("hcl", out value)) return false;
            if (!value.StartsWith('#') || value.Length != 7) return false;
            List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f' };
            foreach (var character in value.TrimStart('#').ToCharArray())
            {
                if (!validChars.Contains(character))
                {
                    return false;
                }
            }

            if (!passport.TryGetValue("ecl", out value)) return false;
            List<string> validClr = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!validClr.Contains(value)) return false;

            if (!passport.TryGetValue("pid", out value)) return false;
            if (value.Length != 9) return false;
            if (!int.TryParse(value.TrimStart('0'), out int _)) return false;

            return true;
        }

        public static bool VerifyYear(string value, int minYear, int maxYear)
        {
            if (value.Length != 4) return false;
            if (!int.TryParse(value, out int result)) return false;
            if (result < minYear || result > maxYear) return false;
            return true;
        }

        public static bool VerifyPassport(Dictionary<string, string> passport)
        {
            if (!passport.ContainsKey("byr"))
            {
                return false;
            }
            if (!passport.ContainsKey("iyr"))
            {
                return false;
            }
            if (!passport.ContainsKey("eyr"))
            {
                return false;
            }
            if (!passport.ContainsKey("hgt"))
            {
                return false;
            }
            if (!passport.ContainsKey("hcl"))
            {
                return false;
            }
            if (!passport.ContainsKey("ecl"))
            {
                return false;
            }
            if (!passport.ContainsKey("pid"))
            {
                return false;
            }
            return true;
        }

        public static List<Dictionary<string, string>> ParsePassports()
        {
            var passports = new List<Dictionary<string, string>>();

            Dictionary<string, string> passport = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(passport);
                    passport = new Dictionary<string, string>();
                }
                else
                {
                    string[] splitLine = line.Split(' ');
                    foreach (var item in splitLine)
                    {
                        var splitItem = item.Split(':');
                        passport.Add(splitItem[0], splitItem[1]);
                    }
                }
            }
            passports.Add(passport);
            return passports;
        }
    }
}
