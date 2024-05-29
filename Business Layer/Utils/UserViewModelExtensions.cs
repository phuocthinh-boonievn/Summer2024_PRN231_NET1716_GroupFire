using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Utils
{
    public static class UserViewModelExtensions
    {
        public static UserViewModel ToUser(this User user)
        {
            return new UserViewModel
            {
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email                
            };
        }
        public static User ToUserViewModel(this UserViewModel model)
        {
            return new User
            {
                FullName = model.FullName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };
        }
    }
}
