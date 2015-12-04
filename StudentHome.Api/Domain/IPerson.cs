namespace StudentHome.Api.Domain
{
    public interface IPerson
    {
        string PIN { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
    }
}
