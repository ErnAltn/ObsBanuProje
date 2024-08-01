import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Degerlendirme } from '../models/Degerlendirme';


@Injectable({
  providedIn: 'root'
})
export class DegerlendirmeService {

  constructor(private httpClient: HttpClient) { }


  getDegerlendirmeList(): Observable<Degerlendirme[]> {

    return this.httpClient.get<Degerlendirme[]>(environment.getApiUrl + '/Degerlendirmes/getall')
  }

  getDegerlendirmeById(id: number): Observable<Degerlendirme> {
    return this.httpClient.get<Degerlendirme>(environment.getApiUrl + '/Degerlendirmes/getbyid?id='+id)
  }

  addDegerlendirme(degerlendirme: Degerlendirme): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/Degerlendirmes/', degerlendirme, { responseType: 'text' });
  }

  updateDegerlendirme(degerlendirme: Degerlendirme): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/Degerlendirmes/', degerlendirme, { responseType: 'text' });

  }

  deleteDegerlendirme(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/Degerlendirmes/', { body: { id: id } });
  }


}