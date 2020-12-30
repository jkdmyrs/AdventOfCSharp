namespace AoC
{
    using System;
    using System.Collections.Generic;

    public static class Day8
    {
        private static string[] lines = System.IO.File.ReadAllLines(@"/Users/jack/projects/aoc/AoC/input/day8input.txt");
        //private static string[] lines = System.IO.File.ReadAllLines(@"Z:\projects\aoc\AoC\input\day8input.txt");

        public static int Part1()
        {
            var program = new Program(lines);
            program.Execute();
            return program.acc;
        }

        public static int Part2()
        {
            int i = 0;
            foreach (var line in lines)
            {
                var instr = new Instruction(line);
                if (instr.Op == Op.jmp || instr.Op == Op.nop)
                {
                    var program = new Program(lines);
                    program.Instructions[i].Op = program.Instructions[i].Op == Op.nop ? Op.jmp : Op.nop;
                    if (program.Execute())
                    {
                        return program.acc;
                    }
                }
                i++;
            }
            throw new Exception("answer not found");
        }

        private enum Op
        {
            acc,
            jmp,
            nop
        }

        private class Program
        {
            public int acc;
            private int _pc;
            private HashSet<Guid> _exHist;

            public Program(string[] lines)
            {
                acc = 0;
                _pc = 0;
                _exHist = new HashSet<Guid>();
                Instructions = new Instruction[lines.Length];
                int i = 0;
                foreach (var line in lines)
                {
                    Instructions[i] = new Instruction(line);
                    i++;
                }
            }

            public Instruction[] Instructions { get; set; }
            private bool IsNext
            {
                get
                {
                    if (_pc < Instructions.Length)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public bool Execute()
            {
                bool cont = true;
                while (cont && IsNext)
                {
                    cont = Execute(Instructions[_pc]);
                }
                return !IsNext;
            }

            public bool Execute(Instruction instruction)
            {
                if (_exHist.Add(instruction.Id))
                {
                    switch (instruction.Op)
                    {
                        case Op.acc:
                            acc += instruction.Arg;
                            _pc++;
                            break;
                        case Op.jmp:
                            _pc += instruction.Arg;
                            break;
                        case Op.nop:
                            _pc++;
                            break;
                    }
                    return true;
                }
                return false;
            }
        }

        private class Instruction
        {
            public Instruction(string line)
            {
                string[] splitLine = line.Split(' ');
                Op = (Op)Enum.Parse(typeof(Op), splitLine[0]);
                if (Op != Op.nop)
                {
                    Arg = int.Parse(splitLine[1].ToString().Trim('+'));
                }
                Id = Guid.NewGuid();
            }

            public Guid Id { get; set; }
            public Op Op { get; set; }
            public int Arg { get; set; }
        }
    }
}
