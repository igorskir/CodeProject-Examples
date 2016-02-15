namespace TLVDemo
{
   partial class CtrlPropertyGrid
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.dgView = new System.Windows.Forms.DataGridView();
         this.propNameDGC = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.propValueDGC = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.ctrlInfoLabel = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
         this.SuspendLayout();
         // 
         // dgView
         // 
         this.dgView.AllowUserToAddRows = false;
         this.dgView.AllowUserToDeleteRows = false;
         this.dgView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.dgView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
         this.dgView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
         this.dgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.propNameDGC,
            this.propValueDGC});
         this.dgView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
         this.dgView.Location = new System.Drawing.Point(3, 43);
         this.dgView.MultiSelect = false;
         this.dgView.Name = "dgView";
         this.dgView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
         this.dgView.Size = new System.Drawing.Size(545, 516);
         this.dgView.TabIndex = 0;
         this.dgView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgView_CellBeginEdit);
         this.dgView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellEndEdit);
         // 
         // propNameDGC
         // 
         this.propNameDGC.HeaderText = "Name";
         this.propNameDGC.Name = "propNameDGC";
         this.propNameDGC.ReadOnly = true;
         this.propNameDGC.Width = 5;
         // 
         // propValueDGC
         // 
         this.propValueDGC.HeaderText = "Value";
         this.propValueDGC.Name = "propValueDGC";
         this.propValueDGC.Width = 5;
         // 
         // ctrlInfoLabel
         // 
         this.ctrlInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ctrlInfoLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.ctrlInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.ctrlInfoLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.ctrlInfoLabel.Location = new System.Drawing.Point(0, 0);
         this.ctrlInfoLabel.Name = "ctrlInfoLabel";
         this.ctrlInfoLabel.Size = new System.Drawing.Size(550, 40);
         this.ctrlInfoLabel.TabIndex = 1;
         this.ctrlInfoLabel.Text = "Control Properties";
         // 
         // CtrlPropertyGrid
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.ctrlInfoLabel);
         this.Controls.Add(this.dgView);
         this.DoubleBuffered = true;
         this.Name = "CtrlPropertyGrid";
         this.Size = new System.Drawing.Size(550, 562);
         this.Load += new System.EventHandler(this.CtrlPropertyGrid_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView dgView;
      private System.Windows.Forms.DataGridViewTextBoxColumn propNameDGC;
      private System.Windows.Forms.DataGridViewTextBoxColumn propValueDGC;
      private System.Windows.Forms.Label ctrlInfoLabel;
   }
}
