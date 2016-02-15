using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TLVDemo
{
   class LVLink
   {      
      public ListViewItem Parent;
      public List<ListViewItem> Children;

      public LVLink(ListViewItem lviParent)
      {
         Parent = lviParent;
      }

      public LVLink(ListViewItem[] lviChildren)
      {
         if (lviChildren == null || lviChildren.Length == 0)
         {
            return;
         }

         if (Children == null)
         {
            Children = new List<ListViewItem>();
         }

         Children.AddRange(lviChildren);
      }

      public bool HasChildren()
      {
         return (Children != null && Children.Count > 0);
      }
   }

   class Person
   {
#if VS2008
      public string Name { get; set; }
      public int Age { get; set; }
      public string Address { get; set; }
#else
      public readonly string Name;
      public readonly int Age;
      public readonly string Address;

      public Person(string name, int age, string address)
      {
         Name = name;
         Age = age;
         Address = address;
      }
#endif

      public override string ToString()
      {  
         return string.Format("{0}. {1}. {2}", Name, Age, Address);
      }

      public string[] ToStringArray()
      {
         return new string[] {
            Name,
            Age.ToString(),
            Address
         };
      }
   }
}