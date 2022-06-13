using System;
using Contracts.Entities;

namespace Contracts.Commands
{
    public class RecruitEmployee
    {
        public Employee EmployeeToRecruit { get; set; }
    }

    public class CheckEmployeeDetails
    {
        public Employee EmployeeToCheck { get; set; }
    }

    public class AddEmployee
    {
        public Employee EmployeeToAdd { get; set; }
    }

    public class RemoveEmployee
    {
        public Guid EmployeeID { get; set; }
    }

    public class GiveWorkToEmployee
    {
        public Guid WorkID { get; set; }
        public Guid EmployeeID { get; set; }
    }

    public class EmployeeWorkDone
    {
        public Guid WorkID { get; set; }
        public Guid EmployeeID { get; set; }
    }
}