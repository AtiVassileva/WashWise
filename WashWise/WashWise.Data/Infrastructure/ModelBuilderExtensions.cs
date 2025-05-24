using Microsoft.EntityFrameworkCore;

namespace WashWise.Data.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyHasTrigger(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null)
                {
                    entityType.AddTrigger("fake_trigger_" + tableName);
                }
            }

            return modelBuilder;
        }
    }
}