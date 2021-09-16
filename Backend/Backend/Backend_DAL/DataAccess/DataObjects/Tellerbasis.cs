using System;
using System.Collections.Generic;

#nullable disable

namespace Backend_DAL.DataAccess.DataObjects
{
    public partial class Tellerbasis
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public int Optie { get; set; }
        public int Actief { get; set; }
        public string Afkorting { get; set; }
        public double MaxWaarde { get; set; }
    }
}
