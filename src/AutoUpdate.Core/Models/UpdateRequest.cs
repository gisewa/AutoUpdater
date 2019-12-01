using AutoUpdate.Core.Abstracts;
using AutoUpdate.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Models
{
    public class UpdateRequest : IUpdateCommand
    {
        public FileBase File { get; set; }
    }
}
