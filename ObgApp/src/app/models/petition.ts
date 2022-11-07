export class Petition {

    IdPetition: string | null;
    MedicineCode: string | null;
    NewQuantity: number | null;

    constructor(IdPetition: string | null, MedicineCode: string | null, NewQuantity: number | null) {
        this.IdPetition = IdPetition;
        this.MedicineCode = MedicineCode;
        this.NewQuantity = NewQuantity;
    }
}