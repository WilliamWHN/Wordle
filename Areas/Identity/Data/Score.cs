namespace Wordle.Areas.Identity.Data
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int ScorePoints { get; set; }
        public User User { get; set; }
    }
}
