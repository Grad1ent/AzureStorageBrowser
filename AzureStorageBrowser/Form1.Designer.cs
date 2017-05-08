namespace AzureStorageBrowser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Blobs", 10, 11);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Files", 3, 4);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Tables", 14, 15);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Queues", 12, 13);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAccountName = new System.Windows.Forms.TextBox();
            this.tbAccountKey = new System.Windows.Forms.TextBox();
            this.tbConnectionString = new System.Windows.Forms.TextBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.cbDefaultEnpointsProtocol = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbDownload = new System.Windows.Forms.ToolStripProgressBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btExpandAll = new System.Windows.Forms.ToolStripButton();
            this.btCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.myTree = new System.Windows.Forms.TreeView();
            this.gvProperties = new System.Windows.Forms.DataGridView();
            this.Column0 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btNew = new System.Windows.Forms.ToolStripButton();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btDownload = new System.Windows.Forms.ToolStripButton();
            this.btUpload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tbLastModified = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.lbSize = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProperties)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 179);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.67252F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.32748F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbAccountName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbAccountKey, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbConnectionString, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btConnect, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btDisconnect, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbDefaultEnpointsProtocol, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.12281F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.87719F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(845, 160);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Protocol:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Account Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Account Key:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 52);
            this.label4.TabIndex = 3;
            this.label4.Text = "Connection String:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbAccountName
            // 
            this.tbAccountName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAccountName.Location = new System.Drawing.Point(110, 29);
            this.tbAccountName.Name = "tbAccountName";
            this.tbAccountName.Size = new System.Drawing.Size(732, 20);
            this.tbAccountName.TabIndex = 4;
            // 
            // tbAccountKey
            // 
            this.tbAccountKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAccountKey.Location = new System.Drawing.Point(110, 55);
            this.tbAccountKey.Name = "tbAccountKey";
            this.tbAccountKey.Size = new System.Drawing.Size(732, 20);
            this.tbAccountKey.TabIndex = 5;
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.BackColor = System.Drawing.SystemColors.Control;
            this.tbConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConnectionString.Location = new System.Drawing.Point(110, 81);
            this.tbConnectionString.Multiline = true;
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConnectionString.Size = new System.Drawing.Size(732, 46);
            this.tbConnectionString.TabIndex = 6;
            // 
            // btConnect
            // 
            this.btConnect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btConnect.Image = ((System.Drawing.Image)(resources.GetObject("btConnect.Image")));
            this.btConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btConnect.Location = new System.Drawing.Point(10, 133);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(94, 24);
            this.btConnect.TabIndex = 7;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btDisconnect.Enabled = false;
            this.btDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("btDisconnect.Image")));
            this.btDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btDisconnect.Location = new System.Drawing.Point(110, 133);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(95, 24);
            this.btDisconnect.TabIndex = 8;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // cbDefaultEnpointsProtocol
            // 
            this.cbDefaultEnpointsProtocol.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbDefaultEnpointsProtocol.FormattingEnabled = true;
            this.cbDefaultEnpointsProtocol.Items.AddRange(new object[] {
            "https",
            "http"});
            this.cbDefaultEnpointsProtocol.Location = new System.Drawing.Point(110, 3);
            this.cbDefaultEnpointsProtocol.Name = "cbDefaultEnpointsProtocol";
            this.cbDefaultEnpointsProtocol.Size = new System.Drawing.Size(85, 21);
            this.cbDefaultEnpointsProtocol.TabIndex = 9;
            this.cbDefaultEnpointsProtocol.Text = "https";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusLabel,
            this.lbStatus,
            this.pbDownload});
            this.statusStrip1.Location = new System.Drawing.Point(0, 92);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(857, 21);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatusLabel
            // 
            this.lbStatusLabel.Name = "lbStatusLabel";
            this.lbStatusLabel.Size = new System.Drawing.Size(42, 16);
            this.lbStatusLabel.Text = "Status:";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 16);
            // 
            // pbDownload
            // 
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(100, 15);
            this.pbDownload.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cFolder.gif");
            this.imageList1.Images.SetKeyName(1, "ofolder.gif");
            this.imageList1.Images.SetKeyName(2, "doc.gif");
            this.imageList1.Images.SetKeyName(3, "containerclosed.gif");
            this.imageList1.Images.SetKeyName(4, "containeropened.gif");
            this.imageList1.Images.SetKeyName(5, "table.gif");
            this.imageList1.Images.SetKeyName(6, "file.gif");
            this.imageList1.Images.SetKeyName(7, "queue.gif");
            this.imageList1.Images.SetKeyName(8, "container.gif");
            this.imageList1.Images.SetKeyName(9, "hdd.gif");
            this.imageList1.Images.SetKeyName(10, "cloudfolderclosed.gif");
            this.imageList1.Images.SetKeyName(11, "cloudfolderopened.gif");
            this.imageList1.Images.SetKeyName(12, "queueFolderClosed.gif");
            this.imageList1.Images.SetKeyName(13, "queueFolderOpened.gif");
            this.imageList1.Images.SetKeyName(14, "tableFolderClosed.gif");
            this.imageList1.Images.SetKeyName(15, "tableFolderOpened.gif");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(857, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.dumpToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 25);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "&Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // dumpToolStripMenuItem
            // 
            this.dumpToolStripMenuItem.Name = "dumpToolStripMenuItem";
            this.dumpToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.dumpToolStripMenuItem.Text = "&Dump...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(120, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 25);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.01869F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.98131F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(857, 214);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 214);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.myTree);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvProperties);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(857, 301);
            this.splitContainer1.SplitterDistance = 341;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btExpandAll,
            this.btCollapseAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(337, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btExpandAll
            // 
            this.btExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExpandAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btExpandAll.Image")));
            this.btExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExpandAll.Name = "btExpandAll";
            this.btExpandAll.Size = new System.Drawing.Size(23, 22);
            this.btExpandAll.Text = "+";
            this.btExpandAll.ToolTipText = "Expand All";
            this.btExpandAll.Click += new System.EventHandler(this.btExpandAll_Click);
            // 
            // btCollapseAll
            // 
            this.btCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCollapseAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("btCollapseAll.Image")));
            this.btCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCollapseAll.Name = "btCollapseAll";
            this.btCollapseAll.Size = new System.Drawing.Size(23, 22);
            this.btCollapseAll.Text = "-";
            this.btCollapseAll.ToolTipText = "Collapse All";
            this.btCollapseAll.Click += new System.EventHandler(this.btCollapseAll_Click);
            // 
            // myTree
            // 
            this.myTree.BackColor = System.Drawing.SystemColors.Window;
            this.myTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTree.ImageIndex = 0;
            this.myTree.ImageList = this.imageList1;
            this.myTree.Location = new System.Drawing.Point(0, 25);
            this.myTree.Name = "myTree";
            treeNode1.ImageIndex = 10;
            treeNode1.Name = "blobNodes";
            treeNode1.SelectedImageIndex = 11;
            treeNode1.Text = "Blobs";
            treeNode1.ToolTipText = "blobNodes";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "fileNodes";
            treeNode2.SelectedImageIndex = 4;
            treeNode2.Text = "Files";
            treeNode2.ToolTipText = "fileNodes";
            treeNode3.ImageIndex = 14;
            treeNode3.Name = "tableNodes";
            treeNode3.SelectedImageIndex = 15;
            treeNode3.Text = "Tables";
            treeNode3.ToolTipText = "tableNodes";
            treeNode4.ImageIndex = 12;
            treeNode4.Name = "queueNodes";
            treeNode4.SelectedImageIndex = 13;
            treeNode4.Text = "Queues";
            treeNode4.ToolTipText = "queueNodes";
            this.myTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.myTree.SelectedImageIndex = 0;
            this.myTree.Size = new System.Drawing.Size(337, 272);
            this.myTree.TabIndex = 0;
            this.myTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.myTree_AfterCollapse);
            this.myTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.myTree_AfterExpand);
            this.myTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.myTree_AfterSelect);
            // 
            // gvProperties
            // 
            this.gvProperties.AllowUserToAddRows = false;
            this.gvProperties.AllowUserToDeleteRows = false;
            this.gvProperties.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gvProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column0,
            this.Column1,
            this.Column3,
            this.Column2,
            this.Column4});
            this.gvProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvProperties.Location = new System.Drawing.Point(0, 25);
            this.gvProperties.Name = "gvProperties";
            this.gvProperties.ReadOnly = true;
            this.gvProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProperties.ShowCellToolTips = false;
            this.gvProperties.Size = new System.Drawing.Size(508, 272);
            this.gvProperties.TabIndex = 0;
            this.gvProperties.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProperties_CellClick);
            // 
            // Column0
            // 
            this.Column0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column0.FillWeight = 2.604999F;
            this.Column0.HeaderText = "";
            this.Column0.Name = "Column0";
            this.Column0.ReadOnly = true;
            this.Column0.Width = 5;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 124.9152F;
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.FillWeight = 59.37977F;
            this.Column3.HeaderText = "Size";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 52;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.FillWeight = 190.3553F;
            this.Column2.HeaderText = "Type";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 56;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column4.FillWeight = 110F;
            this.Column4.HeaderText = "Modified";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 72;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btUp,
            this.toolStripSeparator4,
            this.btNew,
            this.btDelete,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.toolStripSeparator6,
            this.btDownload,
            this.btUpload,
            this.toolStripSeparator3,
            this.btExport});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(508, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btUp
            // 
            this.btUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btUp.Image = ((System.Drawing.Image)(resources.GetObject("btUp.Image")));
            this.btUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(23, 22);
            this.btUp.Text = "Up";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btNew
            // 
            this.btNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btNew.Image = ((System.Drawing.Image)(resources.GetObject("btNew.Image")));
            this.btNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(23, 22);
            this.btNew.Text = "New";
            // 
            // btDelete
            // 
            this.btDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(23, 22);
            this.btDelete.Text = "Delete";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Enabled = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel1.Text = "                            ";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btDownload
            // 
            this.btDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDownload.Image = ((System.Drawing.Image)(resources.GetObject("btDownload.Image")));
            this.btDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDownload.Name = "btDownload";
            this.btDownload.Size = new System.Drawing.Size(23, 22);
            this.btDownload.Text = "toolStripButton1";
            this.btDownload.ToolTipText = "Download";
            this.btDownload.Click += new System.EventHandler(this.btDownload_Click);
            // 
            // btUpload
            // 
            this.btUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btUpload.Image = ((System.Drawing.Image)(resources.GetObject("btUpload.Image")));
            this.btUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(23, 22);
            this.btUpload.Text = "toolStripButton2";
            this.btUpload.ToolTipText = "Upload";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btExport
            // 
            this.btExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExport.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(23, 22);
            this.btExport.Text = "toolStripButton1";
            this.btExport.ToolTipText = "Export";
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(851, 86);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tbLastModified, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbSize, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbURL, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbSize, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbType, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(845, 67);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tbLastModified
            // 
            this.tbLastModified.BackColor = System.Drawing.SystemColors.Control;
            this.tbLastModified.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLastModified.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLastModified.Location = new System.Drawing.Point(91, 51);
            this.tbLastModified.Name = "tbLastModified";
            this.tbLastModified.Size = new System.Drawing.Size(751, 13);
            this.tbLastModified.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Url:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSize
            // 
            this.tbSize.BackColor = System.Drawing.SystemColors.Control;
            this.tbSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSize.Location = new System.Drawing.Point(91, 19);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(751, 13);
            this.tbSize.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Size:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 19);
            this.label7.TabIndex = 2;
            this.label7.Text = "Modified:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbURL
            // 
            this.tbURL.BackColor = System.Drawing.SystemColors.Control;
            this.tbURL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbURL.Location = new System.Drawing.Point(91, 3);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(751, 13);
            this.tbURL.TabIndex = 3;
            // 
            // lbSize
            // 
            this.lbSize.AutoSize = true;
            this.lbSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSize.Location = new System.Drawing.Point(3, 32);
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(82, 16);
            this.lbSize.TabIndex = 6;
            this.lbSize.Text = "Type:";
            this.lbSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbType
            // 
            this.tbType.BackColor = System.Drawing.SystemColors.Control;
            this.tbType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbType.Location = new System.Drawing.Point(91, 35);
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(751, 13);
            this.tbType.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 515);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.56029F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.43972F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(857, 113);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 628);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Azure Storage Browser";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProperties)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAccountName;
        private System.Windows.Forms.TextBox tbAccountKey;
        private System.Windows.Forms.TextBox tbConnectionString;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.ComboBox cbDefaultEnpointsProtocol;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem dumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView myTree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btExpandAll;
        private System.Windows.Forms.ToolStripButton btCollapseAll;
        private System.Windows.Forms.DataGridView gvProperties;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox tbLastModified;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.Label lbSize;
        private System.Windows.Forms.TextBox tbSize;
        private System.Windows.Forms.ToolStripProgressBar pbDownload;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btUp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btDownload;
        private System.Windows.Forms.ToolStripButton btUpload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.ToolStripButton btNew;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.DataGridViewImageColumn Column0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}

