using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminNotes.Models
{
    public class NoteContext:DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
