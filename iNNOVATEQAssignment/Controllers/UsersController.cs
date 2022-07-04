using App.Application.Interfaces;
using App.Contract.Dto;
using App.Core.Domain.Result;
using iNNOVATEQAssignment.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iNNOVATEQAssignment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Add User Information
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(UserDto user)
        {
            var result = await _usersService.AddUserAsync(user);
            result.StatusCode = App.Core.Enums.StatusCode.Success;
            return result.ConvertToActionResult(this);
        }

        /// <summary>
        /// Edit User Information
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserDto user)
        {
            
            var result = await _usersService.EditUserAsync(user);
            result.StatusCode = App.Core.Enums.StatusCode.Success;
            return result.ConvertToActionResult(this);
        }
        /// <summary>
        /// Delete User From Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
          
            var result = await _usersService.DeleteUserAsync(id);
            result.StatusCode = App.Core.Enums.StatusCode.Success;
            return result.ConvertToActionResult(this);
        }
        /// <summary>
        /// Get user Information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            
            var result = await _usersService.GetUserAsync(id);
            result.StatusCode = App.Core.Enums.StatusCode.Success;
            return result.ConvertToActionResult(this);
        }
        /// <summary>
        /// Get Users List
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="order"></param>
        /// <param name="orderBy"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersList(int pageSize, int pageNumber, string? order, string? orderBy, string? searchText)
        {
           
            var result = await _usersService.GetUsersAsync(new PagingInfo(pageNumber, pageSize), searchText, order, orderBy);
            result.StatusCode = App.Core.Enums.StatusCode.Success;
            return result.ConvertToActionResult(this);
        }


    }
}
