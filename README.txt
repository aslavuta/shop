Extensibility (describe for what points/costs your code can be extended)
In the extensibility section, we can mention all the benefits that can give us proper implementation of Cleam Architecture.
We can:
| Swap LiteDB for Mongo/Cosmos/EF Core
| Add a new cart behavior             
| Add a new entities/aggregate (Order, Product, …)  
| Alternate the host (gRPC, worker, CLI) 

Current solution is not async due to sync nature of LiteDB
