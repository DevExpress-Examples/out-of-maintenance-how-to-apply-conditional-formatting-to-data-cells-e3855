using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media;

namespace DXPivotGrid_ConditionalFormatting {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_ConditionalFormatting.nwind.xml";
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }
    }
    public class ValueToColorConverter : MarkupExtension, IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture) {
            decimal decValue = System.Convert.ToDecimal(value);
                if (decValue < 80 && decValue > 0)
                    return new SolidColorBrush(Color.FromArgb(70,255,255,0));
                else
                    return new SolidColorBrush(Colors.Transparent);
        }
        public object ConvertBack(object value, System.Type targetType,
                    object parameter, System.Globalization.CultureInfo culture) {
            throw new System.NotImplementedException();
        }
        #endregion
        public override object ProvideValue(System.IServiceProvider serviceProvider) {
            return this;
        }
    }
}
