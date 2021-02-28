using MRP.Business.Models;
using System;

namespace MRP.Business.Models
{
    public class Paciente : Entity
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string NumeroIdentificacao { get; set; }
        public string NumeroUtente { get; set; }
        public Genero GeneroId { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telemovel { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
    }
}
