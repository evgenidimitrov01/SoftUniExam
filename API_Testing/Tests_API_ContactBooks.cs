using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace API_Testing
{
    [TestFixture]
    public class Tests
    {
        const string baseUrl = "https://contactbook.nakov.repl.co/api";
        private RestClient client;
        [SetUp]
        public void Setup()
        {
            this.client = new RestClient(baseUrl);
            this.client.Timeout = 3000;
        }

        #region GET requests
        [Test, Category("API Tests")]
        public void Test_ListAllApiEndpoints()
        {
            //Arrange
            var request = new RestRequest(Method.GET);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<EndPointsResponse> endPoints = new JsonDeserializer().Deserialize<List<EndPointsResponse>>(response);
            foreach (EndPointsResponse endPoint in endPoints)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(endPoint.Route));
                Assert.IsTrue(!string.IsNullOrEmpty(endPoint.Method));
            }
        }

        [Test, Category("API Tests")]
        public void Test_ListContacts_FirstIsSveveJobs()
        {
            //Arrange
            var request = new RestRequest("/contacts", Method.GET);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            List<ContactsResponse> contacts = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);
            ContactsResponse firstContact = contacts[0];
            Assert.AreEqual(1, firstContact.Id);
            Assert.AreEqual("Steve", firstContact.FirstName);
            Assert.AreEqual("Jobs", firstContact.LastName);
            Assert.IsTrue(Helpers.IsValidEmail(firstContact.Email));
            Assert.IsTrue(!string.IsNullOrEmpty(firstContact.Phone));
            Assert.IsTrue(!string.IsNullOrEmpty(firstContact.Comments));
        }

        [Test, Category("API Tests")]
        public void Test_SearchByValidKeyword()
        {
            //Arrange
            string keyword = "albert";
            var request = new RestRequest("contacts/search/" + keyword, Method.GET);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            List<ContactsResponse> searchedContact = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);
            ContactsResponse contact = searchedContact[0];
            Assert.IsTrue(contact.Id > 0);
            Assert.AreEqual("Albert", contact.FirstName);
            Assert.AreEqual("Einstein", contact.LastName);
            Assert.IsTrue(Helpers.IsValidEmail(contact.Email));
            Assert.IsTrue(!string.IsNullOrEmpty(contact.Phone));
            Assert.IsTrue(!string.IsNullOrEmpty(contact.Comments));
        }

        [Test, Category("API Tests")]
        public void Test_SearchByInvalidKeyword()
        {
            //Arrange
            string keyword = "missing" + Helpers.GetRandomNumber();
            var request = new RestRequest("contacts/search/" + keyword, Method.GET);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            List<ContactsResponse> searchedContact = new JsonDeserializer().Deserialize<List<ContactsResponse>>(response);
            Assert.IsTrue(searchedContact.Count == 0);
        }
        #endregion

        #region POST Requests
        [Test, Category("API Tests")]
        public void Test_CreateInvalidContact()
        {
            //Arrange
            var request = new RestRequest("contacts", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Contact contact = new Contact()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Comments = string.Empty
            };
            string body = JsonConvert.SerializeObject(contact,
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            JObject message = (JObject)JsonConvert.DeserializeObject(response.Content);
            string res = message["errMsg"].ToString();
            Assert.AreEqual("First name cannot be empty!", res);
        }

        [Test, Category("API Tests")]
        public void Test_CreateContact()
        {
            //Arrange
            var request = new RestRequest("contacts", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Contact newContact = new Contact()
            {
                FirstName = Helpers.GetRandomString(10),
                LastName = Helpers.GetRandomString(10),
                Email = "evgeni@abv.bg",
                Phone = "+1234567890",
                Comments = "Random comment: " + Helpers.GetRandomString(20)
            };
            string body = JsonConvert.SerializeObject(newContact,
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            //Act
            var getRequest = new RestRequest("/contacts", Method.GET);
            IRestResponse getResponse = client.Execute(getRequest);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);

            List<ContactsResponse> contacts = new JsonDeserializer().Deserialize<List<ContactsResponse>>(getResponse);

            Assert.That(contacts.Any(cont => cont.FirstName == newContact.FirstName));
        }
        #endregion

    }
}