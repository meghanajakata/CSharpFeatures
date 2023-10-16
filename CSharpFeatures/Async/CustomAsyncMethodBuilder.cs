using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public struct CustomAsyncMethodBuilder
{
    private TaskCompletionSource<bool> tcs;

    public static CustomAsyncMethodBuilder Create()
    {
        var builder = new CustomAsyncMethodBuilder();
        builder.tcs = new TaskCompletionSource<bool>();
        return builder;
    }

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
        // Start the asynchronous operation.
        stateMachine.MoveNext();
    }

    public void SetResult()
    {
        tcs.SetResult(true);
    }

    public void SetException(Exception exception)
    {
        tcs.SetException(exception);
    }

    public Task Task => tcs.Task;

    public CustomAwaiter GetAwaiter()
    {
        return new CustomAwaiter(this); // Return a custom awaiter object.
    }
}

public struct CustomAwaiter : INotifyCompletion
{
    private readonly CustomAsyncMethodBuilder builder;

    public CustomAwaiter(CustomAsyncMethodBuilder builder)
    {
        this.builder = builder;
    }

    public bool IsCompleted => builder.Task.IsCompleted;

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine =>
            awaiter.OnCompleted(stateMachine.MoveNext);

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine =>
            awaiter.UnsafeOnCompleted(stateMachine.MoveNext);

    public void OnCompleted(Action continuation)
    {
        //builder.Task.ContinueWith(_ => continuation.Invoke());
        builder.Task.GetAwaiter().OnCompleted(continuation);
    }

    public void GetResult()
    {
        // This method is required, but in this simple example, we don't need to do anything here.
    }
}

public static class CustomAsyncExtensions
{
    public static CustomAsyncMethodBuilder ToAsync(this Task task)
    {
        var builder = CustomAsyncMethodBuilder.Create();
        Console.WriteLine("Builder value is "+builder.Task);

        return builder;
    }
}