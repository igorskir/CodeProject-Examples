//----------------------------------------------------------------------------
//
// THIS WORK IS PROVIDED "AS IS", "WHERE IS" AND "AS AVAILABLE", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED OR CONDITIONS OR GUARANTEES.
// YOU, THE USER, ASSUME ALL RISK IN ITS USE, INCLUDING COPYRIGHT INFRINGEMENT, PATENT INFRINGEMENT, SUITABILITY, ETC.
// AUTHOR EXPRESSLY DISCLAIMS ALL EXPRESS, IMPLIED OR STATUTORY WARRANTIES OR CONDITIONS, INCLUDING WITHOUT LIMITATION,
// WARRANTIES OR CONDITIONS OF MERCHANTABILITY, MERCHANTABLE QUALITY OR FITNESS FOR A PARTICULAR PURPOSE,
// OR ANY WARRANTY OF TITLE OR NON-INFRINGEMENT, OR THAT THE WORK (OR ANY PORTION THEREOF) IS CORRECT, USEFUL, BUG-FREE OR FREE OF VIRUSES.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataGridTemplateSample
{
	class CustomDataGridTemplateColumn : DataGridTemplateColumn
	{
		private BindingBase _binding;

		protected virtual void OnBindingChanged(BindingBase oldBinding, BindingBase newBinding)
		{
			base.NotifyPropertyChanged("Binding");
		}

		public virtual BindingBase Binding
		{
			get
			{
				return this._binding;
			}
			set
			{
				if (this._binding != value)
				{
					BindingBase oldBinding = this._binding;
					this._binding = value;
					base.CoerceValue(DataGridColumn.SortMemberPathProperty);
					this.OnBindingChanged(oldBinding, this._binding);
				}
			}
		}

		public override BindingBase ClipboardContentBinding
		{
			get
			{
				return (base.ClipboardContentBinding ?? this.Binding);
			}
			set
			{
				base.ClipboardContentBinding = value;
			}
		}

		private DataTemplate ChooseCellTemplate(bool isEditing)
		{
			DataTemplate template = null;
			if (isEditing)
			{
				template = this.CellEditingTemplate;
			}
			if (template == null)
			{
				template = this.CellTemplate;
			}
			return template;
		}

		private DataTemplateSelector ChooseCellTemplateSelector(bool isEditing)
		{
			DataTemplateSelector templateSelector = null;
			if (isEditing)
			{
				templateSelector = this.CellEditingTemplateSelector;
			}
			if (templateSelector == null)
			{
				templateSelector = this.CellTemplateSelector;
			}
			return templateSelector;
		}

		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
		{
			return this.LoadTemplateContent(true, dataItem, cell);
		}

		protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		{
			return this.LoadTemplateContent(false, dataItem, cell);
		}

		private void ApplyBinding(DependencyObject target, DependencyProperty property)
		{
			BindingBase binding = this.Binding;
			if (binding != null)
			{
				BindingOperations.SetBinding(target, property, binding);
			}
			else
			{
				BindingOperations.SetBinding(target, property, new Binding());
			}
		}

		private FrameworkElement LoadTemplateContent(bool isEditing, object dataItem, DataGridCell cell)
		{
			DataTemplate template = this.ChooseCellTemplate(isEditing);
			DataTemplateSelector templateSelector = this.ChooseCellTemplateSelector(isEditing);
			if ((template == null) && (templateSelector == null))
			{
				return null;
			}
			ContentPresenter contentPresenter = new ContentPresenter();
			this.ApplyBinding(contentPresenter, ContentPresenter.ContentProperty);
			contentPresenter.ContentTemplate = template;
			contentPresenter.ContentTemplateSelector = templateSelector;
			return contentPresenter;
		}
	}
}
