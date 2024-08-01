import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_SinavTuru } from '../models/ST_SinavTuru';


@Injectable({
  providedIn: 'root'
})
export class ST_SinavTuruService {

  constructor(private httpClient: HttpClient) { }


  getST_SinavTuruList(): Observable<ST_SinavTuru[]> {

    return this.httpClient.get<ST_SinavTuru[]>(environment.getApiUrl + '/ST_SinavTurus/getall')
  }

  getST_SinavTuruById(id: number): Observable<ST_SinavTuru> {
    return this.httpClient.get<ST_SinavTuru>(environment.getApiUrl + '/ST_SinavTurus/getbyid?id='+id)
  }

  addST_SinavTuru(sT_SinavTuru: ST_SinavTuru): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_SinavTurus/', sT_SinavTuru, { responseType: 'text' });
  }

  updateST_SinavTuru(sT_SinavTuru: ST_SinavTuru): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_SinavTurus/', sT_SinavTuru, { responseType: 'text' });

  }

  deleteST_SinavTuru(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_SinavTurus/', { body: { id: id } });
  }


}