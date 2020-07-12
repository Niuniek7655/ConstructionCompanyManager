using Microsoft.Extensions.Options;

namespace CCM.Constants.Tests
{
    public class SettingsMock : IOptions<Settings>
    {
        public SettingsMock()
        {
            Value = new Settings();
            Value.BasicAccessMessage = CreateAccessMessage();
            Value.BasicAccessSenderData = CreateBasicAccessSenderData();
            Value.HttpRequestBuilderData = CreateHttpRequestBuilderData();
            Value.IncorrectEnumType = "Incorrect enum type";
        }

        private BasicAccessMessage CreateAccessMessage()
        {
            BasicAccessMessage accessMessage = new BasicAccessMessage();
            accessMessage.Ms1 = "User login correct to application";
            accessMessage.Ms2 = "User can not login to applicationn";
            accessMessage.Ms3 = "Can not find user";
            accessMessage.Ms4 = "User account create correct";
            accessMessage.Ms5 = "Can not register user correct";
            accessMessage.Ms6 = "Unexpected error";
            return accessMessage;
        }

        private BasicAccessSenderData CreateBasicAccessSenderData()
        {
            BasicAccessSenderData basicAccessSenderData = new BasicAccessSenderData();
            basicAccessSenderData.AccessClient = "AccessClient";
            basicAccessSenderData.LoginUri = "Basic/Login";
            basicAccessSenderData.RegisterUri = "Basic/Register";
            return basicAccessSenderData;
        }

        private HttpRequestBuilderData CreateHttpRequestBuilderData()
        {
            HttpRequestBuilderData httpRequestBuilderData = new HttpRequestBuilderData();
            httpRequestBuilderData.FirstBuildError = "Add Uri and HTTP method before you add another elements";
            httpRequestBuilderData.SecondBuildError = "Incorrect type to build object";
            httpRequestBuilderData.ThirdBuildError = "Can not add HttpMethod twice";
            httpRequestBuilderData.FourthBuildError = "Can not add request URI twice";
            httpRequestBuilderData.FifthBuildError = "Can not add body twice";
            httpRequestBuilderData.MediaType = "application/json";
            return httpRequestBuilderData;
        }

        public Settings Value { get; private set; }
    }
}
