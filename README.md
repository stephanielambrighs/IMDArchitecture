# API IMDArchitecture

This awesome API provides different calls for events and users.
It can create, update and delete users or events. It can show an overview of the users and events. Events can be overviewed by their target age. Users can be enrolled for events. They can also cancel their attendance.

## Download application
- clone the code 
- open the folder in Visual Studio Code 
- open terminal and run commando's below

## Run application
```sh 
# go to the folder
cd IMDArchitecture.API
# make a migration of the database
dotnet ef migrations add InitialCreate
# update sqlite database
dotnet ef database update
# startup swagger API
dotnet watch run

```
## Test application
``` sh
# go to the folder
cd IMDArchitecture.Test
# unittesting
dotnet test
```