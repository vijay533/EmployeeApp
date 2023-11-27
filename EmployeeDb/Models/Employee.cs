using System.ComponentModel.DataAnnotations;

namespace EmployeeDb.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public int salary { get; set; }
        public string Gender { get; set; }
        public string city { get; set; }
        public string Nationality { get; set; }
        public string PinCode { get; set; }
    }
}
