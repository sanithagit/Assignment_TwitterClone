using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Twitter_Clone.Models;

namespace Twitter_Clone.Data
{
    public class Twitter_CloneDBContext: DbContext
    {
        public Twitter_CloneDBContext() : base("name=Twitter_CloneConnectionString")
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<Following> Following { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            //keys
            modelBuilder.Entity<Person>().HasKey(e => e.User_Id);
            modelBuilder.Entity<Following>().HasKey(e => new { e.User_Id, e.Following_Id });
            modelBuilder.Entity<Tweet>().HasKey(e => e.Tweet_id);

            //identity
            modelBuilder.Entity<Tweet>().Property(e => e.Tweet_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("tweet_id");


            modelBuilder.Entity<Person>().Property(e => e.User_Id)
             .HasColumnName("User_Id")
              .IsRequired()
             .HasMaxLength(25)
             .IsUnicode(false);
            modelBuilder.Entity<Person>().Property(e => e.Password)
            .HasColumnName("Password")
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();
            modelBuilder.Entity<Person>().Property(e => e.FullName)
           .IsRequired()
           .HasColumnName("FullName")
           .HasMaxLength(30)
           .IsUnicode(false);
            modelBuilder.Entity<Person>().Property(e => e.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasMaxLength(50)
            .IsUnicode(false);
            modelBuilder.Entity<Person>().Property(e => e.Joined)
            .IsRequired()
            .HasColumnName("Joined")
            .HasColumnType("datetime");
            modelBuilder.Entity<Person>().Property(e => e.Active)
            .HasColumnName("Active")
            .HasColumnType("bit");
            

            //foreign key
            modelBuilder.Entity<Person>().HasMany(p => p.Following)
                .WithRequired(f => f.User)
               .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Following>().HasRequired(p => p.User)
            //  .WithMany(f => f.Following)
            //  .HasForeignKey(p => p.Following_Id);


            //modelBuilder.Entity<State>().HasMany(t => t.Cities).WithRequired(a => a.State).WillCascadeOnDelete(false);


            modelBuilder.Entity<Tweet>().Property(e => e.User_Id)
               .IsRequired()
               .HasColumnName("User_id")
               .HasMaxLength(25)
               .IsUnicode(false);
            modelBuilder.Entity<Tweet>().Property(e => e.Message)
               .IsRequired()
               .HasColumnName("Message")
               .HasMaxLength(140)
               .IsUnicode(false);
            modelBuilder.Entity<Tweet>().Property(e => e.Created)
                .HasColumnName("Created")
                .HasColumnType("datetime");

            //foreign key
            modelBuilder.Entity<Tweet>().HasRequired(p => p.User)
                .WithMany(t => t.Tweet)
                .HasForeignKey(p => p.User_Id)
                .WillCascadeOnDelete(true);


        }
    }
}