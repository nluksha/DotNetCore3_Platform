using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Platform.Models
{
    public class SeedData
    {
        private CalculationContext context;
        private ILogger<SeedData> logger;

        private Dictionary<int, long> data = new Dictionary<int, long>
        {
            [1] = 1,
            [2] = 3,
            [3] = 6,
            [4] = 10,
            [5] = 15,
            [6] = 21,
            [7] = 28,
            [8] = 36,
            [9] = 45,
            [10] = 55
        };

        public SeedData(CalculationContext context, ILogger<SeedData> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void SeedDatabase()
        {
            context.Database.Migrate();

            if (context.Calculations.Count() == 0)
            {
                logger.LogInformation("Prepering to seed database");
                context.Calculations!.AddRange(data.Select(x => new Calculation() { Count = x.Key, Result = x.Value }));
                context.SaveChanges();
                logger.LogInformation("Database seeded");
            }
            else
            {
                logger.LogInformation("Database not seeded");
            }
        }
    }
}
