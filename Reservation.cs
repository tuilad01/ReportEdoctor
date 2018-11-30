using System;
using System.Collections.Generic;

namespace AutomationTest1
{
    public class Reservation
    {
        public Reservation()
        {
            Service = new List<string>();
        }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string YearOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public IList<string> Service { get; set; }
        public string Room { get; set; }
        public string Price { get; set; }
        public string EmployeeSell { get; set; }
        public string EmployeeDeloy { get; set; }
        public string EmployeeSample { get; set; }
        public string Date => DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
    }
}
