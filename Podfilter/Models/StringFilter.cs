using System;

namespace Podfilter.Models
{
    public class StringFilter : BaseFilter
    {
        public StringFilterMethod? Method { get; set; }
        public string Argument { get; set; }
        public bool CaseInvariant { get; set; } = true;
        
        public StringFilter()
        {
            // Constructor for deserialization.
        }

        public StringFilter(string argument, StringFilterMethod method, bool caseInvariant)
        {
            this.Argument = argument;
            this.Method = method;
            this.CaseInvariant = caseInvariant;
        }
        
        public override bool PassesFilter(object obj)
        {
            if(Method == null)
                throw new InvalidOperationException($"Method hsa not been set.");
            string toTest = ConvertToTDefault<string>(obj);

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

        public enum StringFilterMethod
        {
            Matches,
            DoesNotMatch,
            Contains,
            DoesNotContain
        }
    }
}