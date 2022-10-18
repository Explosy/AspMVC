using AspMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC.Services {
  interface IDataService<T> where T : class {
    Task<ResponseModel<IEnumerable<T>>> GetItems(string email);
    Task<ResponseModel<T>> GetItemById(int id);
    Task<ResponseModel<T>> CreateItem(T item);
    Task<ResponseModel<T>> UpdateItem(T item);
    Task<ResponseModel<T>> DeleteItem(int id);
  }
}
