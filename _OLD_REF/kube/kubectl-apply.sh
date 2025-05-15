#!/bin/bash

# kubectl apply -f kube-keel.yaml

echo ""
echo "----- Install redis -----"
echo ""

kubectl apply -f kube/redis.yaml

echo ""
echo "----- Install rabbitmq -----"
echo ""
kubectl apply -f kube/rabbit-mq.yaml

# kubectl apply -f PkmDataRetrieval/kube-pkm-data-retrieval.yaml
# kubectl apply -f PkmGuessGame/kube-pkm-guess-game.yaml

kubectl get pods
kubectl get services
read -p "Process Complete."