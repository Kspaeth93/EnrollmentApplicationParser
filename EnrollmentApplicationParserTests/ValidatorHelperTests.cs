using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnrollmentApplicationParser;

namespace EnrollmentApplicationParserTests
{
    [TestClass]
    public class ValidatorHelperTests
    {
        [TestMethod]
        public void TestAreRequiredFieldsPresent()
        {
            Assert.AreEqual(false, ValidatorHelper.AreRequiredFieldsPresent(null));
            Assert.AreEqual(false, ValidatorHelper.AreRequiredFieldsPresent("one,two,three"));

            Assert.AreEqual(true, ValidatorHelper.AreRequiredFieldsPresent("name,name,date,type,date"));
        }

        [TestMethod]
        public void TestIsDateStringValid()
        {
            Assert.AreEqual(false, ValidatorHelper.IsDateStringValid(null));
            Assert.AreEqual(false, ValidatorHelper.IsDateStringValid("01/01/2001"));
            Assert.AreEqual(false, ValidatorHelper.IsDateStringValid("13322001"));

            Assert.AreEqual(true, ValidatorHelper.IsDateStringValid("01012001"));
        }

        [TestMethod]
        public void TestIsPlanTypeValid()
        {
            Assert.AreEqual(false, ValidatorHelper.IsPlanTypeValid(null));
            Assert.AreEqual(false, ValidatorHelper.IsPlanTypeValid("InvalidPlanType"));

            Assert.AreEqual(true, ValidatorHelper.IsPlanTypeValid("HSA"));
        }
    }
}
