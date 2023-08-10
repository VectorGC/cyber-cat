Start local Mongo Database:

`docker compose -f docker-compose.yml -f docker-compose.Dev.MongoDb.yml`

Start server in docker on local machine:

`docker compose -f docker-compose.yml -f docker-compose.Dev.yml`

Start server in production environment (don't forget to obtain SSL certificates.):

`docker compose -f docker-compose.yml -f docker-compose.Production.yml`

