Extensibility (describe for what points/costs your code can be extended)
In the extensibility section, we can mention all the benefits that can give us proper implementation of Cleam Architecture.
We can:
| Swap LiteDB for Mongo/Cosmos/EF Core
| Add a new cart behavior             
| Add a new entities/aggregate (Order, Product, …)  
| Alternate the host (gRPC, worker, CLI) 

Task 2 Extensibility cover nest poins:
- We can still switch our SQL DB to any other
- Repositories are isolated and can be substituted without any changes in the Application layer
- extendable with any other entities to be added
