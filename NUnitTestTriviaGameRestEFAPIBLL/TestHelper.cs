
namespace NUnitTestTriviaGameRestEFAPIBLL
{
    public static class TestHelper
    {
        public static string DBConnection
        {
            get
            {
                return "Server=.\\SQLEXPRESS;Database=TriviaGameDB;User Id=sa;password=*#sa; MultipleActiveResultSets=True";
            }
        }
    }
}
