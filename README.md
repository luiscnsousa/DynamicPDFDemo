# DynamicPDF Demo

This is a demo .NET Core Web API using [ceTe.DynamicPDF.Rasterizer.NET](https://www.nuget.org/packages/ceTe.DynamicPDF.Rasterizer.NET/) to draw images based on a PDF document.

It was created in order to easily reproduce an issue using Rasterizer. The issue consists of a vertical stripe of pixels of a different colour on the left side of the image. This appears to be more frequent on images with landscape orientation.

The API exposes only 1 endpoint:
- GET /api/Pdf/{page} where page is the page number (zero-based) of the PDF document being used for tests

There are 3 different profiles to run the API:
- IIS Express
- Kestrel
- Docker (Linux)

The issue does not occurr when running with IIS or Kestrel on Windows. The only way to reproduce is to run in a Linux container with Docker.

### Steps:
- clone repository
- open solution src/DynamicPDFSample.sln
- choose profile "Docker"
- Run the API
- call the endpoint /api/pdf/0

This is the response from the HTTP call when running on Linux (vertical stripe on the left side):
![image](https://user-images.githubusercontent.com/4196491/115901609-529d0980-a459-11eb-8941-124713918efc.png)

This is the response from the HTTP call when running on Windows (no issue):
![image](https://user-images.githubusercontent.com/4196491/115903433-79f4d600-a45b-11eb-8e53-7a0199a2032c.png)
