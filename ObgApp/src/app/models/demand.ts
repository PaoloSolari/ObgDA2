import { Petition } from "./petition";

export enum DemandStatus {
    Accepted,
    Rejected,
    InProgress
}

export class Demand {

    IdDemand: string | null;
    Status: DemandStatus | null;
    Petitions: Petition[] | null;

    constructor(IdDemand: string | null, Status: DemandStatus | null, Petitions: Petition[] | null) {
        this.IdDemand = IdDemand;
        this.Status = Status;
        this.Petitions = Petitions;
    }
}