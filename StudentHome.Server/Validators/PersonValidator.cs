using System;
using System.Collections.Generic;
using StudentHome.Api.Domain;

namespace StudentHome.Server.Validators
{
    public class PersonValidator : IValidator<IPerson>
    {
        public virtual string Validate(IPerson person)
        {
            string errors = string.Empty;
            if (person.PIN.Length != 13)
                errors = string.Concat(errors, string.Format("PIN-ul persoanei {0} {1} nu este format din 13 cifre! {2}",
                    person.Name,
                    person.Surname,
                    Environment.NewLine));

            char[] chars = person.PIN.ToCharArray();
            List<char> digits = new List<char> {'0','1','2','3','4','5','6','7','8','9'};
            foreach (char cifra in chars)
            {
                if (!digits.Contains(cifra))
                {
                    errors = string.Concat(errors, string.Format("PIN-ul persoanei {0} {1} trebuie sa contina numai cifre! {2}",
                    person.Name,
                    person.Surname,
                    Environment.NewLine));
                    break;
                }
            }

            return errors;
        }
    }
}
