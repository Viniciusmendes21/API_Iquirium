namespace API_Iquirium.Models
{
    public class Report
    {
        public int IdReport { get; set; }
        public int IdFeedback { get; set; }
        public int IdTipoFeedback { get; set; }
        public Boolean Deferido { get; set; }

    }


    public class TipoReport
    {
        public string IdTipoReport { get; set; }
        public string Nome {  get; set; }
    }
}
