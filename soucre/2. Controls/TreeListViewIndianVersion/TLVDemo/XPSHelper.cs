using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Drawing;

namespace TLVDemo
{
   static class Names
   {
      public const string ExpenseGroup = "ExpenseGroup";
      public const string ExpenseItem = "ExpenseItem";
      public const string Seperator = "Seperator";

      public const string Date = "Date";
      public const string Name = "Name";
      public const string Recipient = "Recipient";
      public const string Purchaser = "Purchaser";
      public const string Category = "Category";
      public const string PurchaseMode = "PurchaseMode";
      public const string Amount = "Amount";
      public const string Remarks = "Remarks";

      public static readonly string[] ExpenseItemAttributes = new string[] {
         Date,
         Name,
         Recipient,
         Purchaser,
         Category,
         PurchaseMode,
         Amount,
         Remarks
      };
   };

   public class RandomColor
   {
      private const int FirstKnownColor = 0x01;
      private const int LastKnownColor = 0xA7;
      
      private static readonly Random RandomNumberGenerator = new Random(0);

      public static Color Next()
      {
         int knownColorValue = RandomNumberGenerator.Next(FirstKnownColor, LastKnownColor);
         var knownColor = (KnownColor)Enum.ToObject(typeof(KnownColor), knownColorValue);
         return Color.FromKnownColor(knownColor);
      }
   }

   class XPSHelper
   {
      public static void LoadExpenses(TreeListView lView, string xFile)
      {
         Throw.IfFalse(File.Exists(xFile), new FileNotFoundException(xFile));
         InitializeListView(lView);

         var xDoc = XDocument.Load(xFile);
         Debug.Assert(xDoc.Root.Name == "Expenses");
         Debug.Assert(xDoc.Root.Attributes().Count() >= 2);

         var xList = new List<XElement>(xDoc.Root.Elements());
         var lItems = PrepareTree(xList, null);

         foreach (var li in lItems)
         {
            lView.AddItem(li);
         }
      }

      private static ListViewItem2[] PrepareTree(IList<XElement> xList, ListViewItem2 lviParent)
      {
         var lItems = new List<ListViewItem2>();

         for (int i = 0; i < xList.Count; ++i)
         {
            if (string.Compare(Names.ExpenseItem, xList[i].Name.LocalName) == 0)
            {
               var li = GetListViewItem2(xList[i], lviParent);
               li.ImageIndex = 0;
               li.ForeColor = RandomColor.Next();

               lItems.Add(li);
            }
            else if (string.Compare(Names.ExpenseGroup, xList[i].Name.LocalName) == 0)
            {
               var xaName = xList[i].Attribute(Names.Name);
               var xaDate = xList[i].Attribute(Names.Date);

               var subItems = new[] {
                  xaDate!= null ? xaDate.Value : "?",
                  xaName != null ? xaName.Value : "Expense Group"
               };

               var xGroup = new ListViewItem2(subItems, null) {
                  ImageIndex = 1,
                  ForeColor = RandomColor.Next()
               };

               var xItems = new List<XElement>(xList[i].Elements(XName.Get(Names.ExpenseItem)));
               var lviItems = new ListViewItem2[xItems.Count];

               for (int k = 0; k < xItems.Count; ++k)
               {
                  var li = GetListViewItem2(xItems[k], xGroup);
                  li.ImageIndex = 0;
                  li.ForeColor = RandomColor.Next();
                  lviItems[k] = li;
               }

               xGroup.AddChildren(lviItems);

               lItems.Add(xGroup);
            }
            else if (string.Compare(Names.Seperator, xList[i].Name.LocalName) == 0)
            {
               lItems.Add(new ListViewItem2(null, null));
            }
            else
            {
               continue;
            }
         }

         return lItems.ToArray();
      }

      private static ListViewItem2 GetListViewItem2(XElement xe, ListViewItem2 lviParent)
      {
         var subItems = new string[Names.ExpenseItemAttributes.Length];
         for (int i = 0; i < Names.ExpenseItemAttributes.Length; ++i)
         {
            var xa = xe.Attribute(XName.Get(Names.ExpenseItemAttributes[i]));
            subItems[i] = xa != null ? xa.Value : string.Empty;
         }

         return new ListViewItem2(subItems, lviParent);
      }

      public static void InitializeListView(ListView lView)
      {
         lView.Clear();

         var cHeaders = new ColumnHeader[Names.ExpenseItemAttributes.Length];
         for (int i = 0; i < Names.ExpenseItemAttributes.Length; ++i)
         {
            var ch = new ColumnHeader { Text = Names.ExpenseItemAttributes[i], Width = 175 };
            cHeaders[i] = ch;
         }

         lView.Columns.AddRange(cHeaders);
      }
   }

   class ExpenseItem
   {
      public string Date = string.Empty;
      public string Name = "Expense Item";
      public string Recipient = "";
      public string Purchaser = "";
      public string Category = "";
      public string PurchaseMode = "";
      public string Amount = "";
      public string Remarks = "";
   }

   class ExpenseGroup
   {
      private readonly List<ExpenseItem> _xItems = new List<ExpenseItem>();

      public string Name = "Expense Group";
      public string Date = string.Empty;

      public ExpenseGroup(string gName, string gDate, IEnumerable<ExpenseItem> xItems)
      {
         Name = gName ?? "ExpenseGroup";
         Date = gDate ?? "?";
         _xItems.AddRange(xItems);
      }

      public void AddChildren(ExpenseItem[] xItems)
      {
         _xItems.AddRange(xItems);
      }

      public void RemoveChildren(ExpenseItem[] xItems)
      {
         Array.ForEach(xItems, ei => _xItems.Remove(ei));
      }
   }
}