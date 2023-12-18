using server.Dtos.Car;

namespace server.Services.Car
{
    public interface ICarService
    {
        Task<List<GetSingleCarDto>> GetAllCars();
        Task<List<GetSingleCarDto>> GetAllCarsForUser(int userId, int page, int pageSize, string search);
        Task<GetSingleCarDto> GetSingleCar(int carId);
        Task<GetSingleCarDto> AddNewCar(AddNewCarDto newCarDetails);
        Task<GetSingleCarDto> UpdateCar(int carId, UpdateCarDto updateCarDto);
        Task<string> RemoveCar(int carID);
    }
}
