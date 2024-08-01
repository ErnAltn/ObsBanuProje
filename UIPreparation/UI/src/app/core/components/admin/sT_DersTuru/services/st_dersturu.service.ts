import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_DersTuru } from '../models/ST_DersTuru';


@Injectable({
  providedIn: 'root'
})
export class ST_DersTuruService {

  constructor(private httpClient: HttpClient) { }


  getST_DersTuruList(): Observable<ST_DersTuru[]> {

    return this.httpClient.get<ST_DersTuru[]>(environment.getApiUrl + '/ST_DersTurus/getall')
  }

  getST_DersTuruById(id: number): Observable<ST_DersTuru> {
    return this.httpClient.get<ST_DersTuru>(environment.getApiUrl + '/ST_DersTurus/getbyid?id='+id)
  }

  addST_DersTuru(sT_DersTuru: ST_DersTuru): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_DersTurus/', sT_DersTuru, { responseType: 'text' });
  }

  updateST_DersTuru(sT_DersTuru: ST_DersTuru): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_DersTurus/', sT_DersTuru, { responseType: 'text' });

  }

  deleteST_DersTuru(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_DersTurus/', { body: { id: id } });
  }


}