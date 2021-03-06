﻿# food-facts
The micro services' purpose is a REST API with a single GET endpoint in orderto search different products by an ingredient, proxying the third party API.
Third party API documentation: https://documenter.getpostman.com/view/8470508/SVtN3Wzy?version=latest#f564abb6-b306-4f8c-aa32-80c37f3d7fca

Request Parameters:  
`Ingredient : string // The ingredient which should be searched  
Limit: integer // The amount of products which should be returned`

Response Parameters:  
`Product[]:  
Product Name : string  
All Ingredients : string[]`

Response Example:  
`[
{
"productName":"Nutella",
"ingredients":[
"sugar",
"palm oil",
"hazelnuts 13%"
]
},
{
"productName":"Coca Cola",
"ingredients":[
"sugar",
"carbonated water",
"coloring e-150d"
]
}
]`

The solution consist of 3 projects
- Service
- Service.Test
- WebAPI

## Service
**Service** is a Class Library (.NET Core) project that was created to consume OpenFoodFacts API. I separated the 3 parts of it:
- FoodFactsService class, that implements IFoodFactsService interface and sends the GET request to the third-party API to retrieve the JSON string with the products.
- ProductDeserializer class, that gets the raw JSON string and builds up the list of Product objects.
- ServiceRegistrar class that extends IServiceCollection and registers the dependency of my service, so now it can be used via dependency injection anywhere.

## Service.Test
**Service.Test** is a simple xUnit test project that tests 3 scenarios of the FoodFactsService:
- Test if the service retrieved any products at all by search criteria
- Test if the service retrieved the specified amount of products (limit)
- Test if the service retrieved the default amount of products (limit = 20)

## WebAPI
**WebAPI** is a simple .NET Core (Web API) project that has only one controller **ProductController**, which utilizes my Service project to retrieve the products and ingredients by specified search criteria.

### Docker
I have put the WebAPI project in the docker container and exposed the port 5000 to listen to, when using the API.

### Instructions
To run the solution:
1. Clone this repository to your local machine
2. Navigate to the root solution folder with your terminal application
3. Publish the projects with this command:

`dotnet publish -c Release`

4. Build a docker image (I called it **dp_assignment**) running this command (mind the dot at the end):

`docker build -t dp_assignment -f WebAPI\Dockerfile .`

5. reate a container base on the image and run it:

`docker run -p 5000:5000 --name dennis_gets_the_job dp_assignment`

6. Now, in the browser or any API application, open the URL:

`http://localhost:5000/search/ingredient/sugar/limit/10 `

You can change the value for ingredient and limit, or don't use limit at all to get the default amount of products (20):

`http://localhost:5000/search/ingredient/grape `

The solution was built by **Dennis Peld** using:
* Visual Studio 2019 Community (for coding)
* Docker for Windows (for containerizing the app)
* Postman (for testing the APIs)
