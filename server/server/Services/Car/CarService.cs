
using server.Dtos.Car;
using server.Models;
using System.Security.Claims;

namespace server.Services.Car
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);


        public CarService(ICarRepository carRepository, IHttpContextAccessor httpContextAccessor)
        {
            _carRepository = carRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetSingleCarDto>> GetAllCars(int page, int pageSize, string search)
        {
            if (page < 0 || pageSize <= 0)
            {
                return new List<GetSingleCarDto>();
            }


            return await _carRepository.GetAllCars(page, pageSize, search);
        }

        public async Task<GetSingleCarDto> GetSingleCar(int carId)
        {
            if (carId <= 0)
            {
                throw new Exception("Car not found");
            }

            return await _carRepository.GetSingleCar(carId);
        }

        public async Task<GetSingleCarDto> AddNewCar(AddNewCarDto newCarDetails)
        {
            //var currentUser = GetUserId();

            //if (newCarDetails.UserId != currentUser || currentUser == 0)
            //{
            //    throw new Exception("Not authorized!");
            //}

            if (newCarDetails is null)
            {
                throw new Exception("Car not updated successfully.");
            }

            return await _carRepository.AddNewCar(newCarDetails);
        }

        public async Task<GetSingleCarDto> UpdateCar(int carId, UpdateCarDto updateCarDto)
        {
            if (carId <= 0)
            {
                throw new Exception("Car not found");
            }

            if (updateCarDto is null)
            {
                throw new Exception("dto is null");
            }

            //var currentUser = GetUserId();

            //if (newCarDetails.UserId != currentUser || currentUser == 0)
            //{
            //    throw new Exception("Not authorized!");
            //}

            return await _carRepository.UpdateCar(updateCarDto);
        }

        public async Task<string> RemoveCar(int carId)
        {
            //var currentUser = GetUserId();

            //if (newCarDetails.UserId != currentUser || currentUser == 0)
            //{
            //    throw new Exception("Not authorized!");
            //}

            if (carId <= 0)
            {
                throw new Exception("Car not found");
            }

            return await _carRepository.RemoveCar(carId);
        }
    }
}
