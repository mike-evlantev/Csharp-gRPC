// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: calculator.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Calculator {
  public static partial class CalculatorService
  {
    static readonly string __ServiceName = "calculator.CalculatorService";

    static readonly grpc::Marshaller<global::Calculator.SumRequest> __Marshaller_calculator_SumRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Calculator.SumRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Calculator.SumResponse> __Marshaller_calculator_SumResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Calculator.SumResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Calculator.PrimeDecompositionRequest> __Marshaller_calculator_PrimeDecompositionRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Calculator.PrimeDecompositionRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Calculator.PrimeDecompositionResponse> __Marshaller_calculator_PrimeDecompositionResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Calculator.PrimeDecompositionResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Calculator.SumRequest, global::Calculator.SumResponse> __Method_Sum = new grpc::Method<global::Calculator.SumRequest, global::Calculator.SumResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Sum",
        __Marshaller_calculator_SumRequest,
        __Marshaller_calculator_SumResponse);

    static readonly grpc::Method<global::Calculator.PrimeDecompositionRequest, global::Calculator.PrimeDecompositionResponse> __Method_Factorise = new grpc::Method<global::Calculator.PrimeDecompositionRequest, global::Calculator.PrimeDecompositionResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "Factorise",
        __Marshaller_calculator_PrimeDecompositionRequest,
        __Marshaller_calculator_PrimeDecompositionResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Calculator.CalculatorReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of CalculatorService</summary>
    [grpc::BindServiceMethod(typeof(CalculatorService), "BindService")]
    public abstract partial class CalculatorServiceBase
    {
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Calculator.SumResponse> Sum(global::Calculator.SumRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Server Stream
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task Factorise(global::Calculator.PrimeDecompositionRequest request, grpc::IServerStreamWriter<global::Calculator.PrimeDecompositionResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for CalculatorService</summary>
    public partial class CalculatorServiceClient : grpc::ClientBase<CalculatorServiceClient>
    {
      /// <summary>Creates a new client for CalculatorService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public CalculatorServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for CalculatorService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public CalculatorServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected CalculatorServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected CalculatorServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Calculator.SumResponse Sum(global::Calculator.SumRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Sum(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Calculator.SumResponse Sum(global::Calculator.SumRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Sum, null, options, request);
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Calculator.SumResponse> SumAsync(global::Calculator.SumRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SumAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Calculator.SumResponse> SumAsync(global::Calculator.SumRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Sum, null, options, request);
      }
      /// <summary>
      /// Server Stream
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::Calculator.PrimeDecompositionResponse> Factorise(global::Calculator.PrimeDecompositionRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Factorise(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Server Stream
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::Calculator.PrimeDecompositionResponse> Factorise(global::Calculator.PrimeDecompositionRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_Factorise, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override CalculatorServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new CalculatorServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(CalculatorServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Sum, serviceImpl.Sum)
          .AddMethod(__Method_Factorise, serviceImpl.Factorise).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, CalculatorServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Sum, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Calculator.SumRequest, global::Calculator.SumResponse>(serviceImpl.Sum));
      serviceBinder.AddMethod(__Method_Factorise, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Calculator.PrimeDecompositionRequest, global::Calculator.PrimeDecompositionResponse>(serviceImpl.Factorise));
    }

  }
}
#endregion