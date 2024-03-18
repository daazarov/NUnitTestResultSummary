namespace NUnitTestResultSummary
{
    public class OutputGeneratorFactory
    {
        private static Dictionary<OutputFormat, IOutputGenerator> _registry = new Dictionary<OutputFormat, IOutputGenerator>()
        {
            { OutputFormat.markdown, new MarkdownOutputGenerator() }
        };
        
        public static IOutputGenerator CreateGenerator(OutputFormat format)
        {
            if (_registry.TryGetValue(format, out var generator))
            { 
                return generator;
            }

            throw new IndexOutOfRangeException(nameof(format));
        }
    }
}
