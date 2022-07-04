using App.Core.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces.ResultInterface
{
    public interface IResult<T>
    {
        string Version { get; set; }
        List<string>? ErrorMessages { get; set; }
        PagingInfo? PagingInfo { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
