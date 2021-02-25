import {HttpClient, json} from 'aurelia-fetch-client';
  
import {autoinject} from 'aurelia-framework';
import { Asset } from './asset-model';

import {buildHttpClient} from 'shared/http-client-factory';
import * as moment from 'moment';

@autoinject
export class AssetService {
    private httpClient: HttpClient;
    constructor() {
       this.httpClient = buildHttpClient();
    }

    getList(): Promise<Asset[]> {
        return this.httpClient.fetch('/assets').then(c=>c.json()).then(l => l.map(mapResponse).map(a=>Asset.copy(a)));
    }

    find(id: number): Promise<Asset> {
        return this.httpClient.fetch(`/assets/${id}`).then(c=>c.json()).then(data =>  Asset.copy(mapResponse(data)));
    }

    create(assset:Asset): Promise<{}> {
        return this.httpClient.post(`/assets`, json(assset)).then(c=>c.json());
    }

    update(assset:Asset): Promise<{}> {
        return this.httpClient.put(`/assets/${assset.Id}`, json(assset));
    }

    delete(id: number): Promise<{}>{
        return this.httpClient.delete(`/assets/${id}`);
    }
}

const mapResponse = (data) => {
    return {
        Id: data.id,
        AssetName: data.assetName,
        Department: data.department,
        CountryOfDepartment: data.countryOfDepartment,
        EMailAddressOfDepartment: data.eMailAddressOfDepartment,
        PurchaseDate: moment.utc(data.purchaseDate).local().format("YYYY-MM-DDTHH:MM"),
        Broken: data.broken
    }
}