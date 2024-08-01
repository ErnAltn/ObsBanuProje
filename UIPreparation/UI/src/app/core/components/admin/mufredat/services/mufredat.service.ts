import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Mufredat } from '../models/Mufredat';


@Injectable({
  providedIn: 'root'
})
export class MufredatService {

  constructor(private httpClient: HttpClient) { }


  getMufredatList(): Observable<Mufredat[]> {

    return this.httpClient.get<Mufredat[]>(environment.getApiUrl + '/Mufredats/getall')
  }

  getMufredatById(id: number): Observable<Mufredat> {
    return this.httpClient.get<Mufredat>(environment.getApiUrl + '/Mufredats/getbyid?id='+id)
  }

  addMufredat(mufredat: Mufredat): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/Mufredats/', mufredat, { responseType: 'text' });
  }

  updateMufredat(mufredat: Mufredat): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/Mufredats/', mufredat, { responseType: 'text' });

  }

  deleteMufredat(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/Mufredats/', { body: { id: id } });
  }


}