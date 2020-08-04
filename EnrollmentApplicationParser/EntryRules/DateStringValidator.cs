using System;
using System.Collections.Generic;

namespace EnrollmentApplicationParser.EntryRules
{
    public class DateStringValidator : IEntryRuleValidator
    {
        public bool ProcessEntryRule(string[] entries)
        {
            bool valid = true;

            if (entries != null && entries.Length > 0)
            {
                // Perhaps a better way to create this list?
                List<string> dateStrings = new List<string>()
                {
                    entries[2],
                    entries[4]
                };

                foreach (string date in dateStrings)
                {
                    valid = IsDateValid(date);
                }
            }

            return valid;
        }

        private bool IsDateValid(string dateString)
        {
            bool valid = true;

            if (dateString == null || dateString.Length != Constants.DATE_STRING_LENGTH) valid = false;
            if (!IsNumeric(dateString)) valid = false;

            if (valid)
            {
                int month = int.Parse(dateString.Substring(0, 2));
                int day = int.Parse(dateString.Substring(2, 2));
                int year = int.Parse(dateString.Substring(4, 4));

                try
                {
                    DateTime date = new DateTime(year, month, day);
                }
                catch (Exception)
                {
                    valid = false;
                }
            }

            return valid;
        }

        // Move this back to a helper file if it becomes used anywhere else.
        private bool IsNumeric(string number)
        {
            return int.TryParse(number, out int i);
        }
    }
}
