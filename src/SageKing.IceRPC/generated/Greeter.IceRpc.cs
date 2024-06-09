// <auto-generated/>
// slicec-cs version: '0.3.1'
// Generated from file: 'Greeter.slice'

#nullable enable

#pragma warning disable CS1591 // Missing XML Comment
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS0619 // Type or member is obsolete

using IceRpc.Slice;
using ZeroC.Slice;

namespace MyClient;

/// <summary>Represents a simple greeter.</summary>
/// <remarks>The Slice compiler generated this client-side interface from Slice interface <c>VisitorCenter::Greeter</c>.
/// It's implemented by <see cref="GreeterProxy" />.</remarks>
public partial interface IGreeter
{
    /// <summary>Creates a personalized greeting.</summary>
    /// <param name="name">The name of the person to greet.</param>
    /// <param name="features">The invocation features.</param>
    /// <param name="cancellationToken">A cancellation token that receives the cancellation requests.</param>
    /// <returns>The greeting.</returns>
    global::System.Threading.Tasks.Task<string> GreetAsync(
        string name,
        IceRpc.Features.IFeatureCollection? features = null,
        global::System.Threading.CancellationToken cancellationToken = default);
}

/// <summary>Implements <see cref="IGreeter" /> by making invocations on a remote IceRPC service.
/// This remote service must implement Slice interface VisitorCenter::Greeter.</summary>
/// <remarks>The Slice compiler generated this record struct from the Slice interface <c>VisitorCenter::Greeter</c>.</remarks>
[SliceTypeId("::VisitorCenter::Greeter")]
public readonly partial record struct GreeterProxy : IGreeter, IProxy
{
    /// <summary>Provides static methods that encode operation arguments into request payloads.</summary>
    /// <remarks>The Slice compiler generated this static class from the Slice interface <c>VisitorCenter::Greeter</c>.</remarks>
    public static class Request
    {
        /// <summary>Encodes the argument of operation <c>greet</c> into a request payload.</summary>
        /// <param name="name">The name of the person to greet.</param>
        /// <param name="encodeOptions">The Slice encode options.</param>
        /// <returns>The payload encoded with <see cref="SliceEncoding.Slice2" />.</returns>
        public static global::System.IO.Pipelines.PipeReader EncodeGreet(
            string name,
            SliceEncodeOptions? encodeOptions = null)
        {
            var pipe_ = new global::System.IO.Pipelines.Pipe(
                encodeOptions?.PipeOptions ?? SliceEncodeOptions.Default.PipeOptions);
            var encoder_ = new SliceEncoder(pipe_.Writer, SliceEncoding.Slice2, default);

            Span<byte> sizePlaceholder_ = encoder_.GetPlaceholderSpan(4);
            int startPos_ = encoder_.EncodedByteCount;

            encoder_.EncodeString(name);
            encoder_.EncodeVarInt32(Slice2Definitions.TagEndMarker);

            SliceEncoder.EncodeVarUInt62((ulong)(encoder_.EncodedByteCount - startPos_), sizePlaceholder_);

            pipe_.Writer.Complete();
            return pipe_.Reader;
        }
    }

    /// <summary>Provides a <see cref="ResponseDecodeFunc{T}" /> for each operation defined in Slice interface VisitorCenter::Greeter.</summary>
    /// <remarks>The Slice compiler generated this static class from the Slice interface <c>VisitorCenter::Greeter</c>.</remarks>
    public static class Response
    {
        /// <summary>Decodes an incoming response for operation <c>greet</c>.</summary>
        public static global::System.Threading.Tasks.ValueTask<string> DecodeGreetAsync(
            IceRpc.IncomingResponse response,
            IceRpc.OutgoingRequest request,
            IProxy sender,
            global::System.Threading.CancellationToken cancellationToken) =>
            response.DecodeReturnValueAsync(
                request,
                SliceEncoding.Slice2,
                sender,
                (ref SliceDecoder decoder) => decoder.DecodeString(),
                defaultActivator: null,
                cancellationToken);
    }

    /// <summary>Represents the default path for IceRPC services that implement Slice interface
    /// <c>VisitorCenter::Greeter</c>.</summary>
    public const string DefaultServicePath = "/VisitorCenter.Greeter";

    /// <inheritdoc/>
    public SliceEncodeOptions? EncodeOptions { get; init; }

    /// <inheritdoc/>
    public required IceRpc.IInvoker Invoker { get; init; }

    /// <inheritdoc/>
    public IceRpc.ServiceAddress ServiceAddress { get; init; } = _defaultServiceAddress;

    private static IceRpc.ServiceAddress _defaultServiceAddress =
        new(IceRpc.Protocol.IceRpc) { Path = DefaultServicePath };

    /// <summary>Creates a relative proxy from a path.</summary>
    /// <param name="path">The path.</param>
    /// <returns>The new relative proxy.</returns>
    public static GreeterProxy FromPath(string path) =>
        new(IceRpc.InvalidInvoker.Instance, new IceRpc.ServiceAddress { Path = path });

    /// <summary>Constructs a proxy from an invoker, a service address and encode options.</summary>
    /// <param name="invoker">The invocation pipeline of the proxy.</param>
    /// <param name="serviceAddress">The service address. <see langword="null" /> is equivalent to an icerpc service address
    /// with path <see cref="DefaultServicePath" />.</param>
    /// <param name="encodeOptions">The encode options, used to customize the encoding of request payloads.</param>
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public GreeterProxy(
        IceRpc.IInvoker invoker,
        IceRpc.ServiceAddress? serviceAddress = null,
        SliceEncodeOptions? encodeOptions = null)
    {
        Invoker = invoker;
        ServiceAddress = serviceAddress ?? _defaultServiceAddress;
        EncodeOptions = encodeOptions;
    }

    /// <summary>Constructs a proxy from an invoker, a service address URI and encode options.</summary>
    /// <param name="invoker">The invocation pipeline of the proxy.</param>
    /// <param name="serviceAddressUri">A URI that represents a service address.</param>
    /// <param name="encodeOptions">The encode options, used to customize the encoding of request payloads.</param>
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public GreeterProxy(IceRpc.IInvoker invoker, System.Uri serviceAddressUri, SliceEncodeOptions? encodeOptions = null)
        : this(invoker, new IceRpc.ServiceAddress(serviceAddressUri), encodeOptions)
    {
    }

    /// <summary>Constructs a proxy with an icerpc service address with path <see cref="DefaultServicePath" />.</summary>
    public GreeterProxy()
    {
    }

    /// <inheritdoc/>
    public global::System.Threading.Tasks.Task<string> GreetAsync(
        string name,
        IceRpc.Features.IFeatureCollection? features = null,
        global::System.Threading.CancellationToken cancellationToken = default) =>
        this.InvokeAsync(
            "greet",
            Request.EncodeGreet(name, encodeOptions: EncodeOptions),
            payloadContinuation: null,
            Response.DecodeGreetAsync,
            features,
            cancellationToken: cancellationToken);
}

