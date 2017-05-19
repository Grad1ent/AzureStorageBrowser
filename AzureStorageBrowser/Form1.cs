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
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading;

namespace AzureStorageBrowser
{
    public partial class Form1 : Form
    {
        CloudStorageAccount myCloudStorageAccount;
        CloudBlobClient myCloudBlobClient;
        CloudFileClient myCloudFileClient;
        CloudTableClient myCloudTableClient;
        CloudQueueClient myCloudQueueClient;

        CancellationTokenSource myCancellationTokenSource = new CancellationTokenSource();

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

        } //openToolStripMenuItem

        private void btExpandAll_Click(object sender, EventArgs e)
        {
            myTree.ExpandAll();

        } //btExpandAll

        private void btCollapseAll_Click(object sender, EventArgs e)
        {
            myTree.CollapseAll();

        } //btCollapseAll

        private void myTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.ToolTipText)
            {
                case "CloudBlobDirectory":
                    e.Node.ImageIndex = 1;
                    e.Node.SelectedImageIndex = 1;
                    break;

                case "CloudBlobContainer":
                    e.Node.ImageIndex = 11;
                    e.Node.SelectedImageIndex = 11;
                    break;

                case "CloudFileShare":
                    e.Node.ImageIndex = 4;
                    e.Node.SelectedImageIndex = 4;
                    break;

                case "CloudFileDirectory":
                    e.Node.ImageIndex = 1;
                    e.Node.SelectedImageIndex = 1;
                    break;

                case "blobNodes":
                    e.Node.ImageIndex = 11;
                    e.Node.SelectedImageIndex = 11;
                    break;

                case "fileNodes":
                    e.Node.ImageIndex = 4;
                    e.Node.SelectedImageIndex = 4;
                    break;

                case "tableNodes":
                    e.Node.ImageIndex = 15;
                    e.Node.SelectedImageIndex = 15;
                    break;

                case "queueNodes":
                    e.Node.ImageIndex = 13;
                    e.Node.SelectedImageIndex = 13;

                    break;

            } //switch

        } //myTree_AfterExpand

        private void myTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.ToolTipText)
            {
                case "CloudBlobDirectory":
                    e.Node.ImageIndex = 0;
                    e.Node.SelectedImageIndex = 0;
                    break;

                case "CloudBlobContainer":
                    e.Node.ImageIndex = 10;
                    e.Node.SelectedImageIndex = 10;
                    break;

                case "CloudFileShare":
                    e.Node.ImageIndex = 3;
                    e.Node.SelectedImageIndex = 3;
                    break;

                case "CloudFileDirectory":
                    e.Node.ImageIndex = 0;
                    e.Node.SelectedImageIndex = 0;
                    break;

                case "blobNodes":
                    e.Node.ImageIndex = 10;
                    e.Node.SelectedImageIndex = 10;
                    break;

                case "fileNodes":
                    e.Node.ImageIndex = 3;
                    e.Node.SelectedImageIndex = 3;
                    break;

                case "tableNodes":
                    e.Node.ImageIndex = 14;
                    e.Node.SelectedImageIndex = 14;
                    break;

                case "queueNodes":
                    e.Node.ImageIndex = 12;
                    e.Node.SelectedImageIndex = 12;

                    break;

            } //switch

        } //myTree_AfterCollapse

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            myTree.CollapseAll();
            myTree.Nodes[0].Nodes.Clear();
            myTree.Nodes[1].Nodes.Clear();
            myTree.Nodes[2].Nodes.Clear();
            myTree.Nodes[3].Nodes.Clear();

            gvProperties.Rows.Clear();

            tbURL.Text = "";
            tbSize.Text = "";
            tbType.Text = "";
            tbLastModified.Text = "";

            lbStatus.Text = "Disconnected";
            btConnect.Enabled = true;
            btDisconnect.Enabled = false;

            myCloudBlobClient = null;
            myCloudFileClient = null;
            myCloudTableClient = null;
            myCloudQueueClient = null;

        } //btDisconnect

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
                    myTree.CollapseAll();
                }
                catch (Exception ex)
                {
                    lbStatus.Text = ex.Message; 
                    btConnect.Enabled = true;
                }

            } //if

        } //btConnect

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

        private async Task getBlobsAsync()
        {
            foreach (CloudBlobContainer myCloudBlobContainer in myCloudBlobClient.ListContainers())
            {
                TreeNode trNode = new TreeNode(myCloudBlobContainer.Name, 10, 11);

                await Task.Run(() =>
                {
                    addBlobsAsync(trNode, myCloudBlobContainer.ListBlobs());
                });

                trNode.Tag = myCloudBlobContainer.Uri.AbsoluteUri;
                trNode.ToolTipText = myCloudBlobContainer.GetType().Name;
                myTree.Nodes[0].Nodes.Add(trNode);

            } //foreach myCloudBlobContainer

        } //getBlobsAsync

        private async Task addBlobsAsync(TreeNode parentnode_, IEnumerable<IListBlobItem> blobItems)
        {
            foreach (IListBlobItem blobItem in blobItems)
            {
                TreeNode node_ = null;

                if (blobItem.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory cbd_ = (CloudBlobDirectory)blobItem;
                    node_ = new TreeNode(cbd_.Uri.Segments.Last(), 0, 1);

                    //req:
                    await addBlobsAsync(node_, cbd_.ListBlobs());

                    node_.Tag = blobItem.Uri.AbsoluteUri;
                    node_.ToolTipText = blobItem.GetType().Name;
                    parentnode_.Nodes.Add(node_);
                }

            } //foreach blobItem

        } //addBlobsAsync

        private async Task getFilesAsync()
        {
            foreach (CloudFileShare myCloudFileShare in myCloudFileClient.ListShares())
            {
                TreeNode node_ = new TreeNode(myCloudFileShare.Name, 3, 4);

                await Task.Run(() =>
                {
                    addFilesAsync(node_, myCloudFileShare.GetRootDirectoryReference().ListFilesAndDirectories());
                });

                node_.Tag = myCloudFileShare.Uri.AbsoluteUri;
                node_.ToolTipText = myCloudFileShare.GetType().Name;
                myTree.Nodes[1].Nodes.Add(node_);

            } //foreach myCloudFileShare

        } //getFilesAsync

        private async Task addFilesAsync(TreeNode parentnode_, IEnumerable<IListFileItem> fileItems)
        {
            foreach (IListFileItem fileItem in fileItems)
            {
                TreeNode node_ = null;

                if (fileItem.GetType() == typeof(CloudFileDirectory))
                {
                    CloudFileDirectory cfd_ = (CloudFileDirectory)fileItem;
                    node_ = new TreeNode(cfd_.Uri.Segments.Last(), 0, 1);

                    //req:
                    await addFilesAsync(node_, cfd_.ListFilesAndDirectories());

                    node_.Tag = fileItem.Uri.AbsoluteUri;
                    node_.ToolTipText = fileItem.GetType().Name;
                    parentnode_.Nodes.Add(node_);
                }

            } //foreach fileItem

        } //addFilesAsync

        private async Task getTablesAsync()
        {
            foreach (var myCloudTable in myCloudTableClient.ListTables())
            {
                TreeNode node_ = new TreeNode(myCloudTable.Name, 5, 5);

                node_.Tag = myCloudTable.Uri.AbsoluteUri;
                node_.ToolTipText = myCloudTable.GetType().Name;
                myTree.Nodes[2].Nodes.Add(node_);

            } //foreach myCloudTable

        } //getTablesAsync

        private async Task getQueuesAsync()
        {
            foreach (var myCloudQueue in myCloudQueueClient.ListQueues())
            {
                TreeNode node_ = new TreeNode(myCloudQueue.Name, 7, 7);

                node_.Tag = myCloudQueue.Uri.AbsoluteUri;
                node_.ToolTipText = myCloudQueue.GetType().Name;
                myTree.Nodes[3].Nodes.Add(node_);

            } //foreach myCloudQueue

        } //getQueuesAsync

        private void myTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            getNode(e.Node);

        } //myTree_AfterSelect

        private async void myTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {            
            if (e.Node.IsEditing)
            {
                e.CancelEdit = true;
            }
            else
            {
                if (String.IsNullOrEmpty(e.Label))
                {
                    e.Node.Remove();
                }
                else
                {
                    try
                    {
                        Uri parenturi_ = null;

                        CloudFileShare cfs_ = null;
                        CloudFileDirectory cfd_ = null;

                        string type_ = e.Node.Parent.ToolTipText;
                        switch (type_)
                        {
                            case "blobNodes":

                                CloudBlobContainer cbc_ = myCloudBlobClient.GetContainerReference(e.Label);
                                await cbc_.CreateIfNotExistsAsync();

                                e.Node.Tag = cbc_.Uri.AbsoluteUri;
                                e.Node.ToolTipText = cbc_.GetType().Name;

                                getNode(e.Node);

                                break;

                            case "fileNodes":

                                cfs_ = myCloudFileClient.GetShareReference(e.Label);
                                await cfs_.CreateIfNotExistsAsync();
                                cfs_.Properties.Quota = 25;
                                cfs_.SetProperties();

                                e.Node.Tag = cfs_.Uri.AbsoluteUri;
                                e.Node.ToolTipText = cfs_.GetType().Name;

                                getNode(e.Node);

                                break;

                            case "tableNodes":

                                CloudTable ct_ = myCloudTableClient.GetTableReference(e.Label);
                                await ct_.CreateIfNotExistsAsync();

                                e.Node.Tag = ct_.Uri.AbsoluteUri;
                                e.Node.ToolTipText = ct_.GetType().Name;

                                getNode(e.Node);

                                break;

                            case "queueNodes":

                                CloudQueue cq_ = myCloudQueueClient.GetQueueReference(e.Label);
                                await cq_.CreateIfNotExistsAsync();

                                e.Node.Tag = cq_.Uri.AbsoluteUri;
                                e.Node.ToolTipText = cq_.GetType().Name;

                                getNode(e.Node);

                                break;

                            case "CloudBlobContainer":

                                break;

                            case "CloudBlobDirectory":

                                break;

                            case "CloudFileShare":

                                parenturi_ = new Uri(e.Node.Parent.Tag.ToString());

                                cfs_ = myCloudFileClient.GetShareReference(parenturi_.Segments[1]);
                                cfd_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(e.Label);
                                await cfd_.CreateIfNotExistsAsync();

                                e.Node.Tag = cfd_.Uri.AbsoluteUri;
                                e.Node.ToolTipText = cfd_.GetType().Name;

                                getNode(e.Node);

                                break;

                            case "CloudFileDirectory":

                                parenturi_ = new Uri(e.Node.Parent.Tag.ToString());

                                string path_ = "";
                                for (int i = 2; i < parenturi_.Segments.Length; i++)
                                {
                                    path_ = path_ + parenturi_.Segments[i];
                                }

                                cfs_ = myCloudFileClient.GetShareReference(parenturi_.Segments[1]);
                                cfd_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(path_).GetDirectoryReference(e.Label);
                                await cfd_.CreateIfNotExistsAsync();

                                e.Node.Tag = cfd_.Uri.AbsoluteUri;
                                e.Node.ToolTipText = cfd_.GetType().Name;

                                getNode(e.Node);

                                break;

                        } //switch
                    }
                    catch (Exception ex)
                    {
                        lbStatus.Text = ex.Message;
                        myTree.SelectedNode.Remove();
                    }

                } //else

            } //else

            myTree.LabelEdit = false;

        } //myTree_AfterLabelEdit

        private void getNode(TreeNode node_)
        {
            gvProperties.Rows.Clear();

            tbURL.Text = "";
            tbSize.Text = "";
            tbType.Text = "";
            tbLastModified.Text = "";

            try
            {
                if (!String.IsNullOrEmpty(node_.Text))
                if (myCloudBlobClient != null || myCloudFileClient != null || myCloudTableClient != null || myCloudQueueClient != null)
                {
                    string type_ = node_.ToolTipText;
                    System.Uri uri_ = new System.Uri(node_.Tag.ToString());

                    CloudBlobContainer cbc_;
                    CloudBlobDirectory cbd_;
                    CloudBlockBlob cbb_;
                    CloudPageBlob cpb_;
                    CloudBlob cb_;

                    CloudFileShare cfs_;
                    CloudFileDirectory cfd_;
                    CloudFile cf_;

                    CloudTable ct_;

                    CloudQueue cq_;

                    DataGridViewRow row_;

                    string url_ = "", name_ = "", size_ = "", lastmodified_ = "";
                    Image img_ = null;

                    switch (type_)
                    {
                        case "CloudBlobContainer":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            cbc_.FetchAttributes();

                            tbURL.Text = cbc_.Uri.ToString();
                            tbSize.Text = cbc_.ListBlobs().Count().ToString() + " item(s)";
                            tbType.Text = cbc_.GetType().Name;
                            tbLastModified.Text = cbc_.Properties.LastModified.ToString();

                            foreach (var item_ in cbc_.ListBlobs())
                            {
                                if (item_.GetType().Name == "CloudBlobDirectory")
                                {
                                    cbd_ = (CloudBlobDirectory)item_;

                                    url_ = cbd_.Uri.AbsoluteUri;
                                    name_ = cbd_.Uri.Segments.Last();
                                    size_ = cbd_.ListBlobs().Count().ToString() + " item(s)";
                                    type_ = cbd_.GetType().Name;
                                    lastmodified_ = "";
                                    img_ = imageList1.Images[0];
                                }
                                else //CloudBlob
                                {
                                    cb_ = (CloudBlob)item_;
                                    cb_.FetchAttributes();

                                    url_ = cb_.Uri.AbsoluteUri;
                                    name_ = cb_.Uri.Segments.Last();
                                    size_ = getSize(cb_.Properties.Length);
                                    type_ = cb_.GetType().Name;
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

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //forach

                            break;

                        case "CloudBlobDirectory":

                            string path_ = "";
                            for (int i = 2; i < uri_.Segments.Length; i++)
                            {
                                path_ = path_ + uri_.Segments[i];
                            }

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            CloudBlobDirectory cbdroot_ = cbc_.GetDirectoryReference(path_);

                            tbURL.Text = cbdroot_.Uri.ToString();
                            tbSize.Text = cbdroot_.ListBlobs().Count().ToString() + " item(s)";
                            tbType.Text = cbdroot_.GetType().Name;
                            tbLastModified.Text = "";

                            foreach (var item_ in cbdroot_.ListBlobs())
                            {
                                if (item_.GetType().Name == "CloudBlobDirectory")
                                {
                                    cbd_ = (CloudBlobDirectory)item_;

                                    url_ = cbd_.Uri.AbsoluteUri;
                                    name_ = cbd_.Uri.Segments.Last();
                                    size_ = cbd_.ListBlobs().Count().ToString() + " item(s)";
                                    type_ = cbd_.GetType().Name;
                                    lastmodified_ = "";
                                    img_ = imageList1.Images[0];
                                }
                                else //CloudBlob
                                {
                                    cb_ = (CloudBlob)item_;
                                    cb_.FetchAttributes();

                                    url_ = cb_.Uri.AbsoluteUri;
                                    name_ = cb_.Uri.Segments.Last();
                                    size_ = getSize(cb_.Properties.Length);
                                    type_ = cb_.GetType().Name;
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

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //forach

                            break;

                        case "CloudFileShare":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            cfs_.FetchAttributes();

                            tbURL.Text = cfs_.Uri.ToString();
                            tbSize.Text = cfs_.Properties.Quota + " GiB of quota";
                            tbType.Text = cfs_.GetType().Name;
                            tbLastModified.Text = cfs_.Properties.LastModified.ToString();

                            foreach (IListFileItem item_ in cfs_.GetRootDirectoryReference().ListFilesAndDirectories())
                            {
                                if (item_.GetType().Name == "CloudFileDirectory")
                                {
                                    cfd_ = (CloudFileDirectory)item_;
                                    cfd_.FetchAttributes();

                                    url_ = cfd_.Uri.AbsoluteUri;
                                    name_ = cfd_.Name;
                                    size_ = cfd_.ListFilesAndDirectories().Count().ToString() + " item(s)";
                                    type_ = cfd_.GetType().Name;
                                    lastmodified_ = cfd_.Properties.LastModified.ToString();
                                    img_ = imageList1.Images[0];
                                }
                                else //CloudFile
                                {
                                    cf_ = (CloudFile)item_;
                                    cf_.FetchAttributes();

                                    url_ = cf_.Uri.AbsoluteUri;
                                    name_ = cf_.Name;
                                    size_ = getSize(cf_.Properties.Length);
                                    type_ = cf_.GetType().Name;
                                    lastmodified_ = cf_.Properties.LastModified.ToString();
                                    img_ = imageList1.Images[6];
                                }

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //foreach

                            break;

                        case "CloudFileDirectory":

                            path_ = "";
                            for (int i = 2; i < uri_.Segments.Length; i++)
                            {
                                path_ = path_ + uri_.Segments[i];
                            }

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            CloudFileDirectory cfdroot_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(path_);

                            cfdroot_.FetchAttributes();

                            tbURL.Text = cfdroot_.Uri.ToString();
                            tbSize.Text = cfdroot_.ListFilesAndDirectories().Count().ToString() + " item(s)";
                            tbType.Text = cfdroot_.GetType().Name;
                            tbLastModified.Text = cfdroot_.Properties.LastModified.ToString();

                            foreach (IListFileItem item_ in cfdroot_.ListFilesAndDirectories())
                            {
                                if (item_.GetType().Name == "CloudFileDirectory")
                                {
                                    cfd_ = (CloudFileDirectory)item_;
                                    cfd_.FetchAttributes();

                                    url_ = cfd_.Uri.AbsoluteUri;
                                    name_ = cfd_.Name;
                                    size_ = cfd_.ListFilesAndDirectories().Count().ToString() + " item(s)";
                                    type_ = cfd_.GetType().Name;
                                    lastmodified_ = cfd_.Properties.LastModified.ToString();
                                    img_ = imageList1.Images[0];
                                }
                                else //CloudFile
                                {
                                    cf_ = (CloudFile)item_;
                                    cf_.FetchAttributes();

                                    url_ = cf_.Uri.AbsoluteUri;
                                    name_ = cf_.Name;
                                    size_ = getSize(cf_.Properties.Length);
                                    type_ = cf_.GetType().Name;
                                    lastmodified_ = cf_.Properties.LastModified.ToString();
                                    img_ = imageList1.Images[6];
                                }

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //foreach

                            break;

                        case "CloudTable":

                                ct_ = myCloudTableClient.GetTableReference(uri_.Segments[1]);

                                tbURL.Text = ct_.Uri.ToString();
                                tbSize.Text = "";
                                tbType.Text = ct_.GetType().Name;
                                tbLastModified.Text = "";

                                break;

                        case "CloudQueue":

                                cq_ = myCloudQueueClient.GetQueueReference(uri_.Segments[1]);
                                cq_.FetchAttributes();

                                tbURL.Text = cq_.Uri.ToString();
                                tbSize.Text = cq_.ApproximateMessageCount.ToString() + " msg(s)";
                                tbType.Text = cq_.GetType().Name;
                                tbLastModified.Text = "";

                                if (cq_.ApproximateMessageCount.Value > 0)
                                    foreach (CloudQueueMessage qm_ in cq_.PeekMessages(cq_.ApproximateMessageCount.Value))
                                    {
                                        url_ = cq_.Uri.AbsoluteUri;
                                        name_ = qm_.AsString;
                                        size_ = getSize(qm_.AsString.Length);
                                        type_ = qm_.GetType().Name;
                                        lastmodified_ = qm_.InsertionTime.Value.ToString();
                                        img_ = imageList1.Images[2];

                                        row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                        row_.Tag = url_ + "/" + qm_.Id;
                                        row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                        gvProperties.Rows.Add(row_);

                                    } //foreach

                                break;

                        case "blobNodes":

                            tbURL.Text = myCloudBlobClient.BaseUri.ToString();
                            tbSize.Text = myCloudBlobClient.ListContainers().Count().ToString() + " item(s)";
                            tbType.Text = myCloudBlobClient.GetType().Name;
                            tbLastModified.Text = "";

                            foreach (var item_ in myCloudBlobClient.ListContainers())
                            {
                                cbc_ = (CloudBlobContainer)item_;
                                cbc_.FetchAttributes();

                                url_ = cbc_.Uri.AbsoluteUri;
                                name_ = cbc_.Name;
                                size_ = cbc_.ListBlobs().Count().ToString() + " item(s)";
                                type_ = cbc_.GetType().Name;
                                lastmodified_ = cbc_.Properties.LastModified.ToString();
                                img_ = imageList1.Images[10];

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //foreach

                            break;

                        case "fileNodes":

                            tbURL.Text = myCloudFileClient.BaseUri.ToString();
                            tbSize.Text = myCloudFileClient.ListShares().Count().ToString() + " item(s)";
                            tbType.Text = myCloudFileClient.GetType().Name;
                            tbLastModified.Text = "";

                            foreach (var item_ in myCloudFileClient.ListShares())
                            {
                                cfs_ = (CloudFileShare)item_;
                                cfs_.FetchAttributes();

                                url_ = cfs_.Uri.ToString();
                                name_ = cfs_.Name;
                                size_ = cfs_.Properties.Quota + " GiB of quota";
                                type_ = cfs_.GetType().Name;
                                lastmodified_ = cfs_.Properties.LastModified.ToString();
                                img_ = imageList1.Images[3];

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //foreach

                            break;

                        case "tableNodes":

                            tbURL.Text = myCloudTableClient.BaseUri.ToString();
                            tbSize.Text = myCloudTableClient.ListTables().Count().ToString() + " item(s)";
                            tbType.Text = myCloudTableClient.GetType().Name;
                            tbLastModified.Text = "";

                            foreach (var item_ in myCloudTableClient.ListTables())
                            {
                                ct_ = (CloudTable)item_;

                                url_ = ct_.Uri.AbsoluteUri;
                                name_ = ct_.Name;
                                size_ = "";
                                type_ = ct_.GetType().Name;
                                lastmodified_ = "";
                                img_ = imageList1.Images[5];

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //foreach myCloudTable

                            break;

                        case "queueNodes":

                            tbURL.Text = myCloudQueueClient.BaseUri.ToString();
                            tbSize.Text = myCloudQueueClient.ListQueues().Count().ToString() + " item(s)";
                            tbType.Text = myCloudQueueClient.GetType().Name;
                            tbLastModified.Text = "";

                            foreach (var item_ in myCloudQueueClient.ListQueues())
                            {
                                cq_ = (CloudQueue)item_;
                                cq_.FetchAttributes();

                                url_ = cq_.Uri.AbsoluteUri;
                                name_ = cq_.Name;
                                size_ = cq_.ApproximateMessageCount.ToString() + " msg(s)";
                                type_ = cq_.GetType().Name;
                                lastmodified_ = "";
                                img_ = imageList1.Images[7];

                                row_ = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                                row_.Tag = url_;
                                row_.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                                gvProperties.Rows.Add(row_);

                            } //foreach myCloudQueue

                            break;

                    } //switch

                    gvProperties.Sort(gvProperties.Columns[3], ListSortDirection.Descending);
                    gvProperties.ClearSelection();

                } //null
            }
            catch(Exception ex)
            {
                lbStatus.Text = ex.Message;
            }

        } //getNode

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
            saveFileDialog1.Filter = "Filter|*.*";
            saveFileDialog1.Title = "Download";

            myCancellationTokenSource = new CancellationTokenSource();
            CancellationToken token_ = myCancellationTokenSource.Token;

            DataGridViewRow row_ = gvProperties.SelectedRows[0];

            System.Uri uri_ = new System.Uri(row_.Tag.ToString());

            string name_ = row_.Cells[1].Value.ToString();
            string type_ = row_.Cells[3].Value.ToString();
            string path_;

            saveFileDialog1.FileName = name_;

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    lbStatus.Text = "Downloading...";
                    pbProgress.Visible = true;
                    btStopProgress.Visible = true;

                    path_ = Path.GetFullPath(saveFileDialog1.FileName);

                    switch (type_)
                    {
                        case "CloudBlockBlob":

                            CloudBlockBlob cbb_ = (CloudBlockBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);

                            await cbb_.DownloadToFileAsync(path_, FileMode.CreateNew, token_);      

                            break;

                        case "CloudPageBlob":

                            CloudPageBlob cpb_ = (CloudPageBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);

                            await cpb_.DownloadToFileAsync(path_, FileMode.CreateNew, token_);

                            break;

                        case "CloudFile":

                            string srcpath_ = "";
                            for (int i = 2; i < uri_.Segments.Length; i++)
                            {
                                srcpath_ = srcpath_ + uri_.Segments[i];
                            }

                            CloudFileShare cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            CloudFile cf_ = cfs_.GetRootDirectoryReference().GetFileReference(srcpath_);

                            await cf_.DownloadToFileAsync(path_, FileMode.CreateNew, token_);

                            break;

                    } //switch

                    lbStatus.Text = "Downloaded";
                    pbProgress.Visible = false;
                    btStopProgress.Visible = false;

                } //if

            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
                pbProgress.Visible = false;
                btStopProgress.Visible = false;
            }

        } //btDownload

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btDownload_Click(null, null);
        } //downloadToolStripMenuItem

        private async void btUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Filter|*.*";
            openFileDialog1.Title = "Upload";
            openFileDialog1.FileName = "";

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    CloudBlobContainer cbc_;
                    CloudBlobDirectory cbd_;
                    CloudBlockBlob cbb_;
                    CloudPageBlob cpb_;

                    CloudFileShare cfs_;
                    CloudFileDirectory cfd_;
                    CloudFile cf_;

                    DialogResult result_;
                    string message_ = "Upload as Page Blob?\n" +
                        "(No = Block Blob)";

                    System.Uri uri_ = new System.Uri(myTree.SelectedNode.Tag.ToString());

                    string srcpath_ = Path.GetFullPath(openFileDialog1.FileName);
                    string dstpath_ = "";
                    string filename_ = openFileDialog1.FileName.Split('\\').Last().ToString();

                    lbStatus.Text = "Uploading...";
                    pbProgress.Visible = true;
                    btStopProgress.Visible = true;

                    string type_ = myTree.SelectedNode.ToolTipText;
                    switch (type_)
                    {
                        case "CloudBlobContainer":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);

                            result_ = MessageBox.Show(message_, "Uploading...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (result_ == DialogResult.Yes)
                            {
                                cpb_ = cbc_.GetPageBlobReference(filename_);
                                await cpb_.UploadFromFileAsync(srcpath_);
                            }
                            else //Block
                            {
                                cbb_ = cbc_.GetBlockBlobReference(filename_);
                                await cbb_.UploadFromFileAsync(srcpath_);
                            }

                            break;

                        case "CloudBlobDirectory":

                            dstpath_ = "";
                            for (int i = 2; i < uri_.Segments.Length; i++)
                            {
                                dstpath_ = dstpath_ + uri_.Segments[i];
                            }

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            cbd_ = cbc_.GetDirectoryReference(dstpath_);

                            result_ = MessageBox.Show(message_, "Uploading...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (result_ == DialogResult.Yes)
                            {
                                cpb_ = cbd_.GetPageBlobReference(filename_);
                                await cpb_.UploadFromFileAsync(srcpath_);
                            }
                            else //Block
                            {
                                cbb_ = cbd_.GetBlockBlobReference(filename_);
                                await cbb_.UploadFromFileAsync(srcpath_);
                            }

                            break;

                        case "CloudFileShare":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            cf_ = cfs_.GetRootDirectoryReference().GetFileReference(filename_);

                            await cf_.UploadFromFileAsync(srcpath_);

                            break;

                        case "CloudFileDirectory":

                            dstpath_ = "";
                            for (int i = 2; i < uri_.Segments.Length; i++)
                            {
                                dstpath_ = dstpath_ + uri_.Segments[i];
                            }

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            cfd_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(dstpath_);
                            cf_ = cfd_.GetFileReference(filename_);

                            await cf_.UploadFromFileAsync(srcpath_);

                            break;

                    } //switch

                    getNode(myTree.SelectedNode);
                    
                    lbStatus.Text = "Uploaded";
                    pbProgress.Visible = false;
                    btStopProgress.Visible = false;

                } //if OK
            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
                pbProgress.Visible = false;
                btStopProgress.Visible = false;
            }

        } //btUpload

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btUpload_Click(sender, e);
        } //uploadToolStripMenuItem

        private void btExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Filter|*.csv";
            saveFileDialog1.Title = "Export";
            saveFileDialog1.FileName = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gvProperties.MultiSelect = true;
                    gvProperties.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
                    gvProperties.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
                    gvProperties.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
                    gvProperties.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
                    gvProperties.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                    gvProperties.Columns[1].Selected = true;
                    gvProperties.Columns[2].Selected = true;
                    gvProperties.Columns[3].Selected = true;
                    gvProperties.Columns[4].Selected = true;
                        DataObject obj_ = gvProperties.GetClipboardContent();
                    gvProperties.ClearSelection();
                    gvProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    gvProperties.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
                    gvProperties.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
                    gvProperties.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
                    gvProperties.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                    gvProperties.MultiSelect = false;

                    string path_ = Path.GetFullPath(saveFileDialog1.FileName);
                    File.WriteAllText(path_, obj_.GetText(TextDataFormat.CommaSeparatedValue));
                }
                catch(Exception ex)
                {
                    lbStatus.Text = ex.Message;
                }
            } //if

        } //btExport

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btExport_Click(sender, e);
        } //exportToolStripMenuItem

        private void gvProperties_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row_ = gvProperties.Rows[e.RowIndex];

                tbURL.Text = row_.Tag.ToString();
                tbSize.Text = row_.Cells[2].Value.ToString();
                tbType.Text = row_.Cells[3].Value.ToString();
                tbLastModified.Text = row_.Cells[4].Value.ToString();
            } //if

        } //gvProperties click

        private void gvProperties_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row_ = gvProperties.Rows[e.RowIndex];

            foreach (TreeNode node_ in myTree.SelectedNode.Nodes)
            {
                if (node_.Tag.ToString() == row_.Tag.ToString())
                {
                    myTree.SelectedNode = node_;
                    getNode(node_);

                    break;
                } //if

            } //forach

        } //gvProperties_CellDoubleClick

        private async void btDeleteInGrid_Click(object sender, EventArgs e)
        {
            DialogResult result_ = MessageBox.Show("Delete selected item(s)?", "Deleting...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            CloudBlobContainer cbc_;
            CloudFileShare cfs_;
            CloudQueue cq_;

            string path_ = "";

            if (result_ == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row_ in gvProperties.SelectedRows)
                    {
                        System.Uri uri_ = new System.Uri(row_.Tag.ToString());

                        for (int i = 2; i < uri_.Segments.Length; i++)
                        {
                            path_ = path_ + uri_.Segments[i];
                        }

                        string name_ = row_.Cells[1].Value.ToString();
                        string type_ = row_.Cells[3].Value.ToString();

                        lbStatus.Text = "Deleting...";

                        switch (type_)
                        {
                            case "CloudBlobContainer":

                                cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                                await cbc_.DeleteIfExistsAsync();
                                                                
                                break;

                            case "CloudBlobDirectory":

                                cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                                CloudBlobDirectory cbd_ = cbc_.GetDirectoryReference(path_);

                                await Task.Run(() =>
                                {
                                    removeCloudBlobDirectoryAsync(cbd_);
                                });

                                break;

                            case "CloudBlockBlob":

                                CloudBlockBlob cbb_ = (CloudBlockBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);
                                await cbb_.DeleteIfExistsAsync();

                                break;

                            case "CloudPageBlob":

                                CloudPageBlob cpb_ = (CloudPageBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);
                                await cpb_.DeleteIfExistsAsync();

                                break;

                            case "CloudFileShare":

                                cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                                await cfs_.DeleteIfExistsAsync();

                                break;

                            case "CloudFileDirectory":

                                cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                                CloudFileDirectory cfd_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(path_);

                                await Task.Run(() =>
                                {
                                    removeCloudFileDirectoryAsync(cfd_);
                                });

                                break;

                            case "CloudFile":

                                cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                                CloudFile cf_ = cfs_.GetRootDirectoryReference().GetFileReference(path_);
                                await cf_.DeleteIfExistsAsync();

                                break;

                            case "CloudTable":

                                CloudTable ct_ = myCloudTableClient.GetTableReference(uri_.Segments[1]);
                                await ct_.DeleteIfExistsAsync();

                                break;

                            case "CloudQueue":

                                cq_ = myCloudQueueClient.GetQueueReference(uri_.Segments[1]);
                                await cq_.DeleteIfExistsAsync();

                                break;

                            case "CloudQueueMessage":

                                cq_ = myCloudQueueClient.GetQueueReference(uri_.Segments[1]);
                                cq_.FetchAttributes();

                                if (cq_.ApproximateMessageCount.Value > 0)
                                    foreach (CloudQueueMessage cm_ in cq_.GetMessages(cq_.ApproximateMessageCount.Value))
                                    {
                                        if (cm_.Id == path_)
                                        {
                                            await cq_.DeleteMessageAsync(cm_);
                                            break;
                                        } //if

                                    } //foreach

                                break;                                

                        } //switch

                        //Refreshing Grid
                        gvProperties.Rows.Remove(row_);

                        // Refreshing Tree
                        foreach (TreeNode node_ in myTree.SelectedNode.Nodes)
                        {
                            if (node_.Tag.ToString() == row_.Tag.ToString())
                            {
                                node_.Remove();
                                break;
                            } //if

                        } //forach

                        lbStatus.Text = "Deleted";

                    } //foreach

                }
                catch (Exception ex)
                {
                    lbStatus.Text = ex.Message;
                }

            } //result_

        } //btDeleteInGrid

        private async Task removeCloudBlobDirectoryAsync(CloudBlobDirectory cbd_)
        {
            foreach (var item_ in cbd_.ListBlobs())
            {
                if (item_.GetType().Name == "CloudBlobDirectory")
                {
                    //req
                    await removeCloudBlobDirectoryAsync((CloudBlobDirectory)item_);

                } else //CloudBlob
                {
                    CloudBlob cb_ = (CloudBlob)item_;
                    await cb_.DeleteIfExistsAsync();
                }

            } //foreach

        } //removeCloudBlobDirectoryAsync

        private async Task removeCloudFileDirectoryAsync(CloudFileDirectory cfdparent_)
        {
            foreach (var item_ in cfdparent_.ListFilesAndDirectories())
            {
                if (item_.GetType().Name == "CloudFileDirectory")
                {
                    //req
                    CloudFileDirectory cfd_ = (CloudFileDirectory)item_;
                    await removeCloudFileDirectoryAsync(cfd_);
                    await cfd_.DeleteIfExistsAsync();                    
                }
                else //CloudFile
                {
                    CloudFile cf_ = (CloudFile)item_;
                    await cf_.DeleteIfExistsAsync();
                }

            } //foreach

            await cfdparent_.DeleteIfExistsAsync();

        } //removeCloudFileDirectoryAsync

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btDeleteInGrid_Click(sender, e);
        } //deleteToolStripMenuItem

        private async void btDeleteInTree_Click(object sender, EventArgs e)
        {
            DialogResult result_ = MessageBox.Show("Delete selected node?", "Deleting...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            CloudBlobContainer cbc_;
            CloudFileShare cfs_;

            string path_ = "";

            TreeNode node_ = myTree.SelectedNode;

            if (result_ == DialogResult.Yes)
            {
                try
                {
                    lbStatus.Text = "Deleting...";

                    string type_ = node_.ToolTipText;
                    System.Uri uri_ = new System.Uri(node_.Tag.ToString());

                    for (int i = 2; i < uri_.Segments.Length; i++)
                    {
                        path_ = path_ + uri_.Segments[i];
                    }

                    switch (type_)
                    {
                        case "CloudBlobContainer":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            await cbc_.DeleteIfExistsAsync();

                            break;

                        case "CloudBlobDirectory":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            CloudBlobDirectory cbd_ = cbc_.GetDirectoryReference(path_);

                            await Task.Run(() =>
                            {
                                removeCloudBlobDirectoryAsync(cbd_);
                            });

                            break;

                        case "CloudFileShare":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            await cfs_.DeleteIfExistsAsync();

                            break;

                        case "CloudFileDirectory":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            CloudFileDirectory cfd_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(path_);

                            await Task.Run(() =>
                            {
                                removeCloudFileDirectoryAsync(cfd_);
                            });

                            break;

                        case "CloudTable":

                            CloudTable ct_ = myCloudTableClient.GetTableReference(uri_.Segments[1]);
                            await ct_.DeleteIfExistsAsync();

                            break;

                        case "CloudQueue":

                            CloudQueue cq_ = myCloudQueueClient.GetQueueReference(uri_.Segments[1]);
                            await cq_.DeleteIfExistsAsync();

                            break;

                    } //switch

                    if (myTree.SelectedNode.Parent != null)
                    {
                        myTree.SelectedNode.Remove();
                    } //if

                    lbStatus.Text = "Deleted";

                }
                catch (Exception ex)
                {
                    lbStatus.Text = ex.Message;
                }

            } //result_

        }//btDeleteInTree

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.btDeleteInTree_Click(sender, e);
        }//deleteToolStripMenuItem1

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string type_ = myTree.SelectedNode.ToolTipText;
            TreeNode node_ = null;

            switch (type_)
            {
                case "blobNodes":

                    node_ = new TreeNode("",10,11);

                    break;

                case "fileNodes":

                    node_ = new TreeNode("", 3, 4);

                    break;

                case "tableNodes":

                    node_ = new TreeNode("", 5, 5);

                    break;

                case "queueNodes":

                    node_ = new TreeNode("", 7, 7);

                    break;

                case "CloudBlobContainer":

                    break;

                case "CloudBlobDirectory":

                    break;

                case "CloudFileShare":

                    node_ = new TreeNode("", 0, 1);

                    break;

                case "CloudFileDirectory":

                    node_ = new TreeNode("", 0, 1);

                    break;

            } //switch

            if (node_ != null)
            {
                myTree.LabelEdit = true;
                myTree.SelectedNode.Nodes.Add(node_);
                myTree.SelectedNode = node_;

                node_.BeginEdit();

            } //if null

        } //newToolStripMenuItem1

        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message_ = "Name: " + myTree.SelectedNode.Name + "\nText: " + myTree.SelectedNode.Text + "\nTag: " + myTree.SelectedNode.Tag.ToString() + "\nTip: " + myTree.SelectedNode.ToolTipText;
            MessageBox.Show(message_);

        }//propertiesToolStripMenuItem1

        private void btUp_Click(object sender, EventArgs e)
        {
            if (myTree.SelectedNode.Parent != null)
            {
                myTree.SelectedNode = myTree.SelectedNode.Parent;
                myTree.SelectedNode.Collapse(false);
                getNode(myTree.SelectedNode);
            } //if

        } //btUp

        private void btStopProgress_ButtonClick(object sender, EventArgs e)
        {
            myCancellationTokenSource.Cancel();
            myCancellationTokenSource.Dispose();

        } //btStopProgress

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                getNode(myTree.SelectedNode);
            }
            catch(Exception ex)
            {
                lbStatus.Text = ex.Message;
            }

        } //refreshToolStripMenuItem

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloudBlobContainer cbc_;
            CloudFileShare cfs_;
            CloudQueue cq_;

            string message_ = "";
            string path_ = "";

            string ld_ = "", lsu_ = "", lse_ = "";

            if (gvProperties.SelectedRows.Count != 0)
            {
                try
                {
                    DataGridViewRow row_ = gvProperties.SelectedRows[0];
                    Uri uri_ = new Uri(row_.Tag.ToString());

                    for (int i = 2; i < uri_.Segments.Length; i++)
                    {
                        path_ = path_ + uri_.Segments[i];
                    }

                    string type_ = row_.Cells[3].Value.ToString();

                    switch (type_)
                    {
                        case "CloudBlobContainer":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            cbc_.FetchAttributes();

                            ld_ = "Lease duration: " + cbc_.Properties.LeaseDuration.ToString() + "\n";
                            lse_ = "Lease state: " + cbc_.Properties.LeaseState.ToString() + "\n";
                            lsu_ = "Lease status: " + cbc_.Properties.LeaseStatus.ToString() + "\n";
                            string pa_ = "Public access: " + cbc_.Properties.PublicAccess.Value.ToString() + "\n";

                            message_ = ld_ + lse_ + lsu_ + pa_;

                            break;

                        case "CloudBlobDirectory":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            CloudBlobDirectory cbd_ = cbc_.GetDirectoryReference(path_);

                            string prefix_ = "Prefix: " + cbd_.Prefix;

                            message_ = prefix_;

                            break;

                        case "CloudBlockBlob":

                            CloudBlockBlob cbb_ = (CloudBlockBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);
                            cbb_.FetchAttributes();

                            //string ab_ = "Append committed blocks: " + cbb_.Properties.AppendBlobCommittedBlockCount.Value.ToString() + "\n";
                            //string cc_ = "Cache control: " + cbb_.Properties.CacheControl.ToString() + "\n";
                            //string cd_ = "Content disposition: " + cbb_.Properties.ContentDisposition.ToString() + "\n";
                            //string ce_ = "Content encoding: " + cbb_.Properties.ContentEncoding.ToString() + "\n";
                            //string cl_ = "Content language: " + cbb_.Properties.ContentLanguage.ToString() + "\n";
                            //string cm_ = "Content MD5: " + cbb_.Properties.ContentMD5.ToString() + "\n";
                            //string co_ = "Content type: " + cbb_.Properties.ContentType.ToString() + "\n";
                            string ic_ = "Incremental copy: " + cbb_.Properties.IsIncrementalCopy.ToString() + "\n";
                            string se_ = "Server encrypted: " + cbb_.Properties.IsServerEncrypted.ToString() + "\n";
                            ld_ = "Lease duration: " + cbb_.Properties.LeaseDuration.ToString() + "\n";
                            lse_ = "Lease state: " + cbb_.Properties.LeaseState.ToString() + "\n";
                            lsu_ = "Lease status: " + cbb_.Properties.LeaseStatus.ToString() + "\n";

                            message_ = ic_ + se_ + ld_ + lse_ + lsu_;

                            break;

                        case "CloudPageBlob":

                            CloudPageBlob cpb_ = (CloudPageBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);
                        

                            break;

                        case "CloudFileShare":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);

                            break;

                        case "CloudFileDirectory":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            CloudFileDirectory cfd_ = cfs_.GetRootDirectoryReference().GetDirectoryReference(path_);

                            break;

                        case "CloudFile":

                            cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            CloudFile cf_ = cfs_.GetRootDirectoryReference().GetFileReference(path_);

                            break;

                        case "CloudTable":

                            CloudTable ct_ = myCloudTableClient.GetTableReference(uri_.Segments[1]);

                            break;

                        case "CloudQueue":

                            cq_ = myCloudQueueClient.GetQueueReference(uri_.Segments[1]);

                            break;

                        case "CloudQueueMessage":

                            cq_ = myCloudQueueClient.GetQueueReference(uri_.Segments[1]);
                            cq_.FetchAttributes();

                            if (cq_.ApproximateMessageCount.Value > 0)
                                foreach (CloudQueueMessage cm_ in cq_.PeekMessages(cq_.ApproximateMessageCount.Value))
                                {
                                    if (cm_.Id == path_)
                                    {
                                        string body_ = "Message: " + cm_.AsString + "\n\n";
                                        string id_ = "Id: " + cm_.Id + "\n";
                                        string it_ = "Insertion time: " + cm_.InsertionTime.ToString() + "\n";
                                        string et_ = "Expiration time: " + cm_.ExpirationTime.ToString() + "\n";
                                        string nv_ = "Next visible time: " + cm_.NextVisibleTime.ToString() + "\n";
                                        string pr_ = "Pop receipt: " + cm_.PopReceipt + "\n";
                                        string dq_ = "Dequeue count: " + cm_.DequeueCount.ToString();

                                        message_ = body_ + id_ + it_ + et_ + nv_ + pr_ + dq_;

                                        break;
                                    } //if

                                } //foreach

                            break;

                    } //switch

                    MessageBox.Show(message_, "Properties");
                }
                catch(Exception ex)
                {
                     lbStatus.Text = ex.Message;
                }

            } //if

        } //propertiesToolStripMenuItem

    } //Form1

} //AzureStorageBrowser
