using System;
using System.Net.Http;

namespace PokemonApp.Test.Unit.Helpers;

public static class MockClientFactory
{
    public static HttpClient GetClient(HttpContent content)
    {
        var client = new HttpClient(
            new FakeClientHandler { Content = content }
        );

        client.BaseAddress = new Uri("http://mock.url");


        return client;
    }

    public static HttpClient GetClient(Exception exception)
    {
        var client = new HttpClient(
            new FakeClientHandler { Exception = exception }
        );

        client.BaseAddress = new Uri("http://mock.url");


        return client;
    }
}