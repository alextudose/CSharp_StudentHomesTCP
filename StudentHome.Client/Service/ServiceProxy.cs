using StudentHome.Api.Domain;
using StudentHome.Api.Net;
using StudentHome.Api.Service;
using StudentHome.Client.Net;
using StudentHome = StudentHome.Api.Domain.StudentHome;

namespace StudentHome.Client.Service
{
    public class ServiceProxy : IService
    {
        public TcpClient TcpClient { get; internal set; }
        public int CountEmployees()
        {
            Message response = TcpClient.Execute(new Message("CountEmployees"));
            return (int)response.Body;
        }

        public int CountStudents()
        {
            Message response = TcpClient.Execute(new Message("CountStudents"));
            return (int)response.Body;
        }

        public int CountStudentHomes()
        {
            Message response = TcpClient.Execute(new Message("CountStudentHomes"));
            return (int)response.Body;
        }

        public Page<Employee> GetEmployeePage(Pageable pageable)
        {
            Message response = TcpClient.Execute(new Message("GetEmployeePage", pageable));
            return (Page<Employee>)response.Body;
        }

        public Page<Student> GetStudentPage(Pageable pageable)
        {
            Message response = TcpClient.Execute(new Message("GetStudentPage", pageable));
            return (Page<Student>)response.Body;
        }

        public Page<Api.Domain.StudentHome> GetStudentHomePage(Pageable pageable)
        {
            Message response = TcpClient.Execute(new Message("GetStudentHomePage", pageable));
            return (Page<Api.Domain.StudentHome>)response.Body;
        }

        public void AddEmployee(string pin, string name, string surname, string job)
        {
            Employee employee = new Employee(pin,name,surname,job);
            Message response = TcpClient.Execute(new Message("AddEmployee", employee));
            if (response.Title == "Unsuccesful")
                throw new ValidationException("Data not valid : " + (string)response.Body);
        }

        public void AddStudent(string pin, string name, string surname, double annualGrade)
        {
            Student student = new Student(pin,name,surname,annualGrade);
            Message response = TcpClient.Execute(new Message("AddStudent", student));
            if (response.Title == "Unsuccesful")
                throw new ValidationException("Data not valid : " + (string)response.Body);
        }

        public void AddStudentHome(string name, int roomSize, int noOfRooms, double tax)
        {
            Api.Domain.StudentHome studentHome = new Api.Domain.StudentHome(name,roomSize,noOfRooms,tax);
            Message response = TcpClient.Execute(new Message("AddStudentHome", studentHome));
            if (response.Title == "Unsuccesful")
                throw new ValidationException("Data not valid : " + (string)response.Body);
        }

        public void SaveAllToXml()
        {
            Message response = TcpClient.Execute(new Message("SaveAllToXml"));
            if (response.Title == "Unsuccesful")
                throw new ValidationException("Could not save to XML : " + (string)response.Body);
        }
    }
}
