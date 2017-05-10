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
                    myCloudFileClient = myCloudStorageAccount.CreateCloudFileClient();
                    myCloudTableClient = myCloudStorageAccount.CreateCloudTableClient();
                    myCloudQueueClient = myCloudStorageAccount.CreateCloudQueueClient();

                    myTree.Nodes[0].Tag = myCloudBlobClient.BaseUri.ToString();
                    myTree.Nodes[1].Tag = myCloudFileClient.BaseUri.ToString();
                    myTree.Nodes[2].Tag = myCloudTableClient.BaseUri.ToString();
                    myTree.Nodes[3].Tag = myCloudQueueClient.BaseUri.ToString();

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

        } //btConnect

        private async Task getBlobsAsync()
        {
            foreach (CloudBlobContainer myCloudBlobContainer in myCloudBlobClient.ListContainers())
            {
                TreeNode trNode = new TreeNode(myCloudBlobContainer.Name, 10, 10);

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
                    node_ = new TreeNode(cbd_.Uri.Segments.Last(), 0, 0);

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
                TreeNode trNode = new TreeNode(myCloudFileShare.Name, 3, 3);

                await Task.Run(() =>
                {
                    addFilesAsync(trNode, myCloudFileShare.GetRootDirectoryReference().ListFilesAndDirectories());
                });

                trNode.Tag = myCloudFileShare.Uri.AbsoluteUri;
                trNode.ToolTipText = myCloudFileShare.GetType().Name;
                myTree.Nodes[1].Nodes.Add(trNode);

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
                    node_ = new TreeNode(cfd_.Uri.Segments.Last(), 0, 0);

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
                TreeNode trNode = new TreeNode(myCloudTable.Name, 5, 5);

                trNode.Tag = myCloudTable.Uri.ToString();
                trNode.ToolTipText = myCloudTable.GetType().Name;
                myTree.Nodes[2].Nodes.Add(trNode);

            } //foreach myCloudTable

        } //getTablesAsync

        private async Task getQueuesAsync()
        {
            foreach (var myCloudQueue in myCloudQueueClient.ListQueues())
            {
                TreeNode trNode = new TreeNode(myCloudQueue.Name, 7, 7);

                trNode.Tag = myCloudQueue.Uri.ToString();
                trNode.ToolTipText = myCloudQueue.GetType().Name;
                myTree.Nodes[3].Nodes.Add(trNode);

            } //foreach myCloudQueue

        } //getQueuesAsync

        private void myTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            getNode(e.Node);

        } //myTree_AfterSelect

        private void getNode(TreeNode node_)
        {
            gvProperties.Rows.Clear();

            tbURL.Text = "";
            tbSize.Text = "";
            tbType.Text = "";
            tbLastModified.Text = "";

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

                DataGridViewRow gvRow;

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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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
                            else
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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

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

                            url_ = cq_.Uri.AbsoluteUri;
                            name_ = cq_.Name;
                            size_ = cq_.ApproximateMessageCount.ToString() + " msg(s)";
                            type_ = cq_.GetType().Name;
                            lastmodified_ = "";
                            img_ = imageList1.Images[7];

                            gvRow = (DataGridViewRow)gvProperties.RowTemplate.Clone();
                            gvRow.Tag = url_;
                            gvRow.CreateCells(gvProperties, img_, name_, size_, type_, lastmodified_);
                            gvProperties.Rows.Add(gvRow);

                        } //foreach myCloudQueue

                        break;

                } //switch

                gvProperties.Sort(gvProperties.Columns[3], ListSortDirection.Descending);
                gvProperties.ClearSelection();

            } //null

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

            try
            {
                foreach (DataGridViewRow row_ in gvProperties.SelectedRows)
                {
                    System.Uri uri_ = new System.Uri(row_.Tag.ToString());

                    string path_ = "";
                    for (int i = 2; i < uri_.Segments.Length; i++)
                    {
                        path_ = path_ + uri_.Segments[i];
                    }

                    string name_ = row_.Cells[1].Value.ToString();
                    string type_ = row_.Cells[3].Value.ToString();
                    saveFileDialog1.FileName = name_;

                    switch (type_)
                    {
                        case "CloudBlockBlob":

                            CloudBlockBlob cbb_ = (CloudBlockBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);

                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                path_ = Path.GetFullPath(saveFileDialog1.FileName);
                                await cbb_.DownloadToFileAsync(path_, FileMode.CreateNew);
                            }

                            break;

                        case "CloudPageBlob":

                            CloudPageBlob cpb_ = (CloudPageBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);

                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                path_ = Path.GetFullPath(saveFileDialog1.FileName);
                                await cpb_.DownloadToFileAsync(path_, FileMode.CreateNew);
                            }

                            break;

                        case "CloudFile":

                            CloudFileShare cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                            CloudFile cf_ = cfs_.GetRootDirectoryReference().GetFileReference(path_);

                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                lbStatus.Text = "Downloading...";
                                pbProgress.Visible = true;

                                path_ = Path.GetFullPath(saveFileDialog1.FileName);
                                await cf_.DownloadToFileAsync(path_, FileMode.CreateNew);

                                lbStatus.Text = "Downloaded";
                                pbProgress.Visible = false;
                            }

                            break;

                    } //switch

                } //foreach

            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
            }

        } //btDownload

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btDownload_Click(null, null);
        } //download

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

                    CloudFileShare cfs_;
                    CloudFileDirectory cfd_;
                    CloudFile cf_;

                    string type_ = myTree.SelectedNode.ToolTipText;
                    System.Uri uri_ = new System.Uri(myTree.SelectedNode.Tag.ToString());

                    string srcpath_ = Path.GetFullPath(openFileDialog1.FileName);
                    string dstpath_ = "";
                    string filename_ = openFileDialog1.FileName.Split('\\').Last().ToString();

                    lbStatus.Text = "Uploading...";
                    pbProgress.Visible = true;

                    switch (type_)
                    {
                        case "CloudBlobContainer":

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            cbb_ = cbc_.GetBlockBlobReference(filename_);

                            await cbb_.UploadFromFileAsync(srcpath_);

                            break;

                        case "CloudBlobDirectory":

                            dstpath_ = "";
                            for (int i = 2; i < uri_.Segments.Length; i++)
                            {
                                dstpath_ = dstpath_ + uri_.Segments[i];
                            }

                            cbc_ = myCloudBlobClient.GetContainerReference(uri_.Segments[1]);
                            cbd_ = cbc_.GetDirectoryReference(dstpath_);
                            cbb_ = cbd_.GetBlockBlobReference(filename_);

                            await cbb_.UploadFromFileAsync(srcpath_);

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

                } //if OK
            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
            }

        } //btUpload

        private void btExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Filter|*.csv";
            saveFileDialog1.Title = "Export";
            saveFileDialog1.FileName = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path_ = Path.GetFullPath(saveFileDialog1.FileName);
                gvProperties.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                gvProperties.SelectAll();

                DataObject obj_ = gvProperties.GetClipboardContent();               
                File.WriteAllText(path_, obj_.GetText(TextDataFormat.CommaSeparatedValue));

                gvProperties.ClearSelection();
            }

        } //btExport

        private void gvProperties_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow gvRow = gvProperties.Rows[e.RowIndex];           

            tbURL.Text = gvRow.Tag.ToString();
            tbSize.Text = gvRow.Cells[2].Value.ToString();
            tbType.Text = gvRow.Cells[3].Value.ToString();
            tbLastModified.Text = gvRow.Cells[4].Value.ToString();

        } //gvProperties click
        
        private async void btDelete_Click(object sender, EventArgs e)
        {
            DialogResult result_ = MessageBox.Show("Delete selected item(s)?", "Deleting...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result_ == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row_ in gvProperties.SelectedRows)
                    {
                        System.Uri uri_ = new System.Uri(row_.Tag.ToString());

                        string path_ = "";
                        for (int i = 2; i < uri_.Segments.Length; i++)
                        {
                            path_ = path_ + uri_.Segments[i];
                        }

                        string name_ = row_.Cells[1].Value.ToString();
                        string type_ = row_.Cells[3].Value.ToString();

                        lbStatus.Text = "Deleting...";

                        switch (type_)
                        {
                            case "CloudBlockBlob":

                                CloudBlockBlob cbb_ = (CloudBlockBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);
                                await cbb_.DeleteIfExistsAsync();

                                break;

                            case "CloudPageBlob":

                                CloudPageBlob cpb_ = (CloudPageBlob)myCloudBlobClient.GetBlobReferenceFromServer(uri_);
                                await cpb_.DeleteIfExistsAsync();

                                break;

                            case "CloudFile":

                                CloudFileShare cfs_ = myCloudFileClient.GetShareReference(uri_.Segments[1]);
                                CloudFile cf_ = cfs_.GetRootDirectoryReference().GetFileReference(path_);

                                await cf_.DeleteIfExistsAsync();

                                break;

                        } //switch

                        getNode(myTree.SelectedNode);
                        lbStatus.Text = "Deleted";

                    } //foreach

                }
                catch (Exception ex)
                {
                    lbStatus.Text = ex.Message;
                }

            } //result_

        } //btDelete

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btDelete_Click(sender, e);
        }

        private void btNewFolder_Click(object sender, EventArgs e)
        {

        }

        private void btNewFile_Click(object sender, EventArgs e)
        {

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    } //Form1

} //AzureStorageBrowser
