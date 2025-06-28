namespace AccountService.Application.Common.Interfaces;

public class BaseResponse<T>
{
    public string Status { get; set; } = "success";
    public T Data { get; set; }
    public string ErrorMessage { get; set; }
}
