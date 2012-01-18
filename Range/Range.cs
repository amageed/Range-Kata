using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Range
{
    public class Range
    {
        const string RangePattern = 
        @"^             # start of string
        [([]            # character class of opening parens or brackets
        (?<Start>\d+)   # named group Start, one or more numbers
        ,               # literal comma
        (?<End>\d+)     # named group End, one or more numbers
        [)\]]           # character class of closing parens or bracket
        $               # end of string";

        #region Properties
        public string Input { get; private set; }
        public int Start { get; private set; }
        public int End { get; private set; }

        public bool IsInclusiveStart
        {
            get
            {
                return Input.StartsWith("[");
            }
        }

        public bool IsInclusiveEnd
        {
            get
            {
                return Input.EndsWith("]");
            }
        }

        public IEnumerable<int> Sequence
        {
            get
            {
                return GenerateSequence();
            }
        }
        #endregion

        #region Constructor
        public Range(string input)
        {
            Match m = Regex.Match(input, RangePattern, RegexOptions.IgnorePatternWhitespace);
            if (m.Success)
            {
                Input = input;
                SetStart(m.Groups["Start"].Value);
                SetEnd(m.Groups["End"].Value);
            }
            else
            {
                throw new ArgumentException("Invalid input.");
            }
        }
        #endregion

        #region Methods
        private void SetStart(string input)
        {
            int value = int.Parse(input);
            if (IsInclusiveStart)
                Start = value;
            else
                Start = value + 1;
        }

        private void SetEnd(string input)
        {
            int value = int.Parse(input);
            if (IsInclusiveEnd)
                End = value;
            else
                End = value - 1;
        }

        private IEnumerable<int> GenerateSequence()
        {
            for (int i = Start; i <= End; i++)
            {
                yield return i;
            }
        }

        public bool Contains(Range other)
        {
            if (other == null)
                return false;

            return Start <= other.Start && End >= other.End;
        }

        public bool Equals(Range other)
        {
            if (other == null)
                return false;

            return Input == other.Input;
        }
        #endregion
    }
}
