using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backend_DAL.DataAccess.DataObjects
{
    public partial class q3_mms_dbContext : DbContext
    {
        public q3_mms_dbContext()
        {
        }

        public q3_mms_dbContext(DbContextOptions<q3_mms_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MachineMonitoringPoorten> MachineMonitoringPoortens { get; set; }
        public virtual DbSet<MonitoringData202009> MonitoringData202009s { get; set; }
        public virtual DbSet<ProductionDatum> ProductionData { get; set; }
        public virtual DbSet<Tellerbasis> Tellerbases { get; set; }
        public virtual DbSet<Tellerstanden> Tellerstandens { get; set; }
        public virtual DbSet<Treeview> Treeviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;port=3307;user=root;password=root;database=q3_mms_db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MachineMonitoringPoorten>(entity =>
            {
                entity.ToTable("machine_monitoring_poorten");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Board).HasColumnName("board");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.Property(e => e.Volgorde).HasColumnName("volgorde");
            });

            modelBuilder.Entity<MonitoringData202009>(entity =>
            {
                entity.ToTable("monitoring_data_202009");

                entity.HasIndex(e => e.Board, "board");

                entity.HasIndex(e => new { e.Board, e.Port }, "board_port");

                entity.HasIndex(e => new { e.Board, e.Port, e.Datum }, "board_port_datum");

                entity.HasIndex(e => e.Code, "code");

                entity.HasIndex(e => e.Datum, "datum");

                entity.HasIndex(e => e.Port, "port");

                entity.HasIndex(e => e.Timestamp, "timestamp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Board)
                    .HasColumnType("tinyint")
                    .HasColumnName("board");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Code2).HasColumnName("code2");

                entity.Property(e => e.Com)
                    .HasColumnType("tinyint")
                    .HasColumnName("com");

                entity.Property(e => e.Datum)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Port)
                    .HasColumnType("tinyint")
                    .HasColumnName("port");

                entity.Property(e => e.PreviousShotId).HasColumnName("previous_shot_id");

                entity.Property(e => e.ShotTime)
                    .HasColumnType("double(11,6)")
                    .HasColumnName("shot_time");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime(6)")
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<ProductionDatum>(entity =>
            {
                entity.ToTable("production_data");

                entity.HasIndex(e => e.Board, "board");

                entity.HasIndex(e => new { e.EndDate, e.EndTime }, "end_date_end_time");

                entity.HasIndex(e => e.Port, "port");

                entity.HasIndex(e => new { e.StartDate, e.StartTime }, "start_date_start_time");

                entity.HasIndex(e => e.TreeviewId, "treeview_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("double(11,2)")
                    .HasColumnName("amount")
                    .HasComment("Hoeveel wordt er per run geproduceerd");

                entity.Property(e => e.Board)
                    .HasColumnType("tinyint")
                    .HasColumnName("board");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date")
                    .HasDefaultValueSql("'0000-00-00'");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasDefaultValueSql("'00:00:00'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Port)
                    .HasColumnType("tinyint")
                    .HasColumnName("port");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date")
                    .HasDefaultValueSql("'0000-00-00'");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasDefaultValueSql("'00:00:00'");

                entity.Property(e => e.Treeview2Id)
                    .HasColumnName("treeview2_id")
                    .HasComment("Een \"Hot half\"");

                entity.Property(e => e.TreeviewId).HasColumnName("treeview_id");
            });

            modelBuilder.Entity<Tellerbasis>(entity =>
            {
                entity.ToTable("tellerbasis");

                entity.HasIndex(e => e.Actief, "actief");

                entity.HasIndex(e => e.Naam, "naam");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actief).HasColumnName("actief");

                entity.Property(e => e.Afkorting)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("afkorting");

                entity.Property(e => e.MaxWaarde)
                    .HasColumnType("double(11,2)")
                    .HasColumnName("max_waarde");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("naam")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("omschrijving")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optie).HasColumnName("optie");
            });

            modelBuilder.Entity<Tellerstanden>(entity =>
            {
                entity.ToTable("tellerstanden");

                entity.HasIndex(e => e.TellerbasisId, "tellerbasis_id");

                entity.HasIndex(e => e.TreeviewId, "treeview_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datum).HasColumnName("datum");

                entity.Property(e => e.TellerbasisId).HasColumnName("tellerbasis_id");

                entity.Property(e => e.Totaal)
                    .HasColumnType("decimal(11,2)")
                    .HasColumnName("totaal");

                entity.Property(e => e.TreeviewId).HasColumnName("treeview_id");

                entity.Property(e => e.Waarde)
                    .HasColumnType("decimal(11,2)")
                    .HasColumnName("waarde");
            });

            modelBuilder.Entity<Treeview>(entity =>
            {
                entity.ToTable("treeview");

                entity.HasIndex(e => e.Actief, "actief");

                entity.HasIndex(e => e.Bouwjaar, "bouwjaar");

                entity.HasIndex(e => e.EigenaarId, "eigenaar_id");

                entity.HasIndex(e => e.FabrikantenId, "fabrikanten_id");

                entity.HasIndex(e => e.HoofdprocessenId, "hoofdprocessen_id");

                entity.HasIndex(e => e.IsUitgeleend, "is_uitgeleend");

                entity.HasIndex(e => e.Jaarafschrijving, "jaarafschrijving");

                entity.HasIndex(e => e.KeuringsinstantieId, "keuringsinstantie_id");

                entity.HasIndex(e => e.Keuringsplichtig, "keuringsplichtig");

                entity.HasIndex(e => e.KoppelpersoonId, "koppelpersoon_id");

                entity.HasIndex(e => e.Koppelrelatie2Id, "koppelrelatie2_id");

                entity.HasIndex(e => e.KoppelrelatieId, "koppelrelatie_id");

                entity.HasIndex(e => e.KostenplaatsId, "kostenplaats_id");

                entity.HasIndex(e => e.LeverancierenId, "leverancieren_id");

                entity.HasIndex(e => e.LocatiesId, "locaties_id");

                entity.HasIndex(e => e.MedewerkerId, "medewerker_id");

                entity.HasIndex(e => e.Naam, "naam");

                entity.HasIndex(e => e.NewId, "new_id");

                entity.HasIndex(e => e.Nlsfb2Id, "nlsfb2_id");

                entity.HasIndex(e => e.OmschrijvingId, "omschrijving_id");

                entity.HasIndex(e => e.OnderhoudsbedrijfId, "onderhoudsbedrijf_id");

                entity.HasIndex(e => e.Parent, "parent");

                entity.HasIndex(e => e.ProcesstappenId, "processtappen_id");

                entity.HasIndex(e => e.RieNr, "rie_nr");

                entity.HasIndex(e => e.Serienummer, "serienummer");

                entity.HasIndex(e => e.ShowVisual, "show_visual");

                entity.HasIndex(e => e.ToegangTypeId, "toegang_type_id");

                entity.HasIndex(e => e.TreeviewsoortId, "treeviewsoort_id");

                entity.HasIndex(e => e.TreeviewtypeId, "treeviewtype_id");

                entity.HasIndex(e => e.UitleenMagazijnId, "uitleen_magazijn_id");

                entity.HasIndex(e => e.UitleenTreeviewsoortId, "uitleen_treeviewsoort_id");

                entity.HasIndex(e => e.VastgoedEenhedenId, "vastgoed_eenheden_id");

                entity.HasIndex(e => e.Wijzigactief, "wijzigactief");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AangemaaktDoor)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("aangemaakt_door");

                entity.Property(e => e.AangemaaktOp).HasColumnName("aangemaakt_op");

                entity.Property(e => e.Aanschafwaarde)
                    .HasColumnType("decimal(11,2)")
                    .HasColumnName("aanschafwaarde");

                entity.Property(e => e.Actief)
                    .HasColumnName("actief")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("adres")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Afschrijving).HasColumnName("afschrijving");

                entity.Property(e => e.Afschrijvingeen).HasColumnName("afschrijvingeen");

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("barcode");

                entity.Property(e => e.BoomVolgorde).HasColumnName("boom_volgorde");

                entity.Property(e => e.Bouwjaar)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("bouwjaar")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Budgetnu)
                    .HasColumnType("decimal(11,2)")
                    .HasColumnName("budgetnu");

                entity.Property(e => e.Budgetvorig)
                    .HasColumnType("decimal(11,2)")
                    .HasColumnName("budgetvorig");

                entity.Property(e => e.Correctief)
                    .HasColumnName("correctief")
                    .HasComment("Mag er op dit object correctief gemeld worden?");

                entity.Property(e => e.DatumAflevering)
                    .HasColumnType("date")
                    .HasColumnName("datum_aflevering");

                entity.Property(e => e.DatumInbehandeling)
                    .HasColumnType("date")
                    .HasColumnName("datum_inbehandeling");

                entity.Property(e => e.DatumLaatsteUitscan)
                    .HasColumnType("date")
                    .HasColumnName("datum_laatste_uitscan");

                entity.Property(e => e.DatumLaatsteWasbeurt)
                    .HasColumnType("date")
                    .HasColumnName("datum_laatste_wasbeurt");

                entity.Property(e => e.DatumUitleen)
                    .HasColumnType("date")
                    .HasColumnName("datum_uitleen");

                entity.Property(e => e.DeliveryaddressName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("deliveryaddress_name");

                entity.Property(e => e.DeliveryaddressNumber).HasColumnName("deliveryaddress_number");

                entity.Property(e => e.Dragernr)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("dragernr");

                entity.Property(e => e.EigenaarId).HasColumnName("eigenaar_id");

                entity.Property(e => e.ExternToegangsbeleid).HasColumnName("extern_toegangsbeleid");

                entity.Property(e => e.FabrikantenId).HasColumnName("fabrikanten_id");

                entity.Property(e => e.Garantietot).HasColumnName("garantietot");

                entity.Property(e => e.Geaccepteerd)
                    .HasColumnName("geaccepteerd")
                    .HasComment("JOU-1: Is het object geaccepteerd als klant");

                entity.Property(e => e.Gecodeerd)
                    .IsRequired()
                    .HasMaxLength(65)
                    .HasColumnName("gecodeerd")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.GeplandeDatumOntvangst)
                    .HasColumnType("date")
                    .HasColumnName("geplande_datum_ontvangst");

                entity.Property(e => e.HoofdprocessenId).HasColumnName("hoofdprocessen_id");

                entity.Property(e => e.Installatiedatum).HasColumnName("installatiedatum");

                entity.Property(e => e.IsUitgeleend).HasColumnName("is_uitgeleend");

                entity.Property(e => e.Jaarafschrijving).HasColumnName("jaarafschrijving");

                entity.Property(e => e.Kastnr).HasColumnName("kastnr");

                entity.Property(e => e.Kenteken)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("kenteken");

                entity.Property(e => e.KeuringsinstantieId).HasColumnName("keuringsinstantie_id");

                entity.Property(e => e.Keuringsplichtig).HasColumnName("keuringsplichtig");

                entity.Property(e => e.Koppelpersoon2Id).HasColumnName("koppelpersoon2_id");

                entity.Property(e => e.KoppelpersoonId).HasColumnName("koppelpersoon_id");

                entity.Property(e => e.Koppelrelatie2Id).HasColumnName("koppelrelatie2_id");

                entity.Property(e => e.KoppelrelatieId).HasColumnName("koppelrelatie_id");

                entity.Property(e => e.KostenplaatsId).HasColumnName("kostenplaats_id");

                entity.Property(e => e.LaatsteBeurt)
                    .HasColumnName("laatste_beurt")
                    .HasComment("Dit is frequentie van de laatste beurt ");

                entity.Property(e => e.LaatsteBeurtDatum)
                    .HasColumnType("date")
                    .HasColumnName("laatste_beurt_datum")
                    .HasComment("Wanneer is de laatste beurt uitgevoerd voor ingebruikname van dit object.");

                entity.Property(e => e.LaatsteServicebeurtId)
                    .HasColumnName("laatste_servicebeurt_id")
                    .HasComment("Welke servicebeurt is het laatste uitgevoerd voor ingebruikname van dit object");

                entity.Property(e => e.LaatsteTellerstand)
                    .HasColumnType("double(11,2)")
                    .HasColumnName("laatste_tellerstand")
                    .HasComment("Wat was de tellerstand bij de laatste beurt voor de ingebruikname van dit object");

                entity.Property(e => e.Laatstgeteld)
                    .HasColumnType("date")
                    .HasColumnName("laatstgeteld");

                entity.Property(e => e.LeverancierenId).HasColumnName("leverancieren_id");

                entity.Property(e => e.LocatiesId).HasColumnName("locaties_id");

                entity.Property(e => e.Maat)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("maat");

                entity.Property(e => e.MachineMonitoringStreefCyclusTijd)
                    .HasColumnType("double(11,2)")
                    .HasColumnName("machine_monitoring_streef_cyclus_tijd");

                entity.Property(e => e.MachineMonitoringenTimeoutSec)
                    .HasColumnName("machine_monitoringen_timeout_sec")
                    .HasDefaultValueSql("'30'");

                entity.Property(e => e.MedewerkerId)
                    .HasColumnName("medewerker_id")
                    .HasComment("personen_id ( Gemaakt voor Joulz 01-03-2012 )");

                entity.Property(e => e.Melden)
                    .HasColumnName("melden")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("naam")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.NewId).HasColumnName("new_id");

                entity.Property(e => e.Nlsfb2Id).HasColumnName("nlsfb2_id");

                entity.Property(e => e.NonactiefId)
                    .HasColumnName("nonactief_id")
                    .HasComment("Aparte status voor Joulz voor wanneer een object kapot is, of gestolen, etc.. 20-06-2013");

                entity.Property(e => e.Object)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("object")
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.ObjecttemplateId).HasColumnName("objecttemplate_id");

                entity.Property(e => e.OldDatum)
                    .HasColumnType("date")
                    .HasColumnName("old_datum")
                    .HasDefaultValueSql("'0000-00-00'");

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("omschrijving")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OmschrijvingId).HasColumnName("omschrijving_id");

                entity.Property(e => e.Onderhoud).HasColumnName("onderhoud");

                entity.Property(e => e.OnderhoudsbedrijfId)
                    .HasColumnName("onderhoudsbedrijf_id")
                    .HasComment("Derden - Voor onderhoud bedr.");

                entity.Property(e => e.Onderhoudstemplate)
                    .HasColumnName("onderhoudstemplate")
                    .HasComment("Mag dit object worden opgenomen in een onderhoudstemplate?");

                entity.Property(e => e.OpgenomenInBegroting).HasColumnName("opgenomen_in_begroting");

                entity.Property(e => e.OpgenomenInBegrotingDatum).HasColumnName("opgenomen_in_begroting_datum");

                entity.Property(e => e.Parent).HasColumnName("parent");

                entity.Property(e => e.Plaats)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("plaats")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("postcode")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ProcesstappenId).HasColumnName("processtappen_id");

                entity.Property(e => e.RieNr).HasColumnName("rie_nr");

                entity.Property(e => e.Serienummer)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("serienummer")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ShowVisual).HasColumnName("show_visual");

                entity.Property(e => e.Stamkaart)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("stamkaart");

                entity.Property(e => e.StamkaartOld)
                    .HasColumnType("mediumtext")
                    .HasColumnName("stamkaart_old");

                entity.Property(e => e.StamkaartenId).HasColumnName("stamkaarten_id");

                entity.Property(e => e.SweepApi)
                    .HasColumnName("sweep_api")
                    .HasComment("Mogen er werkorders middels de SWEEP API ingeschoten worden voor dit object?");

                entity.Property(e => e.TellerstandOpgenomen)
                    .HasColumnName("tellerstand_opgenomen")
                    .HasComment("Dit geeft aan of de tellerstand al eens is opgegeven. M.a.w. moet er een aparte berekening worden uitgevoerd bij de opnamelijst.");

                entity.Property(e => e.ToegangTypeId).HasColumnName("toegang_type_id");

                entity.Property(e => e.TreeviewsoortId).HasColumnName("treeviewsoort_id");

                entity.Property(e => e.Treeviewtype)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("treeviewtype")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.TreeviewtypeId).HasColumnName("treeviewtype_id");

                entity.Property(e => e.UitleenMagazijnId).HasColumnName("uitleen_magazijn_id");

                entity.Property(e => e.UitleenStatus).HasColumnName("uitleen_status");

                entity.Property(e => e.UitleenTreeviewsoortId).HasColumnName("uitleen_treeviewsoort_id");

                entity.Property(e => e.Uitleenbaar).HasColumnName("uitleenbaar");

                entity.Property(e => e.Vak).HasColumnName("vak");

                entity.Property(e => e.VastgoedAantal)
                    .HasColumnType("decimal(11,2)")
                    .HasColumnName("vastgoed_aantal");

                entity.Property(e => e.VastgoedEenhedenId).HasColumnName("vastgoed_eenheden_id");

                entity.Property(e => e.Vrijgegeven).HasColumnName("vrijgegeven");

                entity.Property(e => e.Werkopdracht)
                    .HasColumnName("werkopdracht")
                    .HasComment("Mag er op dit object een werkopdracht aangemaakt worden?");

                entity.Property(e => e.Wijzigactief).HasColumnName("wijzigactief");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
