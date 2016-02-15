using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text;

namespace TLVDemo
{
   public partial class TLVForm : Form
   {
      private const string InputFile = @"March09.xml";

      private readonly Dictionary<string, Size> _imgSizeMap = new Dictionary<string, Size>();
      
      private Form cpgForm = null;

      #region Ctor(s)

      public TLVForm()
      {
         InitializeComponent();

         InitImageSizeToolStripCombo();
         InitializeImageList(Size.Empty);

         this.listView.OwnerDraw = this.listView.CheckBoxes = true;
         // this.listView.SmallImageList = this.imageList;

         this.ownerDrawToolStripMenuItem.Checked = this.listView.OwnerDraw;
         this.checkBoxesToolStripMenuItem.Checked = this.listView.CheckBoxes;
         this.imagesTSMI.Checked = (this.listView.SmallImageList != null);
      }

      #endregion

      #region Control Event Handlers
            
      private void TLVForm_Load(object sender, EventArgs e)
      {
         XPSHelper.LoadExpenses(this.listView, TLVForm.InputFile);
      }

      private void ownerDrawToolStripMenuItem_Click(object sender, EventArgs e)
      {
         listView.OwnerDraw = !listView.OwnerDraw;
      }

      private void checkBoxesToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.listView.CheckBoxes = !this.listView.CheckBoxes;
      }

