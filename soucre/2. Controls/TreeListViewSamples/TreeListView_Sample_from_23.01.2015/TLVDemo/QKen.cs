using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;

namespace TLVDemo
{
   class QKen
   {
      private static readonly string[] ExpenseItemAttributes = new string[] {
         "Date",
         "Name",
         "Recipient",
         "Purchaser",
         "Category",
         "PurchaseMode",
         "Amount",
         "Remarks"
      };

      public static void InitializeListView(ListView lView)
      {
         lView.Clear();

         var cols = Array.ConvertAll(ExpenseItemAttributes, cName => new ColumnHeader
         {
            Text = cName,
            Width = 175,
            TextAlign = HorizontalAlignment.Left
         });

         lView.Columns.AddRange(cols);
      }

      public static ListViewItem2[] CreateTree(IEnumerable<XElement> xElements, ListViewItem2 lviParent, bool skipEmpty)
      {
         var lvItems = new List<ListViewItem2>();
         foreach (XElement xe in xElements)
         {
            if (skipEmpty && xe.Attributes().All(xa => string.IsNullOrEmpty(xa.Value)))
            {
               continue;
            }

            var subItems = xe.Attributes().Select(xa => xa.Value).ToArray();
            var lvi = new ListViewItem2(subItems, lviParent) { ForeColor = RandomColor.Next(), ImageIndex = 0 };
            lvItems.Add(lvi);

            if (xe.Elements().Count() > 0)
            {
               var lvis = CreateTree(xe.Elements(), lvi, true);
               if (lvis.Length > 0)
               {
                  lvi.AddChildren(lvis);
               }
            }
         }

         return lvItems.ToArray();
      }

      public static T[] ToArray<T>(IEnumerable<T> tn)
      {
         var tList = new List<T>();

         if (tn != null)
         {
            IEnumerator<T> iter = tn.GetEnumerator();

            while (iter.MoveNext())
            {
               tList.Add(iter.Current);
            }
         }

         return tList.ToArray();
      }

      public static ListViewItem2[] CreateTreeFromXML(XElement[] xeArray, ListViewItem2 lviParent)
      {
         var lvItems = new List<ListViewItem2>();
         foreach (XElement xe in xeArray)
         {
            var subItems = new string[] { xe.Name.LocalName, xe.HasElements ? "[...]" : xe.Value };
            var lvi = new ListViewItem2(subItems, lviParent);            
            lvItems.Add(lvi);

            XAttribute[] xas = ToArray<XAttribute>(xe.Attributes());
            if (xas.Length > 0)
            {
               var xaLvis = new ListViewItem2[xas.Length];
               for (int i = 0; i < xas.Length; ++i)
               {
                  var sis = new string[] { xas[i].Name.LocalName, xas[i].Value };
                  xaLvis[i] = new ListViewItem2(sis, lvi);
               }

               lvi.AddChildren(xaLvis);
            }

            lvi.ImageIndex = xas.Length > 0 ? 1 : 0;

            XElement[] xes = ToArray(xe.Elements());
            if (xes.Length > 0)
            {
               var lvis = CreateTreeFromXML(xes, lvi);
               if (lvis.Length > 0)
               {
                  lvi.AddChildren(lvis);
               }
            }
         }

         return lvItems.ToArray();
      }

      private static XAttribute[] ToXAttribute(string[] values, string[] mappingAttrNames)
      {
         Throw.IfTrue(mappingAttrNames.Length < values.Length, "mappingAttrNames.Length < values.Length");

         XAttribute[] xas = new XAttribute[values.Length];
         for (int i = 0; i < values.Length; ++i)
         {
            XName xn = XName.Get(mappingAttrNames[i] ?? string.Empty, string.Empty);
            xas[i] = new XAttribute(xn, values[i] ?? string.Empty);
         }

         return xas;
      }

      public static XElement ToXml(string filePath)
      {
         Throw.IfFalse(File.Exists(filePath), "The specified file '{0}' does not exist", filePath);

         string[] eItems = File.ReadAllLines(filePath);
         
         XElement[] xes = new XElement[eItems.Length];         
         for (int i = 0; i < eItems.Length; ++i)
         {
            string[] eiFields = eItems[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            xes[i] = new XElement(XName.Get("ExpenseItem", string.Empty),
               ToXAttribute(eiFields, ExpenseItemAttributes));
         }

         XElement xe = new XElement(XName.Get("Expenses", string.Empty), xes);

         return xe;
      }
   }
}