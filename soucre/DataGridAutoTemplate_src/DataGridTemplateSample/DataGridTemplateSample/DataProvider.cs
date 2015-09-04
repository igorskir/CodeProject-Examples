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
using System.ComponentModel;
using System.Windows;
using System.Data.SQLite;

namespace DataGridTemplateSample
{
	class DataProvider
	{
		private static string dbConnectionString = "Data Source={0};Version=3;DateTimeFormat=Ticks;";
		private static string dbFile = "database.db";

		/// <summary>
		/// The connection string containing the parameters for the connection
		/// </summary>
		public static string ConnectionString
		{
			get
			{
				return string.Format(dbConnectionString, DataProvider.Database);
			}
		}

		/// <summary>
		/// The database file
		/// </summary>
		public static string Database
		{
			get
			{
				return dbFile;
			}
			set
			{
				dbFile = value;
			}
		}

		/// <summary>
		/// Value indicating running in design mode
		/// </summary>
		static public bool IsInDesignMode
		{
			get { return DesignerProperties.GetIsInDesignMode( new DependencyObject() ); }
		}
		/// <summary>
		/// Value indicating running in runtime mode
		/// </summary>
		static public bool IsInRuntimeMode
		{
			get { return !IsInDesignMode; }
		}
	}
}
