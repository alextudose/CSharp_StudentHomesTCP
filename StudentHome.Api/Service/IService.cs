using StudentHome.Api.Domain;

namespace StudentHome.Api.Service
{
    public interface IService
    {
        int CountEmployees();
        int CountStudents();
        int CountStudentHomes();
        Page<Employee> GetEmployeePage(Pageable pageable);
        Page<Student> GetStudentPage(Pageable pageable);
        Page<Domain.StudentHome> GetStudentHomePage(Pageable pageable);
    }
}
