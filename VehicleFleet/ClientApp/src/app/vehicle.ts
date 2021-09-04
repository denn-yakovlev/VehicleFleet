class CostByYear {
  year: number;
  cost: number;
}

export class Vehicle
{
  id: number;
  name: string;
  manufacturer: string;
  modelName: string;
  productionYear: number;
  enginePowerHp: number;
  engineVolumeLiters: number;
  initialCostRoubles: number;
  gearboxType: string;
  kilometrage: number;
  fuelConsumptionLitersPer100Km: number;
  bookCostByYears: CostByYear[]
}
