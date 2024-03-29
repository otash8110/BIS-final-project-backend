﻿using FinalProject.Core.Common;

namespace FinalProject.Core.Entities
{
    public class Offer: AuditableBaseEntity
    {
        public string Message { get; set; }
        public string DistributorEmail { get; set; }
        public string ManufacturerEmail { get; set; }
        public int ProductId { get; set; }
    }
}
