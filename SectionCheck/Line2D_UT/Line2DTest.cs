using CommonLibrary.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using CommonLibrary.Utility;

namespace Line2D_UT
{
    
    
    /// <summary>
    ///This is a test class for Line2DTest and is intended
    ///to contain all Line2DTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Line2DTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for CommonLibrary.Geometry.ILine2D.Intersection
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CommonLibrary.dll")]
        public void IntersectionTest()
        {
            Point start = new Point(0.0, 0.0);
            Point end = new Point(150.0, 150.0);
            ILine2D target = new Line2D(start, end);
            start = new Point(200.0, -200.0);
            end = new Point(-50.0, 350.0);
            ILine2D other = new Line2D(start, end);
            Point expected = new Point(75.0, 75.0);
            Point? actual = null;
            actual = target.Intersection(other);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
            //
            other = new Line2D(start, new Point(150.0, 350.0));
            other.IsLineSegment = true;
            target.IsLineSegment = true;
            actual = target.Intersection(other);
            Assert.IsNull(actual);
            //
            target.IsLineSegment = false;
            actual = target.Intersection(other);
            Assert.IsNotNull(actual);
            Point testActual = (Point)actual;
            expected = new Point(166.666666666667, 166.666666666667);
            Assert.AreEqual(expected.X, testActual.X, 1e-6);
            Assert.AreEqual(expected.Y, testActual.Y, 1e-6);
        }

        /// <summary>
        ///A test for CommonLibrary.Geometry.ILine2D.IsPointOnLine
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CommonLibrary.dll")]
        public void IsPointOnLineTest()
        {
            ILine2D target = new Line2D(new Point(0.0, 0.0), new Point(150.0, 150.0));
            bool expected = true;
            bool actual = target.IsPointOnLine(new Point(100.0, 100.0));
            Assert.AreEqual(expected, actual);
            actual = target.IsPointOnLine(new Point(200.0, 200.0));
            Assert.AreEqual(expected, actual);
            target.IsLineSegment = true;
            actual = target.IsPointOnLine(new Point(200.0, 200.0));
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommonLibrary.Geometry.ILine2D.Lenght
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CommonLibrary.dll")]
        public void LenghtTest()
        {
            ILine2D target = new Line2D(new Point(0.0, 0.0), new Point(150.0, 150.0));
            double expected = 0.0;
            double actual = target.Lenght();
            Assert.AreEqual(expected, actual);
            target.IsLineSegment = true;
            expected = 212.132034355964;
            actual = target.Lenght();
            Assert.AreEqual(expected, actual, 1e-6);
        }

        /// <summary>
        ///A test for CommonLibrary.Geometry.ILine2D.LinePointDistance
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CommonLibrary.dll")]
        public void LinePointDistanceTest()
        {
            ILine2D target = new Line2D(new Point(0.0, 0.0), new Point(150.0, 150.0));
            double expected = 35.3553390593274;
            double actual;
            actual = target.LinePointDistance(new Point(100.0, 150.0));
            Assert.AreEqual(expected, actual, 1e-6);
            target.IsLineSegment = true;
            actual = target.LinePointDistance(new Point(100.0, 150.0));
            Assert.AreEqual(expected, actual, 1e-6);
            actual = target.LinePointDistance(new Point(150.0, 200.0));
            expected = 50.0;
            Assert.AreEqual(expected, actual, 1e-6);
        }

        /// <summary>
        ///A test for RecreateVector
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CommonLibrary.dll")]
        public void RecreateVectorTest()
        {
            //PrivateObject param0 = (PrivateObject)new Line2D(new Point(0.0, 0.0), new Point(150.0, 150.0));
            Line2D_Accessor target = new Line2D_Accessor(new Point(0.0, 0.0), new Point(150.0, 150.0));
            target.RecreateVector();
            double actual = MathUtils.ToDeg(GeometryOperations.AngleFromHorLine(target.LineVector));
            double expected = 45.0;
            Assert.AreEqual(expected, actual);
            target = new Line2D_Accessor(new Point(150.0, 150.0), new Point(0.0, 0.0));
            actual = MathUtils.ToDeg(GeometryOperations.AngleFromHorLine(target.LineVector));
            expected = -135.0;
            Assert.AreEqual(expected, actual);
        }
    }
}
