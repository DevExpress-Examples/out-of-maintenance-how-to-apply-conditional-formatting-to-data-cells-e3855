Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Windows.Media

Namespace DXPivotGrid_ConditionalFormatting
	Partial Public Class MainPage
		Inherits UserControl
		Private dataFileName As String = "nwind.xml"
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
			Dim [assembly] As System.Reflection.Assembly = _
				System.Reflection.Assembly.GetExecutingAssembly()
			Dim stream As Stream = [assembly].GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub
	End Class
	Public Class ValueToColorConverter
		Inherits MarkupExtension
		Implements IValueConverter
		#Region "IValueConverter Members"
		Public Function Convert(ByVal value As Object, _
					ByVal targetType As System.Type, _
					ByVal parameter As Object, _
					ByVal culture As System.Globalization.CultureInfo) _
					As Object Implements IValueConverter.Convert
			Dim decValue As Decimal = System.Convert.ToDecimal(value)
				If decValue < 80 AndAlso decValue > 0 Then
					Return New SolidColorBrush(Color.FromArgb(70,255,255,0))
				Else
					Return New SolidColorBrush(Colors.Transparent)
				End If
		End Function
		Public Function ConvertBack(ByVal value As Object, _
					ByVal targetType As System.Type, _
					ByVal parameter As Object, _
					ByVal culture As System.Globalization.CultureInfo) _
					As Object Implements IValueConverter.ConvertBack
			Throw New System.NotImplementedException()
		End Function
		#End Region
		Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
			Return Me
		End Function
	End Class
End Namespace
