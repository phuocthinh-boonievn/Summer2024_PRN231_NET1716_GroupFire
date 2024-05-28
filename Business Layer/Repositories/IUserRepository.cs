using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public interface IUserRepository
    {
        Task<APIResponseModel> Login(LoginVM model);
        Task<APIResponseModel> Register(RegisterVM model);
    }
}
