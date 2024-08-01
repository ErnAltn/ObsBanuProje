import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { DersProgrami } from '../models/DersProgrami';


@Injectable({
  providedIn: 'root'
})
export class DersProgramiService {

  constructor(private httpClient: HttpClient) { }


  getDersProgramiList(): Observable<DersProgrami[]> {

    return this.httpClient.get<DersProgrami[]>(environment.getApiUrl + '/DersProgramis/getall')
  }

  getDersProgramiById(id: number): Observable<DersProgrami> {
    return this.httpClient.get<DersProgrami>(environment.getApiUrl + '/DersProgramis/getbyid?id='+id)
  }

  addDersProgrami(dersProgrami: DersProgrami): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/DersProgramis/', dersProgrami, { responseType: 'text' });
  }

  updateDersProgrami(dersProgrami: DersProgrami): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/DersProgramis/', dersProgrami, { responseType: 'text' });

  }

  deleteDersProgrami(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/DersProgramis/', { body: { id: id } });
  }


}