using StudentHome.Api.Domain;
using StudentHome.Api.Service;

namespace StudentHome.Server.Repository
{
    public class EmployeeRepository : CrudRepository<Employee>
    {
        public EmployeeRepository()
        {
            LoadAllFromXml(Constants.EmployeeResourcePath);
            GeneratedId = Count();
        }

        protected override void SetId(Employee obj)
        {
            GeneratedId++;
            obj.Id = GeneratedId;
        }
    }
}
