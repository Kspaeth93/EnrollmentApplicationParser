using System;

namespace EnrollmentApplicationParser
{
    class EnrollmentEntry
    {
        public Constants.STATUS status { get; private set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public DateTime dob { get; private set; }
        public Constants.PLAN_TYPE planType { get; private set; }
        public DateTime effectiveDate { get; private set; }

        public EnrollmentEntry(Constants.STATUS status, string firstName, string lastName,
            DateTime dob, Constants.PLAN_TYPE planType, DateTime effectiveDate)
        {
            this.status = status;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dob = dob;
            this.planType = planType;
            this.effectiveDate = effectiveDate;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}, {4}, {5}]",
                status.ToString(),
                firstName,
                lastName,
                dob.ToShortDateString(),
                planType.ToString(),
                effectiveDate.ToShortDateString());
        }
    }
}
