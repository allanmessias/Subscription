#!/bin/bash
set -e	

export DOCKER_BUILDKIT=0

echo -e "\033[0;32mRemoving docker images if any\033[0m"
if [ "$(docker compose ps 2> /dev/null)" ]; then
  echo -e "\033[0;32mStopping and removing all containers and volumes\033[0m"
  docker compose down -v
  echo -e "\033[0;32mRemoving existing subscription-migrator image\033[0m"
  docker rmi subscription-migrator
fi

echo -e "\033[0;32mBuilding the docker images\033[0m"
docker compose build --no-cache

echo -e "\033[0;32mGetting the infrastructure ready with docker-compose\033[0m"
docker compose up -d

echo -e "\033[0;32mWaiting for the db to get up and running\033[0m"
until docker exec pg-subscription pg_isready -U subscription_user -d subscription_db; do
  sleep 2
done

echo -e "\033[0;32mRunning the migrator container\033[0m"
docker build --no-cache -f Dockerfile.migrator -t subscription-migrator .
docker build -f Dockerfile.migrator -t subscription-migrator .
docker run --rm --network ascan_subscription_subscription-net subscription-migrator

echo -e "\033[0;32mApp up and running :)\033[0m"
