# Shook App

![GitHub issues](https://img.shields.io/github/issues-raw/samofan/shook)
![GitHub last commit](https://img.shields.io/github/last-commit/samofan/shook)
![GitHub forks](https://img.shields.io/github/forks/samofan/shook)

<blockquote class="twitter-tweet"><p lang="en" dir="ltr">There should be an app called Shook. You could make agreements with it - like bets with friends etc. Then you could say &quot;but we Shook on it&quot;</p>&mdash; Matt Jylkka (@mtmograph)</blockquote>

This is the idea of Shook.

## Docker support

Docker integration for the server/REST-Api is work in progress. The docker image has to be built by yourself and can be executed by docker-compose afterwards.

### How-To

1. Build the image by executing create-docker-image.sh
```bash
sh create-docker-image.sh
```
2. Execute docker-compose. A new directory is created that persists the data of the database.
```bash
docker-compose up
```

### Configuration

The credentials of the database are configured in the docker-compose file. The environment variables of db and shook-server have to match:<br>
POSTGRES_PASSWORD == DB_PASSWORD<br>
POSTGRES_USER == DB_USER<br>
POSTGRES_DB == DB_NAME<br>

The port is the postgres default port.