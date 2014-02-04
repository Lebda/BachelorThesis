using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using XEP_CommonLibrary.Interfaces;
using XEP_SectionCheckInterfaces.DataCache;

namespace XEP_SectionCheckInterfaces.SectionDrawer
{
    public interface IPathGeometryCreator
    {
        PathGeometry Create();
    }

    public interface IVisualObejctDrawingData
    {
        Pen GetPen();
        Brush GetBrush();
    }

    public interface XEP_ICssDataBase : IPathGeometryCreator, IVisualObejctDrawingData
    {
        Brush VisualBrush { get; set; }
        Pen VisualPen { get; set; }
    }

    public interface XEP_ICssDataCompressPart : XEP_ICssDataBase
    {
        PointCollection CssCompressPart { get; set; }
    }

    public interface XEP_ICssDataOneReinf : ICloneable
    {
        double Diam { get; set; }   
        Point BarPoint { get; set; }
        double BarArea { get; set; }
    }

    public interface XEP_ICssDataReinforcement : XEP_ICssDataBase
    {
        ObservableCollection<XEP_ICssDataOneReinf> BarData { get; set; }
    }

    public interface XEP_ICssDataShape : XEP_ICssDataBase, ICloneable
    {
        ObservableCollection<XEP_ISectionShapeItem> CssShapeOuter { get; set; }
        ObservableCollection<XEP_ISectionShapeItem> CssShapeInner { get; set; }
        void RecreateShape(ObservableCollection<XEP_ISectionShapeItem> cssShapeOuter, ObservableCollection<XEP_ISectionShapeItem> cssShapeInner);
    }
    
    public interface XEP_ICssDataFibers : XEP_ICssDataBase
    {
        IGeometryMaker GeometryMaker { get; set; }
        ILine2D NeuAxis { get; set; }
        List<ICssDataFiber> Fibers { get; set; }
        PathGeometry GetStrainGeometry(double CssWidth, bool move = true, IStrainStressShape dataDependentObject = null);
        PathGeometry GetStressGeometry(double CssWidth, bool move = true, IStrainStressShape baseLineFromDependentObject = null);
    }
}
