using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnrollmentApplicationParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnrollmentApplicationParserTests
{
    [TestClass]
    public class InputFileValidatorTests
    {
        //[TestMethod]
        //public void TestProcessFile()
        //{
        //    InputFileValidator validator = new InputFileValidator();

        //    IEnumerable<EnrollmentEntry> expected = new List<EnrollmentEntry>() 
        //    {
        //        new EnrollmentEntry(
        //            Constants.STATUS.REJECTED,
        //            "John", "Doe",
        //            new DateTime(2020, 1, 1),
        //            Constants.PLAN_TYPE.HSA,
        //            new DateTime(2020, 8, 1)),
        //        new EnrollmentEntry(
        //            Constants.STATUS.REJECTED,
        //            "John", "Doe",
        //            new DateTime(1990, 1, 1),
        //            Constants.PLAN_TYPE.HRA,
        //            new DateTime(2021, 1, 1)),
        //        new EnrollmentEntry(
        //            Constants.STATUS.ACCEPTED,
        //            "John", "Doe",
        //            new DateTime(1990, 1, 1),
        //            Constants.PLAN_TYPE.FSA,
        //            new DateTime(2020, 8, 1))
        //    }.AsEnumerable();

        //    IEnumerable <EnrollmentEntry> actual = validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test5-ValidInputFile.txt");

        //    for (int i = 0; i < expected.Count(); i++)
        //    {
        //        Assert.AreEqual(expected.ElementAt(i).ToString(), actual.ElementAt(i).ToString());
        //    }
        //}

        [TestMethod]
        public void TestProcessFileThrowsException()
        {
            InputFileValidator validator = new InputFileValidator();

            Assert.ThrowsException<IndexOutOfRangeException>(
                () => validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test1-MissingRequiredFields.txt"));
            Assert.ThrowsException<FormatException>(
                () => validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test2-MalformedDOB.txt"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test2b-InvalidDOB.txt"));
            Assert.ThrowsException<ArgumentException>(
                () => validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test3-InvalidPlanType.txt"));
            Assert.ThrowsException<FormatException>(
                () => validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test4-MalformedEffectiveDate.txt"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => validator.ProcessFile(Constants.INPUT_FILE_DIRECTORY + "Test4b-InvalidEffectiveDate.txt"));
        }
    }
}
