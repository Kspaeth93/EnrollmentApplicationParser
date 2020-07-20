namespace EnrollmentApplicationParser
{
    public class Constants
    {
        public const string INPUT_FILE_DIRECTORY = "..\\..\\..\\..\\InputFiles\\";
        
        public const int EXPECTED_ENTRY_LENGTH = 5;
        
        public const int DATE_STRING_LENGTH = 8;
        
        public enum PLAN_TYPE
        {
            HSA, HRA, FSA
        }

        public enum STATUS
        {
            ACCEPTED, REJECTED
        }
    }
}
