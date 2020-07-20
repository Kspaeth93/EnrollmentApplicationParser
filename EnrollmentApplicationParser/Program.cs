using System;
using System.Collections.Generic;

namespace EnrollmentApplicationParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        /*
         * Available Test Files:
         * Test1-MissingRequiredFields.txt
         * Test2-MalformedDOB.txt
         * Test2b-InvalidDOB.txt
         * Test3-InvalidPlanType.txt
         * Test4-MalformedEffectiveDate.txt
         * Test4b-InvalidEffectiveDate.txt
         * Test5-ValidInputFile.txt
         */
        public Program()
        {
            InputFileValidator validator = new InputFileValidator();
            IEnumerable<EnrollmentEntry> enrollmentEntries = validator.ProcessFile(
                string.Format("{0}{1}", Constants.INPUT_FILE_DIRECTORY, "Test5-ValidInputFile.txt"));

            foreach (EnrollmentEntry entry in enrollmentEntries)
            {
                Console.WriteLine(entry.ToString());
            }
        }
    }
}
