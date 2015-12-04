using System;

namespace StudentHome.Api.Domain
{
    [Serializable]
    public class StudentHome : HasId<int>
    {
        public string Name { get; set; }
        public int RoomSize { get; set; }
        public int NumberOfRooms { get; set; }
        public double Tax { get; set; }
        public int Id { get; set; }

        public StudentHome()
        {

        }

        public StudentHome(string name, int roomSize, int numberOfRooms, double tax)
        {
            Name = name;
            RoomSize = roomSize;
            NumberOfRooms = numberOfRooms;
            Tax = tax;
            Id = -1;
        }

        public override string ToString()
        {
            return string.Format("Name : {0} Persoane in camera : {1} Camere : {2} Regie : {3} RON", Name,
                RoomSize, NumberOfRooms, Tax);
        }

        public override bool Equals(object obj)
        {
            StudentHome studentHome = obj as StudentHome;
            if (studentHome==null)
                return false;
            if ((studentHome.Id == this.Id) && (this.Id != -1))
                return true;
            if (studentHome.Name == this.Name)
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
