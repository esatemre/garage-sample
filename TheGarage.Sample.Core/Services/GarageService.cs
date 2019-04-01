namespace TheGarage.Sample.Core.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using DTOs;

    public class GarageService : IGarageService
    {
        private readonly IRepositoryBase<Garage> _garageRepo;
        private readonly IRepositoryBase<GarageDoor> _garageDoorRepo;
        private readonly IRepositoryBase<GarageDoorUpdate> _garageDoorUpdateRepo;

        public GarageService(IRepositoryBase<Garage> garageRepo, IRepositoryBase<GarageDoor> garageDoorRepo, IRepositoryBase<GarageDoorUpdate> garageDoorUpdateRepo)
        {
            _garageRepo = garageRepo;
            _garageDoorRepo = garageDoorRepo;
            _garageDoorUpdateRepo = garageDoorUpdateRepo;
        }

        public async Task<GarageDetailDto> GetDetail(int garageId)
        {
            return _garageRepo.GetAll().Where(p => p.Id == garageId).Select(p => new GarageDetailDto()
            {
                Owner = new GarageOwnerDto() { FullName = p.GarageOwner.FullName },
                Garage = new GarageDto()
                {
                    Address = p.Address,
                    Name = p.Name,
                    Status = p.Status,
                    Doors = p.GarageDoors.Select(q => new GarageDoorDto()
                    {
                        IpAddress = q.IpAddress,
                        Status = q.Status,
                        Updates = q.GarageDoorUpdates.Select(z => new GarageDoorUpdateDto()
                        {
                            PreviousStatus = z.PreviousStatus,
                            UpdateTime = z.UpdateTime
                        }).ToList()
                    }).ToList()
                }
            }).FirstOrDefault();
        }

        public async Task<GarageDetailDto> RefreshGarage(int garageId)
        {
            await CheckGarage(garageId);
            return await GetDetail(garageId);
        }

        private async Task CheckGarage(int garageId)
        {
            var garageDoors = _garageDoorRepo.GetAll().Where(p => p.GarageId == garageId).ToList(); //Get all doors of the garage
            bool? garageStatus = null; //null: not-changed, true: all-doors online, false: one of them offline
            foreach (var door in garageDoors)
            {
                int failedCount = 0;
                System.Net.NetworkInformation.PingReply result;
                do
                {
                    var ping = new System.Net.NetworkInformation.Ping();
                    result = ping.Send(door.IpAddress);

                } while (result.Status != System.Net.NetworkInformation.IPStatus.Success && failedCount++ < 2);
                if (door.Status != (result.Status == System.Net.NetworkInformation.IPStatus.Success)) //if door status is changed
                {
                    if (garageStatus ?? true) //only change garage status is not updated yet or true
                    {
                        garageStatus = (result.Status == System.Net.NetworkInformation.IPStatus.Success);
                    }
                    var update = new GarageDoorUpdate() { GarageDoorId = door.Id, PreviousStatus = door.Status, UpdateTime = DateTime.Now };
                    update = await _garageDoorUpdateRepo.Add(update);
                    if (update != null && update.Id > 0)
                    {
                        door.Status = (result.Status == System.Net.NetworkInformation.IPStatus.Success);
                        await _garageDoorRepo.Update(door);
                    }
                }
            }
            if (garageStatus.HasValue) //only update the garage status in database if one of the doors status is changed
            {
                var garage = await _garageRepo.GetById(garageId);
                garage.Status = garageStatus.Value;
                await _garageRepo.Update(garage);
            }
        }
    }

    public interface IGarageService
    {
        Task<GarageDetailDto> GetDetail(int garageId);
        Task<GarageDetailDto> RefreshGarage(int garageId);
    }
}
