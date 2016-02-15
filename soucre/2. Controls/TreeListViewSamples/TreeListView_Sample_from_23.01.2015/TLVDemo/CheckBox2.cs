using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TLVDemo
{
    public class CheckBox2 : Control
    {
        private Rectangle textRectangleValue = new Rectangle();
        private bool clicked = false;
        private CheckBoxState state = CheckBoxState.UncheckedNormal;

        public CheckBox2()
            : base()
        {
            this.Location = new Point(50, 50);
            this.Size = new Size(100, 20);
            this.Text = "Click here";
            this.Font = SystemFonts.IconTitleFont;
        }

        // Calculate the text bounds, exluding the check box.
        public Rectangle TextRectangle
        {
            get
            {
                using (Graphics g = this.CreateGraphics())
                {
                    int checkBoxWidth = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal).Width;
                    textRectangleValue.X = ClientRectangle.X + checkBoxWidth;
                    textRectangleValue.Y = ClientRectangle.Y;
                    textRectangleValue.Width = ClientRectangle.Width - checkBoxWidth;
                    // CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal).Width;
                    textRectangleValue.Height = ClientRectangle.Height;
                }

                return textRectangleValue;
            }
        }

        // Draw the check box in the current state.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            CheckBoxRenderer.DrawCheckBox(e.Graphics,
                ClientRectangle.Location, TextRectangle, this.Text,
                this.Font, TextFormatFlags.HorizontalCenter,
                clicked, state);
        }

        // Draw the check box in the checked or unchecked state, alternately.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!clicked)
            {
                clicked = true;
                this.Text = "Clicked!";
                state = CheckBoxState.CheckedPressed;
                Invalidate();
            }
            else
            {
                clicked = false;
                this.Text = "Click here";
                state = CheckBoxState.UncheckedNormal;
                Invalidate();
            }
        }

        // Draw the check box in the hot state. 
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            state = clicked ? CheckBoxState.CheckedHot : CheckBoxState.UncheckedHot;
            Invalidate();
        }

        // Draw the check box in the hot state. 
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.OnMouseHover(e);
        }

        // Draw the check box in the unpressed state.
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            state = clicked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
            Invalidate();
        }
    }
}