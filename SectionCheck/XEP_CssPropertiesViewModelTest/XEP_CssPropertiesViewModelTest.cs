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
        ///A test for XEP_CssPropertiesViewModel Constructor
        ///</summary>
        [TestMethod()]
        public void XEP_CssPropertiesViewModelConstructorTest()
        {
//             XEP_IDataCache dataCache = null; // TODO: Initialize to an appropriate value
//             XEP_UnityResolver<XEP_IInternalForceItem> resolverForce = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel target = new XEP_CssPropertiesViewModel(dataCache, resolverForce);
//             Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CanCopyExecute
        ///</summary>
        [TestMethod()]
        [DeploymentItem("XEP_CssProperties.dll")]
        public void CanCopyExecuteTest()
        {
//             PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel_Accessor target = new XEP_CssPropertiesViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//             bool expected = false; // TODO: Initialize to an appropriate value
//             bool actual;
//             actual = target.CanCopyExecute();
//             Assert.AreEqual(expected, actual);
//             Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CanDeleteExecute
        ///</summary>
        [TestMethod()]
        [DeploymentItem("XEP_CssProperties.dll")]
        public void CanDeleteExecuteTest()
        {
//             PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel_Accessor target = new XEP_CssPropertiesViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//             bool expected = false; // TODO: Initialize to an appropriate value
//             bool actual;
//             actual = target.CanDeleteExecute();
//             Assert.AreEqual(expected, actual);
//             Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CopyExecute
        ///</summary>
        [TestMethod()]
        [DeploymentItem("XEP_CssProperties.dll")]
        public void CopyExecuteTest()
        {
//             PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel_Accessor target = new XEP_CssPropertiesViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//             target.CopyExecute();
//             Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DeleteExecute
        ///</summary>
        [TestMethod()]
        [DeploymentItem("XEP_CssProperties.dll")]
        public void DeleteExecuteTest()
        {
//             PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel_Accessor target = new XEP_CssPropertiesViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//             target.DeleteExecute();
//             Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for NewExecute
        ///</summary>
        [TestMethod()]
        [DeploymentItem("XEP_CssProperties.dll")]
        public void NewExecuteTest()
        {
//             PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel_Accessor target = new XEP_CssPropertiesViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//             target.NewExecute();
//             Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ResetForm
        ///</summary>
        [TestMethod()]
        [DeploymentItem("XEP_CssProperties.dll")]
        public void ResetFormTest()
        {
//             PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel_Accessor target = new XEP_CssPropertiesViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//             target.ResetForm();
//             Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ActiveForce
        ///</summary>
        [TestMethod()]
        public void ActiveForceTest()
        {
//             XEP_IDataCache dataCache = null; // TODO: Initialize to an appropriate value
//             XEP_UnityResolver<XEP_IInternalForceItem> resolverForce = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel target = new XEP_CssPropertiesViewModel(dataCache, resolverForce); // TODO: Initialize to an appropriate value
//             XEP_IInternalForceItem expected = null; // TODO: Initialize to an appropriate value
//             XEP_IInternalForceItem actual;
//             target.ActiveForce = expected;
//             actual = target.ActiveForce;
//             Assert.AreEqual(expected, actual);
//             Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CopyCommand
        ///</summary>
        [TestMethod()]
        public void CopyCommandTest()
        {
//             XEP_IDataCache dataCache = null; // TODO: Initialize to an appropriate value
//             XEP_UnityResolver<XEP_IInternalForceItem> resolverForce = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel target = new XEP_CssPropertiesViewModel(dataCache, resolverForce); // TODO: Initialize to an appropriate value
//             ICommand actual;
//             actual = target.CopyCommand;
//             Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteCommand
        ///</summary>
        [TestMethod()]
        public void DeleteCommandTest()
        {
//             XEP_IDataCache dataCache = null; // TODO: Initialize to an appropriate value
//             XEP_UnityResolver<XEP_IInternalForceItem> resolverForce = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel target = new XEP_CssPropertiesViewModel(dataCache, resolverForce); // TODO: Initialize to an appropriate value
//             ICommand actual;
//             actual = target.DeleteCommand;
//             Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InternalForces
        ///</summary>
        [TestMethod()]
        public void InternalForcesTest()
        {
//             XEP_IDataCache dataCache = null; // TODO: Initialize to an appropriate value
//             XEP_UnityResolver<XEP_IInternalForceItem> resolverForce = null; // TODO: Initialize to an appropriate value
//             XEP_CssPropertiesViewModel target = new XEP_CssPropertiesViewModel(dataCache, resolverForce); // TODO: Initialize to an appropriate value
//             ObservableCollection<XEP_IInternalForceItem> expected = null; // TODO: Initialize to an appropriate value
//             ObservableCollection<XEP_IInternalForceItem> actual;
//             target.InternalForces = expected;
//             actual = target.InternalForces;
//             Assert.AreEqual(expected, actual);
//             Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NewCommand
        ///</summary>
        [TestMethod()]
        public void NewCommandTest()
        {
            // Arrange
            IUnityContainer container = new UnityContainer();
            MainModule.RegisterTypes(container);
            XEP_IDataCacheService dataCacheService = UnityContainerExtensions.Resolve<XEP_IDataCacheService>(container);
            dataCacheService.FileName = "UT_DataCache";
            XEP_IDataCache dataCache = UnityContainerExtensions.Resolve<XEP_IDataCache>(container);
            dataCacheService.Load(dataCache);

            // Act
            (dataCache.Structure.MemberData.Values.First()).SectionsData.First().Value.InternalForces.Clear();
            var target = new XEP_CssPropertiesViewModel(dataCache, UnityContainerExtensions.Resolve<XEP_IResolver<XEP_InternalForceItem>>(container));

            // Assert
            target.NewCommand.Execute(null);
            Assert.IsNull(target.ActiveForce);
            Assert.AreEqual(target.InternalForces.Count, 1);


        }
    }
}
