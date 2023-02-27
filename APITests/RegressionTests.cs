
using APIDemo;
using APIDemo.DTO;
using APITesting;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace APITests
{
    [TestClass]
    public class RegressionTests
    {
        public TestContext TestContext { get; set; }
        public HttpStatusCode statusCode;

        [ClassInitialize]
        public static void Setup(TestContext testContext) //revisado
        {
            var dir = testContext.TestRunDirectory;
            Reporter.SetupExtentReport("API Regression Test", "API Regression Test Report", dir);
        }

        [TestInitialize]
        public void SetupTest() //revisado
        {
            Reporter.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public  void CleanupTest() //revisado

        {
            var testStatus = TestContext.CurrentTestOutcome;
            Status logStatus;

            switch (testStatus)
            {
                case UnitTestOutcome.Failed:
                    logStatus = Status.Fail;
                    Reporter.TestStatus(logStatus.ToString());
                    
                    break;
                case UnitTestOutcome.Inconclusive:

                    break;
                case UnitTestOutcome.Passed:
                    logStatus = Status.Pass;
                    Reporter.TestStatus(logStatus.ToString());
                   
                    break;
                case UnitTestOutcome.InProgress:

                    break;
                case UnitTestOutcome.Error:

                    break;
                case UnitTestOutcome.Timeout:

                    break;
                case UnitTestOutcome.Aborted:

                    break;
                case UnitTestOutcome.Unknown:

                    break;
                case UnitTestOutcome.NotRunnable:

                    break;
                default:
                    break;

            }
        }
        [ClassCleanup]
        public static void  Cleanup() //revisado
        {
            Reporter.FlushReport();
        }


        [TestMethod]
        public void VerifyListOfUsers()
        {
            var demo = new Demo<ListOfUsersDTO>();
            var user = demo.GetUsers("api/users?page=2");
            Assert.AreEqual(2, user.Page, "Page number does not match");
          
            Reporter.LogToReport(Status.Fail, "Page number does not match");
            Assert.AreEqual("Michael", user.Data[0].first_name);
            Reporter.LogToReport(Status.Fail, "User first name  does not match");
        }

        [DeploymentItem("TestData\\TestCase.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "TestCase.csv", "TestCase#csv", DataAccessMethod.Sequential)]
        [TestMethod]
        public void CreateNewUser()
        {
            var users = new CreateUserRequestDTO();
            users.Name= TestContext.DataRow["name"].ToString();
            Reporter.LogToReport(Status.Info, "Test data for name  is: " + users.Name);
            users.Job = TestContext.DataRow["job"].ToString();
            Reporter.LogToReport(Status.Info, "Test data for Job  is: " + users.Name);

            var demo = new Demo<CreateUserDTO>();
            var user = demo.CreateUser("api/users", users);
            Assert.AreEqual("Mike", user.Name);
            Reporter.LogToReport(Status.Pass, "Name is matching: " + users.Name);
            Assert.AreEqual("Lead", user.Job);
            Reporter.LogToReport(Status.Pass, "Job is matching: " + users.Job);

            var demoOne = new Demo<ListOfUsersDTO>();
            var userOne = demoOne.GetUsers("api/users?page=2");
            Assert.AreEqual(2, userOne.Page);
            Assert.AreEqual("Michael", userOne.Data[0].first_name);
        }

        [DeploymentItem("TestData\\CreateUser.json")]
        [TestMethod]
        public void createNewUserWithValidInput()
        {
            var payload = HandleContent.parseJson <CreateUserDTO> ("CreateUser.json");

            var api = new APIHelper<CreateUserDTO>();
            var response = api.createUser("api/users",payload);

            var content = HandleContent.getContent<CreateUserDTO>(response);

            Assert.AreEqual(payload.Name, content.Name);
       
            Assert.AreEqual(payload.Job, content.Job);
        }

     


    }
}
