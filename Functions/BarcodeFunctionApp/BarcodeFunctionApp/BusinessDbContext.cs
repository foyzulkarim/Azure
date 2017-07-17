using System.Data.Entity;

namespace BarcodeFunctionApp
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext()
        {
            Database.Connection.ConnectionString = "server=.\\sqlexpress;database=BizBookDb;Integrated Security=true;";
            Database.Connection.Open();
        }
       
        public DbSet<ProductDetail> ProductDetails { get; set; }

        protected override void Dispose(bool disposing)
        {
            Database.Connection.Close();
            base.Dispose(disposing);
        }
    }
}