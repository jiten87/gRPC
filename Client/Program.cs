﻿using Greet;
using Grpc.Core;
using Grpc.Net.Client;

namespace Client
{
    class program
    {
        static async Task Main(string[] args)
        {
            // The port number must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:7206");
            var client = new Greeter.GreeterClient(channel);

            //DoSimpleGreet(client);
            //await DOGreetClientStreaming(client);
            //await DoGreetServerStreaming(client);
            await DoGreetBiDi(client);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        public static void DoSimpleGreet(Greeter.GreeterClient client)
        {
            var greeting = new Greeting()
            {
                FirstName = "Jiten",
                LastName = "M"
            };

            var request = new GreetRequest() { Greeting = greeting };
            var response = client.Greet(request);

            Console.WriteLine(response.Result);
        }

        public static async Task DOGreetClientStreaming(Greeter.GreeterClient client)
        {
            var greeting = new Greeting()
            {
                FirstName = "Jiten",
                LastName = "M"
            };

            var request = new GreetRequest() { Greeting = greeting };

            var stream = client.GreetClientStreaming();

            foreach (int i in Enumerable.Range(1, 10))
            {
                await stream.RequestStream.WriteAsync(request);
            }

            await stream.RequestStream.CompleteAsync();
            var response = stream.ResponseAsync;
            Console.WriteLine(response.Result);
        }

        public static async Task DoGreetServerStreaming(Greeter.GreeterClient client)
        {
            Greeting greeting = new Greeting()
            {
                FirstName = "jiten",
                LastName = "M"
            };

            var request = new GreetRequest() { Greeting = greeting };
            var response = client.GreetServerStreaming(request);

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                //await Task.Delay(200);
            }


        }

        public static async Task DoGreetBiDi(Greeter.GreeterClient client)
        {
            var stream = client.GreetBiDi();

            var responseReader = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Received : " + stream.ResponseStream.Current.Result);
                }
            });

            Greeting[] greetings =
            {
                new Greeting() { FirstName = "jiten", LastName = "M" },
                new Greeting() { FirstName = "Ashish", LastName = "J" },
                new Greeting() { FirstName = "Ankit", LastName = "S" }
            };
            foreach (var greeting in greetings)
            {
                Console.WriteLine("Sending : " + greeting.ToString());
                await stream.RequestStream.WriteAsync(new GreetRequest()
                {
                    Greeting = greeting
                });
            }
        }



    }
}

