public record PersonName()
{
    public string FirstName { get; private set; }
    
    public string? MiddleName { get; private set; }
    
    public string LastName { get; private set; }
    
    public PersonName(string firstName, string? middleName, string lastName) : this()
    {
        ArgumentNullException.ThrowIfNull(firstName);
        ArgumentNullException.ThrowIfNull(lastName);
        
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }
}