namespace EnrollmentApplicationParser.BusinessRules
{
    interface IBusinessRuleValidator
    {
        Constants.STATUS ProcessBusinessRule(EnrollmentEntry entry);
    }
}
