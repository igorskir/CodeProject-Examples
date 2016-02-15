using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Win32;

public delegate void MethodInvoker();
public delegate void MethodInvoker<T>(T arg);
public delegate void MethodInvoker<T1, T2>(T1 arg1, T2 arg2);

public class TreeListView : ListView
{   
   #region Member Data

   private const string MsgBoxCaption = "Tree List View";
   private const string PlusImageName = "TreeListView.Collapsed.gif";
   private const string MinusImageName = "TreeListView.Expanded.gif";

   private static readonly Image PlusImage = null;
   private static readonly Image MinusImage = null;
   private const int CheckBoxWidth = 15;
   private const int GeneralGapWidth = 3;

   private Point _clickLocation = Point.Empty;

   //private const string ImagePath = @"C:\WINDOWS\Web\exclam.gif";
   //private static readonly Image SampleImage = Image.FromFile(TreeListView.ImagePath);

   #endregion

   #region Type Initializer

   static TreeListView()
   {
      Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(TreeListView.PlusImageName);
      TreeListView.PlusImage = new Bitmap(imageStream);

      imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(TreeListView.MinusImageName);
      TreeListView.MinusImage = new Bitmap(imageStream);
   }

   #endregion

   #region Ctor(s)

   public TreeListView()
      : base()
   {
      View = View.Details;
      DoubleBuffered = true;
      AllowDrop = true;

      DrawItem += new DrawListViewItemEventHandler(OnDrawItem);
      DrawSubItem += new DrawListViewSubItemEventHandler(OnDrawSubItem);
      DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(OnDrawColumnHeader);
      MouseMove += new MouseEventHandler(OnMouseMove);
      ColumnWidthChanging += new ColumnWidthChangingEventHandler(OnColumnWidthChanging);
      //KeyUp += new KeyEventHandler(OnKeyUp);
      //MouseClick += new MouseEventHandler(OnMouseClick);
      //MouseDoubleClick += new MouseEventHandler(OnMouseDoubleClick);      
      //MouseUp += new MouseEventHandler(OnMouseUp);
   }

   void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
   {
      int biggestWidth = 0;
      for (int i = 0; i < Items.Count; ++i)
      {
         ListViewItem lvi = Items[i];
         if (lvi.SubItems.Count < e.ColumnIndex || lvi.Text == string.Empty)
         {
            continue;
         }

         ListViewItem.ListViewSubItem lvsi = lvi.SubItems[e.ColumnIndex];
         SizeF textSize = CreateGraphics().MeasureString(lvsi.Text, lvsi.Font);

         Trace.WriteLine(string.Format("{0}: {1}", lvsi.Text, textSize.Width));

         if (textSize.Width > biggestWidth)
         {
            biggestWidth = (int)textSize.Width;
         }
      }

      e.NewWidth = biggestWidth + 50;
      e.Cancel = false;

      Trace.WriteLine("Biggest Width: " + biggestWidth.ToString());
   }

   #endregion

   #region Control Event Handlers

   private void OnDrawItem(object sender, DrawListViewItemEventArgs e)
   {
      if (e.Item.Selected)
      {
         e.Graphics.FillRectangle(Brushes.LightBlue, e.Item.Bounds);
         e.DrawFocusRectangle();
      }
   }

