
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
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);


        public CarService(IMapper mapper, ICarRepository carRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetSingleCarDto>> GetAllCars(int page, int pageSize, string search)
        {
            if (page < 0 || pageSize <= 0)
            {
                return new List<GetSingleCarDto>();
            }

            var bookList = await _carRepository.GetAllCars(page, pageSize, search);

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
            //var currentUser = GetUserId();

            //if (newCarDetails.UserId != currentUser || currentUser == 0)
            //{
            //    throw new Exception("Not authorized!");
            //}

            if (newCarDetails is null)
            {
                throw new Exception("Car not updated successfully.");
            }

            var carEntity = _mapper.Map<Models.Car>(newCarDetails);
            var newAddedCar = await _carRepository.AddNewCar(carEntity);

            return _mapper.Map<GetSingleCarDto>(newAddedCar);
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

            var carEntity = _mapper.Map<Models.Car>(updateCarDto);
            var updatedCarEntity = await _carRepository.UpdateCar(carId, carEntity);

            return _mapper.Map<GetSingleCarDto>(updatedCarEntity);
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

            bool carRemoved = await _carRepository.RemoveCar(carId);

            if (!carRemoved)
            {
                return "Car couldn't be removed. Please try again.";
            }

            return "Car was removed succesfully";
        }
    }
}
