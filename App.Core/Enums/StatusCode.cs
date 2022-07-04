﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Enums
{
    public enum StatusCode
    {
        Success = 200,
        NoContent = 204,
        BadRequest = 400,
        UnAuthorized = 403,
        UnAuthenticated = 401,
        NotFound = 404,
        InternalError = 500,
        AlreadyExist = 409
    }
}
