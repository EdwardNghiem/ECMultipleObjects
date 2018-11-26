using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSECMultiplObj.Models
{
    public class RoleModels : IdentityRole
    {
        public string Description { get; set; }
    }


}