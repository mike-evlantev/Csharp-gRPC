using Grpc.Core;
using Sum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sum.SumService;

namespace server
{
    public class SumServiceImpl : SumServiceBase
    {
        public override Task<SumResponse> Sum(SumRequest request, ServerCallContext context)
        {
            var result = request.FirstInt + request.SecondInt;
            return Task.FromResult(new SumResponse() { Result = result });
        }
    }
}
