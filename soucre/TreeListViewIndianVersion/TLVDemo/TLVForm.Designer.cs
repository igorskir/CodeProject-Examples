namespace TLVDemo
{
   partial class TLVForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TLVForm));
            this.lvContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.propertiesTSB = new System.Windows.Forms.ToolStripSplitButton();
            this.ownerDrawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagesTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTypeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewTypeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsViewTypeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imageSizeTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.browseTSB = new System.Windows.Forms.ToolStripSplitButton();
            this.browseDirectoryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.browseXmlTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.commandTSB = new System.Windows.Forms.ToolStripSplitButton();
            this.cmd0TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.cmd1TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizeColorBtn = new System.Windows.Forms.ToolStripButton();
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.listView = new TreeListView();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvContextMenu
            // 
            this.lvContextMenu.Name = "lvContextMenu";
            this.lvContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Mail.gif");
            this.imageList.Images.SetKeyName(1, "Tick.gif");
            this.imageList.Images.SetKeyName(2, "Folder.gif");
            this.imageList.Images.SetKeyName(3, "Default.jpg");
            this.imageList.Images.SetKeyName(4, "LeafNode.gif");
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesTSB,
            this.toolStripSeparator3,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.dSelectAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.invertSelectionToolStripMenuItem,
            this.toolStripSeparator4,
            this.imageSizeTSCB,
            this.browseTSB,
            this.commandTSB,
            this.randomizeColorBtn});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1149, 38);
            this.toolStrip.TabIndex = 8;
            this.toolStrip.Text = "Tool Strip";
            // 
            // propertiesTSB
            // 
            this.propertiesTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.propertiesTSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ownerDrawToolStripMenuItem,
            this.checkBoxesToolStripMenuItem,
            this.imagesTSMI,
            this.viewTypeTSMI});
            this.propertiesTSB.Image = ((System.Drawing.Image)(resources.GetObject("propertiesTSB.Image")));
            this.propertiesTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.propertiesTSB.Name = "propertiesTSB";
            this.propertiesTSB.Size = new System.Drawing.Size(138, 35);
            this.propertiesTSB.Text = "Properties";
            // 
            // ownerDrawToolStripMenuItem
            // 
            this.ownerDrawToolStripMenuItem.CheckOnClick = true;
            this.ownerDrawToolStripMenuItem.Name = "ownerDrawToolStripMenuItem";
            this.ownerDrawToolStripMenuItem.Size = new System.Drawing.Size(221, 36);
            this.ownerDrawToolStripMenuItem.Text = "&Owner Draw";
            this.ownerDrawToolStripMenuItem.Click += new System.EventHandler(this.ownerDrawToolStripMenuItem_Click);
            // 
            // checkBoxesToolStripMenuItem
            // 
            this.checkBoxesToolStripMenuItem.CheckOnClick = true;
            this.checkBoxesToolStripMenuItem.Name = "checkBoxesToolStripMenuItem";
            this.checkBoxesToolStripMenuItem.Size = new System.Drawing.Size(221, 36);
            this.checkBoxesToolStripMenuItem.Text = "&CheckBoxes";
            this.checkBoxesToolStripMenuItem.Click += new System.EventHandler(this.checkBoxesToolStripMenuItem_Click);
            // 
            // imagesTSMI
            // 
            this.imagesTSMI.CheckOnClick = true;
            this.imagesTSMI.Name = "imagesTSMI";
            this.imagesTSMI.Size = new System.Drawing.Size(221, 36);
            this.imagesTSMI.Text = "&Images";
            this.imagesTSMI.Click += new System.EventHandler(this.imagesTSMI_Click);
            // 
            // viewTypeTSMI
            // 
            this.viewTypeTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listViewTypeTSMI,
            this.detailsViewTypeTSMI});
            this.viewTypeTSMI.Name = "viewTypeTSMI";
            this.viewTypeTSMI.Size = new System.Drawing.Size(221, 36);
            this.viewTypeTSMI.Text = "&View Type";
            // 
            // listViewTypeTSMI
            // 
            this.listViewTypeTSMI.Name = "listViewTypeTSMI";
            this.listViewTypeTSMI.Size = new System.Drawing.Size(162, 36);
            this.listViewTypeTSMI.Text = "&List";
            // 
            // detailsViewTypeTSMI
            // 
            this.detailsViewTypeTSMI.Name = "detailsViewTypeTSMI";
            this.detailsViewTypeTSMI.Size = new System.Drawing.Size(162, 36);
            this.detailsViewTypeTSMI.Text = "&Details";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.selectAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolStripMenuItem.Image")));
            this.selectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(117, 35);
            this.selectAllToolStripMenuItem.Text = "&Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // dSelectAllToolStripMenuItem
            // 
            this.dSelectAllToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dSelectAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dSelectAllToolStripMenuItem.Image")));
            this.dSelectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dSelectAllToolStripMenuItem.Name = "dSelectAllToolStripMenuItem";
            this.dSelectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 35);
            this.dSelectAllToolStripMenuItem.Text = "&Deselect All";
            this.dSelectAllToolStripMenuItem.Click += new System.EventHandler(this.dSelectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // invertSelectionToolStripMenuItem
            // 
            this.invertSelectionToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.invertSelectionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("invertSelectionToolStripMenuItem.Image")));
            this.invertSelectionToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
            this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(185, 35);
            this.invertSelectionToolStripMenuItem.Text = "&Invert Selection";
            this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 38);
            // 
            // imageSizeTSCB
            // 
            this.imageSizeTSCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageSizeTSCB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.imageSizeTSCB.Items.AddRange(new object[] {
            "16 x 16",
            "32 x 32",
            "48 x 48",
            "64 x 64"});
            this.imageSizeTSCB.Name = "imageSizeTSCB";
            this.imageSizeTSCB.Size = new System.Drawing.Size(121, 38);
            // 
            // browseTSB
            // 
            this.browseTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.browseTSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseDirectoryTSMI,
            this.browseXmlTSMI});
            this.browseTSB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseTSB.Image = ((System.Drawing.Image)(resources.GetObject("browseTSB.Image")));
            this.browseTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browseTSB.Name = "browseTSB";
            this.browseTSB.Size = new System.Drawing.Size(111, 35);
            this.browseTSB.Text = "Browse";
            this.browseTSB.ButtonClick += new System.EventHandler(this.browseTSB_ButtonClick);
            // 
            // browseDirectoryTSMI
            // 
            this.browseDirectoryTSMI.Name = "browseDirectoryTSMI";
            this.browseDirectoryTSMI.Size = new System.Drawing.Size(278, 32);
            this.browseDirectoryTSMI.Text = "Browse &Directory";
            this.browseDirectoryTSMI.Click += new System.EventHandler(this.browseDirectoryTSMI_Click);
            // 
            // browseXmlTSMI
            // 
            this.browseXmlTSMI.Name = "browseXmlTSMI";
            this.browseXmlTSMI.Size = new System.Drawing.Size(278, 32);
            this.browseXmlTSMI.Text = "Browse &XML";
            this.browseXmlTSMI.Click += new System.EventHandler(this.browseXmlTSMI_Click);
            // 
            // commandTSB
            // 
            this.commandTSB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.commandTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.commandTSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmd0TSMI,
            this.cmd1TSMI});
            this.commandTSB.Image = ((System.Drawing.Image)(resources.GetObject("commandTSB.Image")));
            this.commandTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.commandTSB.Name = "commandTSB";
            this.commandTSB.Size = new System.Drawing.Size(142, 35);
            this.commandTSB.Text = "Command";
            this.commandTSB.Visible = false;
            this.commandTSB.ButtonClick += new System.EventHandler(this.commandTSB_ButtonClick);
            // 
            // cmd0TSMI
            // 
            this.cmd0TSMI.Name = "cmd0TSMI";
            this.cmd0TSMI.Size = new System.Drawing.Size(244, 36);
            this.cmd0TSMI.Text = "0";
            this.cmd0TSMI.Click += new System.EventHandler(this.cmd0TSMI_Click);
            // 
            // cmd1TSMI
            // 
            this.cmd1TSMI.Name = "cmd1TSMI";
            this.cmd1TSMI.Size = new System.Drawing.Size(244, 36);
            this.cmd1TSMI.Text = "1";
            this.cmd1TSMI.Click += new System.EventHandler(this.cmd1TSMI_Click);
            // 
            // randomizeColorBtn
            // 
            this.randomizeColorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.randomizeColorBtn.Image = ((System.Drawing.Image)(resources.GetObject("randomizeColorBtn.Image")));
            this.randomizeColorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.randomizeColorBtn.Name = "randomizeColorBtn";
            this.randomizeColorBtn.Size = new System.Drawing.Size(202, 36);
            this.randomizeColorBtn.Text = "Randomize Color";
            this.randomizeColorBtn.Click += new System.EventHandler(this.randomizeColorBtn_Click);
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._propertyGrid.Location = new System.Drawing.Point(850, 41);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.SelectedObject = this.listView;
            this._propertyGrid.Size = new System.Drawing.Size(287, 785);
            this._propertyGrid.TabIndex = 14;
            this._propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this._propertyGrid_PropertyValueChanged);
            // 
            // listView
            // 
            this.listView.AllowColumnReorder = true;
            this.listView.AllowDrop = true;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(12, 41);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(832, 785);
            this.listView.TabIndex = 13;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // TLVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 838);
            this.Controls.Add(this._propertyGrid);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Name = "TLVForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tree List View Form !!!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TLVForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ContextMenuStrip lvContextMenu;
      private System.Windows.Forms.ImageList imageList;
      private System.Windows.Forms.ToolStrip toolStrip;
      private System.Windows.Forms.ToolStripSplitButton propertiesTSB;
      private System.Windows.Forms.ToolStripMenuItem ownerDrawToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem checkBoxesToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.ToolStripButton selectAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripButton dSelectAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripButton invertSelectionToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem viewTypeTSMI;
      private System.Windows.Forms.ToolStripMenuItem listViewTypeTSMI;
      private System.Windows.Forms.ToolStripMenuItem detailsViewTypeTSMI;
      private System.Windows.Forms.ToolStripMenuItem imagesTSMI;
      private System.Windows.Forms.ToolStripComboBox imageSizeTSCB;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
      private System.Windows.Forms.ToolStripSplitButton browseTSB;
      private System.Windows.Forms.ToolStripMenuItem browseXmlTSMI;
      private System.Windows.Forms.ToolStripMenuItem browseDirectoryTSMI;
      private System.Windows.Forms.PropertyGrid _propertyGrid;
      private TreeListView listView;
      private System.Windows.Forms.ToolStripSplitButton commandTSB;
      private System.Windows.Forms.ToolStripMenuItem cmd0TSMI;
      private System.Windows.Forms.ToolStripMenuItem cmd1TSMI;
      private System.Windows.Forms.ToolStripButton randomizeColorBtn;
   }
}

