using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Entities
{
    public class YoutubeResponse
    {
        public List<VideosDetails> Videos { get; set; } = [];

        public string? NextPageToken { get; set; } = string.Empty;
        public string? PrevPageToken { get; set; } = string.Empty;
    }
}
