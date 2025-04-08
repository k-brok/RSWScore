using RSW.WebApp.Entities;

namespace RSW.WebApp.Services
{
    public class CurrentEditionService
    {
        public Action Subscribe;
        public List<Category> Categories { get; set; }
        public Edition Edition { get; set; }
    }
}
