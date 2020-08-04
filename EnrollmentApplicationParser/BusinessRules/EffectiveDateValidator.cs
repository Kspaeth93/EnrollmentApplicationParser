using System;

namespace EnrollmentApplicationParser.BusinessRules
{
    public class EffectiveDateValidator : IBusinessRuleValidator
    {
        public Constants.STATUS ProcessBusinessRule(EnrollmentEntry entry)
        {
            Constants.STATUS status = Constants.STATUS.ACCEPTED;

            if (entry != null)
            {
                if (entry.effectiveDate == null) status = Constants.STATUS.REJECTED;
                if (entry.effectiveDate > DateTime.Now.AddDays(30)) status = Constants.STATUS.REJECTED;
            }

            return status;
        }
    }
}
