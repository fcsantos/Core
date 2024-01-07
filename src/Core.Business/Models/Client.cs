namespace Core.Business.Models
{
    public class Client : Entity
    {
        public string Name { get; set; }

        public string NIF { get; set; }

        public string UserId { get; set; }

        public bool? IsActive { get; set; }

        public string Email { get; set; }
    }
}