   private void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
   {
      SuspendLayout();
      
      var lvItem = e.Item as ListViewItem2;
      if (lvItem == null || lvItem.IsEmpty)
      {
         return;
      }

      var txtMetrics = Helpers.GetTextMetrics(e.Graphics);      
      int yFactor = /*this.SmallImageList == null ? 1 : */(e.Bounds.Height - txtMetrics.tmHeight) / 2;

      bool hasChildren = lvItem.HasChildren;
      int xBound = e.Bounds.X + 5;

      if (e.SubItem == e.Item.SubItems[0])
      {
         int iLevel = lvItem.GetIndentLevel();
         bool hasParent = lvItem.Parent == null ? false : true;

         xBound += hasParent ? iLevel * 14 : 0;

         if (hasChildren)
         {
            var imageLocation = new Point(xBound, e.Bounds.Y + yFactor + 1);
            lvItem.PlusMinusLocation = imageLocation;
            e.Graphics.DrawImage(lvItem.Expanded ? TreeListView.MinusImage : TreeListView.PlusImage, imageLocation);
            xBound += (TreeListView.PlusImage.Width + TreeListView.GeneralGapWidth);

            //Point imageLocation = new Point(xBound, e.Bounds.Y + 1);
            //lvItem.PlusMinusLocation = imageLocation;
            //Size iSize = CalculateCheckBoxSize(e.SubItem);
            //Image img = lvItem.Expanded ? TreeListView.MinusImage : TreeListView.PlusImage;
            //e.Graphics.DrawImage(img, new Rectangle(imageLocation, iSize));
            //xBound += (iSize.Width + TreeListView.GeneralGapWidth);
         }

         if (this.CheckBoxes)
         {
            // Point checkBoxLocation = new Point(xBound, e.Bounds.Y - 2 + (int)e.Bounds.Height / 4);
            //
            //CheckBoxRenderer.DrawCheckBox(e.Graphics,
            //   checkBoxLocation,
            //   Rectangle.Empty,
            //   string.Empty,
            //   this.Font,
            //   TextFormatFlags.Left,
            //   false,
            //   lvItem.Checked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedHot);
            //
            //lvItem.CheckBoxLocation = checkBoxLocation;
            //
            // xBound += TreeListView.CheckBoxWidth + TreeListView.GeneralGapWidth;

            Size cbSize = CalculateCheckBoxSize(e.SubItem);
            Rectangle cbBounds = new Rectangle(new Point(xBound, e.Bounds.Y), cbSize);

            //for (int m = 0; m < 1; ++m)
            //{
            //   Rectangle r = cbBounds;
            //   r.Offset(m, m);
            //   r.Width -= m * 2;
            //   r.Height -= m * 2;

            //   ControlPaint.DrawCheckBox(e.Graphics,
            //      r,
            //      (lvItem.Checked ? ButtonState.Checked : ButtonState.Normal) | ButtonState.Flat);
            //}

            ControlPaint.DrawCheckBox(e.Graphics,
               cbBounds,
               (lvItem.Checked ? ButtonState.Checked : ButtonState.Normal) | ButtonState.Flat);

            lvItem.CheckBoxBounds = cbBounds;
            xBound += cbBounds.Width + TreeListView.GeneralGapWidth;
         }

         if (this.SmallImageList != null && e.Item.ImageIndex >= 0 && e.Item.ImageIndex < this.SmallImageList.Images.Count)
         {
            Image img = e.Item.ImageList.Images[e.Item.ImageIndex];
            int imageWidth = img.Width; // > 16 ? 16 : img.Width;
            int imageHeight = img.Height - 2; // img.Height > e.Bounds.Height - 5 ? e.Bounds.Height - 5 : img.Height;

            e.Graphics.DrawImage(img, new Rectangle(xBound, e.Bounds.Y + 1, imageWidth, imageHeight));
            xBound += imageWidth + TreeListView.GeneralGapWidth;
         }
      }
      
      PointF drawPoint = new PointF(xBound, e.Bounds.Y + yFactor);
      SizeF drawBound = new SizeF(e.Bounds.X + e.Bounds.Width - xBound, e.Bounds.Height);
      RectangleF drawRect = new RectangleF(drawPoint, drawBound);

      StringFormat txtFormat = new StringFormat();
      txtFormat.Trimming = StringTrimming.EllipsisCharacter;
      txtFormat.LineAlignment = ToStringAlignment(e.Header.TextAlign);

      e.Graphics.DrawString(e.SubItem.Text,
         e.Item.Font,
         new SolidBrush(e.Item.ForeColor),
         // xBound, e.SubItem.Bounds.Y
         drawRect,
         txtFormat);

      // e.Item.ListView.AutoResizeColumn(e.ColumnIndex, ColumnHeaderAutoResizeStyle.ColumnContent);

      ResumeLayout(true);
   }

   private void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
   {
      using (StringFormat sf = new StringFormat())
      {
         // Store the column text alignment, letting it default
         // to Left if it has not been set to Center or Right.
         switch (e.Header.TextAlign)
         {
            case HorizontalAlignment.Center:
               sf.Alignment = StringAlignment.Center;
               break;
            case HorizontalAlignment.Right:
               sf.Alignment = StringAlignment.Far;
               break;
         }

         e.DrawBackground();

         // Draw the header text.
         using (Font headerFont = new Font("VERDANA", 10, FontStyle.Bold))
         {
            e.Graphics.DrawString(e.Header.Text,
               headerFont,
               Brushes.Black,
               e.Bounds,
               sf);
         }
      }
   }