/// <summary>Provides an extension method for <see cref="SliceEncoder" /> to encode a <see cref="GreeterProxy" />.</summary>
public static class GreeterProxySliceEncoderExtensions
{
    /// <summary>Encodes a <see cref="GreeterProxy" /> as an <see cref="IceRpc.ServiceAddress" />.</summary>
    /// <param name="encoder">The Slice encoder.</param>
    /// <param name="proxy">The proxy to encode as a service address.</param>
    public static void EncodeGreeterProxy(this ref SliceEncoder encoder, GreeterProxy proxy) =>
        encoder.EncodeServiceAddress(proxy.ServiceAddress);
}

/// <summary>Provides an extension method for <see cref="SliceDecoder" /> to decode a <see cref="GreeterProxy" />.</summary>
public static class GreeterProxySliceDecoderExtensions
{
    /// <summary>Decodes an <see cref="IceRpc.ServiceAddress" /> into a <see cref="GreeterProxy" />.</summary>
    /// <param name="decoder">The Slice decoder.</param>
    /// <returns>The proxy created from the decoded service address.</returns>
    public static GreeterProxy DecodeGreeterProxy(this ref SliceDecoder decoder) =>
        decoder.DecodeProxy<GreeterProxy>();
}

/// <summary>Represents a simple greeter.</summary>
/// <remarks>The Slice compiler generated this server-side interface from Slice interface <c>VisitorCenter::Greeter</c>.
/// Your service implementation must implement this interface.</remarks>
[SliceTypeId("::VisitorCenter::Greeter")]
[IceRpc.DefaultServicePath("/VisitorCenter.Greeter")]
public partial interface IGreeterService
{
    /// <summary>Provides static methods that decode request payloads.</summary>
    /// <remarks>The Slice compiler generated this static class from the Slice interface <c>VisitorCenter::Greeter</c>.</remarks>
    public static class Request
    {
        /// <summary>Decodes the request payload of operation <c>greet</c>.</summary>
        /// <param name="request">The incoming request.</param>
        /// <param name="cancellationToken">A cancellation token that receives the cancellation requests.</param>
        public static global::System.Threading.Tasks.ValueTask<string> DecodeGreetAsync(
            IceRpc.IncomingRequest request,
            global::System.Threading.CancellationToken cancellationToken) =>
            request.DecodeArgsAsync(
                SliceEncoding.Slice2,
                (ref SliceDecoder decoder) => decoder.DecodeString(),
                defaultActivator: null,
                cancellationToken);
    }

