using CommandLine;
using NUnitTestResultSummary.Schemas.NUnit3;

namespace NUnitTestResultSummary
{
    public class Program
    {
        private static Options _options;

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                    (opts) => RunOptionsAndReturnExitCode(opts),
                    (errs) => HandleParseError(errs));

            Console.WriteLine("Return code= {0}", result);
        }

        static int RunOptionsAndReturnExitCode(Options options)
        {
            _options = options;

            if (!File.Exists(_options.InputFile))
            {
                Console.WriteLine($"Error: Nunit File result not found - {_options.InputFile}.");
                return -2; // error
            }

            try
            {
                return NUnitParser.NUnit3.ParseXmlResult(
                    xmlInput: File.ReadAllText(options.InputFile),
                    handleResult: (res) => HandleNUnitTestsResult(res),
                    handleError: (ex) => HandleNUnitTestsResultError(ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -2;
            }
        }

        static int HandleParseError(IEnumerable<Error> errors)
        {
            return -2;
        }

        static int HandleNUnitTestsResult(NUnit3XmlTestsResult result)
        { 
            var summary = new ResultSummary(result);
            var output = OutputGeneratorFactory.CreateGenerator(_options.OutputFormat).GenerateOutput(summary, _options);

            File.WriteAllText($"nunit-testresult-summary.md", output);

            return 0;
        }

        static int HandleNUnitTestsResultError(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -2; // error
        }
    }
}