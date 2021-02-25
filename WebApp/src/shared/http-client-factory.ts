import { HttpClient } from "aurelia-fetch-client";

import * as environment from '../../config/environment.json';


export const buildHttpClient = () => {
    const httpClient = new HttpClient();
    httpClient.configure(x => {
        x.withBaseUrl(environment.apiUrl);
        x.withInterceptor({
            async response(response) {
                if (!response.ok) {
                    let data: any;
                    try {
                        data = await response.json();
                    } finally {
                        if (data) {
                            throw data;
                        } else {
                            throw response;
                        }
                    }
                }

                return response;
            }
        })
    });

    return httpClient;
}