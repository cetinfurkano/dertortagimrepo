using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Entities.DTOs
{
    public class CommentForReturnDto
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public UserForReturnDto User { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
