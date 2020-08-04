using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using EnrollmentApplicationParser.BusinessRules;

namespace EnrollmentApplicationParser
{
    public class InputFileValidator
    {
        public IEnumerable<EnrollmentEntry> ProcessFile(string filePath)
        {
            IList<EnrollmentEntry> enrollmentEntries;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The input file could not be found.");
            }
            else
            {
                enrollmentEntries = new List<EnrollmentEntry>();

                string[] entries = File.ReadAllLines(filePath);
                foreach (string entry in entries)
                {
                    enrollmentEntries.Add(ProcessEntry(entry));
                }
            }

            return enrollmentEntries.AsEnumerable();
        }

        public EnrollmentEntry ProcessEntry(string entry)
        {
            string firstName, lastName;
            DateTime dob, effectiveDate;
            Constants.PLAN_TYPE planType;

            /* Entry is assumed to be ACCEPTED until proved otherwise. */
            Constants.STATUS status = Constants.STATUS.ACCEPTED;

            if (!ValidatorHelper.AreRequiredFieldsPresent(entry))
            {
                throw new ArgumentException(string.Format("Missing requierd fields in entry: {0}.", entry));
            };

            string[] entryData = entry.Split(",");

            firstName = entryData[0];
            lastName = entryData[1];

            string dobString = entryData[2];
            if (!ValidatorHelper.IsDateStringValid(dobString))
            {
                throw new ArgumentException(string.Format("The date '{0}' is not valid.", dobString));
            }
            else
            {
                dob = ValidatorHelper.CreateDateFromString(dobString);
            }

            string planTypeString = entryData[3];
            if (!ValidatorHelper.IsPlanTypeValid(planTypeString))
            {
                throw new ArgumentException(string.Format("The plan type '{0}' is not valid.", planTypeString));
            }
            else
            {
                planType = (Constants.PLAN_TYPE)Enum.Parse(typeof(Constants.PLAN_TYPE), planTypeString);
            }

            string effectiveDateString = entryData[4];
            if (!ValidatorHelper.IsDateStringValid(effectiveDateString))
            {
                throw new ArgumentException(string.Format("The date '{0}' is not valid.", effectiveDateString));
            }
            else
            {
                effectiveDate = ValidatorHelper.CreateDateFromString(effectiveDateString);
            }


            EnrollmentEntry enrollmentEntry = new EnrollmentEntry(firstName, lastName, dob, planType, effectiveDate);

            // This is a placeholder.
            // TODO: Create this list using factory pattern instead.
            List<IBusinessRuleValidator> businessRuleValidators = new List<IBusinessRuleValidator>()
            {
                new DOBValidator(),
                new EffectiveDateValidator()
            };

            enrollmentEntry.status = ProcessEntryBR(businessRuleValidators, enrollmentEntry);

            return enrollmentEntry;
        }

        private Constants.STATUS ProcessEntryBR(List<IBusinessRuleValidator> businessRules, EnrollmentEntry entry)
        {
            Constants.STATUS status = Constants.STATUS.ACCEPTED;

            if (businessRules != null && businessRules.Count > 0)
            {
                foreach (IBusinessRuleValidator rule in businessRules)
                {
                    status = rule.ProcessBusinessRule(entry);
                }
            }

            return status;
        }
    }
}
