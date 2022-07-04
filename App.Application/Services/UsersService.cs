using App.Application.Helpers;
using App.Application.Interfaces;
using App.Contract.Dto;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces.ContextInterface;
using App.Core.Domain.Result;
using App.Core.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class UsersService : IUsersService
    {
        private IApplicationDbContext _applicationDbContext;
        private IMapper _mapper;
        public UsersService(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Using this function you can add new user
        /// </summary>
        /// <param name="userItem"></param>
        /// <returns></returns>
        public async Task<Result<bool>> AddUserAsync(UserDto userItem)
        {
            if (userItem.ValidateNullOrEmpty() == true)
            {
                var validateResult = new Result<bool>(true)
                {
                    Data = false,
                    StatusCode = StatusCode.Success,
                    SuccessMessage = "Please Fill All Fields"
                };
                return validateResult;
            }
            var mappedUser = _mapper.Map<Users>(userItem);
            _applicationDbContext.Users.Add(mappedUser);
            await _applicationDbContext.SaveChangesAsync();
            if (mappedUser.Id != 0)
            {
                var Result = new Result<bool>(true)
                {
                    Data = true,
                    StatusCode = StatusCode.Success,
                    SuccessMessage = "User Added Successfully"
                };
                return Result;

            }
            else
            {
                var Result = new Result<bool>(false)
                {
                    Data = true,
                    StatusCode = StatusCode.BadRequest,
                    SuccessMessage = "User Not Added"
                };
                return Result;

            }

        }
        /// <summary>
        /// using this function you delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<bool>> DeleteUserAsync(int id)
        {
            var userRow = _applicationDbContext.Users.Find(id);
            _applicationDbContext.Users.Remove(userRow);
            await _applicationDbContext.SaveChangesAsync();
            var Result = new Result<bool>(true)
            {
                Data = true,
                StatusCode = StatusCode.Success,
                SuccessMessage = "User Updated Successfully"
            };
            return Result;
        }
        /// <summary>
        /// using this function you can edit user
        /// </summary>
        /// <param name="userItem"></param>
        /// <returns></returns>

        public async Task<Result<bool>> EditUserAsync(UserDto userItem)
        {
            if(userItem.ValidateNullOrEmpty() == true)
            {
                var validateResult = new Result<bool>(true)
                {
                    Data = false,
                    StatusCode = StatusCode.Success,
                    SuccessMessage = "Please Fill All Fields"
                };
                return validateResult;
            }
            var mappedUser = _mapper.Map<Users>(userItem);
            _applicationDbContext.Entry(mappedUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            var result = new Result<bool>(true)
            {
                Data = true,
                StatusCode = StatusCode.Success,
                SuccessMessage = "User Updated Successfully"
            };
            return result;
        }
        /// <summary>
        /// using this function you can get user information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<UserItemDto>> GetUserAsync(int id)
        {
            var userRow = await _applicationDbContext.Users.FindAsync(id);
            var Result = new Result<UserItemDto>();
            if (userRow == null)
            {
                Result = new Result<UserItemDto>()
                {
                    Data = null,
                    StatusCode = StatusCode.BadRequest,
                    SuccessMessage = "User Not Found"
                };
                return Result;
            }
            var mappedItem = _mapper.Map<UserItemDto>(userRow);
            Result = new Result<UserItemDto>(mappedItem)
            {
                Data = mappedItem,
                StatusCode = StatusCode.Success,
                SuccessMessage = "User Retrived Successfully"
            };
            return Result;
        }
        /// <summary>
        /// using this function you can retrive a list of users with filters, sorting and pagination 
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <param name="searhText"></param>
        /// <param name="order"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>

        public async Task<Result<List<UserItemDto>>> GetUsersAsync(PagingInfo pagingInfo, string? searhText, string? order, string? orderBy)
        {
            var query = _applicationDbContext.Users
                .Where(x => (searhText == null ||
                (x.Country.Contains(searhText) ||
                x.Name.Contains(searhText) ||
                x.State.Contains(searhText) ||
                x.Street.Contains(searhText)))).AsQueryable();

            #region Sorting
            switch (orderBy)
            {
                case "name":
                    query = order == "asc" ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                    break;
                case "joiningdate":
                    query = order == "asc" ? query.OrderBy(x => x.JoiningDate) : query.OrderByDescending(x => x.JoiningDate);
                    break;
                default:
                    query = order == "asc" ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                    break;
            }
            #endregion

            var _usersList = await query.ProjectTo<UserItemDto>(_mapper.ConfigurationProvider)
                                        .ToPagedListAsync(pagingInfo);
            var Result = new Result<List<UserItemDto>>(_usersList)
            {
                Data = _usersList,
                StatusCode = StatusCode.Success,
                SuccessMessage = "Users List Retrived Successfully"
            };
            return Result;
        }
    }
}
