﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//
// wojciech@pazdzierkiewicz.pl
//
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;


namespace AzureStorageBrowser
{
    public partial class Form1 : Form
    {
        CloudStorageAccount myCloudStorageAccount;
        CloudBlobClient myCloudBlobClient;
        CloudFileClient myCloudFileClient;
        CloudTableClient myCloudTableClient;
        CloudQueueClient myCloudQueueClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 myAboutBox = new AboutBox1();
            myAboutBox.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbDefaultEnpointsProtocol.Text = "https";
            tbAccountName.Text = "";
            tbAccountKey.Text = "";
            tbConnectionString.Text = "";
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Setting Files|*.txt";
            saveFileDialog1.Title = "Save the Setting File";
            saveFileDialog1.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Setting Files|*.txt";
            openFileDialog1.Title = "Select the Setting File";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string myKey;
                    string myValue;

                    foreach (string strLine in File.ReadLines(openFileDialog1.FileName))
                    {
                        myKey = strLine.Substring(0, strLine.IndexOf('='));
                        myValue = strLine.Substring(strLine.IndexOf('=') + 1);

                        switch (myKey)
                        {
                            case "DefaultEndpointsProtocol":
                                cbDefaultEnpointsProtocol.Text = myValue;
                                break;
                            case "AccountName":
                                tbAccountName.Text = myValue;
                                break;
                            case "AccountKey":
                                tbAccountKey.Text = myValue;
                                break;
                        } //switch

                    } //foreach

                    string strDefaultEnpointsProtcol = "DefaultEndpointsProtocol=" + cbDefaultEnpointsProtocol.Text + ";";
                    string strAccountName = "AccountName=" + tbAccountName.Text + ";";
                    string strAccountKey = "AccountKey=" + tbAccountKey.Text;
                    string strStorageConnectionString = strDefaultEnpointsProtcol + strAccountName + strAccountKey;

