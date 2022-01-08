# Pokemon API

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

Project was built with VS so Just load the `sln` and run.

NOTE: If you are using VS 2022 and see VS Analysis Errors please rebuild the project. It's an issue with VS 2022 Code Generation on build time, [Ref](https://github.com/mcintyre321/OneOf/issues/86)

2. `Docker Compose`

Simply run following command in project root

```bash
docker-compose up
```

### Improvements

1. Could add Redis Cache for Speed and to avoid API calls which for now are rate limit/paid.