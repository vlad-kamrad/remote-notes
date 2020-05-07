using System;
using RN.Application.Common.Interfaces;

namespace RN.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
