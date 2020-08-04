using System;
using System.Collections.Generic;
using System.Text;

namespace EnrollmentApplicationParser.EntryRules
{
    public class RequiredFieldsValidator : IEntryRuleValidator
    {
        public bool ProcessEntryRule(string[] entries)
        {
            return (entries == null || entries.Length != Constants.EXPECTED_ENTRY_LENGTH);
        }
    }
}
