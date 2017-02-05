using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;


namespace moodleTest
{

	[TestFixture]
	public class Test
	{
		AppiumDriver<AndroidElement> driver;

		private String MOODLE_SITE = "http://46.101.125.192/moodle/";
		private String MOODLE_USERNAME = "admin";
		private String MOODLE_PASSWORD = "Ch!3eDre=er5";
		private String SEARCH_TEXT = "Data Structures";
		private Uri TEST_ENDPOINT = new Uri("http://127.0.0.1:4723/wd/hub"); // If Appium is running locally
		private TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(300); /* Change this to a more reasonable value */
		private TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(120); /* Change this to a more reasonable value - This is the time to wait when looking for an item on the page */ 
		private DesiredCapabilities cap = new DesiredCapabilities();
		private Boolean setupComplete = false;

		[SetUp]
		public void Init()
		{
			if (!setupComplete)
			{
				cap.SetCapability("deviceName", "LYO-L21");
				cap.SetCapability("appPackage", "com.moodle.moodlemobile");
				//cap.SetCapability("appActivity", "com.moodle.moodlemobile.MoodleMobile");
				///cap.SetCapability("autoLaunch", true);

				//Launch Android driver
				driver = new AndroidDriver<AndroidElement>(TEST_ENDPOINT, cap, INIT_TIMEOUT_SEC);
				driver.Manage().Timeouts().ImplicitlyWait(IMPLICIT_TIMEOUT_SEC);
				setupComplete = true;
				log("Completed setup");	
			}
		
		}


		[Test]
		public void a_appLoaded()
		{
			//verify if the application is launched
			Assert.IsNotNull(driver.Context);
			wait(1000);
		}

		[Test]
		public void b_connectSite()
		{
			
			AndroidElement siteTxtBox = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[1]/android.widget.EditText[1]");
			siteTxtBox.Clear();
			siteTxtBox.SendKeys(MOODLE_SITE);

			AndroidElement connectBtn = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[2]/android.widget.Button[1]");
			connectBtn.Click();
			wait(100);

			AndroidElement usernameTxtBox = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[1]/android.widget.EditText[1]");
			usernameTxtBox.SendKeys(MOODLE_USERNAME);

			StringAssert.Contains(usernameTxtBox.Text, MOODLE_USERNAME);

			wait(1000);
		}


		[Test]
		public void c_logUserIn()
		{
			AndroidElement usernameTxtBox = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[1]/android.widget.EditText[1]");
			usernameTxtBox.Clear();
			usernameTxtBox.SendKeys(MOODLE_USERNAME);

			AndroidElement passwordTxtBox = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[2]/android.widget.EditText[1]");
			passwordTxtBox.SendKeys(MOODLE_PASSWORD);

			AndroidElement loginBtn = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[3]/android.widget.Button[1]");
			loginBtn.Click();

			//AndroidElement sideMenuToggleBtn = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.widget.Button[1]");
			//sideMenuToggleBtn.Click();

			AndroidElement searchBtn = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View[1]/android.view.View[2]/android.view.View[1]");
			searchBtn.Click();

			AndroidElement searchTxtBox = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.widget.EditText[1]");
			searchTxtBox.SendKeys(SEARCH_TEXT);

			StringAssert.Contains(searchTxtBox.Text, SEARCH_TEXT);
			wait(1000);
		}

		[Test]
		public void d_searchCourse()
		{
			AndroidElement searchTxtBox = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.widget.EditText[1]");
			searchTxtBox.Clear();
			wait(500);
			searchTxtBox.SendKeys(SEARCH_TEXT);

			AndroidElement searchBtn = find("//android.widget.LinearLayout[1]/android.widget.FrameLayout[1]/android.webkit.WebView[1]/android.webkit.WebView[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[3]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.widget.Button[1]\n");
			searchBtn.Click();

			
		}

		string regularizeXpath(string xpathStr)
		{
			return xpathStr.Replace("[1]", "").Replace("[2]", "").Replace("[3]", "");
		}

		string elementXpath(string title)
		{
			//List 
			return "";
		}

		void log(string content)
		{
			System.Console.Error.WriteLine(content);
		}

		AndroidElement find(string xpathStr)
		{
			//var cleanXpath = regularizeXpath(xpathStr);
			return driver.FindElementByXPath(xpathStr);
		}

		void wait(int duration)
		{
			log("Now waiting for" + duration + "seconds"); 
			System.Threading.Thread.Sleep(duration);
		}
	}
}
