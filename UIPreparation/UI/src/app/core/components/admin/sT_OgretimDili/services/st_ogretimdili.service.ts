import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_OgretimDili } from '../models/ST_OgretimDili';


@Injectable({
  providedIn: 'root'
})
export class ST_OgretimDiliService {

  constructor(private httpClient: HttpClient) { }


  getST_OgretimDiliList(): Observable<ST_OgretimDili[]> {

    return this.httpClient.get<ST_OgretimDili[]>(environment.getApiUrl + '/ST_OgretimDilis/getall')
  }

  getST_OgretimDiliById(id: number): Observable<ST_OgretimDili> {
    return this.httpClient.get<ST_OgretimDili>(environment.getApiUrl + '/ST_OgretimDilis/getbyid?id='+id)
  }

  addST_OgretimDili(sT_OgretimDili: ST_OgretimDili): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_OgretimDilis/', sT_OgretimDili, { responseType: 'text' });
  }

  updateST_OgretimDili(sT_OgretimDili: ST_OgretimDili): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_OgretimDilis/', sT_OgretimDili, { responseType: 'text' });

  }

  deleteST_OgretimDili(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_OgretimDilis/', { body: { id: id } });
  }


}