    /// <summary>Provides static methods that encode return values into response payloads.</summary>
    /// <remarks>The Slice compiler generated this static class from the Slice interface <c>VisitorCenter::Greeter</c>.</remarks>
    public static class Response
    {
        /// <summary>Encodes the return value of operation <c>greet</c> into a response payload.</summary>
        /// <param name="returnValue">The operation return value.</param>
        /// <param name="encodeOptions">The Slice encode options.</param>
        /// <returns>A new response payload.</returns>
        public static global::System.IO.Pipelines.PipeReader EncodeGreet(
            string returnValue,
            SliceEncodeOptions? encodeOptions = null)
        {
            var pipe_ = new global::System.IO.Pipelines.Pipe(
                encodeOptions?.PipeOptions ?? SliceEncodeOptions.Default.PipeOptions);
            var encoder_ = new SliceEncoder(pipe_.Writer, SliceEncoding.Slice2, default);

            Span<byte> sizePlaceholder_ = encoder_.GetPlaceholderSpan(4);
            int startPos_ = encoder_.EncodedByteCount;

            encoder_.EncodeString(returnValue);
            encoder_.EncodeVarInt32(Slice2Definitions.TagEndMarker);

            SliceEncoder.EncodeVarUInt62((ulong)(encoder_.EncodedByteCount - startPos_), sizePlaceholder_);

            pipe_.Writer.Complete();
            return pipe_.Reader;
        }
    }

    /// <summary>Creates a personalized greeting.</summary>
    /// <param name="name">The name of the person to greet.</param>
    /// <param name="features">The dispatch features.</param>
    /// <param name="cancellationToken">A cancellation token that receives the cancellation requests.</param>
    /// <returns>The greeting.</returns>
    public global::System.Threading.Tasks.ValueTask<string> GreetAsync(
        string name,
        IceRpc.Features.IFeatureCollection features,
        global::System.Threading.CancellationToken cancellationToken);

    [SliceOperation("greet")]
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected static async global::System.Threading.Tasks.ValueTask<IceRpc.OutgoingResponse> SliceDGreetAsync(
        IGreeterService target,
        IceRpc.IncomingRequest request,
        global::System.Threading.CancellationToken cancellationToken)
    {
        request.CheckNonIdempotent();
        var sliceP_name = await Request.DecodeGreetAsync(request, cancellationToken).ConfigureAwait(false);
        var returnValue = await target.GreetAsync(sliceP_name, request.Features, cancellationToken).ConfigureAwait(false);
        return new IceRpc.OutgoingResponse(request)
        {
            Payload = Response.EncodeGreet(returnValue, request.Features.Get<ISliceFeature>()?.EncodeOptions),
            PayloadContinuation = null
        };
    }
}