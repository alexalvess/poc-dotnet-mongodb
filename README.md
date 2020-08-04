# What is this project?
This projet is a sample POC in .NET Core using MongoDB. 
The goal is to show how can you use mongodb how your principal database. So for it I show you some examples how can we make a some operations and
how can we use Mongo Transactions, allowed after v4.0, in a simple way with Dependency Injection.

## Basic instructions:
To run this project, you will need to inform a valid mongo connection string. To do it, you will need a Mongo Server, cloud or on premise. 
On "appsettings.json" file has an example of connection string, which you just need to inform the login and password of your mongodb server. 
But, pay attention, this connection string can be different, depend where you hosted/created your db.

## Technologies implemented:
* MongoDB.Driver v2.11.0
* Swashbuckle.AspNetCore v5.5.1

### About:
This project was developed by [Alex Alves](https://github.com/alexalvess)