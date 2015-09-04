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
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Data.SQLite;

namespace DataGridTemplateSample
{
	[ValueConversion(typeof(int), typeof(string))]
	public class ItemStatusConverter : IValueConverter
	{
		private bool helperDataFetched = false;
		private Dictionary<Int64, string> status = new Dictionary<Int64, string>();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || !(value is Int64)) return null;
			if (!helperDataFetched) GetHelperData();

			Int64 key = (Int64)value;

			if (!status.ContainsKey(key) || status[key] == null) return null;

			return status[key];
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("This method is intentionally not implemented");
		}

		private void GetHelperData()
		{
			// Prevent refetching data
			this.helperDataFetched = true;
			status.Clear();

			// check for design mode
			if (DataProvider.IsInDesignMode)
			{
				return;
			}
			
			// query data
			string SQLquery = "SELECT * FROM Status";

			SQLiteConnection dbconnectie = new SQLiteConnection(DataProvider.ConnectionString);
			SQLiteCommand command = new SQLiteCommand(SQLquery, dbconnectie);
			SQLiteDataReader reader;

			try
			{
				dbconnectie.Open();	

				reader = command.ExecuteReader();
				while (reader.Read())
				{
					status.Add((Int64)reader["StatusId"], reader["StatusName"] as string);
				}
				reader.Close();
			}
			catch (SQLiteException ex)
			{
				MessageBox.Show(string.Format("Exception:\r\n{0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			finally
			{
				dbconnectie.Close();
			}			
		}
	}
}
