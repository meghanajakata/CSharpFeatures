using CSharpFeatures.Async;
using CSharpFeatures.ExtendedPropertyPattern;
using CSharpFeatures.Structures;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace CSharpFeatures;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // Validating Null values
        /*string? email = null;
        ValidateMail(email);*/

        // Func being converted as var
        Func<string, int> parse = (string s) => int.Parse(s);
        var par = (string s) => int.Parse(s);

        //Readonly Record Struct
        PointAxis point = new PointAxis(2.3, 4.5, 9.0);
        //point.X = 10.2;

        //Record Struct
        /*Doctor doctor = new Doctor(1,"Meghana",12000.0);
        doctor.Name = "Shilpa";

        Person person = new("Nancy", "Singh", "10");
        Console.WriteLine("person details"+person);*/


        // AsyncMethodBuilder
        /*CustomAsync custom = CustomAsync.Create();
        int result = await custom.AsyncMethod();
        Console.WriteLine("Async resukt is "+result);

        var task = RunAsync();
        task.GetAwaiter().GetResult();

        Console.Write("****************");*/

        /*var expected = Guid.NewGuid().ToString();
        

        var actual = await GetValueAsync();

        Console.WriteLine("Expected value is " + actual);
        Console.WriteLine("Actual value is "+expected);

        if (!ReferenceEquals(expected, actual))
        {
            Console.WriteLine($"Expected {expected} {actual}");
        }

        Console.WriteLine("Done!");*/

        // Extended Pattern Matching
        /*Computing computing = new Computing();
        computing.Method();*/


        // Improvement in structure types
        Measurement measurement1 = new();
        Console.WriteLine(measurement1.ToString());

        Measurement measurement2 = new(1,"description");   
        Console.WriteLine(measurement2.ToString());

        Measurement measurement3 = default;
        Console.WriteLine(measurement3.ToString());
    }

    //[public override string ToString() => $"{Value} ({Description})";(typeof(CustomAwaitableAsyncMethodBuilder<string>))]
    [AsyncMethodBuilder(typeof(CustomAwaitableAsyncMethodBuilder<>))]
    static async CustomAwaitable<string> GetValueAsync()
    {
        await Task.Yield();
        return Guid.NewGuid().ToString(); 
    }

    /*static void ValidateMail(string email !!)
    {
        //ArgumentNullException.ThrowIfNull(email, "email");
        Console.Write(email);
    }*/

    public static async Task DelayAsync(int milliseconds)
    {
        await Task.Delay(milliseconds).ToAsync();
    }
    static void ValidateMail(string email)
    {
        ArgumentNullException.ThrowIfNull(email, "email");
        //Console.Write(email);
    }

    static async Task RunAsync()
    {
        Console.WriteLine("Just a second...");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Console.WriteLine("Ok!");
    }

    

}