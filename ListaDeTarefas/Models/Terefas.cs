using System.ComponentModel.DataAnnotations;

namespace ListaDeTarefas.Models
{

    public enum StatusTarefa
    {
        [Display(Name = "Em Andamento")]
        EmAndamento,

        [Display(Name = "Concluída")]
        Concluida
    }

    public class Tarefas
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome da Tarefa")]
        public string? NomeDaTarefa { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Início")]
        public DateTime DtaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Conclusão")]
        public DateTime? DtaFim { get; set; }

        [Display(Name = "Status")]
        public StatusTarefa Status
        {
            get
            {
                if (DtaFim.HasValue && DateTime.Today > DtaFim)
                {
                    return StatusTarefa.Concluida; 
                }
                else
                {
                    return StatusTarefa.EmAndamento; 
                }
            }
        }
    }
}