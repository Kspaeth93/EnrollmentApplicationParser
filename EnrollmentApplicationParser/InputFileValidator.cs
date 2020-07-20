﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace EnrollmentApplicationParser
{
    class InputFileValidator
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

            if (!IsValidDOB(dob)) status = Constants.STATUS.REJECTED;
            if (!IsValidEffectiveDate(effectiveDate)) status = Constants.STATUS.REJECTED;

            return new EnrollmentEntry(status, firstName, lastName, dob, planType, effectiveDate);
        }

        private bool IsValidDOB(DateTime dob)
        {
            bool isValid = true;

            if (dob == null) isValid = false;
            if (dob > DateTime.Now.AddYears(-18)) isValid = false;

            return isValid;
        }

        private bool IsValidEffectiveDate(DateTime effectiveDate)
        {
            bool isValid = true;

            if (effectiveDate == null) isValid = false;
            if (effectiveDate > DateTime.Now.AddDays(30)) isValid = false;

            return isValid;
        }
    }
}