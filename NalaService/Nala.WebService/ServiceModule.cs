using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using System;
using Nala.Core.NLP;

namespace Nala.Service.Web
{
    public class StatementAnalysisResponse
    {
        public string Statement { get; set; }
        public ParsedStatementFactory.ParseResult Parse { get; set; }
    }

    public class StatementAnalysisRequest
    {
        public string Statement { get; set; }
    }

    public class ServiceModule : NancyModule
    {
        private static NlpService _nlpService;
        //private static readonly IWorkerService _workerService;

        static ServiceModule()
        {
           // _workerService = new WorkerService();
            _nlpService = new NlpService();
        }

		public async Task<string> GetData()
		{
			var foo = "Howdy";
			return foo;
		}
        public ServiceModule()
        {
            

            Get["/", true] = async (x, ct) => GetVersion(); //

            Post["/statements", true] = async (_, ct) =>
            {
                var statements = this.Bind<StatementAnalysisRequest>();
                var response = await CreateStatements(statements);
                return Response.AsJson(response);
            };

            //Before += async (ctx, ct) =>
            //    {
            //        //this.AddToLog("Before Hook Delay\n");
            //        await Task.Delay(0);

            //        return null;
            //    };

            //After += async (ctx, ct) =>
            //    {
            //        //this.AddToLog("After Hook Delay\n");
            //        await Task.Delay(0);
            //        //this.AddToLog("After Hook Complete\n");


            //    };
        }

        private string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

			//throw new InvalidOperationException ("woops");

            return version;
        }

        private async Task<StatementAnalysisResponse> CreateStatements(StatementAnalysisRequest statements)
        {
            // farm out to all deps. 

            // compose back and send out
            var response = new StatementAnalysisResponse() { Statement = statements.Statement };
            var parse = _nlpService.ParseStatement(statements.Statement);

            response.Parse = parse;
            return response;
        }
    }

}