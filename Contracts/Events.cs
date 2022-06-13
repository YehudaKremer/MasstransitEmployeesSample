using System;
using Contracts.Entities;

namespace Contracts.Events
{
    public class RecruitmentStarted
    {
        public Employee EmployeeToRecruit { get; set; }
    }

    public class RecruitmentFinished
    {
        public Guid EmployeeID { get; set; }
    }

    public class EmployeeDetailsMatched
    {
        public Employee MatchedEmployee { get; set; }
    }

    public class EmployeeDetailsNotMatched
    {
        public Guid DeniedEmployeeID { get; set; }
    }
}
