using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.Entities;

namespace Netflix.Infrastructure;

public partial class NetflixProjectContext(DbContextOptions<NetflixProjectContext> options) : DbContext(options)
{
    public virtual DbSet<ActorModel> ActorModels { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<DateTimeSession> DateTimeSessions { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmActor> FilmActors { get; set; }

    public virtual DbSet<FilmGenre> FilmGenres { get; set; }

    public virtual DbSet<FilmSession> FilmSessions { get; set; }

    public virtual DbSet<GenreModel> GenreModels { get; set; }

    public virtual DbSet<OperationLog> OperationLogs { get; set; }

    public virtual DbSet<OperationType> OperationTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<SeriesActor> SeriesActors { get; set; }

    public virtual DbSet<SeriesGenre> SeriesGenres { get; set; }
    public virtual DbSet<SeriesEpisode> SeriesEpisode { get; set; }

    public virtual DbSet<Showing> Showings { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ActorMod__3213E83FE59845D5");

            entity.ToTable("ActorModel");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cinema__3213E83FE2700ADC");

            entity.ToTable("Cinema");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(70);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3213E83FE3E883EE");

            entity.ToTable("Client");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("Birth_Date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsActor).HasColumnName("isActor");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(30);
        });

        modelBuilder.Entity<DateTimeSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DateTime__3213E83F5DA07817");

            entity.ToTable("DateTimeSession");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Film__3213E83F94EE4918");

            entity.ToTable("Film");

            entity.HasIndex(e => e.IdProduct, "UQ__Film__5EEC79D03E8FF8F5").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.About).HasMaxLength(1000);
            entity.Property(e => e.AgeLimit).HasColumnName("Age_Limit");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Director).HasMaxLength(70);
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(200)
                .HasColumnName("PictureURL");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(200)
                .HasColumnName("VideoURL");
            entity.Property(e => e.ProductionCompanies)
                .HasMaxLength(150)
                .HasColumnName("Production_Companies");
            entity.Property(e => e.Rating).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release_date");

