using StudentHome.Api.Service;

namespace StudentHome.Server.Repository
{
    public class StudentHomeRepository : CrudRepository<Api.Domain.StudentHome>
    {
        public StudentHomeRepository()
        {
            LoadAllFromXml(Constants.StudentHomeResourcePath);
            GeneratedId = Count();
        }

        protected override void SetId(Api.Domain.StudentHome obj)
        {
            GeneratedId++;
            obj.Id = GeneratedId;
        }
    }
}
