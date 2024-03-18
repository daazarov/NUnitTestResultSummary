namespace NUnitTestResultSummary
{
    public interface IOutputGenerator
    {
        string GenerateOutput(ResultSummary summary, Options options);
    }
}
