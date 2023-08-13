# azure-blob-storage
A service implementation for interacting with Azure Blob Storage using .NET. Using Azure Role restrictions for getting appropriate access to containers (built-in Identity Role Access)

# Description
To run the app successfully, you need to: Create an Azure Storage Account, apply EF migration, configure appsetting.json according to your system configuration

# System overview
The system can access an Azure Storage Account using account access key, user storage is represented with default Microsoft.Identity that allows you to restrict an access to endpoints.
The system provides only image view operation for default users and CRUD and view for developer and admin users.
