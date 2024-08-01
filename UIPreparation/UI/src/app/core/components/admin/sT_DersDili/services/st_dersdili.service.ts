import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_DersDili } from '../models/ST_DersDili';


@Injectable({
  providedIn: 'root'
})
export class ST_DersDiliService {

  constructor(private httpClient: HttpClient) { }


  getST_DersDiliList(): Observable<ST_DersDili[]> {

    return this.httpClient.get<ST_DersDili[]>(environment.getApiUrl + '/ST_DersDilis/getall')
  }

  getST_DersDiliById(id: number): Observable<ST_DersDili> {
    return this.httpClient.get<ST_DersDili>(environment.getApiUrl + '/ST_DersDilis/getbyid?id='+id)
  }

  addST_DersDili(sT_DersDili: ST_DersDili): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_DersDilis/', sT_DersDili, { responseType: 'text' });
  }

  updateST_DersDili(sT_DersDili: ST_DersDili): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_DersDilis/', sT_DersDili, { responseType: 'text' });

  }

  deleteST_DersDili(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_DersDilis/', { body: { id: id } });
  }


}