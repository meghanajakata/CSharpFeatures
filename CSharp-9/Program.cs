using System;
using CSharp_9.Async;
using System.Threading.Tasks;
using CSharp_9.Record;
using System.Runtime.CompilerServices;

namespace CSharp_9
{
    public  class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Person person = new("Nisha", "Sharma");
            Console.WriteLine("Person details" + person);

            Person person1 = person with { FirstName = "Amith" ,LastName = "Trivedi"};
            Console.WriteLine("Person details" + person1);

            Person person2 = new("Nisha", "Sharma");

            //Positional Parameters cannot be changed
            //person.FirstName = "Ankit";

            Employee employee = new Employee
            {
                FirstName = "ABC",
                LastName = "DEF"
            };

            Student student = new Student();
            student.FirstName = person.FirstName;
            student.LastName = person.LastName;

            Console.WriteLine("Person1 equals Person "+person1.Equals(person));
            Console.WriteLine("Equality for person and person1"+ (person == person1));
            Console.WriteLine("Equality for person and person2"+(person == person2));

            var (firstname, id) = GetDetails(person1);

            Console.WriteLine("Name and Id are "+  firstname+ " "+id);

            Product pdt = new Product
            {
                Id = id,
                Name = "Dell"
            };

            Products productRecord = new(1, "Meghana", pdt);
            Console.WriteLine("Product Record"+productRecord);

            A a = new A();
            a.Id = 1;
            a.FirstName = "Meghana";

            var actual = await GetValueAsync();
            Console.WriteLine("actual"+actual);

        }

        static (string name, int id ) GetDetails(Person person)
        {
            var (firstname, lastname) = person;
            Console.WriteLine("Person values are "+ firstname +" "+lastname);
            return (firstname+lastname, 1);
        }

        static async Task<string> GetValueAsync()
        {
            await Task.Yield();
            return Guid.NewGuid().ToString();
        }

        //[AsyncMethodBuilder(typeof(CustomAwaitableAsyncMethodBuilder<>))]
        //static async CustomAwaitable<string> GetValueAsync()
        //{
        //    await Task.Yield();
        //    return Guid.NewGuid().ToString();
        //}
    }
}