## Overview

Although there is ready to use nice, free [Microsoft Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/), I’ve developed my own tool to browse Azure Storage:
<p align="center">
   <img src="/AzureStorageBrowser/pics/asb.png" alt="asb"/>
</p>

It’s been created in Visual C# using Microsoft Azure SDK for .NET – 3.0.
The core code is as follow:
```c#
try
{
    myCloudStorageAccount = CloudStorageAccount.Parse(strStorageConnectionString);
 
    myCloudBlobClient = myCloudStorageAccount.CreateCloudBlobClient();
    myTree.Nodes[0].Tag = myCloudBlobClient.BaseUri.ToString();
 
    switch (getStorageAccountType(myCloudBlobClient))
    {
        case "Standard":
 
            myCloudFileClient = myCloudStorageAccount.CreateCloudFileClient();
            myCloudTableClient = myCloudStorageAccount.CreateCloudTableClient();
            myCloudQueueClient = myCloudStorageAccount.CreateCloudQueueClient();
 
            myTree.Nodes[1].Tag = myCloudFileClient.BaseUri.ToString();
            myTree.Nodes[2].Tag = myCloudTableClient.BaseUri.ToString();
            myTree.Nodes[3].Tag = myCloudQueueClient.BaseUri.ToString();
 
            await Task.WhenAll(getBlobsAsync(), getFilesAsync(), getTablesAsync(), getQueuesAsync());
 
            break;
 
        case "Premium":
 
            await getBlobsAsync();
 
            break;
    } //switch
 
    lbStatus.Text = "Connected";
    btDisconnect.Enabled = true;
}
catch (Exception ex)
{
    lbStatus.Text = ex.Message; 
    btConnect.Enabled = true;
}
```

with one trick to find if type of Storage Account uses Standard or Premium performance tier:
```c#
private string getStorageAccountType(CloudBlobClient cbc_)
{
    try
    {
        cbc_.GetServiceProperties();
        return "Standard";
    }
    catch(Exception ex)
    {
        return "Premium";
    }
}  //getStorageAccountType
```

If connection to Storage Account is established properly, TreeView in the left panel is filled by blobs, files, tables and queues. Clicking any TreeView node allows to present details of its child objects in GridView in the right panel. We can asynchronously upload, download or delete selected objects.

Tool is still at an experimental stage and under development, so all unstable behaviors I fix in daily basis, if any. For now the best is to try it on test environment.

## Reference articles

* [Get started with Azure Blob storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-blobs)
* [Get started with Azure Queue storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-queues)
* [Get started with Azure Table storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-tables)
* [Get started with Azure File storage on Windows](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-files)
