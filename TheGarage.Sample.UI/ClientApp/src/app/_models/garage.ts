export class GarageOverview {
  garage: Garage;
  owner: GarageOwner;
}
export class Garage {
  name: string;
  address: string;
  status: boolean;
  doors: GarageDoor[];
}
export class GarageDoor {
  ipAddress: string;
  status: boolean;
  updates: GarageDoorHistory[]
}
export class GarageOwner {
  fullName: string;
}
export class GarageDoorHistory {
  previousStatus: boolean;
  updateTime: Date;
}
