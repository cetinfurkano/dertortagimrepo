using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Entities.DTOs
{
    public  class PostForAddDto
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
