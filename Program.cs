using Vault;
using Vault.Client;
using Vault.Model;

namespace Example
{
    public class Example
    {
        public static void Main()
        {
            string address = Environment.GetEnvironmentVariable("VAULT_ADDR");
            VaultConfiguration config = new VaultConfiguration(address);

            VaultClient vaultClient = new VaultClient(config);
            vaultClient.SetToken(Environment.GetEnvironmentVariable("VAULT_TOKEN"));

            try 
            {    
                var secretData = new Dictionary<string, string> { { "mypass", "pass" } };

                // Write a secret
                var kvRequestData = new KVv2WriteRequest(secretData);

                vaultClient.Secrets.KVv2Write("mypath", kvRequestData);

                // Read a secret
                VaultResponse<Object> resp = vaultClient.Secrets.KVv2Read("mypath");
                Console.WriteLine(resp.Data);
            }
            catch (VaultApiException e)
            {
                Console.WriteLine("Failed to read secret with message {0}", e.Message);
            }
        }
    }
}