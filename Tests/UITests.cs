using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace Tests
{
    [TestClass]
    public class UITests : AppSession
    {

        private int _lastIndex;

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
        public void PreviousAndNextDoJumpIndexes()
        {
            while (int.Parse(session.FindElementByAccessibilityId("TextBoxId").Text) != 1)
            {
                session.FindElementByAccessibilityId("PreviousButton").Click();
            }
            session.FindElementByAccessibilityId("PreviousButton").Click();
            _lastIndex = int.Parse(session.FindElementByAccessibilityId("TextBoxId").Text);

            session.FindElementByAccessibilityId("NextButton").Click();
            int.Parse(session.FindElementByAccessibilityId("TextBoxId").Text).Should().Be(1).And.Should().NotBe(_lastIndex);
            session.FindElementByAccessibilityId("PreviousButton").Click();
            int.Parse(session.FindElementByAccessibilityId("TextBoxId").Text).Should().Be(_lastIndex);
        }

        [TestMethod]
        public void NextButtonLoadsNextPerson()
        {
            int firstId = Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text);
            session.FindElementByAccessibilityId("NextButton").Click();
            int secondId = Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text);

            secondId.Should().BeOneOf(new[] { firstId + 1, 1 }, "next button increments ID or sets it to 1 if top is reached");
        }

        [TestMethod]
        public void SpamNextButton()
        {
            for (int i = 0; i < 5; i++)
            {
                int Id = Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text);
                session.FindElementByAccessibilityId("NextButton").Click();
                Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text).Should().BeOneOf(new[] { Id + 1, 1 }, "next button increments ID or sets it to 1 if top is reached");
            }
        }

        [TestMethod]
        public void SpamPreviousButton()
        {
            for (int i = 0; i < 5; i++)
            {
                int Id = Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text);
                session.FindElementByAccessibilityId("PreviousButton").Click();
                Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text).Should().BeOneOf(new[] { Id - 1, 6 }, "previous button decrements ID or sets it to the highest element (end of list) if top is reached");
            }
        }

        [TestMethod]
        public void SaveButtonWorks()
        {
            string initialContent = session.FindElementByAccessibilityId("TextBoxFirstName").Text;

            session.FindElementByAccessibilityId("TextBoxFirstName").Click();
            session.FindElementByAccessibilityId("TextBoxFirstName").SendKeys("abc");
            int Id = Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text);
            session.FindElementByAccessibilityId("SaveButton").Click();
            session.LaunchApp();

            while (Int32.Parse(session.FindElementByAccessibilityId("TextBoxId").Text) != Id)
            {
                session.FindElementByAccessibilityId("NextButton").Click();
            }

            session.FindElementByAccessibilityId("TextBoxFirstName").Text.Should().EndWith("abc");
            session.FindElementByAccessibilityId("TextBoxFirstName").Clear();
            session.FindElementByAccessibilityId("TextBoxFirstName").SendKeys(initialContent);
            session.FindElementByAccessibilityId("SaveButton").Click();
        }

    }
}
