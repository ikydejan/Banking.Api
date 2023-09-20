using System;
using Dapper.Contrib.Extensions;

namespace Banking.Api.Models
{
    public class DefaultParam
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Filter { get; set; }
    }
}