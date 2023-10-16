using System.Runtime.CompilerServices;

namespace CSharpFeatures.Async;
public class CustomAsync
{
    public static CustomAsync Create()
    {
        return new CustomAsync();
    }

    public async Task<int> AsyncMethod()
    {
        var builder = AsyncTaskMethodBuilder<int>.Create();

        await Task.Delay(1000);

        builder.SetResult(42);

        return await builder.Task;
    }
}
