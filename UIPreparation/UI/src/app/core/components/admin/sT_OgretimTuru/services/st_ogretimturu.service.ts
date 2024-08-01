import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_OgretimTuru } from '../models/ST_OgretimTuru';


@Injectable({
  providedIn: 'root'
})
export class ST_OgretimTuruService {

  constructor(private httpClient: HttpClient) { }


  getST_OgretimTuruList(): Observable<ST_OgretimTuru[]> {

    return this.httpClient.get<ST_OgretimTuru[]>(environment.getApiUrl + '/ST_OgretimTurus/getall')
  }

  getST_OgretimTuruById(id: number): Observable<ST_OgretimTuru> {
    return this.httpClient.get<ST_OgretimTuru>(environment.getApiUrl + '/ST_OgretimTurus/getbyid?id='+id)
  }

  addST_OgretimTuru(sT_OgretimTuru: ST_OgretimTuru): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_OgretimTurus/', sT_OgretimTuru, { responseType: 'text' });
  }

  updateST_OgretimTuru(sT_OgretimTuru: ST_OgretimTuru): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_OgretimTurus/', sT_OgretimTuru, { responseType: 'text' });

  }

  deleteST_OgretimTuru(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_OgretimTurus/', { body: { id: id } });
  }


}