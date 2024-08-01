import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { OgretimElemani } from '../models/OgretimElemani';


@Injectable({
  providedIn: 'root'
})
export class OgretimElemaniService {

  constructor(private httpClient: HttpClient) { }


  getOgretimElemaniList(): Observable<OgretimElemani[]> {

    return this.httpClient.get<OgretimElemani[]>(environment.getApiUrl + '/OgretimElemanis/getall')
  }

  getOgretimElemaniById(id: number): Observable<OgretimElemani> {
    return this.httpClient.get<OgretimElemani>(environment.getApiUrl + '/OgretimElemanis/getbyid?id='+id)
  }

  addOgretimElemani(ogretimElemani: OgretimElemani): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/OgretimElemanis/', ogretimElemani, { responseType: 'text' });
  }

  updateOgretimElemani(ogretimElemani: OgretimElemani): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/OgretimElemanis/', ogretimElemani, { responseType: 'text' });

  }

  deleteOgretimElemani(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/OgretimElemanis/', { body: { id: id } });
  }


}