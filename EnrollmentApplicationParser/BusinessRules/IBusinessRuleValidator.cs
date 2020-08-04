namespace EnrollmentApplicationParser
{
    interface IBusinessRuleValidator
    {
        Constants.STATUS ProcessBusinessRule(EnrollmentEntry entry);
    }
}
