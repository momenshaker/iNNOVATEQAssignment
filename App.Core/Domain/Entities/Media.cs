using App.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    public class Media : BaseEntity<int>
    {
        public string MediaUrl { get; set; }
    }
}
