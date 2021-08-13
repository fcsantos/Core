using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;

namespace ProjetoMRP.Paciente.ViewModel
{
    public class Mensagens
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid? ReplyMessageId { get; set; }

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string StatusMessage { get; set; }
        public bool? IsActive { get; set; }
        public bool IsReply { get; set; }

        public bool IsMailSender { get; set; }

        public string PatientName { get; set; }

        public string Ativo { get; set; }

        public string DateFormat { get; set; }

        public string DoctorName { get; set; }

        public string StatusMessageFormat { get; set; }

        public Color Color { get; set; }

    }
}
