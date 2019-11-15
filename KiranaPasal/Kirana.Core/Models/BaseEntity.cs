using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.Models
{
    public abstract class BaseEntity//we cannot create an instance of this class and only iplements  the class
    {
        public String Id { get; set; }
        [DisplayName("Added Since")]
        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
