import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_DersSeviyesi } from '../models/ST_DersSeviyesi';


@Injectable({
  providedIn: 'root'
})
export class ST_DersSeviyesiService {

  constructor(private httpClient: HttpClient) { }


  getST_DersSeviyesiList(): Observable<ST_DersSeviyesi[]> {

    return this.httpClient.get<ST_DersSeviyesi[]>(environment.getApiUrl + '/ST_DersSeviyesis/getall')
  }

  getST_DersSeviyesiById(id: number): Observable<ST_DersSeviyesi> {
    return this.httpClient.get<ST_DersSeviyesi>(environment.getApiUrl + '/ST_DersSeviyesis/getbyid?id='+id)
  }

  addST_DersSeviyesi(sT_DersSeviyesi: ST_DersSeviyesi): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_DersSeviyesis/', sT_DersSeviyesi, { responseType: 'text' });
  }

  updateST_DersSeviyesi(sT_DersSeviyesi: ST_DersSeviyesi): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_DersSeviyesis/', sT_DersSeviyesi, { responseType: 'text' });

  }

  deleteST_DersSeviyesi(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_DersSeviyesis/', { body: { id: id } });
  }


}