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

    [SetUp]
    public void Setup() {
      settingsMoq = new Mock<ISettings>(MockBehavior.Strict);
      httpClientProxyMoq = new Mock<IHttpClientProxy>(MockBehavior.Strict);
      usersService = new UsersService(settingsMoq.Object, () => httpClientProxyMoq.Object);
    }

    [Test]
    public void GetAllItemsTest() {
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.GetAsync("test"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.UserServiceGetResult)
          });
          task.Start();
          return task;
        });
      httpClientProxyMoq.Setup(client => client.Dispose());
      IEnumerable<UserDTO> users = usersService.GetAllItems().GetAwaiter().GetResult();
      int expectedUserCount = 4;
      int resultUserCount = users.Count();
      Assert.AreEqual(expectedUserCount, resultUserCount);
    }
  }
}