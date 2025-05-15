#!/bin/bash

docker run -d -p 5000:5000 --restart=always --name registry registry:2

docker build -f PkmDataRetrieval/Dockerfile . -t localhost:5000/pkm-data-retrieval
docker build -f PkmGuessGame/Dockerfile . -t localhost:5000/pkm-guess-game
docker build -f PkmWeb/Dockerfile . -t localhost:5000/pkm-web

docker image push localhost:5000/pkm-data-retrieval
docker image push localhost:5000/pkm-guess-game
docker image push localhost:5000/pkm-web

kubectl apply -f redis/kube-redis-pvs.yaml
kubectl apply -f redis/kube-redis.yaml
kubectl apply -f local-deploy.yaml

kubectl get pods
kubectl get services
read -p "Process Complete."