using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Entities.DTOs
{
    public class CommentForAddDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
