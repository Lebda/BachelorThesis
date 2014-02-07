using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using XEP_SectionCheckInterfaces.DataCache;

namespace XEP_CssProperties.Views
{
    /// <summary>
    /// Interaction logic for XEP_CrossSectionView.xaml
    /// </summary>
    public partial class XEP_CrossSectionView : UserControl
    {
        public XEP_CrossSectionView()
        {
            InitializeComponent();
        }
        public void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                XEP_IESDiagramItem data = closestDataPoint.DataPoint.DataItem as XEP_IESDiagramItem;
                if (data != null)
                {
                    this.strainActual.Text = data.Strain.ManagedValue.ToString("0,0.00");
                    this.stressActual.Text = data.Stress.ManagedValue.ToString("0,0.00");
                }
            }
        }
    }
}
