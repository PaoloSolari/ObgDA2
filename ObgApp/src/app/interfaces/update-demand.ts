import { DemandStatus } from "../models/demand";

export interface IUpdateDemand {
    IdDemand: string | null;
    Status: DemandStatus | null;
}
