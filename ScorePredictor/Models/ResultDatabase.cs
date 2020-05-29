using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ScorePredictor.Models
{
    public class ResultDatabase : DbContext
    {
        public DbSet<Scores> MyProperty { get; set; }
    }
}
