using System;
using k8s;
using k8s.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;



namespace csharp_controller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int replicaCount = 0;
            if (args.Length >= 2 && args[0] == "--replicas")
            {
                replicaCount = int.Parse(args[1]);
            }
            Console.WriteLine($"Changing replica count to {replicaCount}");

            Kubernetes client = await createClient();
            await createDeploymentIfNotExists(client);
            changeDeploymentReplicas(client, replicaCount);
        }

        static void changeDeploymentReplicas(Kubernetes client, int replicaCount)
        {
            var patch = new JsonPatchDocument<V1Deployment>();
            patch.Replace(e => e.Spec.Replicas, replicaCount);
            client.PatchNamespacedDeployment(
                new V1Patch(patch),
                "nginx-deployment",
                "default"
            );
        }

        static async Task createDeploymentIfNotExists(Kubernetes client)
        {
            V1Deployment deployment = await Yaml.LoadFromFileAsync<V1Deployment>("./deploy.yaml");
            try
            {
                client.CreateNamespacedDeployment(
                               deployment,
                               "default"
                           );
            }
            catch (Exception e)
            {
                /* deployment probably exists */
                // Console.WriteLine($"Error creating deployment: {e}");
            }
        }


        static async Task<Kubernetes> createClient()
        {

            // // Load from in-cluster configuration:
            // var config = new KubernetesClientConfiguration { Host = "http://127.0.0.1:8001" };


            var config = KubernetesClientConfiguration.InClusterConfig();

            // Use the config object to create a client.
            var client = new Kubernetes(config);

            return client;
        }
    }
}
