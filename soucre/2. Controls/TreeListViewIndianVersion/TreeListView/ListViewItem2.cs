using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using LVI2List = System.Collections.Generic.List<ListViewItem2>;

public class ListViewItem2 : ListViewItem
{
   #region Member Data

   private LVI2List _lviChildren;

   #endregion

   #region Ctor(s)

   /// <summary>
   /// If the parent is null, then it is a root level item
   /// </summary>
   public ListViewItem2(string[] subItems, ListViewItem2 lviParent)
      : this(subItems, lviParent, null)
   {
   }

   public ListViewItem2(string[] subItems, ListViewItem2 lviParent, ListViewItem2[] lviChildren)
      : base(subItems)
   {
      // Debug.Assert(lviParent != null);
      Parent = lviParent;

      if (lviChildren != null)
      {
         Throw.IfTrue(Array.Exists(lviChildren, delegate(ListViewItem2 lvi2) { return lvi2 == null; }), "One or more items in the ListViewItem2 array (children) is null.");
         _lviChildren = new List<ListViewItem2>(lviChildren);
      }
   }

   #endregion

   #region Public Methods and Properties
   
   public void AddChildren(ListViewItem2[] lviChildren)
   {
      Throw.IfTrue(lviChildren == null || lviChildren.Length == 0, "Specified children array is null or zero length.");
      Throw.IfTrue(Array.Exists(lviChildren, lvi2 => lvi2 == null), "One or more items in the ListViewItem2 array (children) is null.");
      
      if (_lviChildren == null)
      {
         _lviChildren = new List<ListViewItem2>(lviChildren);
      }
      else
      {
         _lviChildren.AddRange(lviChildren);
      }
   }

   public void MakeChildOf(ListViewItem lvi)
   {
      Debug.Assert(false, "Not Implemented!");
   }

   public ListViewItem2 Parent { get; private set; }

   public ListViewItem2[] Children
   {
      get
      {
         return _lviChildren == null ? new ListViewItem2[0] : _lviChildren.ToArray();
      }
   }

   public bool HasChildren
   {
      get
      {
         return _lviChildren != null && _lviChildren.Count > 0;
      }
   }

   public int GetIndentLevel()
   {
      var lviParent = Parent;

      int index = 0;
      while (lviParent != null)
      {
         ++index;
         lviParent = lviParent.Parent;
      }

      return index;
   }

   public bool Expanded { get; internal set; }

   public bool Collapsed
   {
      get
      {
         return !Expanded;
      }
      internal set
      {
         Expanded = !value;
      }
   }

   public bool IsEmpty
   {
      get
      {
         return SubItems.TrueForAll(si => si.Text == string.Empty);
      }
      //internal set
      //{
      //   Debug.Assert(false, "ListViewItem2.IsEmpty - Not Implemented !!!");
      //}
   }

   public Rectangle CheckBoxBounds { get; internal set; }
   public Point PlusMinusLocation { get; internal set; }

   #endregion
}