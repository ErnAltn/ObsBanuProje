import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { DersAcma } from '../models/DersAcma';


@Injectable({
  providedIn: 'root'
})
export class DersAcmaService {

  constructor(private httpClient: HttpClient) { }


  getDersAcmaList(): Observable<DersAcma[]> {

    return this.httpClient.get<DersAcma[]>(environment.getApiUrl + '/DersAcmas/getall')
  }

  getDersAcmaById(id: number): Observable<DersAcma> {
    return this.httpClient.get<DersAcma>(environment.getApiUrl + '/DersAcmas/getbyid?id='+id)
  }

  addDersAcma(dersAcma: DersAcma): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/DersAcmas/', dersAcma, { responseType: 'text' });
  }

  updateDersAcma(dersAcma: DersAcma): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/DersAcmas/', dersAcma, { responseType: 'text' });

  }

  deleteDersAcma(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/DersAcmas/', { body: { id: id } });
  }


}