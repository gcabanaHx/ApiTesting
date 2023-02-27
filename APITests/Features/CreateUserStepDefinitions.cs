using APIDemo;
using APIDemo.DTO;
using APITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace APITests
{
    [Binding]
    public class CreateUserStepDefinitions
    {

        private const string Base_URL = "https://reqres.in";
        private readonly CreateUserDTO createUserDto;
        private RestResponse response;
        public CreateUserStepDefinitions(CreateUserDTO createUserDto)
        {
            this.createUserDto = createUserDto;
        }
        [Given(@"I input name ""([^""]*)""")]
        public void GivenIInputName(string name)
        {
           createUserDto.Name = name;   
        }

        [Given(@"I input role ""([^""]*)""")]
        public void GivenIInputRole(string role)
        {
            createUserDto.Job = role;
        }

        [When(@"I send create user request")]
        public void WhenISendCreateUserRequest()
        {
            var api = new APIHelper<CreateUserDTO>();
            response = api.createUser("api/users",createUserDto);
        }

        [Then(@"validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            var content = HandleContent.getContent<CreateUserDTO>(response);
            Assert.AreEqual(createUserDto.Name, content.Name);
            Assert.AreEqual(createUserDto.Job, content.Job);
        }
    }
}
