using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace COMP2001API.Models
{
    public partial class COMP2001_EJacksonContext : DbContext
    {
        public COMP2001_EJacksonContext()
        {
        }

        public COMP2001_EJacksonContext(DbContextOptions<COMP2001_EJacksonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asimnpassword> Asimnpasswords { get; set; }
        public virtual DbSet<Asimnsession> Asimnsessions { get; set; }
        public virtual DbSet<Asimnuser> Asimnusers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk;Database=COMP2001_EJackson;User Id=EJackson; password=SocO144+");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Asimnpassword>(entity =>
            {
                entity.HasKey(e => e.PasswordId)
                    .HasName("PK_PasswordId");

                entity.ToTable("ASIMNPasswords");

                entity.Property(e => e.DateChanged).HasColumnType("date");

                entity.Property(e => e.password)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Asimnsession>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK_SessionId");

                entity.ToTable("ASIMNSessions");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Asimnuser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserId");

                entity.ToTable("ASIMNUsers");

                entity.Property(e => e.email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.firstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.lastName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.password)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public bool Validate(Asimnuser user)
        {
            SqlParameter valiParameter = new SqlParameter("@loginState", SqlDbType.Int);
            valiParameter.Direction = ParameterDirection.Output;

            Database.ExecuteSqlRaw("EXECUTE @loginState = dbo.UserAuth @password, @useremail", valiParameter,
            new SqlParameter("@password", user.password.ToString()), 
            new SqlParameter("@useremail", user.email.ToString()));

            int result = (int)valiParameter.Value;
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Register(Asimnuser user, out String responseString)
        {
            SqlParameter regiParameter = new SqlParameter("@regOutput", SqlDbType.VarChar, 40);
            regiParameter.Direction = ParameterDirection.Output;

            Database.ExecuteSqlRaw("EXECUTE dbo.UserEntry @first, @last, @password, @useremail, @regOutput OUTPUT",
            new SqlParameter("@first", user.firstName.ToString()),
            new SqlParameter("@last", user.lastName.ToString()),
            new SqlParameter("@password", user.password.ToString()),
            new SqlParameter("@useremail", user.email.ToString()),
            regiParameter);

            responseString = regiParameter.Value.ToString();
        }

        public void Update(Asimnuser user, int Id)
        {
            Database.ExecuteSqlRaw("EXECUTE dbo.UserUpd @first, @last, @password, @useremail, @userId",
            new SqlParameter("@first", user.firstName.ToString()),
            new SqlParameter("@last", user.lastName.ToString()),
            new SqlParameter("@password", user.password.ToString()),
            new SqlParameter("@useremail", user.email.ToString()),
            new SqlParameter("@userId", Id.ToString()));
        }

        public void Delete(int Id)
        {
            Database.ExecuteSqlRaw("EXECUTE dbo.UserDel @userId",
            new SqlParameter("@userId", Id.ToString()));
        }
    }
}
