using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Core.Entities
{
    internal class Result<T> : IResult<T>
    {
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }

        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

        public T? Handle()
        {
            if(ErrorMessage == null)
            {
                return Value;
            }

            throw new InvalidOperationException(ErrorMessage);
        }

        public static IResult<T> Create()
        {
            return new Result<T>();
        }
    }
}
