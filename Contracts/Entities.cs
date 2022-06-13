using System;
using System.Collections.Generic;

namespace Contracts.Entities
{
    public class Office
    {
        public List<Employee> Workers { get; set; }
    }

    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
    }

    public class Work
    {
        public Guid WorkID { get; set; }
        public string Description { get; set; }
        public Guid EmployeeID { get; set; }
        //public WorkStatus WorkStatus { get; set; }
    }

    //public enum WorkStatus
    //{
    //    New,
    //    InProgress,
    //    Finished,
    //    FailToFinish
    //}
}