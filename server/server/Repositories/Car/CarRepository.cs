using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Repositories.Car
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<Models.Car>> GetAllCars(int page, int pageSize, string search)
        {
            var query = await _appDbContext.Cars.ToListAsync();

            //if (!string.IsNullOrEmpty(search))
            //{
            //    query = query.Where(s => s.CarName.Contains(search)).ToListAsync();
            //}

            //if (!string.IsNullOrEmpty(selectedCities))
            //{
            //    var cities = selectedCities.Split(',').Select(city => city.Trim());
            //    query = query.Where(s => cities.Contains(s.SalonCity));
            //}

            //var totalItemCount = await query.CountAsync();

            //var salonsListDb = await query
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            //return new GetSalonListDto
            //{
            //    Salons = salonsListDb,
            //    TotalSalons = totalItemCount
            //};
            return query;
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

        public async Task<bool> RemoveCar(int carID)
        {
            var book = await _appDbContext.Cars.FirstOrDefaultAsync(c => c.Id == carID);

            if (book is null)
            {
                throw new Exception($"Book with Id '{book.Id}' was not found.");
            }

            try
            {
                _appDbContext.Cars.Remove(book);
                await _appDbContext.SaveChangesAsync();

                return true;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
