using System;

namespace EnrollmentApplicationParser
{
    public static class ValidatorHelper
    {
        public static bool AreRequiredFieldsPresent(string entry)
        {
            bool isValid = true;

            if (entry == null)
            {
                isValid = false;
            }
            else
            {
                string[] entryData = entry.Split(",");
                if (entryData.Length != Constants.EXPECTED_ENTRY_LENGTH) isValid = false;
            }

            return isValid;
        }

        public static bool IsDateStringValid(string dateString)
        {
            bool isValid = true;

            if (dateString == null || dateString.Length != Constants.DATE_STRING_LENGTH) isValid = false;
            if (!IsNumeric(dateString)) isValid = false;

            if (isValid)
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
                    isValid = false;
                }
            }

            return isValid;
        }

        public static bool IsPlanTypeValid(string planType)
        {
            bool isValid = true;

            if (planType == null)
            {
                isValid = false;
            }
            else
            {
                if (!Enum.IsDefined(typeof(Constants.PLAN_TYPE), planType)) isValid = false;
            }

            return isValid;
        }

        private static bool IsNumeric(string number)
        {
            return int.TryParse(number, out int i);
        }

        /* Check if date string is valid using IsDateStringValid(string dateString) before invoking. */
        public static DateTime CreateDateFromString(string dateString)
        {
            int month = int.Parse(dateString.Substring(0, 2));
            int day = int.Parse(dateString.Substring(2, 2));
            int year = int.Parse(dateString.Substring(4, 4));

            return new DateTime(year, month, day);
        }
    }
}
