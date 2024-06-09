using System.Runtime;

namespace SortPack.Infrastructure.UnitTests.NUnit
{
    public static class TimeoutHandler
    {
        public static async Task HandleActionWithCancellationToken(int timeOutInMilliseconds, Action<CancellationToken> action)
        {
            using (var cts = new CancellationTokenSource())
            {
                var token = cts.Token;
                Task task = Task.Run(() =>
                {
                    try
                    {
                        action.Invoke(token);
                    }
                    catch (OperationCanceledException)
                    {
                        Assert.Inconclusive("Test exceeded timeout of " + timeOutInMilliseconds + "ms");
                    }
                    catch (InsufficientExecutionStackException)
                    {
                        Assert.Inconclusive("Stack limit reached");
                    }
                    catch (ThreadAbortException)
                    {
                        Assert.Inconclusive("Thread aborted");
                    }
                }, token);
                if (await Task.WhenAny(Task.Delay(timeOutInMilliseconds), task) == task)
                {
                    await task;
                }
                else
                {
                    cts.Cancel();
                    try
                    {
                        await task;
                    }
                    catch (OperationCanceledException)
                    {
                        Assert.Inconclusive("Test exceeded timeout of " + timeOutInMilliseconds + "ms");
                    }
                    catch (InsufficientExecutionStackException)
                    {
                        Assert.Inconclusive("Stack limit reached");
                    }
                    catch (ThreadAbortException)
                    {
                        Assert.Inconclusive("Thread aborted");
                    }
                }
            }
        }


        public static async Task HandleActionWithoutCancellationToken(int timeOutInMilliseconds, Action action)
        {
            using (var cts = new CancellationTokenSource())
            {
                var token = cts.Token;
                Task task = Task.Run(() =>
                {
                    try
                    {
                        ControlledExecution.Run(action, token);
                    }
                    catch (OperationCanceledException)
                    {
                        Assert.Inconclusive("Test exceeded timeout of " + timeOutInMilliseconds + "ms");
                    }
                    catch (InsufficientExecutionStackException)
                    {
                        Assert.Inconclusive("Stack limit reached");
                    }
                    catch (ThreadAbortException)
                    {
                        Assert.Inconclusive("Thread aborted");
                    }
                }, token);
                if (await Task.WhenAny(Task.Delay(timeOutInMilliseconds), task) == task)
                {
                    await task;
                }
                else
                {
                    cts.Cancel();
                    try
                    {
                        await task;
                    }
                    catch (OperationCanceledException)
                    {
                        Assert.Inconclusive("Test exceeded timeout of " + timeOutInMilliseconds + "ms");
                    }
                    catch (InsufficientExecutionStackException)
                    {
                        Assert.Inconclusive("Stack limit reached");
                    }
                    catch (ThreadAbortException)
                    {
                        Assert.Inconclusive("Thread aborted");
                    }
                }
            }
        }

        private static void Run(Action<CancellationToken> action, CancellationToken token, int stackSize)
        {
            var thread = new Thread(() => action.Invoke(token), stackSize);
            thread.Start();
            thread.Join();
        }
    }
}
