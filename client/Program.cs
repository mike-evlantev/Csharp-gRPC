using Dummy;
using Greet;
using Grpc.Core;
using Sum;
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

            #region Sum Service
            //var client = new SumService.SumServiceClient(channel);

            //Console.WriteLine("firstInt: ");
            //var firstInt = int.Parse(Console.ReadLine());
            //Console.WriteLine("secondInt: ");
            //var secondInt = int.Parse(Console.ReadLine());
            //var request = new SumRequest() { FirstInt = firstInt, SecondInt = secondInt };

            //// This function is called from the server over 127.0.0.1:50051
            //var response = client.Sum(request);
            #endregion

            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Mike",
                LastName = "Evlantev"
            };
            //var request = new GreetingRequest() { Greeting = greeting };

            //// This function is called from the server over 127.0.0.1:50051
            //var response = client.Greet(request);

            var request = new GreetManyTimesRequest() { Greeting = greeting };
            var response = client.GreetManyTimes(request);
            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }


            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
