using Calculator;
using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50051";
        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Client connected successfully");
            });

            #region Dummy Service
            //var client = new DummyService.DummyServiceClient(channel);
            #endregion

            #region Greeting Service
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Mike",
                LastName = "Evlantev"
            };
            //var request = new GreetingRequest() { Greeting = greeting };

            //// This function is called from the server over 127.0.0.1:50051
            //var response = client.Greet(request);

            //var request = new GreetManyTimesRequest() { Greeting = greeting };
            //var response = client.GreetManyTimes(request);
            //while (await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Result);
            //    await Task.Delay(200);
            //}

            var request = new LongGreetRequest() { Greeting = greeting };
            var stream = client.LongGreet();
            foreach (var i in Enumerable.Range(1, 10))
            {
                await stream.RequestStream.WriteAsync(request);
            }

            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;

            Console.WriteLine(response.Result);
            #endregion

            #region Calculator Service
            //var client = new CalculatorService.CalculatorServiceClient(channel);

            //var request = GetSumRequest();
            //var response = client.Sum(request); // This function is called from the server over 127.0.0.1:50051

            //var request = GetFactorizationRequest();
            //var response = client.Factorise(request); // This function is called from the server over 127.0.0.1:50051
            //while (await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Result);
            //    await Task.Delay(200);
            //}
            #endregion

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        static SumRequest GetSumRequest()
        {
            Console.WriteLine("firstInt: ");
            var firstInt = int.Parse(Console.ReadLine());
            Console.WriteLine("secondInt: ");
            var secondInt = int.Parse(Console.ReadLine());
            return new SumRequest() { FirstInt = firstInt, SecondInt = secondInt };
        }

        static PrimeDecompositionRequest GetFactorizationRequest()
        {
            Console.WriteLine("Number to factorise: ");
            var num = int.Parse(Console.ReadLine());
            return new PrimeDecompositionRequest() { Int = num };
        }
    }
}
