namespace StudentHome.Server.Validators
{
    public interface IValidator<T>
    {       
        string Validate(T objectToValidate);
    }
}
