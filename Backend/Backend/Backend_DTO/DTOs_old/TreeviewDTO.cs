using System;
using System.Collections.Generic;

#nullable disable

namespace Backend_DTO
{
    public partial class TreeviewDTO
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public int BoomVolgorde { get; set; }
        public string Stamkaart { get; set; }
        public int TreeviewtypeId { get; set; }
        public string Serienummer { get; set; }
        public string Bouwjaar { get; set; }
        public short Actief { get; set; }
        public int Wijzigactief { get; set; }
        public short Vrijgegeven { get; set; }
        public int Installatiedatum { get; set; }
        public int Garantietot { get; set; }
        public decimal Aanschafwaarde { get; set; }
        public int Afschrijving { get; set; }
        public int Jaarafschrijving { get; set; }
        public short Afschrijvingeen { get; set; }
        public decimal Budgetvorig { get; set; }
        public decimal Budgetnu { get; set; }
        public int Melden { get; set; }
        public bool Correctief { get; set; }
        public bool Werkopdracht { get; set; }
        public int FabrikantenId { get; set; }
        public int LeverancierenId { get; set; }
        public int LocatiesId { get; set; }
        public int KostenplaatsId { get; set; }
        public int Parent { get; set; }
        public int NewId { get; set; }
        public DateTime OldDatum { get; set; }
        public string Treeviewtype { get; set; }
        public int KeuringsinstantieId { get; set; }
        public int RieNr { get; set; }
        public int Onderhoud { get; set; }
        public bool Onderhoudstemplate { get; set; }
        public int TreeviewsoortId { get; set; }
        public bool ShowVisual { get; set; }
        public string Gecodeerd { get; set; }
        public int EigenaarId { get; set; }
        public int Keuringsplichtig { get; set; }
        public DateTime? Laatstgeteld { get; set; }
        public int OnderhoudsbedrijfId { get; set; }
        public string StamkaartOld { get; set; }
        public int ObjecttemplateId { get; set; }
        public int KoppelrelatieId { get; set; }
        public int Nlsfb2Id { get; set; }
        public decimal VastgoedAantal { get; set; }
        public int VastgoedEenhedenId { get; set; }
        public int? KoppelpersoonId { get; set; }
        public int? Koppelrelatie2Id { get; set; }
        public int? Koppelpersoon2Id { get; set; }
        public int MedewerkerId { get; set; }
        public int OmschrijvingId { get; set; }
        public int OpgenomenInBegroting { get; set; }
        public DateTime? OpgenomenInBegrotingDatum { get; set; }
        public int UitleenMagazijnId { get; set; }
        public int UitleenTreeviewsoortId { get; set; }
        public int IsUitgeleend { get; set; }
        public int UitleenStatus { get; set; }
        public int Uitleenbaar { get; set; }
        public string Barcode { get; set; }
        public DateTime AangemaaktOp { get; set; }
        public string AangemaaktDoor { get; set; }
        public int NonactiefId { get; set; }
        public string Dragernr { get; set; }
        public int StamkaartenId { get; set; }
        public string Maat { get; set; }
        public int DeliveryaddressNumber { get; set; }
        public string DeliveryaddressName { get; set; }
        public int Kastnr { get; set; }
        public int Vak { get; set; }
        public DateTime DatumInbehandeling { get; set; }
        public DateTime DatumLaatsteWasbeurt { get; set; }
        public string Kenteken { get; set; }
        public DateTime DatumUitleen { get; set; }
        public DateTime GeplandeDatumOntvangst { get; set; }
        public DateTime DatumLaatsteUitscan { get; set; }
        public DateTime DatumAflevering { get; set; }
        public double LaatsteTellerstand { get; set; }
        public DateTime LaatsteBeurtDatum { get; set; }
        public bool TellerstandOpgenomen { get; set; }
        public bool Geaccepteerd { get; set; }
        public bool SweepApi { get; set; }
        public int LaatsteBeurt { get; set; }
        public int? LaatsteServicebeurtId { get; set; }
        public int HoofdprocessenId { get; set; }
        public int ProcesstappenId { get; set; }
        public int MachineMonitoringenTimeoutSec { get; set; }
        public int ToegangTypeId { get; set; }
        public bool ExternToegangsbeleid { get; set; }
        public double MachineMonitoringStreefCyclusTijd { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
    }
}
