using System;

namespace StudentHome.Api.Domain
{
    [Serializable]
    public class Student : IPerson, HasId<int>
    {
        public string PIN { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double AnnualGrade { get; set; }
        public int Id { get; set; }

        public Student()
        {

        }

        public Student(string pin, string name, string surname, double annualGrade)
        {
            this.PIN = pin;
            Name = name;
            Surname = surname;
            AnnualGrade = annualGrade;
            Id = -1;
        }

        public override string ToString()
        {
            return string.Format("PIN : {0} Name : {1} Surname : {2} AnnualGrade : {3} ", PIN, Name, Surname, AnnualGrade);
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            if (other == null)
                return false;
            if (other.PIN == this.PIN)
                return true;
            if ((other.Id == this.Id) && (this.Id != -1))
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
