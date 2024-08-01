import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { DersAlma } from '../models/DersAlma';


@Injectable({
  providedIn: 'root'
})
export class DersAlmaService {

  constructor(private httpClient: HttpClient) { }


  getDersAlmaList(): Observable<DersAlma[]> {

    return this.httpClient.get<DersAlma[]>(environment.getApiUrl + '/DersAlmas/getall')
  }

  getDersAlmaById(id: number): Observable<DersAlma> {
    return this.httpClient.get<DersAlma>(environment.getApiUrl + '/DersAlmas/getbyid?id='+id)
  }

  addDersAlma(dersAlma: DersAlma): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/DersAlmas/', dersAlma, { responseType: 'text' });
  }

  updateDersAlma(dersAlma: DersAlma): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/DersAlmas/', dersAlma, { responseType: 'text' });

  }

  deleteDersAlma(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/DersAlmas/', { body: { id: id } });
  }


}