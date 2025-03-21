using System;
using System.Collections.Generic;

namespace StudentManagement.Infrastructure.Persistance;

public partial class EfmigrationsLock
{
    public int Id { get; set; }

    public string Timestamp { get; set; } = null!;
}
