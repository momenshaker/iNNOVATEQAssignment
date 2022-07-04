using App.Contract.Dto;
using App.Core.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IUsersService
    {
        Task<Result<bool>> AddUserAsync(UserDto userItem);
        Task<Result<bool>> EditUserAsync(UserDto userItem);
        Task<Result<bool>> DeleteUserAsync(int id);
        Task<Result<UserItemDto>> GetUserAsync(int id);
        Task<Result<List<UserItemDto>>> GetUsersAsync(PagingInfo pagingInfo, string? searhText, string? order, string? orderBy);
    }
}
