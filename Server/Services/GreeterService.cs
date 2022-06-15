using Grpc.Core;
using Server;
using Greet;

namespace Server.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<GreetReply> Greet(GreetRequest request, ServerCallContext context)
        {
            string result = String.Format("hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);
            return Task.FromResult(new GreetReply() {Result = result}) ;
        }

        public override Task<ReplyCreateKey> CreateKey(RequestCreateKey requestCreateKey, ServerCallContext context)
        {

            return Task.FromResult(new ReplyCreateKey() { Response = $"{requestCreateKey.Name} with label:  {requestCreateKey.Label} Key Created! and label1: {requestCreateKey.Label1}" });
        }

        public override async Task<GreetReply> GreetClientStreaming(IAsyncStreamReader<GreetRequest> requestStream, ServerCallContext context)
        {
            string result = string.Empty;

            while (await requestStream.MoveNext())
            {
                result += String.Format("Hello {0} {1} {2}",requestStream.Current.Greeting.FirstName, requestStream.Current.Greeting.LastName, Environment.NewLine);

            }
            return new GreetReply() {Result = result};
        }

        public override async Task GreetServerStreaming(GreetRequest request, IServerStreamWriter<GreetReply> replyStream, ServerCallContext context)
        {
            Console.WriteLine("The server received the request : ");
            Console.WriteLine(request.ToString());
            string result = String.Format("hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);

            foreach(var i in Enumerable.Range(0,10))
            {
                await replyStream.WriteAsync(new GreetReply() { Result = result});
            }
        }

        public override async Task GreetBiDi(IAsyncStreamReader<GreetRequest> requestStream, IServerStreamWriter<GreetReply> replyStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                await replyStream.WriteAsync(new GreetReply() { Result = String.Format("Hello {0} {1} {2}", requestStream.Current.Greeting.FirstName, requestStream.Current.Greeting.LastName, Environment.NewLine) });
            };
        }
            



    }
}