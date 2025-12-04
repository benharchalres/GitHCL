## Environment:
- .NET version: 8.0

## Read-Only Files:
- DocumentService.Tests/IntegrationTests.cs
- DocumentService.WebAPI/Controllers/CompaniesController.cs

## Data:
Example of a document data JSON object:
```
{
   "title": "Monthly Revenue Report",
   "size": 300,
   "format": "pdf"
}
```

## Requirements:

A company is launching a service that can validate a document model. The service should be a web API layer using .NET Core 3.0. You already have a prepared infrastructure and need to implement validation logic for the document model as per the guidelines below. Perform validation in models, not in controllers.

Each document data is a JSON object describing the details of the document. Each object has the following properties:

- title: the title of the document. [String]
- size: the size (in MB) of the document. [Integer]
- format: the format of the document. [String]

The following API needs to be implemented:

`POST` request to `api/documents`:

- The HTTP response code should be 200 on success.
- For the body of the request, please use the JSON example of the document model given above.
- If the document model is invalid, return status code 400. When you send 400, add an appropriate error message to the response as described below.

The document model should be validated based on the following rules:

- The title field is required and must contain a minimum of 5 characters and a maximum of 35, and each word should start with an uppercase letter. If the field is invalid, add this error message: `"Title is invalid: Title must contain a minimum of 5 characters and a maximum of 35, and each word should start with an uppercase letter"`.
- The size field is required and must be greater than 0 MB and less than 500 MB. If the field is invalid, add this error message: `"Size is invalid: Size must be greater than 0 MB and less than 500 MB"`.
- The format field is required and must be lowercase and equal one of the following: 'txt', 'pdf', 'docx'. If the field is invalid, add this error message: `"Format is invalid: Format must be lowercase and equal one of the following: 'txt', 'pdf', 'docx'"`.