                    tbConnectionString.Text = strStorageConnectionString;

                } //try
                catch (ArgumentOutOfRangeException ex)
                {
                    lbStatus.Text = ex.Message;
                }
                catch (IOException ex)
                {
                    lbStatus.Text = ex.Message;
                }

            } //if
        }

        private void btExpandAll_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    trBlobs.ExpandAll();
                    break;
                case 1:
                    trFiles.ExpandAll();
                    break;
            } //switch
        }

        private void btCollapseAll_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    trBlobs.CollapseAll();
                    break;
                case 1:
                    trFiles.CollapseAll();
                    break;
            } //switch
        }

        private void trBlobs_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ToolTipText != "CloudBlobContainer")
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
            else
            {
                e.Node.ImageIndex = 11;
                e.Node.SelectedImageIndex = 11;
            }
        }

        private void trBlobs_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ToolTipText != "CloudBlobContainer")
            {
                e.Node.ImageIndex = 0;
                e.Node.SelectedImageIndex = 0;
            } else
            {
                e.Node.ImageIndex = 10;
                e.Node.SelectedImageIndex = 10;
            }
        }

        private void trFiles_AfterExpand(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.ToolTipText)
            {
                case "CloudFileShare":
                    e.Node.ImageIndex = 4;
                    e.Node.SelectedImageIndex = 4;
                    break;
                case "CloudFileDirectory":
                    e.Node.ImageIndex = 1;
                    e.Node.SelectedImageIndex = 1;
                    break;
            } //switch

        }

        private void trFiles_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.ToolTipText)
            {
                case "CloudFileShare":
                    e.Node.ImageIndex = 3;
                    e.Node.SelectedImageIndex = 3;
                    break;
                case "CloudFileDirectory":
                    e.Node.ImageIndex = 0;
                    e.Node.SelectedImageIndex = 0;
                    break;
            } //switch
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            trBlobs.Nodes.Clear();
            trFiles.Nodes.Clear();
            trTables.Nodes.Clear();
            trQueues.Nodes.Clear();
            gvProperties.Rows.Clear();

            lbStatus.Text = "Disconnected";
            btConnect.Enabled = true;
            btDisconnect.Enabled = false;
        }

        private async void btConnect_Click(object sender, EventArgs e)
        {
            string strDefaultEnpointsProtcol = "DefaultEndpointsProtocol=" + cbDefaultEnpointsProtocol.Text + ";";
            string strAccountName = "AccountName=" + tbAccountName.Text + ";";
            string strAccountKey = "AccountKey=" + tbAccountKey.Text;
            string strStorageConnectionString = strDefaultEnpointsProtcol + strAccountName + strAccountKey;

            if (String.IsNullOrWhiteSpace(tbAccountName.Text) || String.IsNullOrWhiteSpace(tbAccountKey.Text))
            {
                lbStatus.Text = "Error: empty field(s)";
            }
            else
            {
                lbStatus.Text = "Connecting...";
                btConnect.Enabled = false;

                try
                {
                    myCloudStorageAccount = CloudStorageAccount.Parse(strStorageConnectionString);

                    myCloudBlobClient = myCloudStorageAccount.CreateCloudBlobClient();
                    myCloudFileClient = myCloudStorageAccount.CreateCloudFileClient();
                    myCloudTableClient = myCloudStorageAccount.CreateCloudTableClient();
                    myCloudQueueClient = myCloudStorageAccount.CreateCloudQueueClient();

                    await Task.WhenAll(getBlobsAsync(), getFilesAsync(), getTablesAsync(), getQueuesAsync());

                    lbStatus.Text = "Connected";
                    btDisconnect.Enabled = true;
                }
                catch (Exception ex)
                {
                    lbStatus.Text = ex.Message; 
                    btConnect.Enabled = true;
                }

            } //if

        }

        private async Task getBlobsAsync()
        {
            foreach (CloudBlobContainer myCloudBlobContainer in myCloudBlobClient.ListContainers())
            {
                //TreeNode trNode = new TreeNode(myCloudBlobContainer.Name, 8, 8);
                TreeNode trNode = new TreeNode(myCloudBlobContainer.Name, 10, 10);

                await Task.Run(() =>
                {
                    addBlobsAsync(trNode, myCloudBlobContainer.ListBlobs());
                });

                trNode.Tag = myCloudBlobContainer.Uri.AbsoluteUri;
                trNode.ToolTipText = myCloudBlobContainer.GetType().Name;
                trBlobs.Nodes.Add(trNode);

            } //foreach myCloudBlobContainer

        } //getBlobsAsync

        private async Task addBlobsAsync(TreeNode parentNode, IEnumerable<IListBlobItem> blobItems)
        {
            foreach (IListBlobItem blobItem in blobItems)
            {
                TreeNode childNode = null;

                if (blobItem.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob myCloudBlockBlob = (CloudBlockBlob)blobItem;

                    string cbbname_ = myCloudBlockBlob.Uri.Segments.Last();
                    if (cbbname_.Contains(".vhd"))
                    {
                        childNode = new TreeNode(cbbname_, 9, 9);
                    }
                    else
                    {
                        childNode = new TreeNode(cbbname_, 2, 2);
                    }
                    
                }
                else if (blobItem.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob myCloudPageBlob = (CloudPageBlob)blobItem;

                    string cpbname_ = myCloudPageBlob.Uri.Segments.Last();
                    if (cpbname_.Contains(".vhd"))
                    {
                        childNode = new TreeNode(cpbname_, 9, 9);
                    } else
                    {
                        childNode = new TreeNode(cpbname_, 2, 2);
                    }
                    
                }
                else if (blobItem.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory myCloudBlobDirectory = (CloudBlobDirectory)blobItem;
                    childNode = new TreeNode(myCloudBlobDirectory.Uri.Segments.Last(), 0, 0);

                    //req:
                    await addBlobsAsync(childNode, myCloudBlobDirectory.ListBlobs());
                }

                childNode.Tag = blobItem.Uri.AbsoluteUri;
                childNode.ToolTipText = blobItem.GetType().Name;
                parentNode.Nodes.Add(childNode);

            } //foreach blobItem

        } //addBlobsAsync

        private async Task getFilesAsync()
        {
            foreach (CloudFileShare myCloudFileShare in myCloudFileClient.ListShares())
            {
                TreeNode trNode = new TreeNode(myCloudFileShare.Name, 3, 3);

                await Task.Run(() =>
                {
                    addFilesAsync(trNode, myCloudFileShare.GetRootDirectoryReference().ListFilesAndDirectories());
                });

                trNode.Tag = myCloudFileShare.Uri.AbsoluteUri;
                trNode.ToolTipText = myCloudFileShare.GetType().Name;
                trFiles.Nodes.Add(trNode);

            } //foreach myCloudFileShare

        } //getFilesAsync

        private async Task addFilesAsync(TreeNode parentNode, IEnumerable<IListFileItem> fileItems)
        {
            foreach (IListFileItem fileItem in fileItems)
            {
                TreeNode childNode = null;

                if (fileItem.GetType() == typeof(CloudFile))
                {
                    CloudFile myCloudFile = (CloudFile)fileItem;
                    childNode = new TreeNode(myCloudFile.Uri.Segments.Last(), 6, 6);
                }
                else if (fileItem.GetType() == typeof(CloudFileDirectory))
                {
                    CloudFileDirectory myCloudFileDirectory = (CloudFileDirectory)fileItem;
                    childNode = new TreeNode(myCloudFileDirectory.Uri.Segments.Last(), 0, 0);

                    //req:
                    await addFilesAsync(childNode, myCloudFileDirectory.ListFilesAndDirectories());
                }

                childNode.Tag = fileItem.Uri.AbsoluteUri;
                childNode.ToolTipText = fileItem.GetType().Name;
                parentNode.Nodes.Add(childNode);

            } //foreach fileItem

        } //addFilesAsync

        private async Task getTablesAsync()
        {
            foreach (var myCloudTable in myCloudTableClient.ListTables())
            {
                TreeNode trNode = new TreeNode(myCloudTable.Name, 5, 5);
                trTables.Nodes.Add(trNode);

            } //foreach myCloudTable

        } //getTablesAsync

        private async Task getQueuesAsync()
        {
            foreach (var myCloudQueue in myCloudQueueClient.ListQueues())
            {
                TreeNode trNode = new TreeNode(myCloudQueue.Name, 7, 7);
                trQueues.Nodes.Add(trNode);

            } //foreach myCloudQueue


        } //getQueuesAsync

        private void trBlobs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            gvProperties.Rows.Clear();

            string type_ = e.Node.ToolTipText;
            System.Uri uri_ = new System.Uri(e.Node.Tag.ToString());
            
            CloudBlobContainer cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
            CloudBlobDirectory cbd_;
            CloudBlockBlob cbb_;
            CloudPageBlob cpb_;
            CloudBlob cb_;

            string name_ = "", size_ = "", lastmodified_ = "";
            Image img_ = null;

            switch (type_)
            {
                case "CloudBlobContainer":

                    cbc_.FetchAttributes();

                    tbURL.Text = cbc_.Uri.ToString();
                    tbType.Text = cbc_.GetType().Name;
                    lbVar.Text = "Public access:";
                    tbVar.Text = cbc_.Properties.PublicAccess.ToString();
                    tbLastModified.Text = cbc_.Properties.LastModified.ToString();

                    foreach (var item_ in cbc_.ListBlobs())
                    {
                        if (item_.GetType().Name == "CloudBlobDirectory")
                        {
                            cbd_ = (CloudBlobDirectory)item_;

                            name_ = cbd_.Uri.Segments.Last();
                            type_ = cbd_.GetType().Name;
                            size_ = "";
                            lastmodified_ = "";
                            img_ = imageList1.Images[0];
                        }
                        else //CloudBlob
                        {
                           cb_ = (CloudBlob)item_;

                            name_ = cb_.Uri.Segments.Last();
                            type_ = cb_.GetType().Name;                            
                            size_ = getSize(cb_.Properties.Length);
                            lastmodified_ = cb_.Properties.LastModified.ToString();

                            if (name_.Contains(".vhd"))
                            {
                                img_ = imageList1.Images[9];
                            }
                            else
                            {
                                img_ = imageList1.Images[2];
                            }
                        }

                        gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);

                    } //forach

                    break;

                case "CloudBlobDirectory":

                    string path_ = "";
                    for (int i = 2; i < uri_.Segments.Length; i++)
                    {
                        path_ = path_ + uri_.Segments[i];
                    }

                    CloudBlobDirectory cbdroot_ = cbc_.GetDirectoryReference(path_);

                    tbURL.Text = cbdroot_.Uri.ToString();
                    tbType.Text = cbdroot_.GetType().Name;
                    lbVar.Text = "";
                    tbVar.Text = "";
                    tbLastModified.Text = "";

                    foreach (var item_ in cbdroot_.ListBlobs())
                    {
                        if (item_.GetType().Name == "CloudBlobDirectory")
                        {
                            cbd_ = (CloudBlobDirectory)item_;

                            name_ = cbd_.Uri.Segments.Last();
                            type_ = cbd_.GetType().Name;
                            size_ = "";
                            lastmodified_ = "";
                            img_ = imageList1.Images[0];
                        }
                        else
                        {
                            cb_ = (CloudBlob)item_;

                            name_ = cb_.Uri.Segments.Last();
                            type_ = cb_.GetType().Name;
                            size_ = getSize(cb_.Properties.Length);
                            lastmodified_ = cb_.Properties.LastModified.ToString();

                            if (name_.Contains(".vhd"))
                            {
                                img_ = imageList1.Images[9];
                            }
                            else
                            {
                                img_ = imageList1.Images[2];
                            }
                        }

                        gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);

                    } //forach

                    break;

                case "CloudBlockBlob":

                    cbb_ = (CloudBlockBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);

                    name_ = cbb_.Uri.Segments.Last();
                    type_ = cbb_.GetType().Name;
                    size_ = getSize(cbb_.Properties.Length);
                    lastmodified_ = cbb_.Properties.LastModified.ToString();

                    tbURL.Text = cbb_.Uri.ToString();
                    tbType.Text = type_;
                    lbVar.Text = "Size:";
                    tbVar.Text = size_;
                    tbLastModified.Text = lastmodified_;

                    if (name_.Contains(".vhd"))
                    {
                        img_ = imageList1.Images[9];
                    }
                    else
                    {
                        img_ = imageList1.Images[2];
                    }

                    gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);

                    break;

                case "CloudPageBlob":

                    cpb_ = (CloudPageBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);

                    name_ = cpb_.Uri.Segments.Last();
                    type_ = cpb_.GetType().Name;
                    size_ = getSize(cpb_.Properties.Length);
                    lastmodified_ = cpb_.Properties.LastModified.ToString();

                    tbURL.Text = cpb_.Uri.ToString();
                    tbType.Text = type_;
                    lbVar.Text = "Size:";
                    tbVar.Text = size_;
                    tbLastModified.Text = lastmodified_;

                    if (name_.Contains(".vhd"))
                    {
                        img_ = imageList1.Images[9];
                    }
                    else
                    {
                        img_ = imageList1.Images[2];
                    }

                    gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);

                    break;
            } //switch

        } //trBlobs_AfterSelect

        private void trFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            gvProperties.Rows.Clear();

            string type_ = e.Node.ToolTipText;
            System.Uri uri_ = new System.Uri(e.Node.Tag.ToString());

            CloudFileShare cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
            CloudFileDirectory cfd_;
            CloudFile cf_;

            string name_ = "", size_ = "", lastmodified_ = "", path_ = "";
            Image img_ = null;

            switch (type_)
            {
                case "CloudFileShare":

                    cfs_.FetchAttributes();

                    tbURL.Text = cfs_.Uri.ToString();
                    tbType.Text = cfs_.GetType().Name;
                    lbVar.Text = "Quota:";
                    tbVar.Text = cfs_.Properties.Quota + " GiB";
                    tbLastModified.Text = cfs_.Properties.LastModified.ToString();

                    foreach (IListFileItem item_ in cfs_.GetRootDirectoryReference().ListFilesAndDirectories())
                    {
                        if (item_.GetType().Name == "CloudFileDirectory")
                        {
                            cfd_ = (CloudFileDirectory)item_;
                            cfd_.FetchAttributes();

                            name_ = cfd_.Name;
                            type_ = cfd_.GetType().Name;
                            lastmodified_ = cfd_.Properties.LastModified.ToString();
                            img_ = imageList1.Images[0];
                        }
                        else //CloudFile
                        {
                            cf_ = (CloudFile)item_;
                            cf_.FetchAttributes();

                            name_ = cf_.Name;
                            size_ = getSize(cf_.Properties.Length);
                            type_ = cf_.GetType().Name;
                            lastmodified_ = cf_.Properties.LastModified.ToString();
                            img_ = imageList1.Images[6];
                        }

                        gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);

                    } //foreach

                    break;

                case "CloudFileDirectory":

                    path_ = "";
                    for (int i = 2; i < uri_.Segments.Length; i++)
                    {
                        path_ = path_ + uri_.Segments[i];
                    }

                    CloudFileDirectory cfdroot_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(path_);

                    cfdroot_.FetchAttributes();

                    tbURL.Text = cfdroot_.Uri.ToString();
                    tbType.Text = cfdroot_.GetType().Name;
                    lbVar.Text = "";
                    tbVar.Text = "";
                    tbLastModified.Text = cfdroot_.Properties.LastModified.ToString();

                    foreach (IListFileItem item_ in cfdroot_.ListFilesAndDirectories())
                    {
                        if (item_.GetType().Name == "CloudFileDirectory")
                        {
                            cfd_ = (CloudFileDirectory)item_;
                            cfd_.FetchAttributes();

                            name_ = cfd_.Name;
                            type_ = cfd_.GetType().Name;
                            lastmodified_ = cfd_.Properties.LastModified.ToString();
                            img_ = imageList1.Images[0];
                        }
                        else //CloudFile
                        {
                            cf_ = (CloudFile)item_;
                            cf_.FetchAttributes();

                            name_ = cf_.Name;
                            size_ = getSize(cf_.Properties.Length);
                            type_ = cf_.GetType().Name;
                            lastmodified_ = cf_.Properties.LastModified.ToString();
                            img_ = imageList1.Images[6];
                        }

                        gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);

                    } //foreach

                    break;

                case "CloudFile":

                    path_ = "";
                    for (int i = 2; i < uri_.Segments.Length; i++)
                    {
                        path_ = path_ + uri_.Segments[i];
                    }

                    cf_ = cfs_.GetRootDirectoryReference().GetFileReference(path_);

                    //MessageBox.Show("Uri.Segments.Last(): " + uri_.Segments.Last());    // -> file.txt
                    //MessageBox.Show("path_: " + path_);                                 // -> folder/file.txt
                    //MessageBox.Show("Uri.AbsolutePath: " + uri_.AbsolutePath);          // -> /share/folder/file.txt
                    //MessageBox.Show("Uri.LocalPath: " + uri_.LocalPath);                // -> /share/folder/file.txt
                    //MessageBox.Show("Uri.AbsoluteUri: " + uri_.AbsoluteUri);            // -> https://storageaccount.file.core.windows.net/share/folder/file.txt

                    cf_.FetchAttributes();

                    name_ = cf_.Name.Split('/').Last().ToString();
                    size_ = getSize(cf_.Properties.Length);
                    type_ = cf_.GetType().Name;
                    lastmodified_ = cf_.Properties.LastModified.ToString();
                    img_ = imageList1.Images[6];

                    tbURL.Text = cf_.Uri.ToString();
                    tbType.Text = type_;
                    lbVar.Text = "Size:";
                    tbVar.Text = size_;
                    tbLastModified.Text = lastmodified_;

                    gvProperties.Rows.Add(img_, name_, type_, size_, lastmodified_);
                    
                    break;
            } //swith

            gvProperties.Sort(gvProperties.Columns[2], ListSortDirection.Descending);
                
        } //trFiles_AfterSelect
       
        private string getSize(long lbytes_)
        {
            float fbytes_ = (float)lbytes_;
            string[] suffix_ = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int i = 0;

            while (fbytes_ >= 1024)
            {
                fbytes_ = fbytes_ / 1024;
                i++;
            }

            return String.Format("{0:0.##} {1}", fbytes_, suffix_[i]);

        } //getSize

        private async void btDownload_Click(object sender, EventArgs e)
        {
            TreeNode node_ = trFiles.SelectedNode;
            System.Uri uri_ = new System.Uri(node_.Tag.ToString());

            string path_ = "";
            for (int i = 2; i < uri_.Segments.Length; i++)
            {
                path_ = path_ + uri_.Segments[i];
            }

            CloudFileShare cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
            CloudFile cf_ = cfs_.GetRootDirectoryReference().GetFileReference(path_);

            saveFileDialog1.Filter = "Filter|*.*";
            saveFileDialog1.Title = "Download";
            saveFileDialog1.FileName = cf_.Name.Split('/').Last();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path_ = Path.GetFullPath(saveFileDialog1.FileName);
                //MessageBox.Show(path_);
                await cf_.DownloadToFileAsync(path_, FileMode.CreateNew);
            }
            
        } //btDownload

       

    } //Form1

} //AzureStorageBrowser