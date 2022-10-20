import config from "@/appconfig";
import { ResponseModel } from "@/models/ResponseModel";

export default class NewApiService {
    private static request(method: string, url: string, data: any | string = ''): Promise<ResponseModel> {
        const body = data === '' ? null : data;
        const headers: { [key: string]: string } = {
            'Content-Type': 'application/json'
        };

        return fetch(config.API_URL + url,
            ({
                method,
                headers,
                body
            }) as any)
            .then((response: any) => {
                return response.json();
            })
	}

    get(url: string): Promise<ResponseModel> {
        return NewApiService.request('GET', url, '');
    }

    delete(url: string): Promise<ResponseModel> {
        return NewApiService.request('DELETE', url);
    }

    put(url: string, data: any | string): Promise<ResponseModel> {
        return NewApiService.request('PUT', url, data);
    }

    post(url: string, data: any | string): Promise<ResponseModel> {
        return NewApiService.request('POST', url, data);
    }
}