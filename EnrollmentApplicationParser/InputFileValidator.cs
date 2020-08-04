using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using EnrollmentApplicationParser.BusinessRules;
using EnrollmentApplicationParser.EntryRules;

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
            string[] entries = entry.Split(",");
            EnrollmentEntry enrollmentEntry;

            // This is a placeholder.
            // TODO: Create this list using factory pattern instead.
            List<IEntryRuleValidator> entryRuleValidators = new List<IEntryRuleValidator>()
            {
                new RequiredFieldsValidator(),
                new DateStringValidator(),
                new PlanTypeValidator()
            };

            if (!ValidateEntryRules(entryRuleValidators, entries))
            {
                throw new ArgumentException(string.Format("The entry '{0}' is invalid.", entry));
            }
            else
            {
                enrollmentEntry = new EnrollmentEntry(
                    entries[0], 
                    entries[1],
                    CreateDateFromString(entries[2]), 
                    (Constants.PLAN_TYPE)Enum.Parse(typeof(Constants.PLAN_TYPE), entries[3]),
                    CreateDateFromString(entries[4])
                    );
            }

            // This is a placeholder.
            // TODO: Create this list using factory pattern instead.
            List<IBusinessRuleValidator> businessRuleValidators = new List<IBusinessRuleValidator>()
            {
                new DOBValidator(),
                new EffectiveDateValidator()
            };

            enrollmentEntry.status = ValidateBusinessRules(businessRuleValidators, enrollmentEntry);

            return enrollmentEntry;
        }

        private bool ValidateEntryRules(List<IEntryRuleValidator> rules, string[] entries)
        {
            bool valid = true;

            foreach (IEntryRuleValidator rule in rules)
            {
                valid = rule.ProcessEntryRule(entries);
            }

            return valid;
        }

        private Constants.STATUS ValidateBusinessRules(List<IBusinessRuleValidator> rules, EnrollmentEntry entry)
        {
            Constants.STATUS status = Constants.STATUS.ACCEPTED;

            foreach (IBusinessRuleValidator rule in rules)
            {
                status = rule.ProcessBusinessRule(entry);
            }

            return status;
        }

        private DateTime CreateDateFromString(string dateString)
        {
            int month = int.Parse(dateString.Substring(0, 2));
            int day = int.Parse(dateString.Substring(2, 2));
            int year = int.Parse(dateString.Substring(4, 4));

            return new DateTime(year, month, day);
        }
    }
}
