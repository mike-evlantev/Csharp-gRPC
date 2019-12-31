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
            //var client = new GreetingService.GreetingServiceClient(channel);

            //var greeting = new Greeting()
            //{
            //    FirstName = "Mike",
            //    LastName = "Evlantev"
            //};
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

            //var request = new LongGreetRequest() { Greeting = greeting };
            //var stream = client.LongGreet();
            //foreach (var i in Enumerable.Range(1, 10))
            //{
            //    await stream.RequestStream.WriteAsync(request);
            //}
            //await stream.RequestStream.CompleteAsync();
            //var response = await stream.ResponseAsync;
            //Console.WriteLine(response.Result);

            //var stream = client.GreetEveryone();
            //var responseReaderTask = Task.Run(async () =>
            //{
            //    while (await stream.ResponseStream.MoveNext())
            //    {
            //        Console.WriteLine("Received: " + stream.ResponseStream.Current.Result);
            //    }
            //});

            //Greeting[] greetings =
            //{
            //    new Greeting() { FirstName = "Andy", LastName = "Anderson" },
            //    new Greeting() { FirstName = "Betty", LastName = "Burgers"},
            //    new Greeting() { FirstName = "Carl", LastName = "Carlson"},
            //    new Greeting() { FirstName = "Debbie", LastName = "Douglas" },
            //    new Greeting() { FirstName = "Eve", LastName = "Evans"},
            //    new Greeting() { FirstName = "Frank", LastName = "Franklin"}
            //};

            //foreach (var greeting in greetings)
            //{
            //    //Console.WriteLine("Sending: " + greeting.ToString());
            //    await stream.RequestStream.WriteAsync(new GreetEveryoneRequest() { Greeting = greeting });
            //}

            //await stream.RequestStream.CompleteAsync();
            //await responseReaderTask;
            #endregion

            #region Calculator Service
            var client = new CalculatorService.CalculatorServiceClient(channel);

            //var request = GetSumRequest();
            //var response = client.Sum(request); // This function is called from the server over 127.0.0.1:50051

            //var request = GetFactorizationRequest();
            //var response = client.Factorise(request); // This function is called from the server over 127.0.0.1:50051
            //while (await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Result);
            //    await Task.Delay(200);
            //}

            //var stream = client.Average();
            //foreach (var i in Enumerable.Range(1, 4))
            //{
            //    await stream.RequestStream.WriteAsync(new AverageRequest() { Int = i });
            //}
            //await stream.RequestStream.CompleteAsync();
            //var response = await stream.ResponseAsync;
            //Console.WriteLine(response.Result);

            //var stream = client.FindMax();
            //var responseReaderTask = Task.Run(async () =>
            //{
            //    while (await stream.ResponseStream.MoveNext())
            //        Console.WriteLine("Received: " + stream.ResponseStream.Current.Result);
            //});

            //int[] numbers = { 1, 5, 3, 6, 2, 20 };

            //foreach (var number in numbers)
            //{
            //    //Console.WriteLine("Sending: " + greeting.ToString());
            //    await stream.RequestStream.WriteAsync(new FindMaxRequest() { Int = number });
            //}

            //await stream.RequestStream.CompleteAsync();
            //await responseReaderTask;

            try
            {
                var number = int.Parse(Console.ReadLine());
                var response = client.Sqrt(new SqrtRequest() { Int = number });
                Console.WriteLine(response.Result);
            }
            catch (RpcException ex)
            {
                Console.WriteLine("RPC Error: " + ex.Status.Detail);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                throw;
            }
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
