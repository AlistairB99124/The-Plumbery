using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class WarehouseRepository: BaseRepository<Warehouse>, IWarehouseRepository {
    }
}
