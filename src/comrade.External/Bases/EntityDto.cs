#region

using comrade.External.Utils;

#endregion

namespace comrade.External.Bases
{
    public class EntityDto : Dto, IEntityDto
    {
        public int Id { get; set; }
    }
}