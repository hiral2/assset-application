﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Services
{
    public interface IValidatorService<T>
    {
        Task ThrowIfNotValidAsync(T obj);
    }
}
