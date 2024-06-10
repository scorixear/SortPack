using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System.Collections.Concurrent;
using TestExecutionContext = NUnit.Framework.Internal.TestExecutionContext;

namespace SortPack.Infrastructure.UnitTests.NUnit;

public class CancelOnInconclusiveAttribute : NUnitAttribute, ITestAction
{
    private static readonly ConcurrentDictionary<string, bool> CancelTestCases = new();
    public void BeforeTest(ITest test)
    {
        string key = GetTestKey(test);
        if (CancelTestCases.TryGetValue(key, out bool cancel) && cancel && IsTestCase(test))
        {
            Assert.Ignore("Skipping test - previous inconclusive");
        }
    }
    public void AfterTest(ITest test)
    {
        if (IsTestCase(test) && TestExecutionContext.CurrentContext.CurrentResult.ResultState == ResultState.Inconclusive)
        {
            string key = GetTestKey(test);
            CancelTestCases[key] = true;
        }
    }

    public ActionTargets Targets => ActionTargets.Test;

    private bool IsTestCase(ITest test)
    {
        return test.IsSuite == false && test.Parent != null && test.Parent.IsSuite && test.Parent is ParameterizedMethodSuite;
    }

    private string GetTestKey(ITest test)
    {
        return $"{test.Parent?.FullName}.{test.MethodName}";
    }
}
