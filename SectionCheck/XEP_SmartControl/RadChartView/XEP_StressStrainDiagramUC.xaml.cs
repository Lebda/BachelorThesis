using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ChartView;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_CommonLibrary.Infrastructure;

namespace XEP_SmartControls
{
    /// <summary>
    /// Interaction logic for XEP_StressStrainDiagram.xaml
    /// </summary>
    public partial class XEP_StressStrainDiagramUC : UserControl
    {
        public XEP_StressStrainDiagramUC()
        { // Be careful you can not change DataContext of whole control
            InitializeComponent();
            _myShellGrid.DataContext = new XEP_StressStrainDiagramUC_ViewModel();
        }

        public void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                XEP_IESDiagramItem data = closestDataPoint.DataPoint.DataItem as XEP_IESDiagramItem;
                if (data != null)
                {
                    this.strainActual.Text = data.Strain.ManagedValueWithUnit;
                    this.stressActual.Text = data.Stress.ManagedValueWithUnit;
                }
            }
        }

        static XEP_StressStrainDiagramUC()
        {
            MaterialData_XEPProperty = DependencyProperty.Register(MaterialData_XEPPropertyName, typeof(XEP_IMaterialData), typeof(XEP_StressStrainDiagramUC),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnMaterialDataChanged)));
        }

        // DEPENDENCY
        private static string MaterialData_XEPPropertyName = "MaterialData_XEP";
        public static DependencyProperty MaterialData_XEPProperty;
        public XEP_IMaterialData MaterialData_XEP
        {
            get { return (XEP_IMaterialData)GetValue(MaterialData_XEPProperty); }
            set { SetValue(MaterialData_XEPProperty, value); }

        }
        private static void OnMaterialDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PropertySupport.OnMyPropertyChanged<XEP_StressStrainDiagramUC, XEP_IMaterialData>((XEP_StressStrainDiagramUC)sender, e, s => { ((XEP_StressStrainDiagramUC)sender).MaterialData_XEP = s; }, ((XEP_StressStrainDiagramUC)sender).OnStressStrainDiagramUCChangedInternal);
        }
        // Integrity solution
        private void OnStressStrainDiagramUCChangedInternal()
        {
            XEP_StressStrainDiagramUC_ViewModel dataContext = this._myShellGrid.DataContext as XEP_StressStrainDiagramUC_ViewModel;
            if (dataContext == null)
            {
                return;
            }
            dataContext.MaterialDataUC = MaterialData_XEP;
            string old = dataContext.SeriesTypeUC;
            dataContext.SeriesTypeUC = null;
            dataContext.SeriesTypeUC = old;
        }
    }
}
