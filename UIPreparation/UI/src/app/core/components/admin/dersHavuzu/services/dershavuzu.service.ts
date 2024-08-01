import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { DersHavuzu } from '../models/DersHavuzu';


@Injectable({
  providedIn: 'root'
})
export class DersHavuzuService {

  constructor(private httpClient: HttpClient) { }


  getDersHavuzuList(): Observable<DersHavuzu[]> {

    return this.httpClient.get<DersHavuzu[]>(environment.getApiUrl + '/DersHavuzus/getall')
  }

  getDersHavuzuById(id: number): Observable<DersHavuzu> {
    return this.httpClient.get<DersHavuzu>(environment.getApiUrl + '/DersHavuzus/getbyid?id='+id)
  }

  addDersHavuzu(dersHavuzu: DersHavuzu): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/DersHavuzus/', dersHavuzu, { responseType: 'text' });
  }

  updateDersHavuzu(dersHavuzu: DersHavuzu): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/DersHavuzus/', dersHavuzu, { responseType: 'text' });

  }

  deleteDersHavuzu(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/DersHavuzus/', { body: { id: id } });
  }


}