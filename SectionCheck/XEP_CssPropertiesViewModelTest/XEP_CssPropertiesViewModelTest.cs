using XEP_CssProperties.ViewModels;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XEP_SectionCheckCommon.DataCache;
using XEP_Prism.Infrastructure;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Moq;
using XEP_SectionCheckCommon.Interfaces;
using SectionCheck.Services;
using XEP_SectionCheckCommon.Infrastructure;
using Microsoft.Practices.Unity;
using XEP_SectionCheck.ModuleDefinitions;
using System.Collections.Generic;

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
            var target = new XEP_CssPropertiesViewModel(_dataCache, UnityContainerExtensions.Resolve<XEP_IResolver<XEP_IInternalForceItem>>(_container));

            // Act

            // Assert
            Assert.IsNull(target.ActiveForce);
        }

        /// <summary>
        ///A test for CopyCommand
        ///</summary>
        [TestMethod()]
        public void CopyForceCommandTest()
        {
            // Arrange
            var target = new XEP_CssPropertiesViewModel(_dataCache, UnityContainerExtensions.Resolve<XEP_IResolver<XEP_IInternalForceItem>>(_container));

            // Act
            int count = target.InternalForces.Count;
            target.CopyForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, count);

            // Act
            target.ActiveForce = _dataCache.Structure.MemberData.Values.First().SectionsData.First().Value.InternalForces.First();
            target.CopyForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, ++count);
            Assert.AreEqual(_dataCache.Structure.MemberData.Values.First().SectionsData.First().Value.InternalForces.First(), _dataCache.Structure.MemberData.Values.First().SectionsData.First().Value.InternalForces.Last());
        }

        /// <summary>
        ///A test for DeleteCommand
        ///</summary>
        [TestMethod()]
        public void DeleteForceCommandTest()
        {
            // Arrange
            var target = new XEP_CssPropertiesViewModel(_dataCache, UnityContainerExtensions.Resolve<XEP_IResolver<XEP_IInternalForceItem>>(_container));

            // Act
            int count = target.InternalForces.Count;
            target.DelereForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, count);

            // Act
            target.ActiveForce = _dataCache.Structure.MemberData.Values.First().SectionsData.First().Value.InternalForces.First();
            target.DelereForceCommand.Execute(null);
            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, --count);

            // Act
            target.ActiveForce = _dataCache.Structure.MemberData.Values.First().SectionsData.First().Value.InternalForces.First();
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
            (_dataCache.Structure.MemberData.Values.First()).SectionsData.First().Value.InternalForces.Clear();
            var target = new XEP_CssPropertiesViewModel(_dataCache, UnityContainerExtensions.Resolve<XEP_IResolver<XEP_IInternalForceItem>>(_container));

            // Act
            target.NewForceCommand.Execute(null);

            // Assert
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, 1);
        }
    }
}
