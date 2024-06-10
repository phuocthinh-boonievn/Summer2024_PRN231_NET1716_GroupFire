using Business_Layer.Repositories;
using Business_Layer.Services;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShipperController : ControllerBase
	{
		private readonly IShipperRepository _shipperRepository;

		public ShipperController(IShipperRepository shipperRepository)
		{
			_shipperRepository = shipperRepository;
		}

		// GET: api/<ShipperController>
		[HttpGet("GetAllShippers")]
		//[Authorize(Roles = UserRole.Admin)]
		public async Task<APIResponseModel> GetAllShipper()
		{
			try
			{
				var result = await _shipperRepository.GetAllAsync();
				return new APIResponseModel()
				{
					code = 200,
					message = "Get successful",
					IsSuccess = true,
					Data = result,
				};
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		// GET api/<ShipperController>/5
		[HttpGet("GetShipperById")]
		//[Authorize(Roles = UserRole.Admin)]
		public async Task<APIResponseModel> GetShipperById(Guid id)
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

				var result = await _shipperRepository.GetByIdAsync(id);
				return new APIResponseModel()
				{
					code = 200,
					message = "Get successful",
					IsSuccess = true,
					Data = result,
				};

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
		[HttpGet("GetShipperByUserId")]
		//[Authorize(Roles = UserRole.Admin)]
		public async Task<APIResponseModel> GetShipperByUserId(System.Guid userId)
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

				var result = await _shipperRepository.GetByIdAsync(userId);
				return new APIResponseModel()
				{
					code = 200,
					message = "Get successful",
					IsSuccess = true,
					Data = result,
				};

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

		// PUT api/<ShipperController>/5
		//[HttpGet("AddShipper")]
		//public async Task<APIResponseModel> AddShipper([FromBody] ShipperVM model)
		//{
		//	try
		//	{
		//		if (!ModelState.IsValid)
		//		{
		//			var errors = ModelState.Values.SelectMany(v => v.Errors)
		//						  .Select(e => e.ErrorMessage).ToList();
		//			return new APIResponseModel
		//			{
		//				code = 400,
		//				Data = errors,
		//				IsSuccess = false,
		//				message = string.Join(";", errors)
		//			};


		//		}

		//		var result = await _shipperRepository.AddAsync(model);
		//		return new APIResponseModel()
		//		{
		//			code = 200,
		//			message = "Get successful",
		//			IsSuccess = true,
		//			Data = result,
		//		};

		//	}
		//	catch (Exception ex)
		//	{
		//		return new APIResponseModel()
		//		{
		//			code = StatusCodes.Status400BadRequest,
		//			message = ex.Message,
		//			Data = ex,
		//			IsSuccess = false
		//		};
		//	}
		//}

		// DELETE api/<ShipperController>/5
		[HttpDelete("DeleteShipper")]
		//[Authorize(Roles = UserRole.Admin)]
		public void Delete(int id)
		{
		}
	}
}
