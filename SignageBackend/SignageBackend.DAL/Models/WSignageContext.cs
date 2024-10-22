using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SignageBackend.DAL.Models;

public partial class WSignageContext : DbContext
{
    public WSignageContext()
    {
    }

    public WSignageContext(DbContextOptions<WSignageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Groupy> Groups { get; set; }

    public virtual DbSet<GroupBy> GroupBies { get; set; }

    public virtual DbSet<GroupCampaign> GroupCampaigns { get; set; }

    public virtual DbSet<GroupSchedule> GroupSchedules { get; set; }

    public virtual DbSet<GroupScreen> GroupScreens { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<Layout> Layouts { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Network> Networks { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerContent> PlayerContents { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Resolution> Resolutions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleScreen> ScheduleScreens { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Widget> Widgets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=103.171.181.223,1433;Database=W_Signage;Integrated Security=False;Persist Security Info=True;User ID=W_SignageUser;Password=WSignange123;Encrypt=True;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK__Alerts__EBB16AEDC7022279");

            entity.Property(e => e.AlertId).HasColumnName("AlertID");
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.Severity).HasMaxLength(50);

            entity.HasOne(d => d.Player).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__Alerts__PlayerID__628FA481");
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__Campaign__3F5E8D797C93471C");

            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Campaigns__Creat__656C112C");

            entity.HasMany(d => d.Contents).WithMany(p => p.Campaigns)
                .UsingEntity<Dictionary<string, object>>(
                    "CampaignContent",
                    r => r.HasOne<Content>().WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CampaignC__Conte__6477ECF3"),
                    l => l.HasOne<Campaign>().WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CampaignC__Campa__6383C8BA"),
                    j =>
                    {
                        j.HasKey("CampaignId", "ContentId").HasName("PK__Campaign__CDCEF7FE988E4D17");
                        j.ToTable("CampaignContent");
                        j.IndexerProperty<int>("CampaignId").HasColumnName("CampaignID");
                        j.IndexerProperty<int>("ContentId").HasColumnName("ContentID");
                    });

            entity.HasMany(d => d.Schedules).WithMany(p => p.Campaigns)
                .UsingEntity<Dictionary<string, object>>(
                    "CampaignSchedule",
                    r => r.HasOne<Schedule>().WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CampaignS__Sched__6754599E"),
                    l => l.HasOne<Campaign>().WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CampaignS__Campa__66603565"),
                    j =>
                    {
                        j.HasKey("CampaignId", "ScheduleId").HasName("PK__Campaign__B69628CF3AE5BEC8");
                        j.ToTable("CampaignSchedules");
                        j.IndexerProperty<int>("CampaignId").HasColumnName("CampaignID");
                        j.IndexerProperty<int>("ScheduleId").HasColumnName("ScheduleID");
                    });
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Content__2907A87E31F2DD63");

            entity.ToTable("Content");

            entity.Property(e => e.ContentId).HasColumnName("ContentID");
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Contents)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Content__Created__68487DD7");

            entity.HasMany(d => d.Folders).WithMany(p => p.Contents)
                .UsingEntity<Dictionary<string, object>>(
                    "ContentFolder",
                    r => r.HasOne<Folder>().WithMany()
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ContentFo__Folde__6A30C649"),
                    l => l.HasOne<Content>().WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ContentFo__Conte__693CA210"),
                    j =>
                    {
                        j.HasKey("ContentId", "FolderId").HasName("PK__ContentF__C3CAD977DE7FB809");
                        j.ToTable("ContentFolders");
                        j.IndexerProperty<int>("ContentId").HasColumnName("ContentID");
                        j.IndexerProperty<int>("FolderId").HasColumnName("FolderID");
                    });
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.FolderId).HasName("PK__Folders__ACD7109FC0F56F41");

            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Folders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Folders__Created__6B24EA82");
        });

        modelBuilder.Entity<Groupy>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__D57795A0AC6BB165");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GroupName)
                .HasMaxLength(255)
                .HasColumnName("group_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<GroupBy>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK_GroupModule");

            entity.ToTable("GroupBy");

            entity.Property(e => e.GroupId)
                .ValueGeneratedNever()
                .HasColumnName("GroupID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GroupCampaign>(entity =>
        {
            entity.HasKey(e => e.GroupCampaignId).HasName("PK__Group_Ca__403D628234C2FD86");

            entity.ToTable("Group_Campaigns");

            entity.Property(e => e.GroupCampaignId).HasColumnName("group_campaign_id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<GroupSchedule>(entity =>
        {
            entity.HasKey(e => e.GroupScheduleId).HasName("PK__Group_Sc__08BEE2434BAAA532");

            entity.ToTable("Group_Schedules");

            entity.Property(e => e.GroupScheduleId).HasColumnName("group_schedule_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<GroupScreen>(entity =>
        {
            entity.HasKey(e => e.GroupScreenId).HasName("PK__Group_Sc__6C7B7D24F0AE0D63");

            entity.ToTable("Group_Screens");

            entity.Property(e => e.GroupScreenId).HasColumnName("group_screen_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.ScreenId).HasColumnName("screen_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.IntegrationId).HasName("PK__Integrat__D89568552FB64A67");

            entity.Property(e => e.IntegrationId).HasColumnName("IntegrationID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Integrati__Creat__6C190EBB");
        });

        modelBuilder.Entity<Layout>(entity =>
        {
            entity.HasKey(e => e.LayoutId).HasName("PK__Layouts__203586F526B4BB5D");

            entity.Property(e => e.LayoutId).HasColumnName("LayoutID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.XmlDesign).HasColumnType("ntext");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Layouts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Layouts__Created__6D0D32F4");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Logs__5E5499A8AE74B7C7");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Logs__UserID__6E01572D");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("Module");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ModuleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Network>(entity =>
        {
            entity.HasKey(e => e.NetworkId).HasName("PK__Networks__4DD57BEB8BC49F64");

            entity.Property(e => e.NetworkId).HasColumnName("NetworkID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Networks)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Networks__Create__6EF57B66");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");

            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permissio__Modul__55F4C372");

            //entity.HasOne(d => d.User).WithMany(p => p.Permissions)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Permissio__UserI__56E8E7AB");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74A85C581028");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Os)
                .HasMaxLength(50)
                .HasColumnName("OS");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasMany(d => d.Networks).WithMany(p => p.Players)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerNetwork",
                    r => r.HasOne<Network>().WithMany()
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerNet__Netwo__71D1E811"),
                    l => l.HasOne<Player>().WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerNet__Playe__72C60C4A"),
                    j =>
                    {
                        j.HasKey("PlayerId", "NetworkId").HasName("PK__PlayerNe__EE932316DCC421FF");
                        j.ToTable("PlayerNetworks");
                        j.IndexerProperty<int>("PlayerId").HasColumnName("PlayerID");
                        j.IndexerProperty<int>("NetworkId").HasColumnName("NetworkID");
                    });
        });

        modelBuilder.Entity<PlayerContent>(entity =>
        {
            entity.HasKey(e => new { e.PlayerId, e.ContentId }).HasName("PK__PlayerCo__B8DE0E2F0DC834F2");

            entity.ToTable("PlayerContent");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.ContentId).HasColumnName("ContentID");

            entity.HasOne(d => d.Content).WithMany(p => p.PlayerContents)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerCon__Conte__6FE99F9F");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerContents)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerCon__Playe__70DDC3D8");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__Regions__ACD844433B2D1C9F");

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.LayoutId).HasColumnName("LayoutID");

            entity.HasOne(d => d.Layout).WithMany(p => p.Regions)
                .HasForeignKey(d => d.LayoutId)
                .HasConstraintName("FK__Regions__LayoutI__73BA3083");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E522FEB777");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.ReportType).HasMaxLength(50);

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.GeneratedBy)
                .HasConstraintName("FK__Reports__Generat__74AE54BC");
        });

        modelBuilder.Entity<Resolution>(entity =>
        {
            entity.ToTable("Resolution");

            entity.Property(e => e.ResolutionId).HasColumnName("ResolutionID");
            entity.Property(e => e.Optional).HasMaxLength(250);
            entity.Property(e => e.ResolutionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Scale).HasColumnType("decimal(12, 12)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleName");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B6940F61982");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.LayoutId).HasColumnName("LayoutID");
            entity.Property(e => e.Recurrence).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Schedules__Creat__08B54D69");

            entity.HasOne(d => d.Layout).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.LayoutId)
                .HasConstraintName("FK__Schedules__Layou__489AC854");
        });

