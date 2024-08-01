import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Danismanlik } from '../models/Danismanlik';


@Injectable({
  providedIn: 'root'
})
export class DanismanlikService {

  constructor(private httpClient: HttpClient) { }


  getDanismanlikList(): Observable<Danismanlik[]> {

    return this.httpClient.get<Danismanlik[]>(environment.getApiUrl + '/Danismanliks/getall')
  }

  getDanismanlikById(id: number): Observable<Danismanlik> {
    return this.httpClient.get<Danismanlik>(environment.getApiUrl + '/Danismanliks/getbyid?id='+id)
  }

  addDanismanlik(danismanlik: Danismanlik): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/Danismanliks/', danismanlik, { responseType: 'text' });
  }

  updateDanismanlik(danismanlik: Danismanlik): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/Danismanliks/', danismanlik, { responseType: 'text' });

  }

  deleteDanismanlik(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/Danismanliks/', { body: { id: id } });
  }


}