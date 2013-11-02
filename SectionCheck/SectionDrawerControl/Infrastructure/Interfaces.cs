﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public interface IVisualShapes
    {
        PathGeometry BaseGeo { get; set; }
        PathGeometry RenderedGeo { get; }
        void UpdateRenderedGeometry(MatrixTransform conventer);
    }

    public interface IPathGeometryCreator
    {
        PathGeometry Create();
    }

    public interface IVisualObejctDrawingData
    {
        Pen GetPen();
        Brush GetBrush();
    }
}
