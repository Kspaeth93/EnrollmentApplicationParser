using System;

namespace EnrollmentApplicationParser.BusinessRules
{
    public class DOBValidator : IBusinessRuleValidator
    {
        public Constants.STATUS ProcessBusinessRule(EnrollmentEntry entry)
        {
            Constants.STATUS status = Constants.STATUS.ACCEPTED;

            if (entry != null)
            {
                if (entry.dob == null) status = Constants.STATUS.REJECTED;
                if (entry.dob > DateTime.Now.AddYears(-18)) status = Constants.STATUS.REJECTED;
            }

            return status;
        }
    }
}
