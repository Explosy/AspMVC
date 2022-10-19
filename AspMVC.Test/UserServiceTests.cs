using AspMVC.Models;
using AspMVC.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;
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
        Age = 26,
        Email = "ml@yandex.ru",
        RegistationDate = new DateTime(2022, 10, 12)
      };
    }

    [Test]
    public void GetItemsTest() {
      string email = "ml@yandex.ru";
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
      
      ResponseModel<IEnumerable<UserDTO>> response = usersService.GetItems(email).GetAwaiter().GetResult();
      int expectedUserCount = 1;
      int resultUserCount = response.Data.Count();
      Assert.AreEqual(expectedUserCount, resultUserCount);
      Assert.True(testUser.Equals(response.Data.First()));
    }

    [Test]
    public void GetItemById() {
      int id = 2;
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.GetAsync($"test{id}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.ExpectedFindByIdContent, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });

      ResponseModel<UserDTO> response = usersService.GetItemById(id).GetAwaiter().GetResult();

      Assert.True(testUser.Equals(response.Data));
    }

    [Test]
    public void CreateUser() {
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpContentProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.PostAsync($"test", httpContentProxyMoq.Object))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.CreateUserContent, Encoding.UTF8, "application/json")
        });
          task.Start();
          return task;
        });

      ResponseModel<UserDTO> response = usersService.CreateItem(testUser).GetAwaiter().GetResult();

      Assert.True(testUser.Equals(response.Data));
    }

    [Test]
    public void UpdateUser() {
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpContentProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.PutAsync($"test{testUser.Id}", httpContentProxyMoq.Object))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.ExpectedFindByIdContent, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });
      
      ResponseModel<UserDTO> response = usersService.UpdateItem(testUser).GetAwaiter().GetResult();
      
      Assert.True(testUser.Equals(response.Data));
    }

    [Test]
    public void DeleteUser() {
      int id = 2;
      settingsMoq.SetupGet(setting => setting.ApiAddress).Returns("test");
      httpClientProxyMoq.Setup(client => client.Dispose());
      httpClientProxyMoq.Setup(client => client.DeleteAsync($"test{id}"))
        .Returns(() => {
          Task<HttpResponseMessage> task = new Task<HttpResponseMessage>(() => new HttpResponseMessage() {
            Content = new StringContent(TestResource.ExpectedFindByIdContent, Encoding.UTF8, "application/json")
          });
          task.Start();
          return task;
        });

      ResponseModel<UserDTO> response = usersService.DeleteItem(id).GetAwaiter().GetResult();

      Assert.True(testUser.Equals(response.Data));
    }
  }
}