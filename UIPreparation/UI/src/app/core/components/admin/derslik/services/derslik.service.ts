import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Derslik } from '../models/Derslik';


@Injectable({
  providedIn: 'root'
})
export class DerslikService {

  constructor(private httpClient: HttpClient) { }


  getDerslikList(): Observable<Derslik[]> {

    return this.httpClient.get<Derslik[]>(environment.getApiUrl + '/Dersliks/getall')
  }

  getDerslikById(id: number): Observable<Derslik> {
    return this.httpClient.get<Derslik>(environment.getApiUrl + '/Dersliks/getbyid?id='+id)
  }

  addDerslik(derslik: Derslik): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/Dersliks/', derslik, { responseType: 'text' });
  }

  updateDerslik(derslik: Derslik): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/Dersliks/', derslik, { responseType: 'text' });

  }

  deleteDerslik(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/Dersliks/', { body: { id: id } });
  }


}