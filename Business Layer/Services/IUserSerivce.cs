using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IUserSerivce
    {
        Task<UserViewModel> GetUserById(Guid id);
        Task<UserViewModel> UpdateUser(Guid id, UserViewModel model);
    }
}
