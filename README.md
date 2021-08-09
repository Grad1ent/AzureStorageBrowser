## Overview

Although there is ready to use nice, free Microsoft Azure Storage Explorer, I’ve developed my own tool to browse Azure Storage:
<p align="center">
   <img src="/AzureStorageBrowser/pics/asb.png" alt="asb"/>
</p>

It’s been created in Visual C# using the latest Microsoft Azure SDK for .NET – 3.0.
The core code is as follow:


with one trick to find if type of Storage Account uses Standard or Premium performance tier:



If connection to Storage Account is established properly, TreeView in the left panel is filled by blobs, files, tables and queues. Clicking any TreeView node allows to present details of its child objects in GridView in the right panel. We can asynchronously upload, download or delete selected objects.

Tool is still at an experimental stage and under development, so all unstable behaviors I fix in daily basis, if any. For now the best is to try it on test environment.

## Reference articles

* [Get started with Azure Blob storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-blobs)
* [Get started with Azure Queue storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-queues)
* [Get started with Azure Table storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-tables)
* [Get started with Azure File storage on Windows](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-files)
