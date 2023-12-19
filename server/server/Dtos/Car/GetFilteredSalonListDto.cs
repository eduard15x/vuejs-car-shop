namespace server.Dtos.Car
{
    public class GetFilteredSalonListDto
    {
        public List<Models.Car> CarList { get; set; }
        public int TotalCars { get; set; }
    }
}
