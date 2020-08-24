# WebAPI_ASPNET_SQL
The project to build Web API for Call em All Coding challenge

@ Task on August 21, 2020 ~ August 23, 2020

<a href="#Description">1. Description</a>

<a href="#Environment">2. Environment</a>

<a href="#Database">3. Database</a>

<a href="#Test-Tool">4. Test Tool </a>

<a href="#Challenge-1">* Challenge 1</a>

<a href="#Challenge">* Challenge 2</a>

<a href="#Challenge">* Challenge 3</a>
 
<a href="#Challenge">* Challenge 4</a>



Description
----
 
This API is to create ASP.NET Web API using SQL server database. 
Accoring to HTTP request such as GET and POST, the endpoint return the appropriate HTTP status code and the JSON data. 
The first two challenges are related to GET request to retrive Student Grades and the finally calculated GPA. Next task is to add a student grade with the valid data.  

Environment setup
----

- Visual Studio 2017 
- .NET Framework 4.8
- SQL Server 2017

Database and Setup
----

SCHOOL database

The schema shown below and the test data are provided.
 
<img src="images/db_schema.png">

After creating database to SQL Server, added ADO.NET Entity Data Model to the project.
Connection String is named SchoolDBContext. (Web.config)
Generated Models for all data entities is shown below.

<img src="images/dbModel.png" width="600px">

Test Tool
----

To run the API, installed Postman.

https://www.postman.com/

Solution Code Files and Directories
----

Most of API controller files are in the Controllers directory.
All Models of data context are in the Models directory and these are all auto-geralated.
As a data transfer object, used additional data entities. These are in the Entities directory.
Special GPA calculator module is in the Libraries directory.


 Challenge 1
 ----
 
 <Task>
 - HPPT Request: GET/Student/{studentId}
 - Response: Add an endpoint for Student information, the calculated GPA, and the grade details
 
 <Solution files>
 - Controllers/StudentController.cs
 - Libraries/CalculateGpa.cs
 - Entities/Student.cs
 - Entities/StudentTranscript.cs
  
 <GPA calculation>
 - Libraries/CalculateGpa.cs
 
 <Test>
 <img src="ch1Result.png" width="600px">
 
