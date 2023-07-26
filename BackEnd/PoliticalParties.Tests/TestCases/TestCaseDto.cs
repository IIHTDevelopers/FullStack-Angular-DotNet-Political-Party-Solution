using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalParties.Tests.TestCases
{
    public class TestCaseResultDto
    {
        public string MethodName { get; set; }
        public string MethodType { get; set; }
        public int? ActualScore { get; set; }
        public int EarnedScore { get; set; }
        public string Status { get; set; }
        public bool IsMandatory { get; set; }
        public string ErroMessage { get; set; }
    }

    public class TestResults
    {
        public string TestCaseResults { get; set; } // string format of dictionary object
        public string CustomData { get; set; }  // custom.ih
    }
}
