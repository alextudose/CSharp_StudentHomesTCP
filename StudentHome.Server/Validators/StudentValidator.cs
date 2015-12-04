using System;
using StudentHome.Api.Domain;

namespace StudentHome.Server.Validators
{
    public class StudentValidator : PersonValidator
    {
        public override string Validate(IPerson person)
        {
            string errors = base.Validate(person);
            Student student = person as Student;
            if ((student.AnnualGrade < 1) || (student.AnnualGrade > 10))
                errors = string.Concat(errors, string.Format("Media anuala a studentului {0} {1} este in intervalul [1,10]! {2}",
                    person.Name,
                    person.Surname,
                    Environment.NewLine));
            return errors;
        }
    }
}
