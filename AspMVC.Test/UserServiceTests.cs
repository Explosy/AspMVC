using AspMVC.Services;
using DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspMVC.Test {
  public class UserServiceTests {
    private UsersService usersService;
    private Mock<ISettings> settingsMoq;
    private Mock<IHttpClientProxy> httpClientProxyMoq;
    private Mock<IHttpContentProxy> httpContentProxyMoq;
    private UserDTO testUser;
    [SetUp]
    public void Setup() {
      settingsMoq = new Mock<ISettings>(MockBehavior.Strict);
      httpClientProxyMoq = new Mock<IHttpClientProxy>(MockBehavior.Strict);
      httpContentProxyMoq = new Mock<IHttpContentProxy>(MockBehavior.Strict);
      usersService = new UsersService(settingsMoq.Object, () => httpClientProxyMoq.Object, (string json) => httpContentProxyMoq.Object );
      testUser = new UserDTO() {
        Id = 2,
        Name = "Andrey",
        Surname = "Kopilov",
        Age = 28,
        Email = "E-mail2",
        RegistationDate = new DateTime(2022, 10, 12)
      };
    }

    [Test]
    public void GetAllItemsTest() {
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.GetAsync("test"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.UserServiceGetAllResult, Encoding.UTF8, "application/json")
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

    [Test]
    public void GetItemById() {
      int id = 2;
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.GetAsync($"test{id}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.UserServiceGetItemResult, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });
      
      UserDTO user = usersService.GetItemById(id).GetAwaiter().GetResult();
      
      Assert.True(testUser.Equals(user));
    }

    [Test]
    public void FindItemsByProperty() {
      string email = "E-mail2";
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.GetAsync($"test?Email={email}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.ExpectedFindContent, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });
      
      IEnumerable<UserDTO> users = usersService.GetItems(email).GetAwaiter().GetResult();
      
      Assert.True(testUser.Equals(users.First()));
    }

    [Test]
    public void CreateUser() {

      StringContent content = new StringContent(TestResource.UpdateContent, Encoding.UTF8, "application/json");
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpContentProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.PostAsync($"test", httpContentProxyMoq.Object))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            StatusCode = HttpStatusCode.OK
          });
          task.Start();
          return task;
        });
      
      bool result = usersService.CreateItem(testUser).GetAwaiter().GetResult();

      Assert.True(result);

      httpClientProxyMoq.Setup(client => client.PostAsync($"test", httpContentProxyMoq.Object))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            StatusCode = HttpStatusCode.BadRequest
          });
          task.Start();
          return task;
        });

      result = usersService.CreateItem(testUser).GetAwaiter().GetResult();

      Assert.False(result);
    }

    [Test]
    public void UpdateUser() {
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpContentProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.PutAsync($"test{testUser.Id}", httpContentProxyMoq.Object))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            StatusCode = HttpStatusCode.OK
          });
          task.Start();
          return task;
        });
      bool result = usersService.UpdateItem(testUser).GetAwaiter().GetResult();

      Assert.True(result);

      httpClientProxyMoq.Setup(client => client.PutAsync($"test{testUser.Id}", httpContentProxyMoq.Object))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            StatusCode = HttpStatusCode.BadRequest
          });
          task.Start();
          return task;
        });
      result = usersService.UpdateItem(testUser).GetAwaiter().GetResult();

      Assert.False(result);
    }

    [Test]
    public void DeleteUser() {
      int id = 2;
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.DeleteAsync($"test{id}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            StatusCode = HttpStatusCode.OK
          });
          task.Start();
          return task;
        });
      
      bool result = usersService.DeleteItem(id).GetAwaiter().GetResult();

      Assert.True(result);

      httpClientProxyMoq.Setup(client => client.DeleteAsync($"test{id}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            StatusCode = HttpStatusCode.NotFound
          });
          task.Start();
          return task;
        });

      result = usersService.DeleteItem(id).GetAwaiter().GetResult();

      Assert.False(result);
    }
  }
}