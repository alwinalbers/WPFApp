using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace Tests
{
    [TestClass]
    public class UITests : AppSession
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch app window
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void LoadsFirstPersonCorrectly()
        {            
            Assert.AreEqual("1",session.FindElementByAccessibilityId("TextBoxId").Text);
        }


    }
}
