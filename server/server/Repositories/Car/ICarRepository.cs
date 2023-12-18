﻿namespace server.Repositories.Car
{
    public interface ICarRepository
    {
        Task<List<Models.Car>> GetAllCars(int page, int pageSize, string search);
        Task<Models.Car> GetSingleCar(int carId);
        Task<Models.Car> AddNewCar(Models.Car newCarDetails);
        Task<Models.Car> UpdateCar(int carId, Models.Car updateCar);
        Task<bool> RemoveCar(int carID);
    }
}