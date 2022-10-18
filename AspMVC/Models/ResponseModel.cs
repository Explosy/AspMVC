namespace AspMVC.Models {
  public class ResponseModel<T> where T : class {
    const string ERROR_MESSAGE = "Data is NULL";
    const string DEFAULT_STACK_TRACE = "DEFAULT";
    private T data;
    private bool isSuccess = false;
    public string Error { get; set; } = ERROR_MESSAGE;
    public string StackTrace { get; set; } = DEFAULT_STACK_TRACE;
    public bool IsSuccess {
      get => isSuccess;
      set {
        isSuccess = value;
        if (value) {
          Error = null;
          StackTrace = null;
        }
      }
    }
    public T Data {
      get => data;
      set {
        if (value != null) {
          IsSuccess = true;
        }
        data = value;
      }
    }
  }
}