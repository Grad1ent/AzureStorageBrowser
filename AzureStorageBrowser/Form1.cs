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
        }

        private void trBlobs_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ToolTipText != "CloudBlobContainer")
            {
                e.Node.ImageIndex = 0;
                e.Node.SelectedImageIndex = 0;
            }
        }

        private void trFiles_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 5;
            e.Node.SelectedImageIndex = 5;
        }

        private void trFiles_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 4;
            e.Node.SelectedImageIndex = 4;
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            trBlobs.Nodes.Clear();
            trFiles.Nodes.Clear();
            trTables.Nodes.Clear();
            trQueues.Nodes.Clear();
            gvBlobs.Rows.Clear();

            lbStatus.Text = "Disconnected";
            lbUri.Text = "";
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
                lbStatus.Text = "Error: filed(s) are empty";
            }
            else
            {
                lbStatus.Text = "Connecting...";
                btConnect.Enabled = false;

                myCloudStorageAccount = CloudStorageAccount.Parse(strStorageConnectionString);

                myCloudBlobClient = myCloudStorageAccount.CreateCloudBlobClient();
                myCloudFileClient = myCloudStorageAccount.CreateCloudFileClient();
                myCloudTableClient = myCloudStorageAccount.CreateCloudTableClient();
                myCloudQueueClient = myCloudStorageAccount.CreateCloudQueueClient();

                await Task.WhenAll(getBlobsAsync(), getFilesAsync(), getTablesAsync(), getQueuesAsync());

                lbStatus.Text = "Connected";
                btDisconnect.Enabled = true;
            } //if

        }

        private async Task getBlobsAsync()
        {
            foreach (CloudBlobContainer myCloudBlobContainer in myCloudBlobClient.ListContainers())
            {
                TreeNode trNode = new TreeNode(myCloudBlobContainer.Name, 15, 15);

                await Task.Run(() =>
                {
                    addBlobsAsync(trNode, myCloudBlobContainer.ListBlobs());
                });

                trNode.Tag = myCloudBlobContainer.Uri.AbsoluteUri;
                trNode.ToolTipText = myCloudBlobContainer.GetType().ToString().Split('.').Last();
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
                    childNode = new TreeNode(myCloudBlockBlob.Uri.Segments.Last(), 2, 2);
                }
                else if (blobItem.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob myCloudPageBlob = (CloudPageBlob)blobItem;
                    childNode = new TreeNode(myCloudPageBlob.Uri.Segments.Last(), 2, 2);
                }
                else if (blobItem.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory myCloudBlobDirectory = (CloudBlobDirectory)blobItem;
                    childNode = new TreeNode(myCloudBlobDirectory.Uri.Segments.Last(), 0, 0);

                    //req:
                    await addBlobsAsync(childNode, myCloudBlobDirectory.ListBlobs());
                }

                childNode.Tag = blobItem.Uri.AbsoluteUri;
                childNode.ToolTipText = blobItem.GetType().ToString().Split('.').Last();
                parentNode.Nodes.Add(childNode);

            } //foreach blobItem

        } //addBlobsAsync

        private async Task getFilesAsync()
        {
            foreach (CloudFileShare myCloudFileShare in myCloudFileClient.ListShares())
            {
                TreeNode trNode = new TreeNode(myCloudFileShare.Name, 4, 4);

                await Task.Run(() =>
                {
                    addFilesAsync(trNode, myCloudFileShare.GetRootDirectoryReference().ListFilesAndDirectories());
                });

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
                    childNode = new TreeNode(myCloudFileDirectory.Uri.Segments.Last(), 4, 4);

                    //req:
                    await addFilesAsync(childNode, myCloudFileDirectory.ListFilesAndDirectories());
                }

                parentNode.Nodes.Add(childNode);

            } //foreach fileItem

        } //addFilesAsync

        private async Task getTablesAsync()
        {
            foreach (var myCloudTable in myCloudTableClient.ListTables())
            {
                TreeNode trNode = new TreeNode(myCloudTable.Name, 3, 3);
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
            
            gvBlobs.Rows.Clear();

            string type_ = e.Node.ToolTipText;
            System.Uri uri_ = new System.Uri(e.Node.Tag.ToString());
            //CloudBlobContainer
            //CloudBlobDirectory
            //CloudBlockBlob
            //CloudPageBlob

            switch (type_)
            {
                case "CloudBlobContainer":

                    string cname_ = e.Node.Tag.ToString().Split('/').Last();
                    CloudBlobContainer myCloudBlobContainer = myCloudBlobClient.GetContainerReference(cname_);
                    
                    foreach (var item_ in myCloudBlobContainer.ListBlobs())
                    {
                        if (item_.GetType().ToString().Split('.').Last() == "CloudBlobDirectory")
                        {
                            
                        }
                        else
                        {
                            CloudBlob myCloudBlob = (CloudBlob)item_;

                            string bname_ = myCloudBlob.Name;
                            string btype_ = myCloudBlob.GetType().ToString().Split('.').Last();
                            string size_ = (myCloudBlob.Properties.Length / 1024 / 1024 / 1024).ToString();
                            string lastmodified_ = myCloudBlob.Properties.LastModified.ToString();

                            gvBlobs.Rows.Add(bname_, btype_, size_, lastmodified_);
                        }
                    }
                    break;

                case "CloudBlobDirectory":

                    //CloudBlobDirectory myCloudBlobDir = myCloudBlobClient.


                    break;

                case "CloudBlockBlob":

                    //System.Uri myUri = new Uri(e.Node.Tag.ToString());
                    //ICloudBlob myCloudBlob = myCloudBlobClient.GetBlobReferenceFromServer(myUri);

                    break;

                case "CloudPageBlob":


                    break;
            } //switch

            lbUri.Text = e.Node.Tag.ToString();
        }
    } //Form1
} //AzureStorageBrowser
