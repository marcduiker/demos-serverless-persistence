# Persistance in Serverless Applications

This repository contains demos for my talk: 'Persistence in Serverless Applications'.

The `src` folder contains an Azure Functions project (.NET 6 in-process) with the following HTTP triggered functions:

| Name | Binding | Description
| -- | -- | --
| StorePlayerWithStreamBlobOutput | Blob output | Using `Stream`
| StorePlayerWithStringBlobOutput | Blob output | Using `string`
| StorePlayerWithStringBlobOutputDynamic | Blob output | Using `IBinder` (dynamic binding)
| GetPlayerWithStreamBlobInput | Blob input | Using `Stream`
| GetPlayerWithStringBlobInputDynamic | Blob input | Using `IBinder` (dynamic binding)
| StorePlayerReturnAttributeTableOutput | Table output | Using return attribute
| StorePlayersWithCollectorTableOutput | Table output | Using `IAsyncCollector<PlayerEntity>`
| GetPlayerByRegionAndIdTableInput | Table input | Using `PlayerEntity`
| GetPlayersByRegionTableClient | Table input | Using `TableClient`
| StorePlayerCosmosOutput | CosmosDB output | Using the `Player`
| StorePlayerReturnAttributeCosmosOutput | CosmosDB output | Using return attribute
| StorePlayersWithCollectorCosmosOutput | CosmosDB output | Using `IAsyncCollector<Player>`
| GetPlayerByRegionAndIdCosmosInput | CosmosDB input | Using `Player`
| GetPlayersByRegionDocumentClientCosmosInput | CosmosDB input | Using `DocumentClient`
| UpdatePlayerScore | Durable Entity | Uses Durable Functions

## Running locally

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0); the SDK and runtime to develop Azure Functions in C# .NET 6.
- [VSCode](https://code.visualstudio.com/Download); a powerful cross platform source editor.
- [Azure Functions extension for VSCode](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions); for creating and running Azure Functions locally (using the Azure Functions Core Tools).
- [Azurite extension for VSCode](https://marketplace.visualstudio.com/items?itemName=Azurite.azurite); Storage emulator for blobs, tables and queues.
- [Azure Databases extension for VSCode](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-cosmosdb); To manage your cloud based CosmosDB from VSCode.
- [Azure CosmosDB](https://docs.microsoft.com/azure/cosmos-db/try-free) or the [CosmosDB Emulator](https://docs.microsoft.com/azure/cosmos-db/local-emulator); a NoSQL data store.
- [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer); Graphical user interface to manage and inspect blobs, tables, and queues, either in the cloud or locally emulated.
- A REST client, such as the [REST Client extension for VSCode](https://marketplace.visualstudio.com/items?itemName=humao.rest-client).

> *All VSCode extensions are automatically recommended to be installed when you open this folder in VSCode.*

### Running the Function App

1. Clone this repo.
2. Open the folder in VSCode.
3. Install the recommended extensions.
4. Ensure that a CosmosDB instance is present in either the cloud or the emulator with these settings:
   - Database: `gamedb`
   - Collection: `players`
   - PartitionKey: `region`
5. Build the project to ensure that there are no errors:
   - Via the build task: `CTRL/CMD+SHIFT+B` or
   - Via the terminal in the `src/ServerlessPersistence` folder: `dotnet build`
6. Start Azurite services:
   - Via the command palette: `CRTL/CMD+SHIFT+P` -> `Azurite: Start`
7. Run the Function App:
   - Via the debug command: `F5` or
   - Via the terminal in the `src/ServerlessPersistence` folder: `func start`.
8. Now use the http files located in `tst` and a REST client to trigger the functions.

## More info

If you want to learn more about Azure Functions, have a look at [Azure Functions University](https://github.com/marcduiker/azure-functions-university), a free and open source curriculum on GitHub and YouTube.

Found a bug or want to ask a question? Feel free to raise an [issue](https://github.com/marcduiker/demos-serverless-persistence/issues) or submit a pull request. Or you can reach out to me on [Twitter](https://twitter.com/marcduiker).
