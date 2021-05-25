namespace comrade.Domain.Bases
{
    public class SortByDto
    {
        public SortByDto(string property, bool desc)
        {
            Property = property;
            Desc = desc;
        }

        public string Property { get; set; }
        public bool Desc { get; set; }
    }
}