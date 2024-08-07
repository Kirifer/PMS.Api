using System.Text.Json.Serialization;

namespace Pms.Shared.Models
{
    public abstract class EntityBaseDto
    {
        [JsonPropertyOrder(-1)]
        public Guid Id { get; set; }
    }

    public class EntityFullBaseDto : EntityBaseDto
    {
        public DateTimeOffset CreatedOn { get; set; }

        public Guid? CreatorId { get; set; }

        public DateTimeOffset? UpdatedOn { get; set; }

        public Guid? UpdaterId { get; set; }
    }
}
