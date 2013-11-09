using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommonLibrary.InterfaceObjects;

namespace CommonLibrary.Interfaces
{
    public interface IStressStrainShapeLoop
    {
        void Prepare(IStrainStressShape shape);
        void DoLoop(double valueScale);
    }

    public interface IStrainStressShape
    {
        ILine2D NeuAxis { get; set; }
        ILine2D BaseLine { get;}
        List<ILine2D> LinesInFibers { get; }
        PointCollection WholeShape { get;}
        PointCollection ValueShape { get; }
        List<StrainStressItem> Items { get; }
        IStressStrainShapeLoop Loop { get; set; }
        // Methods
        double GetMaxValue(List<StrainStressItem> items);
        void ScaleValues(double scale);
        /// <summary>
        /// Translate shape in direction of neutral axis
        /// </summary>
        /// <param name="offset"></param>
        void TranslateInDirNeuAxis(double offset);
        void Transform(Matrix conventer);
        void SetPointValues(List<StrainStressItem> data, double valueScale);
        void SetPointValues4MaxWidth(List<StrainStressItem> data, double maxValue);
    }

    public interface ILine2D
    {
        /// <summary>
        /// return null for no intersection
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        Point? Intersection(ILine2D other);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>line angle in [rad]</returns>
        double GetAngle();
        ILine2D GetPerpendicularLine(Point pointOnNewLine);
        ILine2D GetParallelLine(Point pointOnNewLine);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromStartPoint"></param>
        /// <param name="angle">in [rad]</param>
        /// <returns></returns>
        ILine2D GetLineInAngle(Point pointOnNewLine, double angle);
        double LinePointDistance(Point testPoint);
        bool IsPointOnLine(Point testPoint);
        double Lenght();
        Vector LineVector { get; }
        Point StartPoint { get; set; }
        Point EndPoint { get; set; }
        bool IsLineSegment { get; set; }
        double A { get; }
        double B { get; }
        double C { get; }
    }

    public interface IDataInFiber
    {
    }

    public interface ICssDataFiber
    {
        Dictionary<string, IDataInFiber> Data{get; set;}
        Point Point {get; set;}
        int Index{get; set;}
        double DistanceFromNeuAxis{get; set;}
        // Methods
        void AddFiberData(string dataName, IDataInFiber fiberData);
        void SetFiberData(string dataName, IDataInFiber fiberData);
        T GetFiberData<T>(string dataName) where T : class;

    }
}
