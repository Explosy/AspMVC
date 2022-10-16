using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC.Services {
  interface IDataService<T> {
    Task<IEnumerable<T>> GetAllItems();
    Task<T> GetItemById(int id);
    Task<IEnumerable<T>> FindItemsByProperty(string email);
    Task<bool> CreateItem(T item);
    Task<bool> UpdateItem(T item);
    Task<bool> DeleteItem(int id);
  }
}
