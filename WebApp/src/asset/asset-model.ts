
export enum Department {
    HQ,
    Store1,
    Store2,
    Store3,
    MaintenanceStation
}

export class Asset {
    public Id: number;
    public AssetName: string;
    public Department: Department;
    public CountryOfDepartment: string;
    public EMailAddressOfDepartment: string;
    public PurchaseDate: Date;
    public Broken: boolean;

    public static copy(data:any) {
        const asset = new Asset();
        asset.Id = data.Id;
        asset.AssetName = data.AssetName;
        asset.Department = data.Department;
        asset.CountryOfDepartment = data.CountryOfDepartment;
        asset.EMailAddressOfDepartment = data.EMailAddressOfDepartment;
        asset.PurchaseDate = data.PurchaseDate;
        asset.Broken = data.Broken;

        return asset;
    }
}