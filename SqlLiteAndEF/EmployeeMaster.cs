using System.ComponentModel.DataAnnotations;

namespace SqlLiteAndEF
{
    public class EmployeeMaster
    {
        [Key]
        public int ID { get; set; }

        public string EmpName { get; set; }

        public double Salary { get; set; }

        public string Designation { get; set; }
    }

    public class StudentMaster
    {
        [Key]
        public int ID { get; set; }

        public string EmpName { get; set; }

        public string Salary { get; set; }

        public string Designation { get; set; }
    }
}
