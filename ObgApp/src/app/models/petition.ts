export class Petition {

    idPetition: string;
    medicineCode: string;
    newQuantity: number;

    constructor(idPetition: string, medicineCode:string, newQuantity: number) {
        this.idPetition = idPetition;
        this.medicineCode = medicineCode;
        this.newQuantity = newQuantity;
    }
}