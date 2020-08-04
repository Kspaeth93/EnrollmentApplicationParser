namespace EnrollmentApplicationParser.EntryRules
{
    interface IEntryRuleValidator
    {
        bool ProcessEntryRule(string[] entries);
    }
}
