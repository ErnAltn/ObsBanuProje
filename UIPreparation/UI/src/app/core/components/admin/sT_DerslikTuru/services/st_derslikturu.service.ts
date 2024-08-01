import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_DerslikTuru } from '../models/ST_DerslikTuru';


@Injectable({
  providedIn: 'root'
})
export class ST_DerslikTuruService {

  constructor(private httpClient: HttpClient) { }


  getST_DerslikTuruList(): Observable<ST_DerslikTuru[]> {

    return this.httpClient.get<ST_DerslikTuru[]>(environment.getApiUrl + '/ST_DerslikTurus/getall')
  }

  getST_DerslikTuruById(id: number): Observable<ST_DerslikTuru> {
    return this.httpClient.get<ST_DerslikTuru>(environment.getApiUrl + '/ST_DerslikTurus/getbyid?id='+id)
  }

  addST_DerslikTuru(sT_DerslikTuru: ST_DerslikTuru): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_DerslikTurus/', sT_DerslikTuru, { responseType: 'text' });
  }

  updateST_DerslikTuru(sT_DerslikTuru: ST_DerslikTuru): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_DerslikTurus/', sT_DerslikTuru, { responseType: 'text' });

  }

  deleteST_DerslikTuru(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_DerslikTurus/', { body: { id: id } });
  }


}