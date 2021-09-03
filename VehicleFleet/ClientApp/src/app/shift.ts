import {Driver} from "./driver";
import {Vehicle} from "./vehicle";

export class Shift {
    id: number;
    start: string;
    end: string;
    vehicle: Vehicle;
    driver: Driver;
    kilometrage: number;
    fuelConsumedLiters: string;
}
