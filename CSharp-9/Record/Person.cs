namespace CSharp_9.Record
{
    public record Person(string FirstName, string LastName);
    /*{
        public void Deconstruct(
            out string firstName,
            out string lastName
        ) => (firstName, lastName) = (FirstName, LastName);
    }*/

    public record Employee
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public record Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public record Products(int Id, string Name, Product Product);
    //record struct Doctor(int id, string name);

    /*struct A
    {
        public int Id { get; set; }
        public A()
        {
            Id = 0;
        }
    }*/

    struct A
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
