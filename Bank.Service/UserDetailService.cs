using Bank.Application.Contracts.Services;
using Bank.Domain;

namespace Bank.Service
{
    /// <summary>
    /// Services related to user details.
    /// </summary>
    public class UserDetailService : IUserDetailService
    {
        /// <summary>
        /// method to create a UserDetail object from given parameters.
        /// </summary>
        /// <param name="Name"> string </param>
        /// <param name="CitizenshipNumber"> string </param>
        /// <param name="Dob"> DateOnly </param>
        /// <param name="UserId"> string </param>
        /// <returns> UserDetail </returns>
        public UserDetail CreateUserDetailObject(string Name, string CitizenshipNumber, DateOnly Dob, string UserId)
        {
            UserDetail userDetail = new() 
            {
                Name = Name,
                CitizenshipNumber = CitizenshipNumber,
                Dob = Dob,
                UserId = UserId
            };
            return userDetail;
        }
    }
}
