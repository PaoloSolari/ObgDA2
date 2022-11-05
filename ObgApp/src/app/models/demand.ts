import { Petition } from "./petition";

export enum DemandStatus {
    accepted,
    rejected,
    inProgress
}

export class Demand {

    idDemand: string;
    status: DemandStatus;
    petitions: Petition[] | undefined;

    constructor(idDemand: string, status: DemandStatus, petitions: Petition[] | undefined) {
        this.idDemand = idDemand;
        this.status = status;
        this.petitions = petitions;
    }
}