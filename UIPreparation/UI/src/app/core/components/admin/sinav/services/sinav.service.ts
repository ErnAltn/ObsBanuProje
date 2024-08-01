import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Sinav } from '../models/Sinav';


@Injectable({
  providedIn: 'root'
})
export class SinavService {

  constructor(private httpClient: HttpClient) { }


  getSinavList(): Observable<Sinav[]> {

    return this.httpClient.get<Sinav[]>(environment.getApiUrl + '/Sinavs/getall')
  }

  getSinavById(id: number): Observable<Sinav> {
    return this.httpClient.get<Sinav>(environment.getApiUrl + '/Sinavs/getbyid?id='+id)
  }

  addSinav(sinav: Sinav): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/Sinavs/', sinav, { responseType: 'text' });
  }

  updateSinav(sinav: Sinav): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/Sinavs/', sinav, { responseType: 'text' });

  }

  deleteSinav(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/Sinavs/', { body: { id: id } });
  }


}