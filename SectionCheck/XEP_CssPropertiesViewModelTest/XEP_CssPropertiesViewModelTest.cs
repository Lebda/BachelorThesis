using System;
using System.Linq;
using XEP_SectionCheckInterfaces.DataCache;
using Microsoft.Practices.Unity;
using XEP_CssProperties.ViewModels;
using XEP_SectionCheck.ModuleDefinitions;
using XEP_SectionDrawUI.ModuleDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XEP_CssPropertiesViewModelTest
{
    
    
    /// <summary>
    ///This is a test class for XEP_CssPropertiesViewModelTest and is intended
    ///to contain all XEP_CssPropertiesViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XEP_CssPropertiesViewModelTest
    {
        static IUnityContainer _container = new UnityContainer();
        static XEP_IDataCache _dataCache = null;
        static XEP_IDataCacheService _dataCacheService = null;
        static XEP_CssPropertiesViewModel s_target = null;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            MainModule.RegisterTypes(_container);
            XEP_SectionDrawModule.RegisterTypes(_container);
            _dataCacheService = UnityContainerExtensions.Resolve<XEP_IDataCacheService>(_container);
            _dataCacheService.FileName = "UT_DataCache";
            _dataCache = UnityContainerExtensions.Resolve<XEP_IDataCache>(_container);
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _dataCacheService.Load(_dataCache);
            s_target = UnityContainerExtensions.Resolve<XEP_CssPropertiesViewModel>(_container);
        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for ActiveForce
        ///</summary>
        [TestMethod()]
        public void ActiveForceTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsNull(s_target.ActiveForce);
        }

        /// <summary>
        ///A test for CopyCommand
        ///</summary>
        [TestMethod()]
        public void CopyForceCommandTest()
        {
            // Arrange
            var target = s_target;

            // Act
            int count = target.InternalForces.Count;
            target.CopyForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, count);

            // Act
            target.ActiveForce = _dataCache.Structure.MemberData[0].SectionsData[0].InternalForces[0];
            target.CopyForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, ++count);
            Assert.AreEqual(_dataCache.Structure.MemberData[0].SectionsData[0].InternalForces[0], _dataCache.Structure.MemberData[0].SectionsData[0].InternalForces.Last());
        }

        /// <summary>
        ///A test for DeleteCommand
        ///</summary>
        [TestMethod()]
        public void DeleteForceCommandTest()
        {
            // Arrange
            var target = s_target;

            // Act
            int count = target.InternalForces.Count;
            target.DelereForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, count);

            // Act
            target.ActiveForce = _dataCache.Structure.MemberData[0].SectionsData[0].InternalForces.First();
            target.DelereForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, --count);

            // Act
            target.ActiveForce = _dataCache.Structure.MemberData[0].SectionsData[0].InternalForces.First();
            target.DelereForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, --count);

            // Act
            target.DelereForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, 0);
        }

        /// <summary>
        ///A test for NewCommand
        ///</summary>
        [TestMethod()]
        public void NewForceCommandTest()
        {
            // Arrange
            _dataCache.Structure.MemberData[0].SectionsData[0].InternalForces.Clear();
            var target = s_target;

            // Act
            target.NewForceCommand.Execute(null);

            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, 1);
        }
    }
}
