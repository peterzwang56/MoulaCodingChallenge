# Moula Coding Chanllenge
Welcome to my implement of the Moula Coding Challenge.

## Prerequisites
1. Web server that allow to run .Net core, such as Azure App service
2. An MSSQL (SQLExpress for dev/local environment) database for this implementation.


## Assumptions
During this coding challenge implementation, following assumptions are made:
Functional:
1. Assumping account information in this implementation is private. Therefore it requires certain level of Authentication and Authorisation (AA).
2. An user may have 0 to many accounts.
3. In order to prevent malious enumeration against account ID, wrong conbination of username and account ID will be considered as not found.
4. This is standalone(microservice) which has it own dedicated data repository(database)


## Overview
This implementation is consisted by three layers:
1. Controller, which is the API controller and entry point that contains only WEB api related logic, such as authentication/authorisation and logic of HTTP responding
2. Service, which is the layer for business logic
3. Data, which is the layer for data access from data source, in this implementation is MSSQL.

MSSQL AND EF Core (code first) are selected for this implementation for illustration purposes. As Data access is a separate layer, it can be easily refactored to other technology in case of need.

Also for illustrated reason, by default it will seed following data:
* Account number: 1	|	Username: Eren
* Account number: 2	|	UserName: Eren
* Account number: 3	|	 UserName: Mikasa

## API route:
* Swagger UT can be found in following location: **GET {baseurl/swagger**
* ACCOUNT api: **GET {baseurl}/api/v1/account/{accountID}** requires JWT token with username

* Dummy Login api (for illustration purposes) **POST {baseurl}/api/v1/dummylogin/{username}**
* Dummy login api will return a JSON:
```
{
	"accessToken":"{access token}"
}
```

User need to use the {access token} as auth header to request Account API.

## Deployment
Connection string is in AppSettings.json. By default it use local sqlexpress with integrated security.

With Azure DevOps agents: With this project it has an YMAL file created for Azure DevOps with managed agent.

With Powershell Script: 
```
    > cd MoulaCodingChallenge\MoulaCodingChallenge
    > dotnet restore
    > dotnet build --configuration release
    > dotnet run --launch-profile prod
```




