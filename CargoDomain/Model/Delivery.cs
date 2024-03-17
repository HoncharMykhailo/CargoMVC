using System;
using System.Collections.Generic;

namespace CargoDomain.Model;

public partial class Delivery
{
    public int Id { get; set; }

    public int TruckId { get; set; }

    public int StationId { get; set; }
}
