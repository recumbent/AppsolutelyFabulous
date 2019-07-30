# AppsolutelyFabulous

Presentation on Fabulous 2019

## Helpful hints

[Docker Desktop Launc](https://gitpitch.com/docs/pro-features/desktop-launch/)

Make sure one can talk to the registry:

```bash
docker login
```

Make sure one has the desktop:

```bash
docker pull gitpitch/desktop:pro
```

Then run as follows:

```bash
docker run -it -v /Users/james.murphy/src/AppsolutelyFabulous:/repo -p 9000:9000 gitpitch/desktop:pro
```
