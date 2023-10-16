namespace CSharpFeatures.StructConstructor;
struct Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Employee()
    {
        Name = "Reetu";
        Id = 0;
    }

}
