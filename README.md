# Pokemon API


## Libraries/Endpoints

App uses following design decisions, libraries and Endpoints.

1. For Pokemon info `https://pokeapi.co` and for Fun Translations `https://api.funtranslations.com` Endpoints.
2. [OneOf](https://github.com/mcintyre321/OneOf/) for Control Flow rather than `Exceptions`
3. [Redis](https://hub.docker.com/_/redis/) to Cache rate-limited Endpoints + API Latency
4. [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis) for Redis Connection handler
5. [Polly](https://github.com/App-vNext/Polly) for resilient HttpClient as extension with retry policies
6. [NSubsitute](https://github.com/nsubstitute/NSubstitute) for Mocking

## Details

There are 2 endpoints:

```
GET /pokemon/{name}
```
Returns Basic Info on give Pokemon


```
GET /pokemon/translated/{name}
```

Returns `Yoda`/`Shakespeare` translated Pokemon info.


## Run

1. With `Visual Studio`

Pre-requisite is to have a Redis instance running on `localhost:6379` 

then, Just load the `sln` and run. Swagger Docs should auto open at `http://localhost:5000/swagger/`

NOTE: If you are using VS 2022 and see VS Analysis Errors please rebuild the project. It's an issue with VS 2022 Code Generation on build time, [Ref](https://github.com/mcintyre321/OneOf/issues/86)

2. `Docker Compose`

Simply run following command in project root

```bash
docker-compose up
```

Open Swagger Docs at: `http://localhost:5000/swagger/`

### Improvements

1. ~~Could add Redis Cache for Speed and to avoid API calls which for now are rate limit/paid.~~ (ADDED)