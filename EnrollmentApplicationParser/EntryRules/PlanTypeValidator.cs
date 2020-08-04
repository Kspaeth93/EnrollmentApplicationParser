using System;
using System.Collections.Generic;

namespace EnrollmentApplicationParser.EntryRules
{
    class PlanTypeValidator : IEntryRuleValidator
    {
        public bool ProcessEntryRule(string[] entries)
        {
            bool valid = true;

            if (entries != null && entries.Length > 0)
            {
                // Perhaps a better way to create this list?
                List<string> planStrings = new List<string>()
                {
                    entries[3]
                };

                foreach (string plan in planStrings)
                {
                    valid = IsPlanValid(plan);
                }
            }

            return valid;
        }

        private bool IsPlanValid(string plan)
        {
            bool valid = true;

            if (plan == null)
            {
                valid = false;
            }
            else
            {
                if (!Enum.IsDefined(typeof(Constants.PLAN_TYPE), plan)) valid = false;
            }

            return valid;
        }
    }
}
