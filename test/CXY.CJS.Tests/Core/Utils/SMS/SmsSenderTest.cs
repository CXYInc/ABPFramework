using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.HttpClient;
using CXY.CJS.Core.Utils.SMS;
using CXY.CJS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace CXY.CJS.Tests.Core.Utils.SMS
{
    public class SmsSenderTest: AbpIntegratedTestBase<CJSTestModule>
    {
        private readonly ISmsSender _smsSender;
        public SmsSenderTest()
        {
            //var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            //var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            //{
            //    StatusCode = HttpStatusCode.OK,
            //    Content = new StringContent(JsonConvert.SerializeObject(new SendSmsResult
            //    {
            //        Status= true,
            //        Code = 1000,
            //        Msg = "短信发送成功"
            //    }), Encoding.UTF8, "application/json")
            //});
            //var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            //httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);

            //LocalIocManager.IocContainer.Register(
            //    Component
            //        .For<IHttpClientFactory>()
            //        .Instance(httpClientFactoryMock)
            //        .LifestyleSingleton()
            //);
            _smsSender = LocalIocManager.IocContainer.Resolve<ISmsSender>();
        }


        [Fact]
        public async Task SendSms()
        {
            var result = await _smsSender.SendSmsAsync("telephone", "pars", "templeteid", "modulename");
        }


    }
}