using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }
        [Required(ErrorMessage = "O Id do evento é obrigatório")]
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "Nome da pessoa que fez a reserva, é obrigatório")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "A quantidade de reservas, é obrigatória")]
        public long Quantity { get; set; }
    }
}
