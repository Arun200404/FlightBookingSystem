# Migration Required

The Admin model has been updated (added Email field) and new unique indexes were added.

Run the following commands to apply the changes:

```bash
dotnet ef migrations add AdminEmailAndIndexes
dotnet ef database update
```
