using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApplication4.Models
{
    public class ClaOuv
    {     
        public List<OUVRAGE> Ouvrages { get; set; }
        public List<CLASSIFICATION> Classifications { get; set; }
        public string selectedClassification { get; set; }
        public string chercher { get; set; }
    }
}

