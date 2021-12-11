using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Entities.DTOs
{
    public class PostForUpdateDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Text { get; set; }

    }
}
