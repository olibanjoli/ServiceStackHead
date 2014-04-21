using System;
using System.Diagnostics;
using Funq;
using ServiceStack;

namespace ServiceStackHead
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start("http://localhost:1337/");

            var client = new JsonServiceClient("http://localhost:1337");
            client.Head(new SampleHeadRequest { EMail = "test@blub.com" });
        }
    }

    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
            : base("ServiceStack HEAD Test", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
        }
    }

    [Route("/sample/{EMail}", "HEAD")]
    public class SampleHeadRequest : IReturnVoid
    {
        public string EMail { get; set; }
    }

    public class SampleService : Service
    {
        public object Head(SampleHeadRequest request)
        {
            Console.WriteLine("EMail: " + request.EMail);

            // EMail is not properly set here
            Debugger.Break();

            return null;
        }
    }
}
