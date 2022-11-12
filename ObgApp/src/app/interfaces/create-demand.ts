import { ICreatePetition } from "./create-petition";

export interface ICreateDemand {
    Petitions: ICreatePetition[] | null;
}
