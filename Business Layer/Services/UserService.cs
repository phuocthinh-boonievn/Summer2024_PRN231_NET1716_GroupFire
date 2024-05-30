using Business_Layer.Commons;
using Business_Layer.Repositories;
using Business_Layer.Utils;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class UserService : IUserSerivce
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimsService _claimsService;
        
        public UserService(IUserRepository userRepository, IClaimsService claimsService)
        {
            _userRepository = userRepository;
            _claimsService = claimsService;
        }
        public async Task<UserViewModel> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User is not existed !");
            }
            var userViewModel = user.ToUserViewModel();
            return userViewModel;
        }

        public async Task<UserViewModel> UpdateUser(Guid id, UserViewModel model)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User is not existed !");
            }
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            _userRepository.Update(user);
            bool isSuccessed = await _userRepository.SaveAsync() > 0;
            if(!isSuccessed)
            {
                throw new Exception("Update Failed !");
            }
            return model;
        }
        public async Task<bool> DeleteUser (Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User is not existed !");
            }
            _userRepository.Remove(user);
            bool isSuccessed = await _userRepository.SaveAsync() > 0;
            if(!isSuccessed)
            {
                throw new Exception("Delete Failed !");
            }
            return isSuccessed;
        }
        public async Task<Pagination<UserViewModel>> GetUserPagingsionsAsync(int pageIndex = 0, int pageSize = 10)
        {
            var users = await _userRepository.ToPagination(pageIndex, pageSize);
            var result = users.ToUserViewModel();
            return result;
        }
    }
}
