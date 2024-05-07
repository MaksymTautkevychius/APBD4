namespace APBD4;

public class Visits
{
    public class Visit
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string Date { get; set; }
        public string DescriptionOfTheVisit { get; set; }
        public double Price { get; set; }
    }
}