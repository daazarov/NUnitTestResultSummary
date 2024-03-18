using NUnitTestResultSummary.Schemas.NUnit3;
using System.Xml.Linq;

namespace NUnitTestResultSummary
{
    public class ResultSummary
    {
        private readonly TestCaseElement[] totalTestCases;
        private readonly TestCaseElement[] passedTestCases;
        private readonly TestCaseElement[] failedTestCases;
        private readonly TestCaseElement[] skippedTestCases;
        private readonly TestCaseElement[] warningTestCases;
        private readonly TestCaseElement[] inconclusiveTestCases;

        public ResultSummary(NUnit3XmlTestsResult originalSummary)
        {
            var rootTestSuite = originalSummary.TestSuite;

            totalTestCases = FindUsingStack(rootTestSuite);

            passedTestCases = totalTestCases.Where(x => x.Result == Result.Passed).ToArray();
            failedTestCases = totalTestCases.Where(x => x.Result == Result.Failed).ToArray();
            inconclusiveTestCases = totalTestCases.Where(x => x.Result == Result.Inconclusive).ToArray();
            skippedTestCases = totalTestCases.Where(x => x.Result == Result.Skipped).ToArray();
            warningTestCases = totalTestCases.Where(x => x.Result == Result.Warning).ToArray();
        }

        public int TotalTestCount => totalTestCases.Count();
        public int FailedTestCount => failedTestCases.Count();
        public int SkippedTestCount => skippedTestCases.Count();
        public int PassedTestCount => passedTestCases.Count();
        public int InconclusiveTestCount => inconclusiveTestCases.Count();
        public int WarningTestCount => warningTestCases.Count();

        public TestCaseElement[] Failed => failedTestCases;
        public TestCaseElement[] Passed => passedTestCases;
        public TestCaseElement[] Skipped => skippedTestCases;
        public TestCaseElement[] Warning => warningTestCases;
        public TestCaseElement[] Inconclusive => inconclusiveTestCases;

        public void FindRecursive(TestSuiteElement testSuite, List<TestCaseElement> cases)
        {
            if (testSuite.TestCases != null && testSuite.TestCases.Count() > 0)
            {
                cases.AddRange(testSuite.TestCases);
            }

            if (testSuite.Inners != null && testSuite.Inners.Count() > 0)
            {
                foreach (var item in testSuite.Inners)
                {
                    FindRecursive(item, cases);
                }
            }
        }

        public TestCaseElement[] FindUsingStack(TestSuiteElement rootElement)
        {
            var stack = new Stack<TestSuiteElement>();
            var list = new List<TestCaseElement>();

            stack.Push(rootElement);

            while (stack.Any())
            {
                var current = stack.Pop();

                if (current.TestCases != null && current.TestCases.Count() > 0)
                {
                    list.AddRange(current.TestCases);
                }

                if (current.Inners != null && current.Inners.Count() > 0)
                {
                    foreach (var item in current.Inners)
                    {
                        stack.Push(item);
                    }
                }
            }

            return list.ToArray();
        }
    }
}
