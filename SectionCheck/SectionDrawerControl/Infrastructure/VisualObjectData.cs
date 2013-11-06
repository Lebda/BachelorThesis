using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public class VisualObjectData
    {
        public delegate void DrawDelegate(VisualObjectData visual);

        DrawDelegate _callBack4Change = null;
        public DrawDelegate CallBack4Change
        {
            get { return _callBack4Change; }
            set { _callBack4Change = value; }
        }
        DrawDelegate _delagate4Draw = null;
        public DrawDelegate Delagate4Draw
        {
            get { return _delagate4Draw; }
            set { _delagate4Draw = value; }
        }
        IVisualShapes _visualShape = VisualFactory.Instance().CreateShapes();
        public IVisualShapes VisualShape
        {
            get { return _visualShape; }
            set { _visualShape = value; }
        }
        Visual _visual = new DrawingVisual();
        public System.Windows.Media.DrawingVisual VisualObject
        {
            get { return (DrawingVisual)_visual; }
            set { _visual = value; }
        }
        //
        public VisualObjectData(Visual visual)
        {
            _visual = visual;
        }
        public VisualObjectData()
        {
        }
        //
        public void Draw()
        {
            if (_delagate4Draw == null)
            {
                return;
            }
            _delagate4Draw(this);
        }
        //
        public void CallBack4ShapeChanged()
        {
            if (CallBack4Change == null)
            {
                return;
            }
            CallBack4Change(this);
        }
    }
}
