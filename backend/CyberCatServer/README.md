Start local Mongo Database:
1. `start_local_mongo_in_docker.sh`

Start the server on a local or remote machine in Docker with the ability to connect to it via an Unity Editor:
1. `start_server_in_docker_for_dev.sh`

Launch the server on a remote machine for production. Requests are only accepted from domains specified in **AllowedHosts** in appsettings.Production.json. Don't forget to obtain SSL certificates:
1. `docker compose -f docker-compose.yml -f docker-compose.Production.yml up`

