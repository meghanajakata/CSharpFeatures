using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_9.Async
{
    [AsyncMethodBuilder(typeof(CustomAwaitableAsyncMethodBuilder<>))]
    public readonly struct CustomAwaitable<T>
    {
        readonly ValueTask<T> _valueTask;

        public CustomAwaitable(ValueTask<T> valueTask)
        {
            _valueTask = valueTask;
        }

        public ValueTaskAwaiter<T> GetAwaiter() => _valueTask.GetAwaiter();
    }

    public class CustomAwaitableAsyncMethodBuilder<T>
    {
        Exception? _exception;
        bool _hasResult;
        SpinLock _lock;
        T? _result;
        TaskCompletionSource<T>? _source;

        public CustomAwaitable<T> Task
        {
            get
            {
                var lockTaken = false;
                try
                {
                    _lock.Enter(ref lockTaken);
                    if (_exception is not null)
                        return new CustomAwaitable<T>(ValueTask.FromException<T>(_exception));
                    if (_hasResult)
                        return new CustomAwaitable<T>(ValueTask.FromResult(_result!));
                    return new CustomAwaitable<T>(
                        new ValueTask<T>(
                            (_source ??= new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously))
                            .Task
                        )
                    );
                }

                finally
                {
                    if (lockTaken)
                        _lock.Exit();
                }

            }
        }

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

        public static CustomAwaitableAsyncMethodBuilder<T> Create() => new()
        {
            _lock = new SpinLock(Debugger.IsAttached)
        };

        public void SetException(Exception exception)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                if (Volatile.Read(ref _source) is { } source)
                {
                    source.TrySetException(exception);
                }
                else
                {
                    _exception = exception;
                }
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit();
            }
        }

        public void SetResult(T result)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                if (Volatile.Read(ref _source) is { } source)
                {
                    source.TrySetResult(result);
                }
                else
                {
                    _result = result;
                    _hasResult = true;
                }
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit();
            }
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine) { }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine => stateMachine.MoveNext();


    }


}


