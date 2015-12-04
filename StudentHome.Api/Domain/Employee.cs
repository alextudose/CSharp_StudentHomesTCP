using System;

namespace StudentHome.Api.Domain
{
    [Serializable]
    public class Employee : IPerson, HasId<int>
    {
        public string PIN { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public int Id { get; set; }

        public Employee()
        {

        }

        public Employee(string pin, string name, string surname, string job)
        {
            this.PIN = pin;
            Name = name;
            Surname = surname;
            Job = job;
            Id = -1;
        }

        public override string ToString()
        {
            return string.Format("PIN : {0} Name : {1} Surname : {2} Job : {3} ", PIN, Name, Surname, Job);
        }

        public override bool Equals(object obj)
        {
            Employee employee = obj as Employee;
            if (employee == null)
                return false;
            if (employee.PIN == this.PIN)
                return true;
            if ((employee.Id == this.Id) && (this.Id != -1))
                return true;
            return false;
        }

        public int getId()
        {
            return Id;
        }

        public void setId(int id)
        {
            Id = id;
        }
    }
}
