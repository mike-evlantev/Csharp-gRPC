using Calculator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calculator.CalculatorService;

namespace server
{
    public class CalculatorServiceImpl : CalculatorServiceBase
    {
        public override Task<SumResponse> Sum(SumRequest request, ServerCallContext context)
        {
            var result = request.FirstInt + request.SecondInt;
            return Task.FromResult(new SumResponse() { Result = result });
        }

        public override async Task Factorise(PrimeDecompositionRequest request, IServerStreamWriter<PrimeDecompositionResponse> responseStream, ServerCallContext context)
        {
            for (int b = 2; request.Int > 1; b++)
            {
                while (request.Int % b == 0)
                {
                    request.Int /= b;
                    await responseStream.WriteAsync(new PrimeDecompositionResponse() { Result = b });
                }
            }
        }

        public override async Task<AverageResponse> Average(IAsyncStreamReader<AverageRequest> requestStream, ServerCallContext context)
        {
            List<int> streamResult = new List<int>();
            while (await requestStream.MoveNext())
            {
                streamResult.Add(requestStream.Current.Int);
            }
            double result = streamResult.Average();
            return new AverageResponse() { Result = result };
        }
    }
}
