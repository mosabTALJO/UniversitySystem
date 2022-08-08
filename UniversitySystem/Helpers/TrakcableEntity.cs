using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using URF.Core.EF.Trackable;

namespace UniversitySystem.Helpers
{
    public interface ITrackable
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
        [NotMapped]
        bool UpdateCreatedAt { get; set; }
    }

    public class TrackableEntity : Entity , ITrackable
    {
        public DateTime CreatedAt { get; set; }
        [MaxLength(40)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [MaxLength(40)]
        public string UpdatedBy { get; set; }
        [DefaultValue(false)]
        [NotMapped]
        public bool UpdateCreatedAt { get; set; }
    }
}
