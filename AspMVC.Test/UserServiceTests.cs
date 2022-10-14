using AspMVC.Services;
using DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspMVC.Test {
  public class UserServiceTests {
    private UsersService usersService;
    private Mock<ISettings> settingsMoq;
    private Mock<IHttpClientProxy> httpClientProxyMoq;
    private Mock<NewHttpContent> contentMoq;

    [SetUp]
    public void Setup() {
      settingsMoq = new Mock<ISettings>(MockBehavior.Strict);
      httpClientProxyMoq = new Mock<IHttpClientProxy>(MockBehavior.Strict);
      contentMoq = new Mock<NewHttpContent>();
      usersService = new UsersService(settingsMoq.Object, () => httpClientProxyMoq.Object);
    }

    [Test]
    public void GetAllItemsTest() {
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.GetAsync("test"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = contentMoq.Object
          });
          task.Start();
          return task;
        });
      httpClientProxyMoq.Setup(client => client.Dispose());
      contentMoq.Setup(content => content.ReadAsStringAsync()).
        Returns(new Task<string>(() => TestResource.UserServiceGetResult));
      IEnumerable<UserDTO> users = usersService.GetAllItems().GetAwaiter().GetResult();
      int expected = 4;
      Assert.Equals(expected, users.Count());
    }
  }

  public class NewHttpContent : HttpContent {
    protected override Task SerializeToStreamAsync(Stream stream, TransportContext context) {
      throw new System.NotImplementedException();
    }
    protected override bool TryComputeLength(out long length) {
      throw new System.NotImplementedException();
    }
    public new virtual Task<string> ReadAsStringAsync() {
      return new Task<string>(() => TestResource.UserServiceGetResult);
    }
  }
}