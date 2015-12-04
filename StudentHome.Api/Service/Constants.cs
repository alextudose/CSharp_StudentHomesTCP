﻿namespace StudentHome.Api.Service
{
    public class Constants
    {
        public static void SetPaths()
        {
            repositoryPath = repositoryPath.Replace("bin\\Debug", "");
            repositoryPath += "Repository\\XML Files\\";
        }

        private static string repositoryPath = System.Environment.CurrentDirectory;
        public static string EmployeeResourcePath 
        {
            get { return repositoryPath + "employees.xml"; }
        }
        public static string StudentHomeResourcePath
        {
            get { return repositoryPath + "studentHomes.xml"; }
        }
        public static string StudentResourcePath
        {
            get { return repositoryPath + "students.xml"; }
        }

        public static int PAGE_SIZE = 3;
    }
}