        modelBuilder.Entity<ScheduleScreen>(entity =>
        {
            entity.ToTable("ScheduleScreen");

            entity.Property(e => e.ScheduleScreenId)
                .ValueGeneratedNever()
                .HasColumnName("ScheduleScreenID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleScreens)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ScheduleS__Sched__57DD0BE4");

            entity.HasOne(d => d.Screen).WithMany(p => p.ScheduleScreens)
                .HasForeignKey(d => d.ScreenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ScheduleS__Scree__58D1301D");
        });

        modelBuilder.Entity<Screen>(entity =>
        {
            entity.ToTable("Screen");

            entity.Property(e => e.ScreenId).HasColumnName("ScreenID");
            entity.Property(e => e.CurrentLayout).HasMaxLength(50);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(20)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.MacAddressId)
                .HasMaxLength(50)
                .HasColumnName("MacAddressID");
            entity.Property(e => e.ScreenName).HasMaxLength(50);
            entity.Property(e => e.StatusDevice).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC76F8C306");

            entity.ToTable(tb => tb.HasTrigger("trg_Users_Update"));

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534BD025AAF").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_userRoles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Widget>(entity =>
        {
            entity.HasKey(e => e.WidgetId).HasName("PK__Widgets__ADFD30728A840AC8");

            entity.Property(e => e.WidgetId).HasColumnName("WidgetID");
            entity.Property(e => e.ContentId).HasColumnName("ContentID");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Content).WithMany(p => p.Widgets)
                .HasForeignKey(d => d.ContentId)
                .HasConstraintName("FK__Widgets__Content__778AC167");

            entity.HasOne(d => d.Region).WithMany(p => p.Widgets)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK__Widgets__RegionI__787EE5A0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
