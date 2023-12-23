using Bank.Application.ServiceResponse;

namespace Bank.Application.Contracts.Services
{
    public interface IResponseService
    {
        ServiceResponse<T> GenerateResponse<T>(T data, bool success, string message, int status);
    }
}
