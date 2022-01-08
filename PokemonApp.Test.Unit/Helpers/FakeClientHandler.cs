using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonApp.Test.Unit.Helpers;

public class FakeClientHandler : HttpMessageHandler
{
    public HttpContent? Content { private get; set; }
    public Exception? Exception { private get; set; }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (Exception is not null)
        {
            throw Exception;
        }

        return Task.FromResult(
            new HttpResponseMessage
            {
                Content = Content,
                StatusCode = HttpStatusCode.Accepted
            }
        );
    }
}