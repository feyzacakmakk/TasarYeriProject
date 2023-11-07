using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Concrete
{
	public class Context:IdentityDbContext<AppUser,AppRole,int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; initial catalog=TasarYeriDB; integrated Security=true");
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);

            builder.Entity<Message>()
                .HasOne(m => m.SenderUser)
                .WithMany(u => u.MessageSender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Message>()
                .HasOne(m => m.ReceiverUser)
                .WithMany(u => u.MessageReceiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
        public DbSet<Product>? Products { get; set; }
		public DbSet<Category>? Categories { get; set; }
		public DbSet<Notification>? Notifications { get; set; }
		public DbSet<Message>? Messages { get; set; }
		public DbSet<Comment>? Comments { get; set; }
		public DbSet<Contact>? Contacts { get; set; }

    }
}
