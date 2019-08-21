using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class AppSession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723"; // '/wd/hub' falls appium anhängen
        private const string AppId = @"C:\Users\AlbersAl\Work\WPFApp\WPFApplication\bin\Debug\WPFApplication.exe";
        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context)
        {
            // Launch application if it is not yet launched
            if (session == null)
            {
                // Create a new session to bring up an instance of the application
                // Note: Multiple app-windows (instances) share the same process Id
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", AppId);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(session);

                session.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}
