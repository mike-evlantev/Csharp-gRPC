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
    }
}
