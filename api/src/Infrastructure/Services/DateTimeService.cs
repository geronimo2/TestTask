using Volue.Application.Common.Interfaces;
using System;

namespace Volue.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
