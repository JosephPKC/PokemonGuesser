#!/bin/bash

# https://www.rabbitmq.com/kubernetes/operator/using-operator

# Confirm Service Availability
echo ""
echo "----- Confirm service availability -----"
echo ""
kubectl get customresourcedefinitions.apiextensions.k8s.io

# Verify that rabbit-mq k8 cluster is running
echo ""
echo "----- Verify rabbit-mq k8 cluster -----"
echo ""
kubectl get all -l app.kubernetes.io/name=rabbit-mq

read -p "Process Complete."