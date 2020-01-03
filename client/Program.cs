using Calculator;
using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calculator.CalculatorService;
using static Greet.GreetingService;

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
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("*************** Greeting Service ***************");
            Console.WriteLine("Greet               - Unary                  - 1");
            Console.WriteLine("GreetManyTimes      - Server Stream          - 2");
            Console.WriteLine("LongGreet           - Client Stream          - 3");
            Console.WriteLine("GreetEveryone       - Bidirectional Stream   - 4");
            Console.WriteLine("GreetWithDeadline   - Unary w/ Deadline      - 5");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("************** Calculator Service **************");
            Console.WriteLine("Sum                  - Unary                 - 6");
            Console.WriteLine("Factorize            - Server Stream         - 7");
            Console.WriteLine("Average              - Client Stream         - 8");
            Console.WriteLine("FindMax              - Bidirectional Stream  - 9");
            Console.WriteLine("SquareRoot           - Unary w/ Err Handling - 10");

            int selection = -1;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Please select an option:");
            var isInt = int.TryParse(Console.ReadLine(), out selection);
            while (!isInt || selection < 1 || selection > 10)
            {
                Console.WriteLine("Invalid selection. Try again:");
                isInt = int.TryParse(Console.ReadLine(), out selection);
            }

            switch (selection)
            {
                case 1:
                    DoGreet(new GreetingServiceClient(channel));
                    break;
                case 2:
                    await DoGreetManyTimes(new GreetingServiceClient(channel));
                    break;
                case 3:
                    await DoLongGreet(new GreetingServiceClient(channel));
                    break;
                case 4:
                    await DoGreetEveryone(new GreetingServiceClient(channel));
                    break;
                case 5:
                    DoGreetWithDeadline(new GreetingServiceClient(channel));
                    break;
                case 6:
                    DoSum(new CalculatorServiceClient(channel));
                    break;
                case 7:
                    await DoFactorization(new CalculatorServiceClient(channel));
                    break;
                case 8:
                    await DoAverage(new CalculatorServiceClient(channel));
                    break;
                case 9:
                    await DoFindMax(new CalculatorServiceClient(channel));
                    break;
                case 10:
                    DoSqrt(new CalculatorServiceClient(channel));
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        #region Greeting Service

        // DoGreet()
        static void DoGreet(GreetingServiceClient client)
        {
            var greeting = new Greeting()
            {
                FirstName = "Mike",
                LastName = "Evlantev"
            };
            var request = new GreetingRequest() { Greeting = greeting };

            // This function is called from the server over 127.0.0.1:50051
            var response = client.Greet(request);
            Console.WriteLine(response.Result);
        }

        // DoGreetManyTimes()
        static async Task DoGreetManyTimes(GreetingServiceClient client)
        {
            var greeting = new Greeting()
            {
                FirstName = "Mike",
                LastName = "Evlantev"
            };
            var request = new GreetManyTimesRequest() { Greeting = greeting };
            var response = client.GreetManyTimes(request);
            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }
        }

        // DoLongGreet()
        static async Task DoLongGreet(GreetingServiceClient client)
        {
            var greeting = new Greeting()
            {
                FirstName = "Mike",
                LastName = "Evlantev"
            };
            var request = new LongGreetRequest() { Greeting = greeting };
            var stream = client.LongGreet();
            foreach (var i in Enumerable.Range(1, 10))
            {
                await stream.RequestStream.WriteAsync(request);
            }
            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;
            Console.WriteLine(response.Result);
        }

        // DoGreetEveryone()
        static async Task DoGreetEveryone(GreetingServiceClient client)
        {
            var stream = client.GreetEveryone();
            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Received: " + stream.ResponseStream.Current.Result);
                }
            });

            Greeting[] greetings =
            {
                new Greeting() { FirstName = "Andy", LastName = "Anderson" },
                new Greeting() { FirstName = "Betty", LastName = "Burgers"},
                new Greeting() { FirstName = "Carl", LastName = "Carlson"},
                new Greeting() { FirstName = "Debbie", LastName = "Douglas" },
                new Greeting() { FirstName = "Eve", LastName = "Evans"},
                new Greeting() { FirstName = "Frank", LastName = "Franklin"}
            };

            foreach (var greeting in greetings)
            {
                //Console.WriteLine("Sending: " + greeting.ToString());
                await stream.RequestStream.WriteAsync(new GreetEveryoneRequest() { Greeting = greeting });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;
        }

        // DoGreetWithDeadline()
        static void DoGreetWithDeadline(GreetingServiceClient client)
        {
            try
            {
                var greeting = new Greeting()
                {
                    FirstName = "Mike",
                    LastName = "Evlantev"
                };
                var deadline = 500; // 100 - deadline will be exceeded, 500 - deadline will not be exceeded
                Console.WriteLine("GreetWithDeadline(): Server method has a delay of 300 ms");
                Console.WriteLine($"GreetWithDeadline(): Client method has a deadline of {deadline} ms");
                var response = client.GreetWithDeadline(new GreetingRequest() { Greeting = greeting }, // GreetWithDeadline() has 300 delay
                                                        deadline: DateTime.UtcNow.AddMilliseconds(deadline));
                Console.WriteLine(response.Result);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("Error: " + ex.Status.Detail);
                throw;
            }
        }

        #endregion

        #region Calculator Service

        // DoSum()
        static void DoSum(CalculatorServiceClient client)
        {
            var request = GetSumRequest();
            var response = client.Sum(request); // This function is called from the server over 127.0.0.1:50051
            Console.WriteLine(response.Result);
        }

        // DoFactorization()
        static async Task DoFactorization(CalculatorServiceClient client)
        {
            var request = GetFactorizationRequest();
            var response = client.Factorise(request); // This function is called from the server over 127.0.0.1:50051
            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }
        }

        // DoAverage()
        static async Task DoAverage(CalculatorServiceClient client)
        {
            var stream = client.Average();
            foreach (var i in Enumerable.Range(1, 4))
            {
                await stream.RequestStream.WriteAsync(new AverageRequest() { Int = i });
            }
            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;
            Console.WriteLine(response.Result);
        }

        // DoFindMax()
        static async Task DoFindMax(CalculatorServiceClient client)
        {
            var stream = client.FindMax();
            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                    Console.WriteLine("Received: " + stream.ResponseStream.Current.Result);
            });

            int[] numbers = { 1, 5, 3, 6, 2, 20 };

            foreach (var number in numbers)
            {
                //Console.WriteLine("Sending: " + greeting.ToString());
                await stream.RequestStream.WriteAsync(new FindMaxRequest() { Int = number });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;
        }

        // DoSquareRoot()
        static void DoSqrt(CalculatorServiceClient client)
        {
            try
            {
                Console.WriteLine("Square root of what number?");
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
        }

        #endregion

        #region Helper Methods

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

        #endregion
    }
}
