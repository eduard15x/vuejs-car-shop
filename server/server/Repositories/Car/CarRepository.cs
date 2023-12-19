using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Car;

namespace server.Repositories.Car
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Models.Car>> GetAllCars()
        {
            return await _appDbContext.Cars.ToListAsync();
        }

        public async Task<GetFilteredSalonListDto> GetAllCarsForUser(int userId, int page, int pageSize, string search)
        {
            var query = _appDbContext.Cars
                .Where(s => s.UserId == userId);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.CarName.Contains(search));
            }

            var totalItemsCount = await query.CountAsync();

            var carListDb = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GetFilteredSalonListDto
            {
                CarList = carListDb,
                TotalCars = totalItemsCount
            };
        }

        public async Task<Models.Car> GetSingleCar(int carId)
        {
            return await _appDbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        }

        public async Task<Models.Car> AddNewCar(Models.Car newCarDetails)
        {
            try
            {
                _appDbContext.Cars.Add(newCarDetails);
                await _appDbContext.SaveChangesAsync();

                return newCarDetails;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Models.Car> UpdateCar(int carId, Models.Car updateCar)
        {
            var existingCar = await _appDbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);

            existingCar.CarName = updateCar.CarName;
            existingCar.CarModel = updateCar.CarModel;
            existingCar.CarYear = updateCar.CarYear;
            existingCar.CarPrice = updateCar.CarPrice;
            existingCar.Engine = updateCar.Engine;

            await _appDbContext.SaveChangesAsync();

            return existingCar;
        }

        public async Task<string> RemoveCar(int carId)
        {
            var car = await _appDbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);

            if (car is null)
            {
                throw new Exception($"Car with Id '{carId}' was not found.");
            }

            _appDbContext.Cars.Remove(car);
            await _appDbContext.SaveChangesAsync();

            return "Car deleted successfully.";
        }

    }
}
