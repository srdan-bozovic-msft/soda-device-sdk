# SODA Device SDK

## Overview

This is the C# SDK that represents complete port of Android SDK for the Socrata Open Data API (SODA). Please refer to the developer site (http://dev.socrata.com/) for a deeper discussion of the underlying protocol.

Word "device" in the SDK name is used to emphasize that by using Microsoft and / or Xamarin development tools you can build mobile apps for any mobile platform:

- Universal ( Windows 8.1 / Windows Phone 8.1 )
- Windows Phone 8
- Android
- iOS

Currently this SDK only contains support for the Consumer aspects of the SODA API but may be extended later to include the publisher capabilities as well.

### Examples

Currently only Universal app ( Windows 8.1 / Windows Phone 8.1 ) example is provided but Android and Windows Phone 8 are coming soon. 

### Build from sources

Complete solution has been built using Visual Studio 2013 Update 2 RC with Xamarin tooling installed. 

In order to build MSC.Socrata.Device library alone, you can use Visual Studio or Xamarin Studio Tools  

## SODA

## Consumer

### Initialization

The MSC.Socrata.Device.Client.Consumer is the main interface to communicate with the SODA consumer API. A SODA consumer is created and initialized with the domain and token provided by Socrata.
If you are just getting started with the SODA API please take a look at the [Getting Started Guide](http://dev.socrata.com/consumers/getting-started)

```csharp
var consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
```
Instances of a consumer can be held in memory for further requests.

### Getting Data

All requests to the SODA API are non blocking and performed asynchronously in a background thread. C# inovative async/await style interface ensures that responses are delivered to the caller in the Main thread where it is is safe to update the user interface.

You can lookup records by IDs or query with a SQL style language as defined in (http://dev.socrata.com/docs/queries).

#### Get by ID

Get by ID returns a single result 

```csharp
var consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
var response = await consumer.GetObjectAsync<Earthquake>("earthquakes", earthquakeId);
if(response.HasEntity)
{
  var earthquake = response.Entity;
  ...
}
```

#### Querying

The Consumer accepts both String based queries and Query objects that once executed against the SODA Consumer API will return a subset of records constrained by the criteria specified in the query.

##### Query By String

Consumer accepts raw SoQL strings as input to the MSC.Socrata.Device.Client.Consumer#GetObjects method. You can use any standard SoQL query passed to this method as defined in (http://dev.socrata.com/docs/queries).

```csharp
var consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
var response = await consumer.GetObjects<Earthquake>("earthquakes", "select * where magnitude > 2.0"); 
if(response.HasEntity)
{
  var earthquakes = response.Entity;
  ...
}
```

##### Query Building

You can build queries based on simple or complex criteria.

Query Example to get Earthquakes with magnitude > 2.0

```csharp

var consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
var query = new Query<Earthquake>("earthquakes");
query.AddWhere(Expression.Gt("magnitude", "2.0"));
var response = await consumer.GetObjectsAsync<Earthquake>(query);
if(response.HasEntity)
{
  var earthquakes = response.Entity;
  ...
}
```

###### Geo Queries

The SODA Device SDK supports geo queries by including a query.WhereWithinBox("location", new GeoBox(north, east, south, west)) clause that takes a dataset location property and a geo bounding box with the NE, SW coordinates.

```csharp

var consumer = new Consumer("soda.demo.socrata.com", "YOUR_TOKEN");
var query = new Query<Earthquake>("earthquakes");
query.AddWhereWithinBox("location", new GeoBox(north, east, south, west));
var response = await consumer.GetObjectsAsync<Earthquake>(query);
if(response.HasEntity)
{
  var earthquakes = response.Entity;
  ...
}
```

