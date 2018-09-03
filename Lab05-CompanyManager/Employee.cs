using System.Collections.Generic;

namespace Lab05_CompanyManager
{
    public class Employee
    {
        // attributes
        public int Ssn { get; set; }
        public string FName { get; set; }
        public char MInit { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public float Salary { get; set; }
        public char Sex { get; set; }
        //relationships
        public Department WorksFor { get; set; }
        public Department Manages { get; set; }
        public List<WorksOn> WorksOn { get; set; }
        public List<Dependent> Dependents { get; set; }
        public Employee Supervisor { get; set; }
        public List<Employee> Supervisees { get; set; }
    }
}