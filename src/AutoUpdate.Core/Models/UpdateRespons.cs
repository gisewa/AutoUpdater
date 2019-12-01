using AutoUpdate.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Models
{
    public class UpdateRespons : IUpdateCommand
    {
        public SyncType SyncType { get; set; }

        public UpdateType UpdateType { get; set; }

        public Explain Explain { get; set; }

        public UpdatePacket UpdatePacket { get; set; }
    }
}
