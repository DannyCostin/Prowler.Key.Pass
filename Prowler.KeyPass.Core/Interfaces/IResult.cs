using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core
{
    public interface IResult<T>
    {
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; }
        public T? Handle();
    }
}
