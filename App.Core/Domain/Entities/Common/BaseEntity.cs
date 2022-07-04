using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
