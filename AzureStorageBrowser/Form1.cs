using System;
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


namespace AzureStorageBrowser
{
    public partial class Form1 : Form
    {
        CloudStorageAccount myCloudStorageAccount;
        CloudBlobClient myCloudBlobClient;
        CloudFileClient myCloudFileClient;
        CloudTableClient myCloudTableClient;

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
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }

        private void trBlobs_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
        }

        private void trFiles_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }

        private void trFiles_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            trBlobs.Nodes.Clear();
            trFiles.Nodes.Clear();

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

                await Task.WhenAll(getBlobsAsync(), getFilesAsync(), getTablesAsync());

                lbStatus.Text = "Connected";
                btDisconnect.Enabled = true;
            } //if

        }

        private async Task getBlobsAsync()
        {
            foreach (CloudBlobContainer myCloudBlobContainer in myCloudBlobClient.ListContainers())
            {
                TreeNode trNode = new TreeNode(myCloudBlobContainer.Name, 0, 0);

                await Task.Run(() =>
                {
                    addBlobsAsync(trNode, myCloudBlobContainer.ListBlobs());
                });

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
                    childNode = new TreeNode("Block: " + myCloudBlockBlob.Uri.Segments.Last(), 2, 2);
                }
                else if (blobItem.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob myCloudPageBlob = (CloudPageBlob)blobItem;
                    childNode = new TreeNode("Page: " + myCloudPageBlob.Uri.Segments.Last(), 2, 2);
                }
                else if (blobItem.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory myCloudBlobDirectory = (CloudBlobDirectory)blobItem;
                    childNode = new TreeNode(myCloudBlobDirectory.Uri.Segments.Last(), 0, 0);

                    //req:
                    await addBlobsAsync(childNode, myCloudBlobDirectory.ListBlobs());
                }

                parentNode.Nodes.Add(childNode);

            } //foreach blobItem

        } //addBlobsAsync

        private async Task getFilesAsync()
        {
            foreach (CloudFileShare myCloudFileShare in myCloudFileClient.ListShares())
            {
                TreeNode trNode = new TreeNode(myCloudFileShare.Name, 0, 0);

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
                    childNode = new TreeNode(myCloudFile.Uri.Segments.Last(), 2, 2);
                }
                else if (fileItem.GetType() == typeof(CloudFileDirectory))
                {
                    CloudFileDirectory myCloudFileDirectory = (CloudFileDirectory)fileItem;
                    childNode = new TreeNode(myCloudFileDirectory.Uri.Segments.Last(), 0, 0);

                    //req:
                    await addFilesAsync(childNode, myCloudFileDirectory.ListFilesAndDirectories());
                }

                parentNode.Nodes.Add(childNode);

            } //foreach fileItem

        } //addFilesAsync

        private async Task getTablesAsync()
        {

            //await Task.Run(() =>
            //{
            //addTablesAsync();
            //});

            
            CloudTable myCloudTable = myCloudTableClient.GetTableReference("EventRegistrations");

            TreeNode trNode = new TreeNode(myCloudTable.Name, 3, 3);
            trTables.Nodes.Add(trNode);
            
        } //getTablesAsync

        private async Task addTablesAsync()
        {
            //MessageBox.Show(myCloudTableClient.ToString());

            //foreach (var myCloudTable in myCloudTableClient.ListTables())
            //{
            //    MessageBox.Show(myCloudTable.Name);
                
            //    TreeNode trNode = new TreeNode(myCloudTable.Name, 3, 3);
            //    trTables.Nodes.Add(trNode);
            //} //foreach tableItem

        } //addTablesAsync

        private void dumpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    } //Form1
} //AzureStorageBrowser
