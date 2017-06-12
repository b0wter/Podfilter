using System;

namespace Podfilter.Models
{
    public class StringFilter : BaseFilter<StringFilter.StringFilterMethod, string>
    {
        public bool CaseInvariant { get; set; } = true;
        
        public StringFilter()
        {
            // Constructor for deserialization.
        }

        public StringFilter(string argument, StringFilterMethod method, bool caseInvariant) : base(method, argument)
        {
            this.CaseInvariant = caseInvariant;
        }
        
        protected override bool PassesFilter(string toTest)
        {
            if (CaseInvariant)
                return PassesFilterWithoutCase(toTest, Argument);
            else
                return PassesFilterWithCase(toTest, Argument);
        }

        private bool PassesFilterWithoutCase(string toTest, string argument)
        {
            return PassesFilterWithCase(toTest.ToLower(), argument.ToLower());
        }

        private bool PassesFilterWithCase(string toTest, string argument)
        {
            switch (Method)
            {
                case StringFilterMethod.Contains:
                    return toTest.Contains(argument);
                case StringFilterMethod.DoesNotContain:
                    return !toTest.Contains(argument);
                case StringFilterMethod.DoesNotMatch:
                    return toTest != argument;
                case StringFilterMethod.Matches:
                    return toTest == argument;
                default:
                    throw new InvalidOperationException($"The given method: {Method} is unknown.");
            }
        }

        protected override string ParseString(string stringifiedObject)
        {
            return stringifiedObject;
        }

        public enum StringFilterMethod
        {
            Matches,
            DoesNotMatch,
            Contains,
            DoesNotContain
        }
    }
}