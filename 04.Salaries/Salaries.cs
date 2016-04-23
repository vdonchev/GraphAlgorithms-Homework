namespace _04.Salaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Salaries
    {
        public static void Main()
        {
            Dictionary<int, Employee> employees = 
                new Dictionary<int, Employee>();

            var totalEmployees = int.Parse(Console.ReadLine());
            for (int i = 0; i < totalEmployees; i++)
            {
                var subEmployees = Console.ReadLine();
                
                if (!employees.ContainsKey(i))
                {
                    employees.Add(i, new Employee());
                }

                var currentEmployee = employees[i];

                for (int j = 0; j < totalEmployees; j++)
                {
                    if (subEmployees[j] == 'Y')
                    {
                        if (!employees.ContainsKey(j))
                        {
                            employees.Add(j, new Employee());
                        }

                        var subEmployee = employees[j];
                        currentEmployee.Employees.Add(subEmployee);
                    }
                }
            }

            Console.WriteLine(employees.Select(e => e.Value.Salary).Sum());
        }

        private class Employee
        {
            private decimal? salary;

            public Employee()
            {
                this.Employees = new List<Employee>();
            }

            public List<Employee> Employees { get; private set; }

            public decimal Salary
            {
                get
                {
                    if (this.salary == null)
                    {
                        this.CalculateSalary();
                    }

                    return this.salary.Value;
                }
            }

            private void CalculateSalary()
            {
                if (this.Employees.Count == 0)
                {
                    this.salary = 1;
                }
                else
                {
                    this.salary = 0;
                    foreach (var employee in this.Employees)
                    {
                        this.salary += employee.Salary;
                    }
                }
            }
        }
    }
}
