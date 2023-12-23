using Bank.Application.ServiceResponse;
using Bank.Application.Contracts.Services;

namespace Bank.Service
{
    public class ResponseService : IResponseService
    {
        public ResponseService()
        {

        }

        public ServiceResponse<T> GenerateResponse<T>(T data, bool success, string message, int status)
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Success = success,
                Message = message,
                Status = status
            };
        }
    }
}