            entity.HasOne(d => d.IdProductNavigation).WithOne(p => p.Film)
                .HasForeignKey<Film>(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Film__idProduct__4316F928");

            entity.HasMany<ActorModel>(f => f.Actors).WithMany(a => a.Films).UsingEntity<FilmActor>();

            entity.HasMany<GenreModel>(f => f.Genres).WithMany(a => a.Films).UsingEntity<FilmGenre>();
        });

        modelBuilder.Entity<FilmActor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FilmActo__3213E83F04B84BE4");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdActorsInRoles).HasColumnName("idActorsInRoles");
            entity.Property(e => e.IdFilm).HasColumnName("idFilm");

            entity.HasOne(d => d.IdActorsInRolesNavigation).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.IdActorsInRoles)
                .HasConstraintName("FK__FilmActor__idAct__60A75C0F");

            entity.HasOne(d => d.IdFilmNavigation).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.IdFilm)
                .HasConstraintName("FK__FilmActor__idFil__5FB337D6");
        });

        modelBuilder.Entity<FilmGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FilmGenr__3213E83F4F99C09D");

            entity.ToTable("FilmGenre");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdFilm).HasColumnName("idFilm");
            entity.Property(e => e.IdGenreInFilm).HasColumnName("idGenreInFilm");

            entity.HasOne(d => d.IdFilmNavigation).WithMany(p => p.FilmGenres)
                .HasForeignKey(d => d.IdFilm)
                .HasConstraintName("FK__FilmGenre__idFil__5AEE82B9");

            entity.HasOne(d => d.IdGenreInFilmNavigation).WithMany(p => p.FilmGenres)
                .HasForeignKey(d => d.IdGenreInFilm)
                .HasConstraintName("FK__FilmGenre__idGen__5BE2A6F2");
        });

        modelBuilder.Entity<FilmSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FilmSess__3213E83FC9452FF2");

            entity.ToTable("FilmSession");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdCinema).HasColumnName("idCinema");
            entity.Property(e => e.IdFilm).HasColumnName("idFilm");
            entity.Property(e => e.Is3d).HasColumnName("is3d");
            entity.Property(e => e.IsSubtitles).HasColumnName("isSubtitles");

            entity.HasOne(d => d.IdCinemaNavigation).WithMany(p => p.FilmSessions)
                .HasForeignKey(d => d.IdCinema)
                .HasConstraintName("FK__FilmSessi__idCin__73BA3083");

            entity.HasOne(d => d.IdFilmNavigation).WithMany(p => p.FilmSessions)
                .HasForeignKey(d => d.IdFilm)
                .HasConstraintName("FK__FilmSessi__idFil__72C60C4A");
        });

        modelBuilder.Entity<GenreModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GenreMod__3213E83F9185B16B");

            entity.ToTable("GenreModel");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.GenreName).HasMaxLength(50);
        });

        modelBuilder.Entity<OperationLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3213E83FAB818DA6");

            entity.ToTable("OperationLog");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.OperationDateTimeEnd).HasColumnType("datetime");
            entity.Property(e => e.OperationDateTimeStart).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.OperationLogs)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Operation__idCli__6B24EA82");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.OperationLogs)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__Operation__idPro__6C190EBB");

            entity.HasOne(d => d.OperationTypeNavigation).WithMany(p => p.OperationLogs)
                .HasForeignKey(d => d.OperationType)
                .HasConstraintName("FK__Operation__Opera__6A30C649");
        });

        modelBuilder.Entity<OperationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3213E83F4359062C");

            entity.ToTable("OperationType");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.TypeName).HasMaxLength(30);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83F4420113F");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.IdShowing).HasColumnName("idShowing");
            entity.Property(e => e.TicketCount).HasColumnName("Ticket_Count");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Order__idClient__7D439ABD");

            entity.HasOne(d => d.IdShowingNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdShowing)
                .HasConstraintName("FK__Order__idShowing__7C4F7684");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83F8A74FC9E");

            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Series__3213E83F3EA6D419");

            entity.HasIndex(e => e.IdProduct, "UQ__Series__5EEC79D036703ABB").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.About).HasMaxLength(1000);
            entity.Property(e => e.AgeLimit).HasColumnName("Age_Limit");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Director).HasMaxLength(70);
            entity.Property(e => e.EpisodeCount).HasColumnName("Episode_Count");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IsFinished).HasColumnName("isFinished");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(200)
                .HasColumnName("PictureURL");
            entity.Property(e => e.ProductionCompanies)
                .HasMaxLength(150)
                .HasColumnName("Production_Companies");
            entity.Property(e => e.Rating).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release_date");
            entity.Property(e => e.SeasonCount).HasColumnName("Season_Count");

            entity.HasOne(d => d.IdProductNavigation).WithOne(p => p.Series)
                .HasForeignKey<Series>(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Series__idProduc__4AB81AF0");

            entity.HasMany<ActorModel>(f => f.Actors).WithMany(a => a.Series).UsingEntity<SeriesActor>();

            entity.HasMany<GenreModel>(f => f.Genres).WithMany(a => a.Series).UsingEntity<SeriesGenre>();

        });

        modelBuilder.Entity<SeriesEpisode>(entity =>
        {
            entity.HasKey(e => e.EpisodeId).HasName("PK__SeriesEp__AC6609F56A616652");


            entity.Property(e => e.EpisodeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EpisodeId");

            entity.Property(e => e.EpisodeName).HasColumnName("EpisodeName");
            entity.Property(e => e.EpisodeNumber).HasColumnName("EpisodeNumber");
            entity.Property(e => e.EpisodeNumberInSeason).HasColumnName("EpisodeNumberInSeason");
            entity.Property(e => e.SeasonNumber).HasColumnName("SeasonNumber");
            entity.Property(e => e.VideoURL).HasColumnName("VideoURL");
            entity.Property(e => e.PictureURL).HasColumnName("PictureURL");

            entity.HasOne(d => d.Series).WithMany(p => p.SeriesEpisodes)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SeriesEpi__Serie__05D8E0BE");
            //entity.HasOne(d => d.Series).WithMany(s => s.SeriesEpisodes)
            //    .HasForeignKey(d => d.SeriesId)
            //    .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SeriesActor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeriesAc__3213E83FC5B9A0A9");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdActorsInRoles).HasColumnName("idActorsInRoles");
            entity.Property(e => e.IdSeries).HasColumnName("idSeries");

            entity.HasOne(d => d.IdActorsInRolesNavigation).WithMany(p => p.SeriesActors)
                .HasForeignKey(d => d.IdActorsInRoles)
                .HasConstraintName("FK__SeriesAct__idAct__52593CB8");

            entity.HasOne(d => d.IdSeriesNavigation).WithMany(p => p.SeriesActors)
                .HasForeignKey(d => d.IdSeries)
                .HasConstraintName("FK__SeriesAct__idSer__5165187F");
        });

        modelBuilder.Entity<SeriesGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeriesGe__3213E83F320717F9");

            entity.ToTable("SeriesGenre");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdGenreInSeries).HasColumnName("idGenreInSeries");
            entity.Property(e => e.IdSeries).HasColumnName("idSeries");

            entity.HasOne(d => d.IdGenreInSeriesNavigation).WithMany(p => p.SeriesGenres)
                .HasForeignKey(d => d.IdGenreInSeries)
                .HasConstraintName("FK__SeriesGen__idGen__571DF1D5");

            entity.HasOne(d => d.IdSeriesNavigation).WithMany(p => p.SeriesGenres)
                .HasForeignKey(d => d.IdSeries)
                .HasConstraintName("FK__SeriesGen__idSer__5629CD9C");
        });



        modelBuilder.Entity<Showing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Showing__3213E83F1B56C874");

            entity.ToTable("Showing");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdDateTimeSession).HasColumnName("idDateTimeSession");
            entity.Property(e => e.IdFilmSession).HasColumnName("idFilmSession");

            entity.HasOne(d => d.IdDateTimeSessionNavigation).WithMany(p => p.Showings)
                .HasForeignKey(d => d.IdDateTimeSession)
                .HasConstraintName("FK__Showing__idDateT__787EE5A0");

            entity.HasOne(d => d.IdFilmSessionNavigation).WithMany(p => p.Showings)
                .HasForeignKey(d => d.IdFilmSession)
                .HasConstraintName("FK__Showing__idFilmS__778AC167");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3213E83F613A78B0");

            entity.ToTable("Ticket");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
