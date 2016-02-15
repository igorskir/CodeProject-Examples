using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

static class Extensions
{
   public static bool IsInRect(this Point pt, Rectangle rect)
   {
      return pt.X >= rect.X && pt.X <= rect.Right && pt.Y >= rect.Y && pt.Y <= rect.Bottom;
   }

   public static Point GetShift(this Point pt, int dx, int dy)
   {
      return new Point(pt.X + dx, pt.Y + dy);
   }

   public static void Shift(this Point pt, int dx, int dy)
   {
      pt.X += dx;
      pt.Y += dy;
   }

   public static Size GetAdjust(this Size sz, int w, int h)
   {
      return new Size(sz.Width + w, sz.Height + h);
   }

   public static bool TrueForAll(this ListViewItem2.ListViewSubItemCollection lviCollxn,
      Predicate<ListViewItem.ListViewSubItem> dlgFunc)
   {
      Debug.Assert(dlgFunc != null, "The specified dlgFunc is NULL.");
      Debug.Assert(dlgFunc != null, "The specified dlgFunc is NULL.");
      
      foreach (ListViewItem.ListViewSubItem si in lviCollxn)
      {
         if (!dlgFunc(si))
         {
            return false;
         }
      }

      return true;
   }

   public static object[] ToArray(this IEnumerable iEnum)
   {
      List<object> tList = new List<object>();
      IEnumerator iter = iEnum.GetEnumerator();

      while (iter.MoveNext())
      {
         tList.Add(iter.Current);
      }

      return tList.ToArray();
   }

   public static T[] ToArray<T>(this IEnumerable iEnum)
   {
      List<T> tList = new List<T>();
      IEnumerator iter = iEnum.GetEnumerator();
      
      while (iter.MoveNext())
      {
         tList.Add((T)iter.Current);
      }

      return tList.ToArray();
   }
}

//partial class TreeListView
//{
//   #region Drag Drop Row Reordering

//   private const string REORDER = "REORDER";

//   protected override void OnItemDrag(ItemDragEventArgs e)
//   {
//      base.OnItemDrag(e);
//      base.DoDragDrop(REORDER, DragDropEffects.Move);
//   }
//   protected override void OnDragEnter(DragEventArgs e)
//   {
//      base.OnDragEnter(e);
//      if (!e.Data.GetDataPresent(DataFormats.Text))
//      {
//         e.Effect = DragDropEffects.None;
//         return;
//      }
//      String text = (String)e.Data.GetData(REORDER.GetType());
//      if (text.CompareTo(REORDER) == 0)
//      {
//         e.Effect = DragDropEffects.Move;
//      }
//      else
//      {
//         e.Effect = DragDropEffects.None;
//      }
//   }
//   protected override void OnDragOver(DragEventArgs e)
//   {
//      if (!e.Data.GetDataPresent(DataFormats.Text))
//      {
//         e.Effect = DragDropEffects.None;
//         return;
//      }

//      Point cp = base.PointToClient(new Point(e.X, e.Y));
//      ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
//      if (hoverItem == null)
//      {
//         e.Effect = DragDropEffects.None;
//         return;
//      }

//      foreach (ListViewItem moveItem in base.SelectedItems)
//      {
//         if (moveItem.Index == hoverItem.Index)
//         {
//            e.Effect = DragDropEffects.None;
//            hoverItem.EnsureVisible();
//            return;
//         }
//      }

//      base.OnDragOver(e);
//      String text = (String)e.Data.GetData(REORDER.GetType());
//      if (text.CompareTo(REORDER) == 0)
//      {
//         e.Effect = DragDropEffects.Move;
//         hoverItem.EnsureVisible();
//      }
//      else
//      {
//         e.Effect = DragDropEffects.None;
//      }
//   }
//   protected override void OnDragDrop(DragEventArgs e)
//   {
//      base.OnDragDrop(e);

//      if (base.SelectedItems.Count == 0)
//      {
//         return;
//      }
//      Point cp = base.PointToClient(new Point(e.X, e.Y));
//      ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
//      if (dragToItem == null)
//      {
//         return;
//      }
//      int dropIndex = dragToItem.Index;
//      if (dropIndex > base.SelectedItems[0].Index)
//      {
//         dropIndex++;
//      }

//      List<ListViewItem> insertItems = new List<ListViewItem>(); // ListViewItem[base.SelectedItems.Count];
//      foreach (ListViewItem item in base.SelectedItems)
//      {
//         insertItems.Add(item);
//         Items.Remove(item);
//      }

//      for (int i = insertItems.Count - 1; i >= 0; i--)
//      {
//         base.Items.Insert(dropIndex, insertItems[i]);
//      }

//      //foreach (ListViewItem removeItem in base.SelectedItems)
//      //{
//      //   base.Items.Remove(removeItem);
//      //}
//   }

//   #endregion
//}