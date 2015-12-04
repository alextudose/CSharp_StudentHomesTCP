using StudentHome.Api.Domain;
using StudentHome.Api.Service;

namespace StudentHome.Server.Repository
{
    public class StudentRepository : CrudRepository<Student>
    {
        public StudentRepository()
        {
            LoadAllFromXml(Constants.StudentResourcePath);
            GeneratedId = Count();
        }

        protected override void SetId(Student obj)
        {
            GeneratedId++;
            obj.Id = GeneratedId;
        }
    }
}
