namespace Core.Business.Models
{
    public class Client : Entity
    {
        public string Name { get; set; }

        public string NIF { get; set; }

        public string UserId { get; set; }

        public bool? IsActive { get; set; }

        public string Email { get; set; }

        public string SecretKey { get; set; }

        public string ApiKey { get; set; }

        public string IVBase64 { get; set; }

        public string Certificate { get; set; }

        public string CertificatePathPfx { get; set; }

        public string PasswordPfx { get; set; }

        public string CertificatePathCer { get; set; }

        public string UsernameAT { get; set; }

        public string PasswordAT { get; set; }
    }
}