   private void OnMouseClick(object sender, MouseEventArgs e)
   {
      if (!CheckBoxes)
      {
         return;
      }

      ListViewItem2 lvi = GetItemAt(e.X, e.Y) as ListViewItem2;
      if (lvi == null)
      {
         return;
      }

      Point cbLoc = this.PointToScreen(lvi.CheckBoxBounds.Location).GetShift(1, 1);
      Rectangle cbRect = new Rectangle(cbLoc, new Size(10, 10));      
      if (e.Location.IsInRect(cbRect))
      {
         lvi.Checked = !lvi.Checked;
      }
   }

   private void OnMouseDoubleClick(object sender, MouseEventArgs e)
   {
      try
      {
         ListViewItem2 lvi = GetItemAt(e.X, e.Y) as ListViewItem2;
         if (lvi != null && lvi.HasChildren)
         {
            ToggleExpandState(lvi);
         }
      }
      catch (Exception ex)
      {
         MessageBox.Show(ex.Message, TreeListView.MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
   }

   private void OnKeyUp(object sender, KeyEventArgs e)
   {
      //if (e.Modifiers != Keys.Control || e.KeyCode != Keys.Left || e.KeyCode != Keys.Right)
      //{
      //   return;
      //}
      if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
      {
         try
         {
            ListViewItem2 lvi = (SelectedItems.Count == 1 ? SelectedItems[0] : null) as ListViewItem2;
            if (lvi == null)
            {
               return;
            }

            switch (e.KeyCode)
            {
               case Keys.Left:
                  Collapse(lvi);
                  break;

               case Keys.Right:
                  Expand(lvi);
                  break;
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, TreeListView.MsgBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }
   }

   /// <summary>
   /// Selects and focuses an item when it is clicked anywhere along 
   /// its width. The click must normally be on the parent item text.
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   private void OnMouseUp(object sender, MouseEventArgs e)
   {
      //ListViewItem clickedItem = GetItemAt(e.X, e.Y);
      //if (clickedItem != null)
      //{
      //   clickedItem.Selected = clickedItem.Focused = true;
      //}
   }

   /// <summary>
   /// Forces each row to repaint itself the first time the mouse moves over 
   /// it, compensating for an extra DrawItem event sent by the wrapped 
   /// Win32 control.
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   private void OnMouseMove(object sender, MouseEventArgs e)
   {
      //ListViewItem item = listView.GetItemAt(e.X, e.Y);
      //if (item != null)
      //{
      //   listView.Invalidate(item.Bounds);
      //}
   }

   protected override void WndProc(ref Message msg)
   {
      bool msgHandled = false;

      switch (msg.Msg)
      {
         case Messages.WM_LBUTTONDBLCLK:
            Point clickLoc = Helpers.ToPoint(msg.LParam.ToInt32());
            msgHandled = LButtonDblClickProc(clickLoc);
            break;

         case Messages.WM_LBUTTONDOWN:
            clickLoc = Helpers.ToPoint(msg.LParam.ToInt32());
            msgHandled = LButtonDownProc(clickLoc);
            break;

         case Messages.WM_LBUTTONUP:
            clickLoc = Helpers.ToPoint(msg.LParam.ToInt32());
            msgHandled = LButtonUpProc(clickLoc);
            break;

         case Messages.WM_KEYDOWN:
            msgHandled = KeyDownProc(ref msg);
            break;
      }

      if (!msgHandled)
      {
         base.WndProc(ref msg);
      }
   }
 
   #endregion

   #region Public (Instance\Static) Methods and Properties

   public ListViewItem2 AddItem(ListViewItem2 lviItem)
   {
      Throw.IfNull(lviItem, "Specified item is null.");
      Throw.IfNotNull(lviItem.Parent, "Parent has to be a root level item.");
      Throw.IfTrue(lviItem.Parent == lviItem, "Self reference. Parent is same as self.");

      if (lviItem.Parent == null)
      {
         Items.Add(lviItem);
      }

      return lviItem;
   }

   //public static void AssignParent(ListViewItem lvi, ListViewItem lviParent)
   //{
   //   if (lvi.Tag == null)
   //   {
   //      lvi.Tag = new LVLink { Parent = lviParent };
   //   }
   //   else
   //   {
   //      Debug.Assert(lvi.Tag is LVLink);
   //      ((LVLink)lvi.Tag).Parent = lviParent;
   //   }
   //}

   //public static void AddChildren(ListViewItem lvi, ListViewItem[] lvChildren)
   //{
   //   if (lvi.Tag == null)
   //   {
   //      lvi.Tag = new LVLink { Children = new List<ListViewItem>(lvChildren) };
   //   }
   //   else
   //   {
   //      Debug.Assert(lvi.Tag is LVLink);
   //      List<ListViewItem> lvis = ((LVLink)lvi.Tag).Children;
   //      if (lvis == null)
   //      {
   //         lvis = new List<ListViewItem>(lvChildren);
   //      }
   //      else
   //      {
   //         lvis.AddRange(lvChildren);
   //      }
   //   }
   //}

   //public bool IsParentOf(ListViewItem lvi, ListViewItem lviParent)
   //{
   //   if (lvi.Tag == null || !(lvi.Tag is LVLink))
   //   {
   //      return false;
   //   }

   //   return lviParent == ((LVLink)lvi.Tag).Parent;
   //}

   public void Expand(ListViewItem2 lvi)
   {
      Throw.IfNull(lvi, "Specified item is null.");
      if (!lvi.HasChildren || lvi.Expanded)
      {
         return;
      }

      foreach (ListViewItem2 lv in lvi.Children)
      {
         Items.Insert(lvi.Index + 1, lv);
      }

      lvi.Expanded = true;

      //Debug.Assert(this.expandedLVList.Contains(lvi) == false);
      //this.expandedLVList.Add(lvi);

      //LVLink lvs = (LVLink)lvi.Tag;
      //foreach (ListViewItem lv in lvs.Children)
      //{
      //   Items.Insert(lvi.Index + 1, lv);
      //}
   }

   public void Collapse(ListViewItem2 lvItem)
   {
      Throw.IfNull(lvItem, "Specified item is null.");
      if (!lvItem.HasChildren || !lvItem.Expanded)
      {
         return;
      }

      foreach (ListViewItem2 lv in lvItem.Children)
      {
         Collapse(lv);
         Items.Remove(lv);
      }

      lvItem.Expanded = false;
   }

   public void ToggleExpandState(ListViewItem2 lvi)
   {
      if (lvi.Expanded)
      {
         Collapse(lvi);
         return;
      }

      Expand(lvi);
   }

   //public bool HasChildNodes(ListViewItem lvi)
   //{
   //   if (lvi.Tag == null || !(lvi.Tag is LVLink))
   //   {
   //      return false;
   //   }

   //   LVLink lvl = (LVLink)lvi.Tag;
   //   return lvl.HasChildren();
   //}

   //public ListViewItem GetParent(ListViewItem lvi)
   //{
   //   if (lvi.Tag == null || !(lvi.Tag is LVLink))
   //   {
   //      return null;
   //   }

   //   LVLink lvl = (LVLink)lvi.Tag;
   //   return lvl.Parent;
   //}

   //public int GetIndentLevel(ListViewItem lvi)
   //{
   //   ListViewItem lviParent = GetParent(lvi);

   //   int index = 0;
   //   while (lviParent != null)
   //   {
   //      ++index;
   //      lviParent = GetParent(lviParent);
   //   }

   //   return index;
   //}

   //public int GetRowIndex(ListViewItem lvi)
   //{
   //   return lvi.Index;
   //}

   #endregion
   
   #region Private Helper Methods

   private bool LButtonDblClickProc(Point clickLoc)
   {
      if (!OwnerDraw)
      {
         return false;
      }

      ListViewItem2 lvi2 = GetItemAt(clickLoc.X, clickLoc.Y) as ListViewItem2;
      if (lvi2 ==  null)
      {
         return false;
      }

      if (lvi2.HasChildren)
      {
         ToggleExpandState(lvi2);
      }

      return true;
   }

   private bool LButtonDownProc(Point clickLoc)
   {
      if (!OwnerDraw)
      {
         return false;
      }

      _clickLocation = clickLoc;

      return true;
      //if (!OwnerDraw || !CheckBoxes)
      //{
      //   return false;
      //}

      //ListViewItem2 lvi2 = GetItemAt(clickLoc.X, clickLoc.Y) as ListViewItem2;
      //if (lvi2 == null)
      //{
      //   return false;
      //}

      //if (lvi2.HasChildren)
      //{
      //   Rectangle pmImageRect = new Rectangle(lvi2.PlusMinusLocation.GetShift(1, 1),
      //      new Size(TreeListView.PlusImage.Width - 1, TreeListView.PlusImage.Height - 1));

      //   if (!clickLoc.IsInRect(pmImageRect))
      //   {
      //      return false;
      //   }

      //   ToggleExpandState(lvi2);
      //   return true;
      //}

      //return false;
   }

   private bool LButtonUpProc(Point clickLoc)
   {
      if (!OwnerDraw || !clickLoc.Equals(this._clickLocation))
      {
         return false;
      }

      // A mouse (left button) click has happened
      ListViewItem2 clickedItem = GetItemAt(clickLoc.X, clickLoc.Y) as ListViewItem2;
      if (clickedItem == null)
      {
         return false;
      }

      clickedItem.Selected = clickedItem.Focused = true;
      if (clickedItem.HasChildren)
      {
         Rectangle pmImageRect = new Rectangle(clickedItem.PlusMinusLocation.GetShift(1, 1),
            new Size(TreeListView.PlusImage.Width - 1, TreeListView.PlusImage.Height - 1));

         if (clickLoc.IsInRect(pmImageRect))
         {
            ToggleExpandState(clickedItem);
            return true;
         }
      }

      Rectangle cbRect = new Rectangle(clickedItem.CheckBoxBounds.Location.GetShift(1, 1),
         new Size(clickedItem.CheckBoxBounds.Width - 1, clickedItem.CheckBoxBounds.Height - 1));

      if (clickLoc.IsInRect(cbRect))
      {
         clickedItem.Checked = !clickedItem.Checked;
         return true;
      }

      return false;
   }

   private bool KeyDownProc(ref Message msg)
   {
      Debug.Assert(msg.Msg == Messages.WM_KEYDOWN);

      int wParam = msg.WParam.ToInt32();

      //int lParam = msg.LParam.ToInt32();
      //uint scanCode = (uint)((lParam >> 15) & 0x0000007f);
      //uint scanCode = (uint)((lParam >> 16) & 255);
      //uint vkCode = PInvoke.MapVirtualKey(scanCode, (uint)VirtualKeyMappingOption.MAPVK_VSC_TO_VK);
      //bool extendedKey = (lParam & 0x00800000) == 0x00800000;

      Keys keyCode = (Keys)Enum.ToObject(typeof(Keys), wParam);
      short ctrlPressed = PInvoke.GetKeyState((int)VirtualKeyCodes.VK_CONTROL);
      short shiftPressed = PInvoke.GetKeyState((int)VirtualKeyCodes.VK_SHIFT);

      bool expandCmd = keyCode == Keys.Add || (Control.ModifierKeys == Keys.Control && keyCode == Keys.Right);
      bool collapseCmd = keyCode == Keys.Subtract || (Control.ModifierKeys == Keys.Control && keyCode == Keys.Left);

      // CTRL-RightArrow or +: Expand the list view item (if it has children and is collapsed)
      if (expandCmd)
      {
         ListViewItem2 lvi2 = SelectedItems.Count == 1 ? SelectedItems[0] as ListViewItem2 : null;
         if (lvi2 != null && lvi2.Collapsed)
         {
            Expand(lvi2);
            return true;
         }
      }

      // CTRL-LeftArrow or -: Collapse the list view item (if it has children and is expanded)
      if (collapseCmd)
      {
         ListViewItem2 lvi2 = SelectedItems.Count == 1 ? SelectedItems[0] as ListViewItem2 : null;
         if (lvi2 != null && lvi2.Expanded)
         {
            Collapse(lvi2);
            return true;
         }
      }

      return false;
   }

   private Size CalculateCheckBoxSize(ListViewItem.ListViewSubItem lvsi)
   {
      //Debug.Assert(lvsi != null);
      //int cbWidth = this.SmallImageList == null || this.SmallImageList.Images.Count == 0 ? TreeListView.CheckBoxWidth : this.SmallImageList.Images[0].Width;
      //return new Size(cbWidth, lvsi.Bounds.Height - 1);      
      return new Size(lvsi.Bounds.Height - 1, lvsi.Bounds.Height - 1);
   }

   private static StringAlignment ToStringAlignment(HorizontalAlignment horizontalAlignment)
   {
      if (horizontalAlignment == HorizontalAlignment.Center) return StringAlignment.Center;
      else if (horizontalAlignment == HorizontalAlignment.Left) return StringAlignment.Near;
      else return StringAlignment.Far;
   }

   #endregion
}