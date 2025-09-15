using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First.Models
{
    internal class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string bref { get; set; }
        public DateTime Date {  get; set; }
        [ForeignKey("author")]
        public int authorId { get; set; }
        [ForeignKey("category")]
        public int Cat_Id { get; set; }
        public Author author { get; set; }
        public Category category { get; set; }
    }
}
