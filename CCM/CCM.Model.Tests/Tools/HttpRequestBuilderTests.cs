using CCM.Constants.Tests;
using CCM.Domain.Tools;
using CCM.Model.Exceptions;
using CCM.Model.Tools;
using System.Net.Http;
using Xunit;
using Newtonsoft.Json;

namespace CCM.Model.Tests.Tools
{
    public class HttpRequestBuilderTests
    {
        private HttpRequestBuilder CreateTestHttpRequestBuilder()
        {
            HttpRequestBuilderLoggerMock loggerMock = new HttpRequestBuilderLoggerMock();
            SettingsMock settingsMock = new SettingsMock();
            HttpRequestBuilder builder = new HttpRequestBuilder(loggerMock, settingsMock);
            return builder;
        }

        #region AddContent
        [Fact]
        public void AddContent_AddContentForNullRequestMessage_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder builder = CreateTestHttpRequestBuilder();
            object testObject = new object();

            //Act
            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => builder.AddContent(testObject));
        }

        [Fact]
        public void AddContent_AddContentForNotNullRequestMessage_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();
            IHttpRequestBuilder builder = testBuilder
                                                    .AddHttpMethod(HttpMethod.Get)
                                                    .AddUri("test");
            object testObject = new object();
            //Act
            builder.AddContent(testObject);

            //Assert
            Assert.Equal(testBuilder, builder);
        }

        [Fact]
        public void AddContent_AddContentForNotNullRequestMessageTwice_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();
            IHttpRequestBuilder builder = testBuilder
                                                    .AddHttpMethod(HttpMethod.Get)
                                                    .AddUri("test");
            object testObject = new object();
            object testObject_2 = new object();
            //Act
            builder.AddContent(testObject);

            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => builder.AddContent(testObject_2));
        }
        #endregion AddContent

        #region AddHeader
        [Fact]
        public void AddHeader_AddHeaderForNullRequestMessage_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder builder = CreateTestHttpRequestBuilder();
            RequestHeader header = new RequestHeader("test", "test");

            //Act
            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => builder.AddHeader(header));
        }

        [Fact]
        public void AddHeader_AddHeaderForNotNullRequestMessage_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();
            IHttpRequestBuilder builder = testBuilder
                                                    .AddHttpMethod(HttpMethod.Get)
                                                    .AddUri("test");

            RequestHeader header = new RequestHeader("test", "test");
            //Act
            builder.AddHeader(header);

            //Assert
            Assert.Equal(testBuilder, builder);
        }

        [Fact]
        public void AddHeader_AddHeaderForNotNullRequestMessageSeveralTimes_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();
            IHttpRequestBuilder builder = testBuilder
                                                    .AddHttpMethod(HttpMethod.Get)
                                                    .AddUri("test");

            RequestHeader header = new RequestHeader("test", "test");
            RequestHeader header_2 = new RequestHeader("test_2", "test_2");
            RequestHeader header_3 = new RequestHeader("test_3", "test_3");
            //Act
            builder.AddHeader(header);
            builder.AddHeader(header_2);
            builder.AddHeader(header_3);

            //Assert
            Assert.Equal(testBuilder, builder);
        }
        #endregion AddHeader

        #region AddHttpMethod
        [Fact]
        public void AddHttpMethod_AddHttpMethodIfMethodAndUriNotAdd_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder.AddHttpMethod(HttpMethod.Get);

            //Assert
            Assert.Equal(testBuilder, builder);
        }

        [Fact]
        public void AddHttpMethod_AddHttpMethodIfMethodAddButUriNotAdd_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder.AddHttpMethod(HttpMethod.Get);

            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => builder.AddHttpMethod(HttpMethod.Post));
        }

        [Fact]
        public void AddHttpMethod_AddHttpMethodIfMethodNotAddAndUriAdd_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder
                                                .AddUri("test")
                                                .AddHttpMethod(HttpMethod.Get);

            //Assert
            Assert.Equal(testBuilder, builder);
        }
        #endregion AddHttpMethod

        #region AddUri
        [Fact]
        public void AddUri_AddUriIfMethodAndUriNotAdd_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder.AddUri("test");

            //Assert
            Assert.Equal(testBuilder, builder);
        }

        [Fact]
        public void AddUri_AddUriIfUriAddButMethodNotAdd_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder.AddUri("test");

            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => builder.AddUri("test_2"));
        }

        [Fact]
        public void AddUri_AddUriIfUriNotAddAndMethodAdd_ReturnIHttpRequestBuilder()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder
                                                .AddHttpMethod(HttpMethod.Get)
                                                .AddUri("test");

            //Assert
            Assert.Equal(testBuilder, builder);
        }
        #endregion AddUri

        #region Build
        [Fact]
        public void Build_BuildNullRequestForIncorrectType_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => testBuilder.Build<HttpMethod>());
        }

        [Fact]
        public void Build_BuildNullRequestForCorrectType_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => testBuilder.Build<HttpRequestMessage>());
        }

        [Fact]
        public void Build_BuildRequestForIncorrectType_ThrowHttpRequestBuilderException()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();

            //Act
            IHttpRequestBuilder builder = testBuilder
                                                .AddHttpMethod(HttpMethod.Get)
                                                .AddUri("test");

            //Assert
            Assert.Throws<HttpRequestBuilderException>(() => builder.Build<HttpMethod>());
        }

        [Fact]
        public void Build_BuildRequestForCorrectType_ReturnHttpRequestMessage()
        {
            //Arrange
            HttpRequestBuilder testBuilder = CreateTestHttpRequestBuilder();
            HttpRequestMessage testMessage = new HttpRequestMessage(HttpMethod.Get, "test");

            //Act
            HttpRequestMessage message = testBuilder
                                                .AddHttpMethod(HttpMethod.Get)
                                                .AddUri("test")
                                                .Build<HttpRequestMessage>();

            //Assert
            string messageJson = JsonConvert.SerializeObject(message);
            string testMessageJson = JsonConvert.SerializeObject(testMessage);
            Assert.Equal(testMessageJson, messageJson);
        }
        #endregion Build
    }
}