      private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (this.listView.CheckBoxes)
         {
            foreach (ListViewItem lvi in this.listView.Items)
            {
               lvi.Checked = true;
            }
         }
      }

      private void dSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (this.listView.CheckBoxes)
         {
            foreach (ListViewItem lvi in this.listView.Items)
            {
               lvi.Checked = false;
            }
         }
      }

      private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (this.listView.CheckBoxes)
         {
            foreach (ListViewItem lvi in this.listView.Items)
            {
               lvi.Checked = !lvi.Checked;
            }
         }
      }

      private void imagesTSMI_Click(object sender, EventArgs e)
      {
         listView.SmallImageList = (listView.SmallImageList == null ? imageList : null);
      }

      private void browseTSB_ButtonClick(object sender, EventArgs e)
      {
         try
         {
            BrowseDirAndPopulateTreeListView();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void browseDirectoryTSMI_Click(object sender, EventArgs e)
      {
         try
         {
            BrowseDirAndPopulateTreeListView();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void browseXmlTSMI_Click(object sender, EventArgs e)
      {
         try
         {
            MessageBox.Show("Not Implemented!");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }      

      private void commandTSB_ButtonClick(object sender, EventArgs e)
      {         
      }

      private void cmd0TSMI_Click(object sender, EventArgs e)
      {
         try
         {
            LogColumnWidths(0);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void cmd1TSMI_Click(object sender, EventArgs e)
      {
         try
         {
            LogColumnWidths(1);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void _propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
      {
         try
         {
            listView.Invalidate();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void randomizeColorBtn_Click(object sender, EventArgs e)
      {
         try
         {
            foreach (ListViewItem lvi in listView.Items)
            {
               lvi.ForeColor = RandomColor.Next();
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void propsTSB_Click(object sender, EventArgs e)
      {
         //if (cpgForm == null)
         //{
         //   CtrlPropertyGrid cpGrid = new CtrlPropertyGrid(this.listView);
         //   cpGrid.Name = "cpGrid";
         //   cpGrid.Dock = DockStyle.Fill;

         //   cpgForm = new Form {Text = "Control Property Grid"};
         //   cpgForm.Controls.Add(cpGrid);
         //   cpgForm.Size = new Size((int)this.Width / 4, this.Height);
         //   cpgForm.Location = Point.Empty;
         //}

         //cpgForm.Show();
      }

      private void imageSizeTSCB_SelectedIndexChanged(object sender, EventArgs e)
      {
         try
         {
            Size sz = _imgSizeMap.ContainsKey(this.imageSizeTSCB.Text) ? _imgSizeMap[this.imageSizeTSCB.Text] : new Size(16, 16);
            InitializeImageList(sz);
            this.imagesTSMI.Checked = (this.imageSizeTSCB.Text == string.Empty ? false : true);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void dirTB_KeyUp(object sender, KeyEventArgs e)
      {
         //if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(dirTB.Text))
         //{
         //   try
         //   {
         //      if (Path.HasExtension(dirTB.Text) && Path.GetExtension(dirTB.Text) == ".xml" && File.Exists(dirTB.Text))
         //      {
         //         LoadXml(dirTB.Text);
         //      }

         //      if (Directory.Exists(dirTB.Text))
         //      {
         //         InitializeListView(dirTB.Text);
         //      }
         //   }
         //   catch (Exception ex)
         //   {
         //      MessageBox.Show(ex.Message);
         //   }
         //}
      }

      private void browseBtn_Click(object sender, EventArgs e)
      {
         var fbDialog = new FolderBrowserDialog
         {
            Description = "Select a folder to populate the 'Simplest Tree View Grid'",
            ShowNewFolderButton = false
         };

         try
         {
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
               InitializeListView(fbDialog.SelectedPath);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void TLVForm_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.D)
         {
            //this.dirTB.Focus();
            //this.dirTB.SelectAll();
         }
      }

      private void listViewTypeTSMI_Click(object sender, EventArgs e)
      {
         this.listView.View = View.List;
      }

      private void detailsViewTypeTSMI_Click(object sender, EventArgs e)
      {
         this.listView.View = View.Details;
      }

      #endregion

      #region Private Helper Methods

      enum CheckState
      {
         Check,
         Uncheck,
         Invert
      }

      private void InitializeListView(string dirPath)
      {
         listView.Clear();
         listView.Columns.AddRange(new[] {

            new ColumnHeader { Text = "Name", Width = 250, TextAlign = HorizontalAlignment.Left },
            new ColumnHeader { Text = "Type", Width = 100, TextAlign = HorizontalAlignment.Left },
            new ColumnHeader { Text = "Length", Width = 200, TextAlign = HorizontalAlignment.Left },
            new ColumnHeader { Text = "Date Created", Width = 300, TextAlign = HorizontalAlignment.Left }
         });

         if (false)
         {
            ListViewItem2 rootItem = new ListViewItem2(new string[] { Path.GetFileName(dirPath) }, null);
            this.listView.Items.Add(rootItem);

            ListViewItem2[] fseItems = GenerateFileSystemItems(dirPath, rootItem);
            rootItem.AddChildren(fseItems);
         }
         else
         {
            var fseItems = GenerateFileSystemItems(dirPath, null);
            foreach (var fseLvi in fseItems)
            {
               listView.Items.Add(fseLvi);
            }
         }
      }

      private static ListViewItem2[] GenerateFileSystemItems(string dirPath, ListViewItem2 lviParent)
      {
         var fseItems = new List<ListViewItem2>();

         var subDirs = Directory.GetDirectories(dirPath);
         foreach (var d in subDirs)
         {
            var di = new DirectoryInfo(d);
            var subItems = new[] { di.Name, di.Extension, di.CreationTime.ToLongDateString(), di.LastAccessTime.ToLongDateString() };

            var lvi = new ListViewItem2(subItems, lviParent) { ForeColor = RandomColor.Next(), ImageIndex = 0 };

            var lviFSItems = GenerateFileSystemItems(d, lvi);
            if (lviFSItems != null && lviFSItems.Length > 0)
            {
               lvi.AddChildren(lviFSItems);
               // TreeListView.AssignParent(lvi, lviParent);
            }

            fseItems.Add(lvi);
         }

         var lviFiles = GetFileItems(dirPath, lviParent);
         fseItems.AddRange(lviFiles);
         return fseItems.ToArray();
      }

      private static ListViewItem2[] GetFileItems(string dirPath, ListViewItem2 lviParent)
      {
         var fseItems = new List<ListViewItem2>();

         string[] files = Directory.GetFiles(dirPath);
         foreach (string f in files)
         {
            var fi = new FileInfo(f);
            var subItems = new[] { fi.Name, fi.Extension, fi.Length.ToString(), fi.CreationTime.ToLongDateString() };

            var lvi = new ListViewItem2(subItems, lviParent) { ForeColor = RandomColor.Next(), ImageIndex = 1 };
            // TreeListView.AssignParent(lvi, lviParent);

            fseItems.Add(lvi);
         }

         return fseItems.ToArray();
      }

      private void InitImageSizeToolStripCombo()
      {
         List<object> sList = new List<object>();
         for (int i = 16; i < 128; i += 16)
         {
            string displayText = string.Format("{0}x{0}", i);
            sList.Add(displayText);
            _imgSizeMap.Add(displayText, new Size(i, i));
         }

         this.imageSizeTSCB.Items.Clear();
         this.imageSizeTSCB.Items.AddRange(sList.ToArray());
      }

      private void InitializeImageList(Size imageSize)
      {
         if (!imageSize.IsEmpty)
         {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TLVForm));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.ImageSize = imageSize;
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.Images.SetKeyName(0, "Mail.gif");
            this.imageList.Images.SetKeyName(1, "Tick.gif");
            this.imageList.Images.SetKeyName(2, "Folder.gif");
            this.imageList.Images.SetKeyName(3, "Default.jpg");
            this.imageList.Images.SetKeyName(4, "LeafNode.gif");
         }
      }      

      private void BrowseDirAndPopulateTreeListView()
      {
         var fbDialog = new FolderBrowserDialog {
            Description = "Select a folder to populate the 'Simplest Tree View Grid'",
            ShowNewFolderButton = false,
            SelectedPath = @"C:\Program Files"
            // RootFolder = Environment.SpecialFolder.Personal
         };

         if (fbDialog.ShowDialog() == DialogResult.OK)
         {
            InitializeListView(fbDialog.SelectedPath);
         }
      }

      private void LogColumnWidths(int colIndex)
      {
         var sb = new StringBuilder();

         foreach (ListViewItem lvi in this.listView.Items)
         {
            if (lvi.SubItems.Count == 0 || lvi.Text.Length == 0)
            {
               continue;
            }

            sb.Length = 0;            

            for (int siIndex = 0; siIndex < lvi.SubItems.Count; ++siIndex)
            {
               var lvsi = lvi.SubItems[siIndex];

               if (colIndex == -1 || colIndex == siIndex)
               {
                  sb.AppendFormat("{0}...{1}  ", lvsi.Text, lvsi.Bounds.Width);
               }
            }

            Trace.WriteLine(sb.ToString());
         }
      }

      private static void UpdateCheckState(ListViewItem[] lvItems, CheckState chkState)
      {
         Array.ForEach(lvItems, lvi =>
         {
            switch (chkState)
            {
               case CheckState.Check:
                  lvi.Checked = true;
                  break;

               case CheckState.Uncheck:
                  lvi.Checked = false;
                  break;

               case CheckState.Invert:
                  lvi.Checked = !lvi.Checked;
                  break;
            }
         });
      }

      private void LoadXml(string xmlFile)
      {
         QKen.InitializeListView(this.listView);
         XDocument xd = XDocument.Load(xmlFile);
         XElement[] xElements = QKen.ToArray(xd.Elements().Elements());

         // ListViewItem2[] lvItems = QKen.CreateTree(xElements, null, true);
         ListViewItem2[] lvItems = QKen.CreateTreeFromXML(xElements, null);

         Array.ForEach<ListViewItem2>(lvItems, lvi2 => this.listView.AddItem(lvi2));
      }

      private static ColumnHeader CreateColumnHeader(string displayText, int width, HorizontalAlignment txtAlign)
      {
         return new ColumnHeader
         {
            Text = displayText ?? string.Empty,
            Width = width,
            TextAlign = txtAlign
         };
      }

      private void SetImageSize(ToolStripMenuItem tsmi, Size sz)
      {
         Debug.Assert(tsmi != null);

         if (!tsmi.Checked)
         {
            this.imageList.ImageSize = sz == Size.Empty ? new Size(16, 16) : sz;
            tsmi.Checked = this.imagesTSMI.Checked = true;
         }
      }
      
      #endregion
   }
}