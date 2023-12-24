using Bank.Application.ServiceResponse;
using Bank.Application.Contracts.Services;

namespace Bank.Service
{
    /// <summary>
    /// Generates responses to send to user.
    /// </summary>
    public class ResponseService : IResponseService
    {
        public ResponseService()
        {

        }

        /// <summary>
        /// Method to generate response of user entered class.
        /// </summary>
        /// <typeparam name="T"> T(user entered) </typeparam>
        /// <param name="data"> T </param>
        /// <param name="success"> bool </param>
        /// <param name="message"> string </param>
        /// <param name="status"> int </param>
        /// <returns> ServiceResponse<T> </returns>
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
