# k8s-deployment-example

Running either of the two jobs (`kubectl apply -f one-pod-job.yaml`, `kubectl apply -f four-pod-job.yaml`) will create a deployment (if it doesn't already exist) and then scale the replicas to the specified amount.


A change to the default service account may need to be applied to allow create/patch permissions, the sample config (`service.yaml`) should work.
