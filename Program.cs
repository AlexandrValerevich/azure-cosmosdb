using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;


// See https://aka.ms/new-console-template for more information
public class Program
{
    // Replace <documentEndpoint> with the information created earlier
    private static readonly string EndpointUri = "https://justcosmos.documents.azure.com:443/";

    // Set variable to the Primary Key from earlier.
    private static readonly string PrimaryKey = "ZbbkbpS4lZni8VmTfk3FYcrUXJKcDVC5K07fDgnwGAYj8HUGb8rIamLv9bs6p1Ny87Xvn1zo3KLkRt618RR7Hw==";

    // The Cosmos client instance
    private CosmosClient cosmosClient;

    // The database we will create
    private Database database;

    // The container we will create.
    private Container container;

    // The names of the database and container we will create
    private string databaseId = "az204Database";
    private string containerId = "az204Container";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new();
            await p.CosmosAsync();
            await p.CreateDatabaseAsync();
            await p.CreateContainerAsync();

        }
        catch (CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }

    public async Task CosmosAsync()
    {
        // Create a new instance of the Cosmos Client
        cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // await this.CreateDatabaseAsync();
        // await this.CreateContainerAsync();

    }

    private async Task CreateDatabaseAsync()
    {
        // Create a new database using the cosmosClient
        database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", database.Id);
    }

    private async Task CreateContainerAsync()
    {
        // Create a new container
        container = await database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", container.Id);
    }
}