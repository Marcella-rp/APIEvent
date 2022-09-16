using System.ComponentModel.DataAnnotations;


namespace APIEvent.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "O título do evento é obrigatório")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "A data e a hora do evento são obrigatórias")]
        public DateTime DateHourEvent { get; set; }
        [Required(ErrorMessage = "O local do evento é obrigatório")]
        public string Local { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Status é obrigatório")]
        public bool Status { get; set; }
    }
}
