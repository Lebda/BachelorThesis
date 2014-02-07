using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using System.Windows.Media;
using System.Windows.Data;
using XEP_SectionCheckInterfaces.DataCache;

namespace XEP_SmartControls
{
    public class XEP_SeriesTypeSwitch : DependencyObject
    {
        public static readonly DependencyProperty SeriesTypeProperty = DependencyProperty.RegisterAttached("SeriesTypeNew",
                                                                                                   typeof(string),
                                                                                                   typeof(XEP_SeriesTypeSwitch),
                                                                                                   new PropertyMetadata("", OnSeriesTypeNewChanged));

        public static string GetSeriesTypeNew(DependencyObject obj)
        {
            return (string)obj.GetValue(SeriesTypeProperty);
        }

        public static void SetSeriesTypeNew(DependencyObject obj, string value)
        {
            obj.SetValue(SeriesTypeProperty, value);
        }

        private static void OnSeriesTypeNewChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            RadCartesianChart chart = sender as RadCartesianChart;
            if (chart == null)
            {
                return;
            }
            string newValue = (string)e.NewValue;
            chart.Series.Clear();
            if (newValue == "Scatter point")
                CreateScatterPointSeries(chart);
            else if (newValue == "Scatter line")
                CreateScatterLineSeries(chart);
            else if (newValue == "Scatter spline")
                CreateScatterSplineSeries(chart);
            else if (newValue == "Scatter area")
                CreateScatterAreaSeries(chart);
            else if (newValue == "Scatter spline area")
                CreateScatterSplineAreaSeries(chart);

            XEP_StressStrainDiagramUC_ViewModel dataContext = chart.DataContext as XEP_StressStrainDiagramUC_ViewModel;
            if (dataContext == null)
            {
                SetMinMax(chart, null);
            }
            SetMinMax(chart, dataContext.MaterialDataUC);
        }
  
        private static void SetMinMax(RadCartesianChart chart, XEP_IMaterialData material)
        {
            if (chart == null)
            {
                return;
            }
            LinearAxis linAxis = chart.HorizontalAxis as LinearAxis;
            LinearAxis verAxis = chart.VerticalAxis as LinearAxis;
            linAxis.MajorStep = 2;
            verAxis.MajorStep = 5;
            if (linAxis == null || verAxis == null)
            {
                return;
            }
            if (material == null || material.StressStrainDiagram == null)
            {
                linAxis.Minimum = -5;
                linAxis.Maximum = 5;
                verAxis.Minimum = -50;
                verAxis.Maximum = 50;
            }
            else
            {
                linAxis.Minimum = material.StressStrainDiagram.Min(item => item.Strain.ManagedValue) - 1;
                linAxis.Maximum = material.StressStrainDiagram.Max(item => item.Strain.ManagedValue) + 1;
                verAxis.Minimum = material.StressStrainDiagram.Min(item => item.Stress.ManagedValue) - 2;
                verAxis.Maximum = material.StressStrainDiagram.Max(item => item.Stress.ManagedValue) + 2;
            }
        }

        private static void CreateScatterAreaSeries(RadCartesianChart chart)
        {
            ScatterAreaSeries seriesPrivateData = new ScatterAreaSeries()
            {
                Fill = new SolidColorBrush(Color.FromArgb(0xBF, 0x1B, 0x9D, 0xDE)),
                Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0x9D, 0xDE)),
                StrokeThickness = 2,
                LegendSettings = new SeriesLegendSettings { Title = "Private Sector" },
            };
            SetBindings(seriesPrivateData);
            SetSourceBindingPrivateData(seriesPrivateData);
            chart.Series.Add(seriesPrivateData);
        }

        private static void CreateScatterSplineAreaSeries(RadCartesianChart chart)
        {
            var privateDataColor = new SolidColorBrush(Color.FromArgb(0xBF, 0x1B, 0x9D, 0xDE));
            ScatterSplineAreaSeries seriesPrivateData = new ScatterSplineAreaSeries()
            {
                Fill = privateDataColor,
                Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0x9D, 0xDE)),
                StrokeThickness = 2,
                LegendSettings = new SeriesLegendSettings { Title = "Private Sector" },
            };
            SetBindings(seriesPrivateData);
            SetSourceBindingPrivateData(seriesPrivateData);
            chart.Series.Add(seriesPrivateData);
        }

        private static void CreateScatterLineSeries(RadCartesianChart chart)
        {
            ScatterLineSeries seriesPrivateData = new ScatterLineSeries()
            {
                IsHitTestVisible = true,
                Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0x9D, 0xDE)),
                LegendSettings = new SeriesLegendSettings { Title = "Private Sector" },
            };
            SetBindings(seriesPrivateData);
            SetSourceBindingPrivateData(seriesPrivateData);
            seriesPrivateData.PointTemplate = chart.Resources["PointTemplate"] as DataTemplate;
            chart.Series.Add(seriesPrivateData);
        }

        private static void CreateScatterSplineSeries(RadCartesianChart chart)
        {
            ScatterSplineSeries seriesPrivateData = new ScatterSplineSeries()
            {
                IsHitTestVisible = true,
                Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0x9D, 0xDE)),
                LegendSettings = new SeriesLegendSettings { Title = "Private Sector" },
            };
            SetBindings(seriesPrivateData);
            SetSourceBindingPrivateData(seriesPrivateData);
            seriesPrivateData.PointTemplate = chart.Resources["PointTemplate"] as DataTemplate;
            chart.Series.Add(seriesPrivateData);
        }

        private static void CreateScatterPointSeries(RadCartesianChart chart)
        {
            ScatterPointSeries seriesPrivateData = new ScatterPointSeries()
            {
                LegendSettings = new SeriesLegendSettings { Title = "Private Sector", MarkerGeometry = new RectangleGeometry { Rect = new Rect(1, 1, 10, 10) } },
            };
            SetBindings(seriesPrivateData);
            SetSourceBindingPrivateData(seriesPrivateData);
            seriesPrivateData.PointTemplate = chart.Resources["PointTemplate"] as DataTemplate;
            chart.Series.Add(seriesPrivateData);
        }

        private static void SetBindings(ScatterPointSeries series)
        {
            series.XValueBinding = new GenericDataPointBinding<XEP_IESDiagramItem, double>() { ValueSelector = product => product.Strain.ManagedValue };
            series.YValueBinding = new GenericDataPointBinding<XEP_IESDiagramItem, double>() { ValueSelector = product => product.Stress.ManagedValue };
        }

        private static void SetSourceBindingPrivateData(ChartSeries series)
        {
            Binding sourceBinding = new Binding("MaterialDataUC.StressStrainDiagram");
            series.SetBinding(ChartSeries.ItemsSourceProperty, sourceBinding);
        }
    }
}
