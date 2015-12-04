using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StudentHome.Api.Domain;
using StudentHome.Api.Net;
using StudentHome.Api.Service;
using StudentHome.Server.Repository;
using StudentHome.Server.Validators;
using StudentHome = StudentHome.Api.Domain.StudentHome;

namespace StudentHome.Server.Service
{
    public class ServiceImpl : IService
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private StudentRepository studentRepository = new StudentRepository();
        private StudentHomeRepository studentHomeRepository = new StudentHomeRepository();

        public void SaveAllToXml()
        {
            employeeRepository.SaveAllToXml(Constants.EmployeeResourcePath);
            studentHomeRepository.SaveAllToXml(Constants.StudentHomeResourcePath);
            studentRepository.SaveAllToXml(Constants.StudentResourcePath);
        }

        public int CountEmployees()
        {
            return employeeRepository.Count();
        }

        public int CountStudents()
        {
            return studentRepository.Count();
        }

        public int CountStudentHomes()
        {
            return studentHomeRepository.Count();
        }

        public Page<Employee> GetEmployeePage(Pageable pageable)
        {
            IList<Employee> employees = employeeRepository.FindAll();
            Page<Employee> page = new Page<Employee>();
            int cix = employeeRepository.FindAll().Count;

            int pageSize = pageable.getPageSize();
            int pageNumber = pageable.getPageNumber();
            page.setPageNumber(pageNumber);
            page.setPageSize(pageSize);

            var orderedEmployees = employees
                .OrderBy(employee => employee.Name);
            List<Employee> listed = orderedEmployees.ToList();

            int noOfElementsLeft = listed.Count - (pageNumber - 1) * pageSize;
            int noOfelementsToPut = Constants.PAGE_SIZE;
            if (noOfElementsLeft < noOfelementsToPut)
                noOfelementsToPut = noOfElementsLeft;

            List<Employee> paged = listed.GetRange((pageNumber - 1) * pageSize, noOfelementsToPut);

            page.setNoOfElements(paged.Count);
            page.setItems(paged);
            return page;
        }

        public Page<Student> GetStudentPage(Pageable pageable)
        {
            IList<Student> students = studentRepository.FindAll();
            Page<Student> page = new Page<Student>();

            int pageSize = pageable.getPageSize();
            int pageNumber = pageable.getPageNumber();
            page.setPageNumber(pageNumber);
            page.setPageSize(pageSize);

            var orderedStudents = students
                .OrderBy(student => student.Name);
            List<Student> listed = orderedStudents.ToList();

            int noOfElementsLeft = listed.Count - (pageNumber - 1) * pageSize;
            int noOfelementsToPut = Constants.PAGE_SIZE;
            if (noOfElementsLeft < noOfelementsToPut)
                noOfelementsToPut = noOfElementsLeft;

            List<Student> paged = listed.GetRange((pageNumber - 1) * pageSize, noOfelementsToPut);

            page.setNoOfElements(paged.Count);
            page.setItems(paged);
            return page;
        }

        public Page<Api.Domain.StudentHome> GetStudentHomePage(Pageable pageable)
        {
            IList<Api.Domain.StudentHome> studentHomes = studentHomeRepository.FindAll();
            Page<Api.Domain.StudentHome> page = new Page<Api.Domain.StudentHome>();

            int pageSize = pageable.getPageSize();
            int pageNumber = pageable.getPageNumber();
            page.setPageNumber(pageNumber);
            page.setPageSize(pageSize);

            var orderedStudentHomes = studentHomes
                .OrderBy(studentHome => studentHome.Name);
            List<Api.Domain.StudentHome> listed = orderedStudentHomes.ToList();

            int noOfElementsLeft = listed.Count - (pageNumber - 1) * pageSize;
            int noOfelementsToPut = Constants.PAGE_SIZE;
            if (noOfElementsLeft < noOfelementsToPut)
                noOfelementsToPut = noOfElementsLeft;

            List<Api.Domain.StudentHome> paged = listed.GetRange((pageNumber - 1) * pageSize, noOfelementsToPut);

            page.setNoOfElements(paged.Count);
            page.setItems(paged);
            return page;
        }

        public void AddEmployee(Employee employee)
        {
            PersonValidator validator = new PersonValidator();
            string errors = validator.Validate(employee);
            if (errors != "")
                throw new ValidationException(errors);
            employeeRepository.Save(employee);
        }

        public void AddStudent(Student student)
        {
            StudentValidator validator = new StudentValidator();
            string errors = validator.Validate(student);
            if (errors != "")
                throw new ValidationException(errors);
            studentRepository.Save(student);
        }

        public void AddStudentHome(Api.Domain.StudentHome studentHome)
        {
            StudentHomeValidator validator = new StudentHomeValidator();
            string errors = validator.Validate(studentHome);
            if (errors != "")
                throw  new ValidationException(errors);
            studentHomeRepository.Save(studentHome);
        }

        public Message Process(Message request)
        {
            switch (request.Title)
            {
                case "SaveAllToXml":
                    try
                    {
                        SaveAllToXml();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Could not save to xml ! ");
                        return new Message("Unsuccesful", e.Message);
                    }
                    return new Message("Succesful", null);

                case "AddStudent":
                    Student student = request.Body as Student;
                    try
                    {
                        AddStudent(student);
                        Trace.WriteLine(student + " a fost adaugat cu succes !");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(student + " este invalid ! ");
                        return new Message("Unsuccesful", e.Message);   
                    }
                    return new Message("Succesful",null);

                case "AddStudentHome":
                    Api.Domain.StudentHome studentHome = request.Body as Api.Domain.StudentHome;
                    try
                    {
                        AddStudentHome(studentHome);
                        Trace.WriteLine(studentHome + " a fost adaugat cu succes !");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(studentHome + " este invalid ! ");
                        return new Message("Unsuccesful", e.Message);   
                    }
                    return new Message("Succesful",null);

                case "AddEmployee":
                    Employee employee = request.Body as Employee;
                    try
                    {
                        AddEmployee(employee);
                        Trace.WriteLine(employee + " a fost adaugat cu succes !");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(employee + " este invalid ! ");
                        return new Message("Unsuccesful", e.Message);   
                    }
                    return new Message("Succesful",null);

                case "CountEmployees":
                    return new Message("Succesful", CountEmployees());
                case "CountStudents":
                    return new Message("Succesful", CountStudents());
                case "CountStudentHomes":
                    return new Message("Succesful", CountStudentHomes());
                case "GetStudentHomePage":
                    return new Message("Succesful", GetStudentHomePage((Pageable)request.Body));
                case "GetStudentPage":
                    return new Message("Succeful", GetStudentPage((Pageable)request.Body));
                case "GetEmployeePage":
                    return new Message("Succesful", GetEmployeePage((Pageable)request.Body));
                default:
                    throw new ServiceException("Title of request invalid : " + request.Title);
            }
        }
    }
}
