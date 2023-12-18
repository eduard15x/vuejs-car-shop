
using AutoMapper;
using server.Dtos.Car;
using server.Repositories.Car;
using System.Security.Claims;

namespace server.Services.Car
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
            {
                // Handle the case when the user is not authenticated
                // You might want to log the event or return a default value
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                // Handle the case when the user ID claim is missing or empty
                // You might want to log the event or return a default value
                throw new InvalidOperationException("User ID claim is missing or empty.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                // Handle the case when the user ID cannot be parsed as an integer
                // You might want to log the event or return a default value
                throw new InvalidOperationException("User ID claim is not a valid integer.");
            }

            return userId;
        }

        public CarService(IMapper mapper, ICarRepository carRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetSingleCarDto>> GetAllCars()
        {
            var bookList = await _carRepository.GetAllCars();

            return _mapper.Map<List<GetSingleCarDto>>(bookList);
        }

        public async Task<List<GetSingleCarDto>> GetAllCarsForUser(int userId, int page, int pageSize, string search)
        {
            var currentUser = GetUserId();

            if (userId != currentUser || currentUser == 0)
            {
                throw new Exception("Not authorized! Please login.");
            }

            if (page < 0 || pageSize <= 0)
            {
                return new List<GetSingleCarDto>();
            }

            var bookList = await _carRepository.GetAllCarsForUser(userId, page, pageSize, search);

            return _mapper.Map<List<GetSingleCarDto>>(bookList);
        }

        public async Task<GetSingleCarDto> GetSingleCar(int carId)
        {
            if (carId <= 0)
            {
                throw new Exception("Car not found");
            }

            var carEntity = await _carRepository.GetSingleCar(carId);

            return _mapper.Map<GetSingleCarDto>(carEntity);
        }

        public async Task<GetSingleCarDto> AddNewCar(AddNewCarDto newCarDetails)
        {
            var currentUser = GetUserId();

            if (currentUser <= 0)
            {
                throw new Exception("Not authorized! Please login.");
            }

            if (newCarDetails is null)
            {
                throw new Exception("Error adding a new car, object can not be null.");
            }
            else if (newCarDetails.UserId != currentUser)
            {
                throw new Exception("Your current data in the object is wrong. Please check user id.");
            }
            else if (newCarDetails.CarYear < 2015 || newCarDetails.CarYear > 2024)
            {
                throw new Exception("Error adding a new car, car year must be between 2015 - 2024.");
            }
            else if (newCarDetails.CarPrice < 5000 || newCarDetails.CarPrice > 5000000)
            {
                throw new Exception("Error adding a new car, car price must be between 5.000 - 5.000.000.");
            }

            var carEntity = _mapper.Map<Models.Car>(newCarDetails);
            var newAddedCar = await _carRepository.AddNewCar(carEntity);

            return _mapper.Map<GetSingleCarDto>(newAddedCar);
        }

        public async Task<GetSingleCarDto> UpdateCar(int carId, UpdateCarDto updateCarDto)
        {
            var currentUser = GetUserId();

            if (updateCarDto.UserId != currentUser || currentUser == 0)
            {
                throw new Exception("Not authorized! Please login.");
            }

            if (carId <= 0)
            {
                throw new Exception("Car not found");
            }

            if (carId != updateCarDto.Id)
            {
                throw new Exception("Error. Please check object id and id passed as a param in the endpoint.");
            }

            if (updateCarDto is null)
            {
                throw new Exception("Error adding a new car, object can not be null.");
            }
            else if (updateCarDto.CarYear < 2015 || updateCarDto.CarYear > 2024)
            {
                throw new Exception("Error adding a new car, car year must be between 2015 - 2024.");
            }
            else if (updateCarDto.CarPrice < 5000 || updateCarDto.CarPrice > 5000000)
            {
                throw new Exception("Error adding a new car, car price must be between 5.000 - 5.000.000.");
            }

            var carEntity = _mapper.Map<Models.Car>(updateCarDto);
            var updatedCarEntity = await _carRepository.UpdateCar(carId, carEntity);

            return _mapper.Map<GetSingleCarDto>(updatedCarEntity);
        }

        public async Task<string> RemoveCar(int carId)
        {
            var currentUser = GetUserId();

            if ( currentUser <= 0)
            {
                throw new Exception("Not authorized! Please login.");
            }

            if (carId <= 0)
            {
                throw new Exception("Car not found");
            }

            return await _carRepository.RemoveCar(carId);
        }
    }
}
