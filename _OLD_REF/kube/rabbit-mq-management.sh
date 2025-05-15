#!/bin/bash

kubectl port-forward -n pkm-guess rabbit-mq-server-0 8080:15672 