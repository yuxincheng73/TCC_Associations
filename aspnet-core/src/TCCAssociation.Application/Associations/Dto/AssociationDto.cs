﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TCCAssociation.Models;

namespace TCCAssociation.Associations.Dto
{
    [AutoMap(typeof(Association))]
    public class AssociationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Name_TC { get; set; }
        public string Name_SC { get; set; }
        public string Description { get; set; }
        public string Description_TC { get; set; }
        public string Description_SC { get; set; }
        public string Url { get; set; }
        public string Url_TC { get; set; }
        public string Url_SC { get; set; }
        public string Logo { get; set; }
        public DateTime LaunchDate { get; set; }
        public string TimeZone { get; set; }
    }
}
