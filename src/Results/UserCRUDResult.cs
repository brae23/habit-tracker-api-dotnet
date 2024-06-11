using System.Net;
using HabitTracker.Api.Models;

namespace HabitTracker.Api.Results;

public class UserCRUDResult
{
    public UserCRUDResult(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public UserCRUDResult(HttpStatusCode statusCode, UserDTO user)
    {
        StatusCode = statusCode;
        User = user;
    }

    public HttpStatusCode StatusCode { get; set; }
    public UserDTO? User { get; set; }
}