using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace TLVDemo
{
   public partial class CtrlPropertyGrid : UserControl
   {
      private readonly Control _ctrlObject;

      public CtrlPropertyGrid()
      {
         Debug.Assert(false, "Design Time Ctor Invoked");
         InitializeComponent();
      }

      public CtrlPropertyGrid(Control ctrl)
      {
         Debug.Assert(ctrl != null);
         InitializeComponent();

         _ctrlObject = ctrl;
         var piList = ctrl.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
         
         var browsableProps = Array.FindAll(piList, delegate(PropertyInfo pi)
         {
            var pAttrs = pi.GetCustomAttributes(true);            
            var ebaObject = pAttrs.FirstOrDefault(attr => attr is EditorBrowsableAttribute || pAttrs.Length == 3);
            return (ebaObject != null);
         });

         foreach (var pi in browsableProps)
         {
            int rowIndex = dgView.Rows.Add();

            var dgvRow = dgView.Rows[rowIndex];
            dgvRow.SetValues(pi.Name, pi.GetValue(ctrl, null));            
            dgvRow.Cells[1].ValueType = pi.PropertyType;
            dgvRow.Tag = pi;
         }

         ctrlInfoLabel.Text = string.Format("Name:  {0}\r\nType:   {1}", ctrl.Name, ctrl.GetType());
      }

      private void CtrlPropertyGrid_Load(object sender, EventArgs e)
      {

      }

      private void dgView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
      {
         try
         {
            var pi = this.dgView[e.ColumnIndex, e.RowIndex].Tag as PropertyInfo;
            if (pi != null)
            {
               object cellValue = this.dgView[e.ColumnIndex, e.RowIndex].Value;
               object propertyValue = Convert.ChangeType(cellValue, pi.PropertyType);
            }
         }
         catch (Exception ex)
         {
            e.Cancel = true;
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void dgView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {
         try
         {
            var dgRow = this.dgView.Rows[e.RowIndex];
            var pi = dgRow.Tag as PropertyInfo;

            if (pi != null)
            {
               var dgCell = this.dgView[e.ColumnIndex, e.RowIndex];
               pi.SetValue(this._ctrlObject, dgCell.Value, null);               
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }
   }
}
