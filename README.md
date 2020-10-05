# Gamescope.Backend
This project developed for DataScope with .NET. In this project I've aimed to strict with the Best Practices and Clean Architecture with CQRS pattern.

### [Demo](https://gamescope-api.azurewebsites.net/)
You can visit and explore the api via swagger documentation.

## Requirements
Develop an app with your capabilities containing frontend&backend. There are three entities named User, Game (Name, Description, Release Date) and Ratings.
* Users can add, edit, delete and read the games.
* Only authorized users can use the system.
* Users can rate the games.
* Users only able to CRUD their games.
* Users only update their rates.
* Users need to be authenticated.
* Users can register to the system.

## Dependencies
This project has dependecies to listed nugets:
* MediatR
* Automapper
* FluentValidation
* FluenAssertations
* EntityframeworkCore

## Build
Please don't forget the set the ConnectionString in the appsettings.json to your local database. 
Then open Package-Manager and select Infra.Data in it and set Api projet as startup project.
Then insert update-database to PM and your database will be prepared.

## Tests
Tests are developed with Xunit.
