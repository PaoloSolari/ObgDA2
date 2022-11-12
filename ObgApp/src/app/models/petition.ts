export class Petition {

    idPetition: string | null;
    medicineCode: string | null;
    newQuantity: number | null;

    constructor(idPetition: string | null, medicineCode: string | null, newQuantity: number | null) {
        this.idPetition = idPetition;
        this.medicineCode = medicineCode;
        this.newQuantity = newQuantity;
    }
}