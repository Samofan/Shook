# Docker integration

Docker integration is work in progress. The docker image has to be built by yourself and can be executed by docker-compose afterwards.

## How-to

1. Build the image by executing create-docker-image.sh
```bash
sh create-docker-image.sh
```
2. Execute docker-compose. A new directory is created that persists the data of the database. Make sure to change the credentials of the database.
```bash
docker-compose up
```