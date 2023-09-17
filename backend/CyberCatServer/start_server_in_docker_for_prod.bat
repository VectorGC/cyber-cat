REM For unclear reasons, if the new build kit is not disabled, the Docker build hangs at startup from time to time.
set DOCKER_BUILDKIT=0
docker compose -f docker-compose.yml -f docker-compose.Prod.yml up