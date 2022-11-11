import { Petition } from "./petition";

export enum DemandStatus {
    accepted,
    rejected,
    inProgress
}

export class Demand {

    idDemand: string | null;
    status: DemandStatus | null;
    petitions: Petition[] | null;

    constructor(idDemand: string | null, status: DemandStatus | null, petitions: Petition[] | null) {
        this.idDemand = idDemand;
        this.status = status;
        this.petitions = petitions;
    }
}