Extensibility (describe for what points/costs your code can be extended)

| Change                                   | Cost                                                                                  |
|------------------------------------------|---------------------------------------------------------------------------------------|
| Swap LiteDB for Mongo/Cosmos/EF Core     | New DAL project implementing `ICartRepository`; swap `AddLightDb()` for `AddXxx()`.  |
| Add a new cart behavior                  | Method on `ICartService` + controller action + test. Invariants stay on the entity.   |
| Add a new aggregate (Order, Product, …)  | New entity + repository interface + service + DAL implementation. ~5 new files.       |
| Cross-cutting concerns (log/cache/retry) | Decorator over `ICartService`, registered in DI. No changes to `CartService`.         |
| Alternative host (gRPC, worker, CLI)     | New host project; reuse `AddApplication()` + `AddLightDb()`.                          |

Current solution is not async due to sync nature of LiteDB