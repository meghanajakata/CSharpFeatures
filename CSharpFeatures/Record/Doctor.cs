namespace CSharpFeatures.Record;

public record struct Doctor(int Id, string Name, double Salary);

public readonly record struct PointAxis(double X, double Y, double Z);

public class Patient
{
    public int Id;
    public string Name;
}

public record Person(string FirstName, string LastName, string Id)
{
    internal string Id { get; init; } = Id;
}
