using AspMVC.Services;
using DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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
    private UserDTO TestUser;
    [SetUp]
    public void Setup() {
      settingsMoq = new Mock<ISettings>(MockBehavior.Strict);
      httpClientProxyMoq = new Mock<IHttpClientProxy>(MockBehavior.Strict);
      usersService = new UsersService(settingsMoq.Object, () => httpClientProxyMoq.Object);
      TestUser = new UserDTO() {
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
      httpClientProxyMoq.Setup(client => client.GetAsync($"test{id}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.UserServiceGetItemResult, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });
      httpClientProxyMoq.Setup(client => client.Dispose());
      
      UserDTO user = usersService.GetItemById(id).GetAwaiter().GetResult();
      Assert.True(TestUser.Equals(user));
    }

    [Test]
    public void FindItemsByProperty() {
      string email = "E-mail2";
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.GetAsync($"test?Email={email}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.ExpectedFindContent, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });
      httpClientProxyMoq.Setup(client => client.Dispose());

      IEnumerable<UserDTO> users = usersService.FindItemsByProperty(email).GetAwaiter().GetResult();
      Assert.True(TestUser.Equals(users.First()));
    }

    [Test]
    public void CreateUser() {
      usersService.CreateItem(TestUser).GetAwaiter().GetResult();
    }

    [Test]
    public void UpdateUser() {
      usersService.CreateItem(TestUser).GetAwaiter().GetResult();
    }

    [Test]
    public void DeleteUser() {
      usersService.CreateItem(TestUser).GetAwaiter().GetResult();
    }
  }
}