using Microsoft.AspNetCore.Mvc;
using server.Dtos.Car;
using server.Services.Car;
using System.Net;

namespace server.Controllers
{
    [ApiController]
    [Route("api/cars")]
    [Produces("application/json")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllCars()
        {
            try
            {
                var response = await _carService.GetAllCars();
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> GetAllCarsForUser(int userId, int page, int pageSize, string search = "")
        {
            try
            {
                var carList = await _carService.GetAllCarsForUser(userId, page, pageSize, search);
                return Json(Ok(carList));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [HttpGet("{carId}")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetSingleCar([FromRoute] int carId)
        {
            try
            {
                var response = await _carService.GetSingleCar(carId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> AddNewCar([FromBody] AddNewCarDto newCarDetails)
        {
            try
            {
                var response = await _carService.AddNewCar(newCarDetails);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [HttpPut("update/{carId}")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult> UpdateCar([FromRoute] int carId, [FromBody] UpdateCarDto updateCarDto)
        {
            try
            {
                var response = await _carService.UpdateCar(carId, updateCarDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [HttpDelete("delete/{carId}")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> RemoveCar(int carId)
        {
            try
            {
                var response = await _carService.RemoveCar(carId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }
    }
}
