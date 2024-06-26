using Business_Layer.Repositories.Interfaces;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class OrderStatusController : ControllerBase
	{
		private readonly IOrderStatusRepository _statusRepository;

		public OrderStatusController(IOrderStatusRepository statusRepository)
		{
			_statusRepository = statusRepository;
		}

		[HttpPost("CreateOrderStatus")]
		//[Authorize(Roles = UserRole.Admin)]
		public async Task<APIResponseModel> CreateOrderStatus([FromBody] OrderStatus model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					var errors = ModelState.Values.SelectMany(v => v.Errors)
								  .Select(e => e.ErrorMessage).ToList();
					return new APIResponseModel
					{
						code = 400,
						Data = errors,
						IsSuccess = false,
						message = string.Join(";", errors)
					};


				}

				var result = _statusRepository.CreateOrderStatus(model);
				return await result;

			}
			catch (Exception ex)
			{
				return new APIResponseModel()
				{
					code = StatusCodes.Status400BadRequest,
					message = ex.Message,
					Data = ex,
					IsSuccess = false
				};
			}
		}

		// POST api/<ShipperController>
		[HttpGet("GetOrderStatusByShipperId")]
			//[Authorize(Roles = UserRole.Admin)]
			public async Task<APIResponseModel> GetOrderStatusByShipperId(string userId)
			{
				try
				{
					if (!ModelState.IsValid)
					{
						var errors = ModelState.Values.SelectMany(v => v.Errors)
									  .Select(e => e.ErrorMessage).ToList();
						return new APIResponseModel
						{
							code = 400,
							Data = errors,
							IsSuccess = false,
							message = string.Join(";", errors)
						};


					}

					var result = _statusRepository.GetOrderStatusByShipperId(userId);
					return await result;

				}
				catch (Exception ex)
				{
					return new APIResponseModel()
					{
						code = StatusCodes.Status400BadRequest,
						message = ex.Message,
						Data = ex,
						IsSuccess = false
					};
				}
			}

		[HttpPost("ChangeOrderStatus")]
		//[Authorize(Roles = UserRole.Admin)]
		public async Task<APIResponseModel> ChangeOrderStatus(string orderStatusId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					var errors = ModelState.Values.SelectMany(v => v.Errors)
								  .Select(e => e.ErrorMessage).ToList();
					return new APIResponseModel
					{
						code = 400,
						Data = errors,
						IsSuccess = false,
						message = string.Join(";", errors)
					};
				}

				var result = _statusRepository.ChangeOrderStatus(orderStatusId);
				return await result;

			}
			catch (Exception ex)
			{
				return new APIResponseModel()
				{
					code = StatusCodes.Status400BadRequest,
					message = ex.Message,
					Data = ex,
					IsSuccess = false
				};
			}
		}
	}
}
