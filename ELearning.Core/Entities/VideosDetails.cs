using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Entities
{
    public class VideosDetails
    {
        public string? Title { get; set; } = string.Empty;
        public string? Link { get; set; } = string.Empty;
        public string? Thumbnail { get; set; } = string.Empty;
        public DateTimeOffset? PublishedAt { get; set; }
    }
}
