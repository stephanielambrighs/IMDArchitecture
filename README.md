# API IMDArchitecture
## Run application
```sh 
# make a migration of the database
dotnet ef migrations add InitialCreate
# update sqlite database
dotnet ef database update
# startup swagger API
dotnet watch run

```
## Test application
``` sh
# unittesting
dotnet test
```