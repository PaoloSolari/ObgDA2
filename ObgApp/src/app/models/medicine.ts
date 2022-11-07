export enum PresentationMedicine {
    Capsulas,
    Comprimidos,
    SolucionSoluble,
    StickPack,
    Liquido
}

export class Medicine {
    Code: string | null;
    Name: string | null;
    SymtompsItTreats: string | null;
    Presentation: PresentationMedicine | null;
    Quantity: number | null;
    Unit: string | null;
    Price: number | null;
    Stock: number | null;
    Prescription: boolean | null;
    IsActive: boolean | null;

    constructor(Code: string | null, Name: string | null, SymtompsItTreats: string | null, Presentation: PresentationMedicine | null, Quantity: number | null, Unit: string | null, Price: number | null, Stock: number | null, Prescription: boolean | null, IsActive: boolean | null) {
        this.Code = Code;
        this.Name = Name;
        this.SymtompsItTreats = SymtompsItTreats;
        this.Presentation = Presentation;
        this.Quantity = Quantity;
        this.Unit = Unit;
        this.Price = Price;
        this.Stock = Stock;
        this.Prescription = Prescription;
        this.IsActive = IsActive;
    }
}