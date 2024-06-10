namespace SortPack.Infrastructure.UnitTests.NUnit;

public static class TimeoutHandler
{
    public static async Task HandleActionWithCancellationToken(int timeOutInMilliseconds, Action<CancellationToken> action)
    {
        using (CancellationTokenSource cts = new())
        {
            CancellationToken token = cts.Token;
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
                catch (OutOfMemoryException)
                {
                    Assert.Inconclusive("Out of memory");
                }
            }, token);
            if (await Task.WhenAny(Task.Delay(timeOutInMilliseconds), task) == task)
            {
                await task;
            }
            else
            {
                await cts.CancelAsync();
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
                catch (OutOfMemoryException)
                {
                    Assert.Inconclusive("Out of memory");
                }
            }
        }
    }
}
