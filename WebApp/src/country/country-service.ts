import {HttpClient} from 'aurelia-fetch-client';
  
import {autoinject} from 'aurelia-framework';
import { Country } from './country-model';

import {buildHttpClient} from 'shared/http-client-factory';

@autoinject
export class CountryService {
    private httpClient: HttpClient;
    
    constructor() {
       this.httpClient = buildHttpClient();
    }

    getList(name: string): Promise<Country[]> {
        return this.httpClient.fetch(`/countries?name=${name||''}`).then(c=>c.json()).then(c => c.map(mapResponse));
    }

    find(name: string): Promise<Country> {
        return this.httpClient.fetch(`/countries/${name}`).then(c=>c.json()).then(c => mapResponse(c));
    }
}

const mapResponse = (data) => {
    return {
        Name: data.name,
    }
}